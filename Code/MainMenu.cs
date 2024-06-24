using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class MainMenu : Form
    {
        public int LoggedUserID { get; private set; }

        // Constructor to initialize the main menu form
        public MainMenu(int userID)
        {
            LoggedUserID = userID;
            InitializeComponent();
        }

        // Event handler for creating a new tournament
        private void buttonCreateTournament_Click(object sender, EventArgs e)
        {
            // Handling code for creating a new tournament
            Form1 form1 = new Form1(LoggedUserID);
            form1.ShowDialog();
        }

        // Event handler for browsing existing tournaments
        private void buttonBrowseTournaments_Click(object sender, EventArgs e)
        {
            // Handling code for browsing tournaments
            BrowseTournaments BrowseTournaments = new BrowseTournaments(LoggedUserID);
            BrowseTournaments.ShowDialog();
        }

        // Event handler for logging out
        private void buttonLogut_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the current form
            Application.Restart(); // Restart the application
        }
    }
}
