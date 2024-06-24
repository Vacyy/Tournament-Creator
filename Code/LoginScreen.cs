using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentCreatorWinForms
{
    public partial class LoginForm : Form
    {
        public bool IsAuthenticated { get; private set; } // Whether the user is authenticated
        public string LoggedInUser { get; private set; } // Logged-in username
        public int UserID { get; private set; } // Logged-in user's ID

        public string Username { get; private set; } // Username

        public LoginForm()
        {
            this.UserID = -1; // Initialize user ID to -1, indicating no logged-in user
            InitializeComponent(); // Initialize form components
        }

        // Method handling login button click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text; // Get username from text box
            string password = txtPassword.Text; // Get password from text box
            string hashedPassword = PasswordHasher.HashPassword(password); // Hash the password

            if (AuthenticateUser(username, hashedPassword))
            {
                // If authentication succeeds
                IsAuthenticated = true; // Set authenticated flag to true
                LoggedInUser = username; // Assign logged-in username
                this.DialogResult = DialogResult.OK; // Set form result to OK
                this.Close(); // Close the form
            }
            else
            {
                MessageBox.Show("Invalid username or password."); // Show message for invalid credentials
            }
        }

        // Method redirecting to user registration window
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        // Method checking if user exists
        private bool AuthenticateUser(string username, string hashedPassword)
        {
            UserID = -1; // ID -1 means user not found; rest will fail if user isn't found
            string query = "SELECT user_id, username FROM users WHERE username = @Username AND password = @Password";

            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    UserID = reader.GetInt32("user_id");
                    Username = reader.GetString("username");
                }
            });

            return UserID != -1; // Returns true if user with given login data is found
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

        // Method checking if a user is an administrator
        public bool GetUserStatus(string username)
        {
            string query = "SELECT Admin FROM users WHERE username = @username";
            bool isAdmin = false;

            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("username", username);
            },
            (reader) =>
            {
                if (reader.Read())
                {
                    isAdmin = reader.GetBoolean("Admin");
                }
            });

            return isAdmin;
        }
    }
}
