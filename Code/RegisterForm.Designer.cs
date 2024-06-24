namespace TournamentCreatorWinForms
{
    partial class RegisterForm
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
            txtUsername = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            lblUsername = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(140, 35);
            txtUsername.Margin = new Padding(4, 3, 4, 3);
            txtUsername.MaxLength = 16;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(174, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(140, 81);
            txtEmail.Margin = new Padding(4, 3, 4, 3);
            txtEmail.MaxLength = 80;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(174, 23);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(140, 127);
            txtPassword.Margin = new Padding(4, 3, 4, 3);
            txtPassword.MaxLength = 32;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(174, 23);
            txtPassword.TabIndex = 2;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblUsername.Location = new Point(35, 35);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(73, 17);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblEmail.Location = new Point(35, 81);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 17);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.White;
            lblPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblPassword.Location = new Point(35, 127);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 17);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password:";
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(140, 173);
            btnRegister.Margin = new Padding(4, 3, 4, 3);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(88, 27);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg_bggenerator_com__2_;
            ClientSize = new Size(350, 231);
            Controls.Add(btnRegister);
            Controls.Add(lblPassword);
            Controls.Add(lblEmail);
            Controls.Add(lblUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "RegisterForm";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnRegister;
    }
}