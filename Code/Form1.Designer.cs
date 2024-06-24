namespace TournamentCreatorWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnCreateTournament = new System.Windows.Forms.Button();
            this.txtTeamName = new System.Windows.Forms.TextBox();
            this.btnAddTeam = new System.Windows.Forms.Button();
            this.listBoxTeams = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextTournamentName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCreateTournament
            // 
            this.btnCreateTournament.Enabled = false;
            this.btnCreateTournament.Location = new System.Drawing.Point(12, 261);
            this.btnCreateTournament.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateTournament.Name = "btnCreateTournament";
            this.btnCreateTournament.Size = new System.Drawing.Size(170, 27);
            this.btnCreateTournament.TabIndex = 0;
            this.btnCreateTournament.Text = "Create Tournament";
            this.btnCreateTournament.UseVisualStyleBackColor = true;
            btnCreateTournament.Click += btnCreateTournament_Click;
            // 
            // txtTeamName
            // 
            this.txtTeamName.Location = new System.Drawing.Point(12, 44);
            this.txtTeamName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTeamName.MaxLength = 30;
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.Size = new System.Drawing.Size(172, 23);
            this.txtTeamName.TabIndex = 1;
            // 
            // btnAddTeam
            // 
            this.btnAddTeam.Location = new System.Drawing.Point(12, 73);
            this.btnAddTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddTeam.Name = "btnAddTeam";
            this.btnAddTeam.Size = new System.Drawing.Size(172, 27);
            this.btnAddTeam.TabIndex = 2;
            this.btnAddTeam.Text = "Add Team";
            this.btnAddTeam.UseVisualStyleBackColor = true;
            this.btnAddTeam.Click += btnAddTeam_Click;
            // 
            // listBoxTeams
            // 
            this.listBoxTeams.FormattingEnabled = true;
            this.listBoxTeams.ItemHeight = 15;
            this.listBoxTeams.Location = new System.Drawing.Point(192, 44);
            this.listBoxTeams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxTeams.Name = "listBoxTeams";
            this.listBoxTeams.Size = new System.Drawing.Size(157, 244);
            this.listBoxTeams.TabIndex = 3;
            listBoxTeams.MouseDoubleClick += ListBoxTeams_MouseDoubleClick;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Team Name:";
            // 
            // TextTournamentName
            // 
            this.TextTournamentName.Location = new System.Drawing.Point(12, 232);
            this.TextTournamentName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextTournamentName.MaxLength = 60;
            this.TextTournamentName.Name = "TextTournamentName";
            this.TextTournamentName.Size = new System.Drawing.Size(170, 23);
            this.TextTournamentName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tournament Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TournamentCreatorWinForms.Properties.Resources.bg_bggenerator_com__4_;
            this.ClientSize = new System.Drawing.Size(372, 348);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextTournamentName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxTeams);
            this.Controls.Add(this.btnAddTeam);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.btnCreateTournament);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Tournament Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnCreateTournament;
        private System.Windows.Forms.TextBox txtTeamName;
        private System.Windows.Forms.Button btnAddTeam;
        private System.Windows.Forms.ListBox listBoxTeams;
        private Label label1;
        private TextBox TextTournamentName;
        private Label label2;
    }
}
