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

    public partial class Winner : Form
    {
        private int TournamentId;
        public Winner(int tournamentId)
        {
            this.TournamentId = tournamentId;
            GetTeamPositions();
            InitializeComponent();

        }
        private void GetTeamPositions()
        {
            string winnerName = "";
            string runnerUpName = "";

            var query1 = "SELECT name FROM team WHERE Team_ID =(SELECT Winner_ID FROM Matches WHERE Tournament_ID = @TournamentID AND Round = (SELECT MAX(Round) FROM Matches WHERE Tournament_ID = @TournamentID));";
            ExecuteQuery(query1, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", TournamentId);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    winnerName = reader.GetString("name");
                }
            });

            var query2 = "SELECT name " +
                "FROM team " +
                "WHERE Team_ID = (" +
                "SELECT CASE " +
                "WHEN Team_ID_1 = Winner_ID THEN Team_ID_2 " +
                "ELSE Team_ID_1 " +
                "END " +
                "FROM matches " +
                "WHERE Tournament_ID = @TournamentID " +
                "AND Round = (SELECT MAX(Round) " +
                "FROM matches " +
                "WHERE Tournament_ID = @TournamentID));";
            ExecuteQuery(query2, (cmd) =>
            {
                cmd.Parameters.AddWithValue("@TournamentID", TournamentId);
            }, (reader) =>
            {
                if (reader.Read())
                {
                    runnerUpName = reader.GetString("name");
                }
            });

            // Create labels
            Label label1 = new Label()
            {
                Text = winnerName,
                AutoSize = true,
                BackColor = Color.FromArgb(255, 220, 0),
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic),
                Location = new Point(180, 39),
                Name = "label1",
                Padding = new Padding(10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label label2 = new Label()
            {
                Text = runnerUpName,
                AutoSize = true,
                BackColor = Color.FromArgb(165, 169, 180),
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic),
                Location = new Point(180, 102),
                Name = "label2",
                Padding = new Padding(10),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // Add labels to form
            this.Controls.Add(label1);
            this.Controls.Add(label2);

            // Calculate preferred widths
            int preferredWidth = Math.Max(label1.PreferredWidth, label2.PreferredWidth);

            // Set both labels to the same width
            label1.Width = preferredWidth;
            label2.Width = preferredWidth;
        }
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
    }
}
