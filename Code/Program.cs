namespace TournamentCreatorWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                if (loginForm.GetUserStatus(loginForm.Username)) // Checking if user is an admin
                {
                    Application.Run(new AdminMenu(loginForm.UserID));
                }
                else
                {
                    Application.Run(new MainMenu(loginForm.UserID)); 
                }
            }
        }

    }
}