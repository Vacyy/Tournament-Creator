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
    public partial class ManageUsers : Form
    {
        // Form initialization
        public ManageUsers() // Initialize the form
        {
            InitializeComponent();
        }

        // Methods used for the proper functioning of the form

        private void ManageUsers_Load(object sender, EventArgs e) // Load the table with users
        {
            LoadUsers();
            CenterTableLayoutPanel();
        }

        private void CenterTableLayoutPanel() // Method to center the table
        {
            int x = (this.ClientSize.Width - tableLayoutPanel.Width) / 2;

            tableLayoutPanel.Location = new Point(x);
        }

        private void LoadUsers() // Method to generate and manage a table of users
        {
            string connectionString = "server=localhost;user=Tester;database=tournamentdb;password=123";
            string query = "SELECT user_ID, username, email FROM users WHERE Admin = 0";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open database connection
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Refresh the table
                    tableLayoutPanel.Controls.Clear();
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.RowStyles.Clear();
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                    // Add table headers
                    AddHeaderLabel("User ID", 0, 0);
                    AddHeaderLabel("Username", 1, 0);
                    AddHeaderLabel("User Email", 2, 0);
                    AddHeaderLabel("Action", 3, 0);

                    int rowIndex = 1;

                    // Display each user sequentially
                    while (reader.Read())
                    {
                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                        int userID = reader.GetInt32(reader.GetOrdinal("user_ID"));
                        Label userIDLabel = new Label()
                        {
                            Text = userID.ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Label userNameLabel = new Label()
                        {
                            Text = reader["username"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Label userEmailLabel = new Label()
                        {
                            Text = reader["email"].ToString(),
                            AutoSize = true,
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                            Dock = DockStyle.Fill
                        };
                        Button userDeleteButton = new Button()
                        {
                            Text = "Delete",
                            AutoSize = true,
                            Dock = DockStyle.Fill,

                        };

                        // Handle user deletion when clicked
                        userDeleteButton.Click += (sender, e) =>
                        {
                            DeleteUsers(userID);

                            LoadUsers();
                        };

                        tableLayoutPanel.Controls.Add(userIDLabel, 0, rowIndex);
                        tableLayoutPanel.Controls.Add(userNameLabel, 1, rowIndex);
                        tableLayoutPanel.Controls.Add(userEmailLabel, 2, rowIndex);
                        tableLayoutPanel.Controls.Add(userDeleteButton, 3, rowIndex);

                        rowIndex++;
                        tableLayoutPanel.RowCount = rowIndex;
                    }

                    // Adjust table size to fit window size
                    tableLayoutPanel.Size = new Size(tableLayoutPanel.PreferredSize.Width, tableLayoutPanel.PreferredSize.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message); // Display database connection error message
                }
            }
        }

        // Method to connect to the database
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

        // Method to generate table headers
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

        // Method to delete a selected user
        private void DeleteUsers(int UserID)
        {
            string query = "DELETE FROM users WHERE user_ID = @UserID";
            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);
            });
        }
    }
}
