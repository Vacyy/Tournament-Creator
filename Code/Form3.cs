using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TournamentCreatorWinForms
{
    public partial class Form3 : Form
    {
        private int tournamentId;
        private List<Team> teams;
        private Random rnd = new Random();
        private List<TableLayoutPanel> tableLayoutPanels;

        // Constructor to initialize the form and set up initial display
        public Form3(int Tournament_id)
        {
            tournamentId = Tournament_id;
            InitializeComponent();
            InitializeTableLayoutPanelsList();
            LoadTeams();
            CreateMatchups();
            for (int i = 0; i < 15; i++)
            {
                AddControlsToTableLayoutPanel(i);
            }
        }

        // Method to execute SQL queries with optional parameters and action after reading
        private void ExecuteQuery(string query, Action<MySqlCommand> parameterize, Action<MySqlDataReader> readAction = null)
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
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        // Method to load teams participating in the tournament
        private void LoadTeams()
        {
            string query = "SELECT * FROM team WHERE Tournament_ID = @TournamentID";
            teams = new List<Team>();

            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
            }, (reader) =>
            {
                while (reader.Read())
                {
                    teams.Add(new Team
                    {
                        Id = reader.GetInt32("Team_ID"),
                        Name = reader.GetString("Name")
                    });
                }
            });

            // Randomize the order of teams
            teams = teams.OrderBy(x => rnd.Next()).ToList();
        }

        // Method to create initial matchups for the tournament
        private void CreateMatchups()
        {
            if (GetMatchCount() == 0)
            {
                int round = 1;
                int matchesCreated = 0;
                string query = "INSERT INTO matches (Tournament_ID, Round, Team_ID_1, Team_ID_2, bracket_id) VALUES (@TournamentID, @Round, @Team1ID, @Team2ID, @bracket_id)";
                for (int i = 0; i < teams.Count; i++)
                {
                    ExecuteQuery(query, (cmd) =>
                    {
                        cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                        cmd.Parameters.AddWithValue("@Round", round);
                        cmd.Parameters.AddWithValue("@Team1ID", teams[i].Id);
                        cmd.Parameters.AddWithValue("@Team2ID", teams[i + 1].Id);
                        cmd.Parameters.AddWithValue("@bracket_id", (i / 2));
                    });
                    i++;
                    matchesCreated++;
                }

                int a = matchesCreated;
                int v = 0;
                while (matchesCreated < teams.Count - 1)
                {
                    round++;
                    if (teams.Count == 8 || teams.Count == 4) { v = 8 - a; }
                    int b = matchesCreated;

                    query = "INSERT INTO matches (Tournament_ID, Round, bracket_id) VALUES (@TournamentID, @Round, @bracket_id);";
                    for (int i = a; i < a + (a / 2); i++)
                    {
                        ExecuteQuery(query, (cmd) =>
                        {
                            cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                            cmd.Parameters.AddWithValue("@Round", round);
                            cmd.Parameters.AddWithValue("@bracket_id", v + matchesCreated);
                        });
                        matchesCreated++;
                    }

                    a = matchesCreated - b;
                }
            }
        }

        // Team class to hold team information
        public class Team
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // Method to initialize the list of TableLayoutPanel controls
        private void InitializeTableLayoutPanelsList()
        {
            tableLayoutPanels = new List<TableLayoutPanel>
            {
                tableLayoutPanel1, tableLayoutPanel2, tableLayoutPanel3, tableLayoutPanel4, tableLayoutPanel5,
                tableLayoutPanel6, tableLayoutPanel7, tableLayoutPanel8, tableLayoutPanel9, tableLayoutPanel10,
                tableLayoutPanel11, tableLayoutPanel12, tableLayoutPanel13, tableLayoutPanel14, tableLayoutPanel15,
            };
        }

        // Method to add labels for teams and match results to a specific TableLayoutPanel
        private void AddControlsToTableLayoutPanel(int bracket_id)
        {
            string query2 = "SELECT Winner_ID, t1.Team_ID AS Team1_ID, t2.Team_ID AS Team2_ID, t1.Name AS Team1_Name, t2.Name AS Team2_Name" +
                            " FROM matches m" +
                            " LEFT JOIN team t1 ON m.Team_ID_1 = t1.Team_ID" +
                            " LEFT JOIN team t2 ON m.Team_ID_2 = t2.Team_ID" +
                            " WHERE m.Tournament_ID = @TournamentID AND m.bracket_id = @bracket_id;";

            ExecuteQuery(query2, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                cmd.Parameters.AddWithValue("@bracket_id", bracket_id);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    Label label1 = new Label
                    {
                        Text = reader.IsDBNull(reader.GetOrdinal("Team1_Name")) ? "TBD" : reader.GetString("Team1_Name"),
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                        Dock = DockStyle.Fill
                    };
                    Label label2 = new Label
                    {
                        Text = reader.IsDBNull(reader.GetOrdinal("Team2_Name")) ? "TBD" : reader.GetString("Team2_Name"),
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                        Dock = DockStyle.Fill
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal("Winner_ID")))
                    {
                        int winnerID = reader.GetInt32(reader.GetOrdinal("Winner_ID"));
                        int team1ID = reader.IsDBNull(reader.GetOrdinal("Team1_ID")) ? -1 : reader.GetInt32(reader.GetOrdinal("Team1_ID"));
                        int team2ID = reader.IsDBNull(reader.GetOrdinal("Team2_ID")) ? -1 : reader.GetInt32(reader.GetOrdinal("Team2_ID"));

                        if (winnerID == team1ID)
                        {
                            label1.BackColor = Color.LightGreen; // Winner
                            label2.BackColor = Color.LightPink; // Loser
                        }
                        else if (winnerID == team2ID)
                        {
                            label1.BackColor = Color.LightPink; // Loser
                            label2.BackColor = Color.LightGreen; // Winner
                        }
                    }
                    else
                    {
                        label1.BackColor = Color.White;
                        label2.BackColor = Color.White;
                    }

                    // Event handlers for clicking on labels to update winners
                    label1.Click += (sender, e) =>
                    {
                        if (label1.Text == "TBD" || label2.Text == "TBD")
                        {
                            // Handle case where teams are not yet decided
                        }
                        else
                        {
                            string query3 = "CALL UpdateWinnerAndNextMatch(@bracket_id, @TournamentID, @labelClicked)";
                            ExecuteQuery(query3, (cmd) =>
                            {
                                cmd.Parameters.AddWithValue("@bracket_id", bracket_id);
                                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                                cmd.Parameters.AddWithValue("@labelClicked", 1);
                            });
                            label1.BackColor = Color.LightGreen;
                            label2.BackColor = Color.LightPink;
                            ClearAndUpdate(bracket_id);
                        }
                    };

                    label2.Click += (sender, e) =>
                    {
                        if (label1.Text == "TBD" || label2.Text == "TBD")
                        {
                            // Handle case where teams are not yet decided
                        }
                        else
                        {
                            string query3 = "CALL UpdateWinnerAndNextMatch(@bracket_id, @TournamentID, @labelClicked)";
                            ExecuteQuery(query3, (cmd) =>
                            {
                                cmd.Parameters.AddWithValue("@bracket_id", bracket_id);
                                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                                cmd.Parameters.AddWithValue("@labelClicked", 2);
                            });

                            label2.BackColor = Color.LightGreen;
                            label1.BackColor = Color.LightPink;
                            ClearAndUpdate(bracket_id);
                        }
                    };

                    tableLayoutPanels[bracket_id].Controls.Add(label1, 0, 0);
                    tableLayoutPanels[bracket_id].Controls.Add(label2, 0, 1);
                }
            });
        }

        // Method to retrieve the number of matches in the tournament
        private int GetMatchCount()
        {
            int matchCount = 0;
            string query = "SELECT COUNT(*) AS MatchCount FROM matches WHERE Tournament_ID = @TournamentID";

            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    matchCount = reader.GetInt32("MatchCount");
                }
            });

            return matchCount;
        }

        // Method to clear and update TableLayoutPanel controls after
        private void ClearAndUpdate(int bracket_id)
        {
            if (bracket_id != 14)
            {
                bracket_id = 8 + (bracket_id / 2);

                tableLayoutPanels[bracket_id].Controls.Clear();
                AddControlsToTableLayoutPanel(bracket_id);
                if (bracket_id < 12)
                {
                    tableLayoutPanels[8 + (bracket_id / 2)].Controls.Clear();
                    AddControlsToTableLayoutPanel(8 + (bracket_id / 2));
                }
                tableLayoutPanels[14].Controls.Clear();
                AddControlsToTableLayoutPanel(14);


            }

        }
        //Get the Winner screen
        private void btnApprove_Click(object sender, EventArgs e)
        {

            var query = "SELECT name FROM team WHERE Team_ID =(SELECT Winner_ID FROM Matches WHERE Tournament_ID = @TournamentID AND Round = (SELECT MAX(Round) FROM Matches WHERE Tournament_ID = @TournamentID));";
            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    Winner winner = new Winner(tournamentId);
                    winner.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Winner was not selected yet.");
                }
            });
        }
    }
}
