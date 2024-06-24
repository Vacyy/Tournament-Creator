using Microsoft.VisualBasic.ApplicationServices;
using TournamentCreatorWinForms.Properties;

namespace TournamentCreatorWinForms
{
    partial class MainMenu
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
            buttonCreateTournament.Location = new Point(82, 113);
            buttonCreateTournament.Name = "buttonCreateTournament";
            buttonCreateTournament.Size = new Size(163, 38);
            buttonCreateTournament.TabIndex = 1;
            buttonCreateTournament.Text = "Create Tournament";
            buttonCreateTournament.UseVisualStyleBackColor = true;
            buttonCreateTournament.Click += buttonCreateTournament_Click;
            // 
            // buttonBrowseTournaments
            // 
            buttonBrowseTournaments.Cursor = Cursors.Hand;
            buttonBrowseTournaments.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBrowseTournaments.Location = new Point(82, 170);
            buttonBrowseTournaments.Name = "buttonBrowseTournaments";
            buttonBrowseTournaments.Size = new Size(163, 51);
            buttonBrowseTournaments.TabIndex = 2;
            buttonBrowseTournaments.Text = "View my tournaments";
            buttonBrowseTournaments.UseVisualStyleBackColor = true;
            buttonBrowseTournaments.Click += buttonBrowseTournaments_Click;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(224, 224, 224);
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(66, 23);
            label1.Name = "label1";
            label1.Padding = new Padding(10);
            label1.Size = new Size(190, 41);
            label1.TabIndex = 3;
            label1.Text = "Tournament Creator";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonLogut
            // 
            buttonLogut.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonLogut.Location = new Point(114, 310);
            buttonLogut.Name = "buttonLogut";
            buttonLogut.Size = new Size(84, 37);
            buttonLogut.TabIndex = 4;
            buttonLogut.Text = "Log out";
            buttonLogut.UseVisualStyleBackColor = true;
            buttonLogut.Click += buttonLogut_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resources.bg_bggenerator_com;
            ClientSize = new Size(321, 359);
            Controls.Add(buttonLogut);
            Controls.Add(label1);
            Controls.Add(buttonBrowseTournaments);
            Controls.Add(buttonCreateTournament);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainMenu";
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