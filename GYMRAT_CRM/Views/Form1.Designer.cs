namespace CRM.winforms
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.topPanel = new System.Windows.Forms.Panel();
            this.profileLabel = new System.Windows.Forms.Label();
            this.notificationLabel = new System.Windows.Forms.Label();
            this.logoLabel = new System.Windows.Forms.Label();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.syncBadgePanel = new System.Windows.Forms.Panel();
            this.syncDotPanel = new System.Windows.Forms.Panel();
            this.syncBadgeLabel = new System.Windows.Forms.Label();
            this.topPanelBorder = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();

            // ensure new controls are instantiated before property assignment to avoid NullReferenceExceptions
            // (syncBadgePanel, syncDotPanel, syncBadgeLabel, topPanelBorder) were missing instantiation previously
            // and caused syncBadgePanel to be null at runtime. This change is safe: it mirrors how the designer
            // normally instantiates controls before setting properties.

            //
            // topPanel
            // NOTE: added to Controls LAST (see Form1.cs comment / InitializeComponent
            // at bottom) so it docks above the sidebar and spans the full width.
            //
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.profileLabel);
            this.topPanel.Controls.Add(this.notificationLabel);
            this.topPanel.Controls.Add(this.logoLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1000, 60);
            this.topPanel.TabIndex = 2;
            //
            // logoLabel
            //
            this.logoLabel.AutoSize = true;
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.logoLabel.ForeColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.logoLabel.Location = new System.Drawing.Point(24, 18);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(90, 25);
            this.logoLabel.TabIndex = 0;
            this.logoLabel.Text = "GymRat";
            //
            // notificationLabel
            //
            this.notificationLabel.AutoSize = true;
            this.notificationLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.notificationLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.notificationLabel.Location = new System.Drawing.Point(700, 20);
            this.notificationLabel.Name = "notificationLabel";
            this.notificationLabel.Size = new System.Drawing.Size(20, 21);
            this.notificationLabel.TabIndex = 1;
            this.notificationLabel.Text = "\U0001F514";
            //
            // profileLabel
            //
            this.profileLabel.AutoSize = true;
            this.profileLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profileLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.profileLabel.Location = new System.Drawing.Point(760, 20);
            this.profileLabel.Name = "profileLabel";
            this.profileLabel.Size = new System.Drawing.Size(120, 21);
            this.profileLabel.TabIndex = 2;
            this.profileLabel.Text = "Moni Roy \u25BE";
            this.profileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // syncBadgePanel
            //
            this.syncBadgePanel.Location = new System.Drawing.Point(860, 18);
            this.syncBadgePanel.Name = "syncBadgePanel";
            this.syncBadgePanel.Size = new System.Drawing.Size(120, 24);
            this.syncBadgePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //
            // syncDotPanel
            //
            this.syncDotPanel.Location = new System.Drawing.Point(6, 7);
            this.syncDotPanel.Name = "syncDotPanel";
            this.syncDotPanel.Size = new System.Drawing.Size(10, 10);
            this.syncDotPanel.TabIndex = 0;
            this.syncDotPanel.BackColor = System.Drawing.Color.FromArgb(34, 197, 94); // green
            this.syncDotPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.SyncDotPanel_Paint);
            //
            // syncBadgeLabel
            //
            this.syncBadgeLabel.AutoSize = true;
            this.syncBadgeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.syncBadgeLabel.Location = new System.Drawing.Point(22, 3);
            this.syncBadgeLabel.Name = "syncBadgeLabel";
            this.syncBadgeLabel.Size = new System.Drawing.Size(38, 15);
            this.syncBadgeLabel.TabIndex = 1;
            this.syncBadgeLabel.Text = "Synced";
            this.syncBadgeLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            //
            // topPanelBorder
            //
            this.topPanelBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.topPanelBorder.Height = 1;
            this.topPanelBorder.BackColor = System.Drawing.Color.FromArgb(229, 231, 235);
            // assemble sync badge and add to top panel
            this.syncBadgePanel.Controls.Add(this.syncDotPanel);
            this.syncBadgePanel.Controls.Add(this.syncBadgeLabel);
            this.topPanel.Controls.Add(this.syncBadgePanel);
            this.topPanel.Controls.Add(this.topPanelBorder);
            //
            // sidebarPanel
            //
            this.sidebarPanel.BackColor = System.Drawing.Color.White;
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 60);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(220, 540);
            this.sidebarPanel.TabIndex = 0;
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(220, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(780, 540);
            this.contentPanel.TabIndex = 1;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            // Order matters for docking: Fill first, then Left, then Top last.
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.topPanel);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GymRat CRM";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Label notificationLabel;
        private System.Windows.Forms.Label profileLabel;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel syncBadgePanel;
        private System.Windows.Forms.Panel syncDotPanel;
        private System.Windows.Forms.Label syncBadgeLabel;
        private System.Windows.Forms.Panel topPanelBorder;
    }
}