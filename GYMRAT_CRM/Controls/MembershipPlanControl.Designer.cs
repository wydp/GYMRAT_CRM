namespace CRM.winforms.Controls
{
    partial class MembershipPlanControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.formCard = new System.Windows.Forms.Panel();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.dgvPlans = new System.Windows.Forms.DataGridView();
            this.headerPanel.SuspendLayout();
            this.formCard.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlans)).BeginInit();
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
            this.titleLabel.Text = "Membership Plans";
            //
            // formCard
            //
            this.formCard.BackColor = System.Drawing.Color.White;
            this.formCard.Controls.Add(this.chkActive);
            this.formCard.Controls.Add(this.lblDuration);
            this.formCard.Controls.Add(this.txtDuration);
            this.formCard.Controls.Add(this.lblPrice);
            this.formCard.Controls.Add(this.txtPrice);
            this.formCard.Controls.Add(this.lblDescription);
            this.formCard.Controls.Add(this.txtDescription);
            this.formCard.Controls.Add(this.lblName);
            this.formCard.Controls.Add(this.txtName);
            this.formCard.Controls.Add(this.lblCode);
            this.formCard.Controls.Add(this.txtCode);
            this.formCard.Controls.Add(this.btnAdd);
            this.formCard.Controls.Add(this.btnUpdate);
            this.formCard.Controls.Add(this.btnDeactivate);
            this.formCard.Controls.Add(this.btnRefresh);
            this.formCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.formCard.Location = new System.Drawing.Point(0, 64);
            this.formCard.Name = "formCard";
            this.formCard.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.formCard.Size = new System.Drawing.Size(948, 220);
            this.formCard.TabIndex = 1;
            //
            // lblCode
            //
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCode.Location = new System.Drawing.Point(24, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 15);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Plan Code";
            //
            // txtCode
            //
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.Location = new System.Drawing.Point(24, 34);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(200, 25);
            this.txtCode.TabIndex = 1;
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblName.Location = new System.Drawing.Point(248, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Plan Name";
            //
            // txtName
            //
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(248, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 25);
            this.txtName.TabIndex = 3;
            //
            // lblPrice
            //
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblPrice.Location = new System.Drawing.Point(472, 16);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(40, 15);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price";
            //
            // txtPrice
            //
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.Location = new System.Drawing.Point(472, 34);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(140, 25);
            this.txtPrice.TabIndex = 5;
            //
            // lblDuration
            //
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDuration.Location = new System.Drawing.Point(632, 16);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(110, 15);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "Duration (Days)";
            //
            // txtDuration
            //
            this.txtDuration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDuration.Location = new System.Drawing.Point(632, 34);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(140, 25);
            this.txtDuration.TabIndex = 7;
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDescription.Location = new System.Drawing.Point(24, 74);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 15);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Description";
            //
            // txtDescription
            //
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(24, 92);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(748, 25);
            this.txtDescription.TabIndex = 9;
            //
            // chkActive
            //
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.chkActive.Location = new System.Drawing.Point(24, 134);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(60, 19);
            this.chkActive.TabIndex = 10;
            this.chkActive.Text = "Active";
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(24, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 36);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            //
            // btnUpdate
            //
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(146, 168);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 36);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            //
            // btnDeactivate
            //
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(268, 168);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(110, 36);
            this.btnDeactivate.TabIndex = 13;
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            //
            // btnRefresh
            //
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(390, 168);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 36);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh / Clear";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            //
            // gridPanel
            //
            this.gridPanel.Controls.Add(this.dgvPlans);
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Location = new System.Drawing.Point(0, 284);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Padding = new System.Windows.Forms.Padding(24, 0, 24, 24);
            this.gridPanel.Size = new System.Drawing.Size(948, 276);
            this.gridPanel.TabIndex = 2;
            //
            // dgvPlans
            //
            this.dgvPlans.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlans.Location = new System.Drawing.Point(24, 0);
            this.dgvPlans.Name = "dgvPlans";
            this.dgvPlans.Size = new System.Drawing.Size(900, 252);
            this.dgvPlans.TabIndex = 0;
            //
            // MembershipPlanControl
            // Add order: gridPanel (Fill) first, formCard (Top) second,
            // headerPanel (Top) last — same rule as CustomerControl/Form1.
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.formCard);
            this.Controls.Add(this.headerPanel);
            this.Name = "MembershipPlanControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.formCard.ResumeLayout(false);
            this.formCard.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlans)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel formCard;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.DataGridView dgvPlans;
    }
}
