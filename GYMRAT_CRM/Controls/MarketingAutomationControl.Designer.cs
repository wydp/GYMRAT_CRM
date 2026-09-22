namespace CRM.winforms.Controls
{
    partial class MarketingAutomationControl
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCampaigns = new System.Windows.Forms.TabPage();
            this.tabPromotions = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabCampaigns);
            this.tabControl.Controls.Add(this.tabPromotions);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 600);
            this.tabControl.TabIndex = 0;
            //
            // tabCampaigns
            //
            this.tabCampaigns.Location = new System.Drawing.Point(4, 24);
            this.tabCampaigns.Name = "tabCampaigns";
            this.tabCampaigns.Padding = new System.Windows.Forms.Padding(3);
            this.tabCampaigns.Size = new System.Drawing.Size(792, 572);
            this.tabCampaigns.TabIndex = 0;
            this.tabCampaigns.Text = "Campaigns";
            this.tabCampaigns.UseVisualStyleBackColor = true;
            //
            // tabPromotions
            //
            this.tabPromotions.Location = new System.Drawing.Point(4, 24);
            this.tabPromotions.Name = "tabPromotions";
            this.tabPromotions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPromotions.Size = new System.Drawing.Size(792, 572);
            this.tabPromotions.TabIndex = 1;
            this.tabPromotions.Text = "Promotions && Discounts";
            this.tabPromotions.UseVisualStyleBackColor = true;
            //
            // MarketingAutomationControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "MarketingAutomationControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCampaigns;
        private System.Windows.Forms.TabPage tabPromotions;
    }
}