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
    public partial class BrowseAllTournaments : Form
    {
        public int LoggedUserID { get; private set; }

        // Call window initialization
        public BrowseAllTournaments() // Window initialization
        {
            InitializeComponent();
        }


        // Method used for proper window operation
        private void BrowseAllTournaments_Load(object sender, EventArgs e)
        {
            LoadAllTournaments();
            CenterTableLayoutPanel();
        }

        private void CenterTableLayoutPanel() // Method to center the table
        {
            // Calculate the center position
            int x = (this.ClientSize.Width - tableLayoutPanel.Width) / 2;

            // Set the location
            tableLayoutPanel.Location = new Point(x);
        }


        // Method to generate a table with all tournaments
        private void LoadAllTournaments()
        {
            string connectionString = "server=localhost;user=Tester;database=tournamentdb;password=123";
            string query = "SELECT name, teams_counter, tournament_id, user_ID FROM tournament";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    // Opening a connection to the database
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Refreshing the table
                    tableLayoutPanel.Controls.Clear();
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.RowStyles.Clear();
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                    // Adding table headers
                    AddHeaderLabel("Tournament", 0, 0);
                    AddHeaderLabel("ser ID", 1, 0);
                    AddHeaderLabel("Tournament Name", 2, 0);
                    AddHeaderLabel("Team quantity", 3, 0);
                    AddHeaderLabel("Delete", 4, 0);

                    int rowIndex = 1;


                    // Displaying each tournament stored in the database
                    while (reader.Read())
                    {
                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                        int tournamentID = reader.GetInt32("tournament_id");
                        Label tournamentIDLabel = new Label()
                        {
                            Text = reader["tournament_ID"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Label userIDLabel = new Label()
                        {
                            Text = reader["user_ID"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Label tournamentNameLabel = new Label()
                        {
                            Text = reader["Name"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Label teamCountLabel = new Label()
                        {
                            Text = reader["Teams_counter"].ToString(),
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

                        tournamentIDLabel.Click += (sender, e) =>
                        {
                            Form3 form = new Form3(tournamentID);
                            form.Show();
                        };
                        userIDLabel.Click += (sender, e) =>
                        {
                            Form3 form = new Form3(tournamentID);
                            form.Show();
                        };
                        tournamentNameLabel.Click += (sender, e) =>
                        {
                            Form3 form = new Form3(tournamentID);
                            form.Show();
                        };
                        teamCountLabel.Click += (sender, e) =>
                        {
                            Form3 form = new Form3(tournamentID);
                            form.Show();
                        };


                        // Calling the deletion of a specific tournament
                        tournamentDeleteButton.Click += (sender, e) =>
                        {
                            DeleteTournament(tournamentID);

                            LoadAllTournaments();
                        };

                        tableLayoutPanel.Controls.Add(tournamentIDLabel, 0, rowIndex);
                        tableLayoutPanel.Controls.Add(userIDLabel, 1, rowIndex);
                        tableLayoutPanel.Controls.Add(tournamentNameLabel, 2, rowIndex);
                        tableLayoutPanel.Controls.Add(teamCountLabel, 3, rowIndex);
                        tableLayoutPanel.Controls.Add(tournamentDeleteButton, 4, rowIndex);

                        rowIndex++;
                        tableLayoutPanel.RowCount = rowIndex;
                    }
                    // Adjusting the size of the table to the window
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


        // Method generating column headers
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


        // Method deleting a specific tournament by its ID
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
