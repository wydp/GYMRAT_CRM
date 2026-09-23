namespace CRM.winforms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.leftPanel = new System.Windows.Forms.Panel();
            this.brandLabel = new System.Windows.Forms.Label();
            this.taglineLabel = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.cardPanel = new System.Windows.Forms.Panel();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.cardPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // leftPanel
            //
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.leftPanel.Controls.Add(this.taglineLabel);
            this.leftPanel.Controls.Add(this.brandLabel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(500, 700);
            this.leftPanel.TabIndex = 0;
            //
            // brandLabel
            //
            this.brandLabel.AutoSize = false;
            this.brandLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.brandLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.brandLabel.ForeColor = System.Drawing.Color.White;
            this.brandLabel.Location = new System.Drawing.Point(0, 250);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.brandLabel.Size = new System.Drawing.Size(500, 70);
            this.brandLabel.TabIndex = 0;
            this.brandLabel.Text = "GymRat";
            this.brandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // taglineLabel
            //
            this.taglineLabel.AutoSize = false;
            this.taglineLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.taglineLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.taglineLabel.ForeColor = System.Drawing.Color.FromArgb(220, 235, 255);
            this.taglineLabel.Location = new System.Drawing.Point(0, 320);
            this.taglineLabel.Name = "taglineLabel";
            this.taglineLabel.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.taglineLabel.Size = new System.Drawing.Size(500, 30);
            this.taglineLabel.TabIndex = 1;
            this.taglineLabel.Text = "Gym Customer Relationship Management";
            this.taglineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // rightPanel
            //
            this.rightPanel.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.rightPanel.Controls.Add(this.cardPanel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(500, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(500, 700);
            this.rightPanel.TabIndex = 1;
            //
            // cardPanel
            //
            this.cardPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cardPanel.BackColor = System.Drawing.Color.White;
            this.cardPanel.Controls.Add(this.btnLogin);
            this.cardPanel.Controls.Add(this.lblError);
            this.cardPanel.Controls.Add(this.txtPassword);
            this.cardPanel.Controls.Add(this.lblPassword);
            this.cardPanel.Controls.Add(this.txtUsername);
            this.cardPanel.Controls.Add(this.lblUsername);
            this.cardPanel.Controls.Add(this.subtitleLabel);
            this.cardPanel.Controls.Add(this.welcomeLabel);
            this.cardPanel.Location = new System.Drawing.Point(50, 150);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Size = new System.Drawing.Size(400, 420);
            this.cardPanel.TabIndex = 0;
            //
            // welcomeLabel
            //
            this.welcomeLabel.AutoSize = false;
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.welcomeLabel.Location = new System.Drawing.Point(40, 40);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(320, 40);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Welcome back";
            //
            // subtitleLabel
            //
            this.subtitleLabel.AutoSize = false;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.subtitleLabel.Location = new System.Drawing.Point(40, 80);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(320, 20);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Sign in to your account to continue";
            //
            // lblUsername
            //
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblUsername.Location = new System.Drawing.Point(40, 130);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(60, 15);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            //
            // txtUsername
            //
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(40, 152);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(320, 25);
            this.txtUsername.TabIndex = 3;
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPassword.Location = new System.Drawing.Point(40, 195);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            //
            // txtPassword
            //
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(40, 217);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(320, 25);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            //
            // lblError
            //
            this.lblError.AutoSize = false;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblError.Location = new System.Drawing.Point(40, 255);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(320, 20);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "";
            //
            // btnLogin
            //
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(40, 290);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(320, 42);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Sign In";
            this.btnLogin.UseVisualStyleBackColor = false;
            //
            // LoginForm
            //
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GymRat CRM — Sign In";
            this.leftPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.cardPanel.ResumeLayout(false);
            this.cardPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label taglineLabel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel cardPanel;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnLogin;
    }
}