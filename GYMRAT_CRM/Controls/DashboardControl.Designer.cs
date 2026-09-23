namespace CRM.winforms.Controls
{
    partial class DashboardControl
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
            this.cardsPanel = new System.Windows.Forms.Panel();
            this.chartsHost = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
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
            this.titleLabel.Size = new System.Drawing.Size(120, 30);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Dashboard";
            //
            // cardsPanel
            //
            this.cardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardsPanel.Location = new System.Drawing.Point(0, 64);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.cardsPanel.Size = new System.Drawing.Size(948, 130);
            this.cardsPanel.TabIndex = 1;
            //
            // chartsHost
            // Container for the two chart cards, side by side. Padding matches
            // cardsPanel so the chart card edges align with the KPI card edges.
            //
            this.chartsHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartsHost.Location = new System.Drawing.Point(0, 194);
            this.chartsHost.Name = "chartsHost";
            this.chartsHost.Padding = new System.Windows.Forms.Padding(24, 0, 24, 24);
            this.chartsHost.Size = new System.Drawing.Size(948, 366);
            this.chartsHost.TabIndex = 2;
            //
            // DashboardControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.chartsHost);
            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel cardsPanel;
        private System.Windows.Forms.Panel chartsHost;
    }
}