using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TournamentCreatorWinForms
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.ToolTip toolTip;
        private List<string> teamNames = new List<string>();
        private Random rnd = new Random();
        private int TournamentType;
        public int LoggedUserID { get; private set; }


        // Initialize the form
        public Form1(int userID)
        {
            LoggedUserID = userID;
            InitializeComponent();
        }

        // Method to execute database queries
        private int ExecuteQuery(string query, Action<MySqlCommand> parameterize, Action<MySqlDataReader> readAction = null)
        {
            string connectionString = "Server=localhost;Database=tournamentdb;User ID=Tester;Password=123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        parameterize?.Invoke(command);

                        if (readAction != null)
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                readAction(reader);
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }
                        return (int)command.LastInsertedId; // Return the last inserted ID
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error occurred: " + ex.Message);
                    return -1; // Return -1 to indicate an error
                }
            }
        }

        // Method to create a new tournament
        private void btnCreateTournament_Click(object sender, EventArgs e)
        {
            int TournamentID = CreateTournament();
            AddTeams(TournamentID);
            MessageBox.Show("Tournament created successfully!");
            this.Close();
        }

        // Method to create a new tournament in the database
        private int CreateTournament()
        {
            var query = "INSERT INTO Tournament(Name, Teams_Counter, User_ID) VALUES (@Name, @TeamsCounter, @UserID);";
            string TournamentName = TextTournamentName.Text;
            if (string.IsNullOrEmpty(TournamentName)) { TournamentName = "New Tournament"; }
            int IDofTournament = ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@Name", TournamentName);
                cmd.Parameters.AddWithValue("@TeamsCounter", teamNames.Count);
                cmd.Parameters.AddWithValue("@UserID", LoggedUserID);
            });
            return IDofTournament;
        }

        // Method to add a team to the tournament
        private void btnAddTeam_Click(object sender, EventArgs e)
        {
            if (txtTeamName.Text == "")
            {
                MessageBox.Show("You can't create a team without a name.");
            }
            else
            {
                var teamName = txtTeamName.Text;

                teamNames.Add(teamName);
                listBoxTeams.Items.Add($"{teamName}");
                txtTeamName.Clear();
                UpdateCreateTournamentButtonState();
            }
        }

        // Method to remove a team from the list of teams
        private void ListBoxTeams_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxTeams.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string selectedTeam = listBoxTeams.Items[index].ToString();
                teamNames.Remove(selectedTeam);
                listBoxTeams.Items.Remove(selectedTeam);
                listBoxTeams.Refresh();
                UpdateCreateTournamentButtonState();
                MessageBox.Show($"You've deleted team: {selectedTeam}");
            }
        }

        // Method to add teams to the database
        private void AddTeams(int tournamentID)
        {
            foreach (var teamName in teamNames)
            {
                var query = "INSERT INTO Team (Name, Tournament_ID) VALUES (@teamName, @TournamentID);";
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@teamName", teamName);
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentID);
                });
            }
        }

        // Method to enable the create tournament button based on the number of teams
        private void UpdateCreateTournamentButtonState()
        {
            btnCreateTournament.Enabled = teamNames.Count == 2 || teamNames.Count == 4 || teamNames.Count == 8 || teamNames.Count == 16;
        }

    }
}
