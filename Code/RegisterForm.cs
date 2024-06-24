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
using System.Security.Cryptography;

namespace TournamentCreatorWinForms
{
    public partial class RegisterForm : Form
    {
        // Initialize the form
        public RegisterForm()
        {
            InitializeComponent();
        }

        // Method for registering a user
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Input data validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.");
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.");
                return;
            }

            if (password.Length < 3)
            {
                MessageBox.Show("Password must be at least 3 characters long.");
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            string hashedPassword = PasswordHasher.HashPassword(password);

            if (RegisterUser(username, email, hashedPassword))
            {
                MessageBox.Show("Registration successful. You can now log in.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }
        }

        // Method for registering a user in the database
        private bool RegisterUser(string username, string email, string password)
        {
            // Implement your registration logic here
            string query = "INSERT INTO users (username, email, password) VALUES (@Username, @Email, @Password)";

            if (UserExists(username, email))
            {
                MessageBox.Show("This username or email is already taken.");
                return false;
            }
            else
            {
                try
                {
                    ExecuteQuery(query, (cmd) =>
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                    });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // Method checking if a user with given credentials already exists
        private bool UserExists(string username, string email)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @Username OR email = @Email";
            bool exists = false;

            ExecuteQuery(query, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
            },
            (reader) =>
            {
                if (reader.Read())
                {
                    exists = reader.GetInt32(0) > 0;
                }
            });

            return exists;
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

        // Method to verify email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    // Password hashing class
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
