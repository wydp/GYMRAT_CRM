namespace CRM.winforms.Controls
{
    partial class CustomerSupportControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInquiries = new System.Windows.Forms.TabPage();
            this.tabFeedback = new System.Windows.Forms.TabPage();
            this.headerPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(948, 64);
            this.headerPanel.TabIndex = 0;
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.titleLabel.Location = new System.Drawing.Point(24, 18);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(200, 30);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Customer Support";
            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabInquiries);
            this.tabControl.Controls.Add(this.tabFeedback);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 64);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(948, 496);
            this.tabControl.TabIndex = 1;
            //
            // tabInquiries
            //
            this.tabInquiries.Location = new System.Drawing.Point(4, 29);
            this.tabInquiries.Name = "tabInquiries";
            this.tabInquiries.Padding = new System.Windows.Forms.Padding(0);
            this.tabInquiries.Size = new System.Drawing.Size(940, 463);
            this.tabInquiries.TabIndex = 0;
            this.tabInquiries.Text = "Inquiries";
            this.tabInquiries.UseVisualStyleBackColor = true;
            //
            // tabFeedback
            //
            this.tabFeedback.Location = new System.Drawing.Point(4, 29);
            this.tabFeedback.Name = "tabFeedback";
            this.tabFeedback.Padding = new System.Windows.Forms.Padding(0);
            this.tabFeedback.Size = new System.Drawing.Size(940, 463);
            this.tabFeedback.TabIndex = 1;
            this.tabFeedback.Text = "Feedback && Complaints";
            this.tabFeedback.UseVisualStyleBackColor = true;
            //
            // CustomerSupportControl
            // Add order: tabControl (Fill) first, headerPanel (Top) last.
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.headerPanel);
            this.Name = "CustomerSupportControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInquiries;
        private System.Windows.Forms.TabPage tabFeedback;
    }
}
