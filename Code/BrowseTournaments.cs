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
    public partial class BrowseTournaments : Form
    {
        public int LoggedUserID { get; private set; }

        // Window initialization
        public BrowseTournaments(int userID)
        {
            LoggedUserID = userID; // Initialize window with user ID assignment
            InitializeComponent();
        }

        // Method used for proper window operation
        private void BrowseTournaments_Load(object sender, EventArgs e)
        {
            LoadTournaments();
            CenterTableLayoutPanel();
        }


        // Centering the table in the window
        private void CenterTableLayoutPanel()
        {
            int x = (this.ClientSize.Width - tableLayoutPanel.Width) / 2;

            tableLayoutPanel.Location = new Point(x);
        }


        // Method loading all tournaments into the view
        private void LoadTournaments()
        {
            string connectionString = "server=localhost;user=Tester;database=tournamentdb;password=123";
            string query = "SELECT name, teams_counter, tournament_id FROM tournament WHERE User_ID = @userID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Opening connection to the database
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userID", LoggedUserID);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Refreshing the table
                    tableLayoutPanel.Controls.Clear();
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.RowStyles.Clear();
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                    // Adding table headers
                    AddHeaderLabel("Tournament Name", 0, 0);
                    AddHeaderLabel("Team quantity", 1, 0);
                    AddHeaderLabel("Delete", 2, 0);

                    int rowIndex = 1;


                    // Displaying each tournament created by the logged-in user
                    while (reader.Read())
                    {
                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                        int tournamentID = reader.GetInt32("tournament_id");
                        Label nameLabel = new Label()
                        {
                            Text = reader["name"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        nameLabel.Click += (sender, e) =>
                        {
                            Form3 form = new Form3(tournamentID);
                            form.Show();
                        };
                        Label teamCountLabel = new Label()
                        {
                            Text = reader["teams_counter"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Button tournamentDeleteButton = new Button()
                        {
                            Text = "Delete",
                            AutoSize = true,
                            Dock = DockStyle.Fill,

                        };

                        // Calling tournament deletion by button
                        tournamentDeleteButton.Click += (sender, e) =>
                        {
                            DeleteTournament(tournamentID);

                            LoadTournaments();
                        };

                        tableLayoutPanel.Controls.Add(nameLabel, 0, rowIndex);
                        tableLayoutPanel.Controls.Add(teamCountLabel, 1, rowIndex);
                        tableLayoutPanel.Controls.Add(tournamentDeleteButton, 2, rowIndex);

                        rowIndex++;
                        tableLayoutPanel.RowCount = rowIndex;
                    }
                    // Adjusting table size
                    tableLayoutPanel.Size = new Size(tableLayoutPanel.PreferredSize.Width, tableLayoutPanel.PreferredSize.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        // Method connecting to the database
        private void ExecuteQuery(string query, Action<MySqlCommand> parameterize, Action<MySqlDataReader> readAction = null)
        {
            string connectionString = "Server=localhost;Database=tournamentdb;User ID=Tester;Password=123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
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
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        // Method generating headers
        private void AddHeaderLabel(string text, int column, int row)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 5, 0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            tableLayoutPanel.Controls.Add(label, column, row);
        }


        // Method deleting a tournament by its ID
        private void DeleteTournament(int tournamentID)
        {
            string query = "DELETE FROM tournament WHERE Tournament_ID = @tournamentID";
            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@tournamentID", tournamentID);
            });
        }
    }
}
