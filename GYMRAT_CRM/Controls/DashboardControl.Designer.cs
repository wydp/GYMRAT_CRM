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
            this.chartCard = new System.Windows.Forms.Panel();
            this.chartTitleLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.chartCard.SuspendLayout();
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
            // Individual KPI card panels are built in code (BuildKpiCards) —
            // this is just the container that holds them, Dock=Top so it sits
            // between the header and the chart.
            //
            this.cardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardsPanel.Location = new System.Drawing.Point(0, 64);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.cardsPanel.Size = new System.Drawing.Size(948, 142);
            this.cardsPanel.TabIndex = 1;
            //
            // chartCard
            //
            this.chartCard.BackColor = System.Drawing.Color.White;
            this.chartCard.Controls.Add(this.chartTitleLabel);
            this.chartCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCard.Location = new System.Drawing.Point(0, 206);
            this.chartCard.Margin = new System.Windows.Forms.Padding(24);
            this.chartCard.Name = "chartCard";
            this.chartCard.Padding = new System.Windows.Forms.Padding(24, 16, 24, 24);
            this.chartCard.Size = new System.Drawing.Size(948, 354);
            this.chartCard.TabIndex = 2;
            //
            // chartTitleLabel
            //
            this.chartTitleLabel.AutoSize = true;
            this.chartTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chartTitleLabel.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.chartTitleLabel.Location = new System.Drawing.Point(24, 16);
            this.chartTitleLabel.Name = "chartTitleLabel";
            this.chartTitleLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.chartTitleLabel.Size = new System.Drawing.Size(160, 33);
            this.chartTitleLabel.TabIndex = 0;
            this.chartTitleLabel.Text = "Revenue (Last 6 Months)";
            //
            // DashboardControl
            // Add order: chartCard (Fill) first, cardsPanel (Top) second,
            // headerPanel (Top) last — same convention as the rest of the app.
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.chartCard);
            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.chartCard.ResumeLayout(false);
            this.chartCard.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel cardsPanel;
        private System.Windows.Forms.Panel chartCard;
        private System.Windows.Forms.Label chartTitleLabel;
    }
}
