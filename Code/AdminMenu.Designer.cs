using Microsoft.VisualBasic.ApplicationServices;
using TournamentCreatorWinForms.Properties;

namespace TournamentCreatorWinForms
{
    partial class AdminMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonCreateTournament = new Button();
            buttonBrowseTournaments = new Button();
            printDialog1 = new PrintDialog();
            label1 = new Label();
            buttonLogut = new Button();
            SuspendLayout();
            // 
            // buttonCreateTournament
            // 
            buttonCreateTournament.Cursor = Cursors.Hand;
            buttonCreateTournament.FlatAppearance.BorderColor = Color.Black;
            buttonCreateTournament.FlatAppearance.BorderSize = 10;
            buttonCreateTournament.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateTournament.Location = new Point(79, 96);
            buttonCreateTournament.Name = "buttonCreateTournament";
            buttonCreateTournament.Size = new Size(163, 36);
            buttonCreateTournament.TabIndex = 1;
            buttonCreateTournament.Text = "Manage users";
            buttonCreateTournament.UseVisualStyleBackColor = true;
            buttonCreateTournament.Click += buttonManageUsers_Click;
            // 
            // buttonBrowseTournaments
            // 
            buttonBrowseTournaments.Cursor = Cursors.Hand;
            buttonBrowseTournaments.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBrowseTournaments.Location = new Point(79, 138);
            buttonBrowseTournaments.Name = "buttonBrowseTournaments";
            buttonBrowseTournaments.Size = new Size(163, 52);
            buttonBrowseTournaments.TabIndex = 2;
            buttonBrowseTournaments.Text = "Manage tournaments";
            buttonBrowseTournaments.UseVisualStyleBackColor = true;
            buttonBrowseTournaments.Click += buttonBrowseAllTournaments_Click;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(255, 220, 0);
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(69, 23);
            label1.Name = "label1";
            label1.Padding = new Padding(10);
            label1.Size = new Size(187, 43);
            label1.TabIndex = 3;
            label1.Text = "TournamentMachine";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonLogut
            // 
            buttonLogut.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogut.Location = new Point(120, 310);
            buttonLogut.Name = "buttonLogut";
            buttonLogut.Size = new Size(84, 37);
            buttonLogut.TabIndex = 4;
            buttonLogut.Text = "Log out";
            buttonLogut.UseVisualStyleBackColor = true;
            buttonLogut.Click += buttonLogut_Click;
            // 
            // AdminMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resources.trophy;
            ClientSize = new Size(321, 359);
            Controls.Add(buttonLogut);
            Controls.Add(label1);
            Controls.Add(buttonBrowseTournaments);
            Controls.Add(buttonCreateTournament);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AdminMenu";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonCreateTournament;
        private System.Windows.Forms.Button buttonBrowseTournaments;
        private PrintDialog printDialog1;
        private Label label1;
        private Button buttonLogut;
    }
}