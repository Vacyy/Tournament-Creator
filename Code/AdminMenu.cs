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
    public partial class AdminMenu : Form
    {
        public int LoggedUserID { get; private set; }

        public AdminMenu(int UserID)
        {
            LoggedUserID = UserID; // Set the logged-in user ID
            InitializeComponent(); // Initialize form components
        }

        private void buttonManageUsers_Click(object sender, EventArgs e)
        {
            // Navigate to the user management panel
            ManageUsers manageUsersForm = new ManageUsers();
            manageUsersForm.ShowDialog();
        }

        private void buttonBrowseAllTournaments_Click(object sender, EventArgs e)
        {
            // Code handling browsing all tournaments
            BrowseAllTournaments browseTournamentsForm = new BrowseAllTournaments();
            browseTournamentsForm.ShowDialog();
        }

        private void buttonLogut_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the current form (AdminMenu)
            Application.Restart(); // Restart the application
        }
    }
}
