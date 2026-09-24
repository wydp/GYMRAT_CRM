namespace CRM.winforms.Controls
{
    partial class SalesForceControl
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
            this.tabProcessSale = new System.Windows.Forms.TabPage();
            this.dgvRecentSales = new System.Windows.Forms.DataGridView();
            this.lblRecent = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnProcessSale = new System.Windows.Forms.Button();
            this.dtpSaleDate = new System.Windows.Forms.DateTimePicker();
            this.lblSaleDate = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.cmbPlan = new System.Windows.Forms.ComboBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.tabMemberships = new System.Windows.Forms.TabPage();
            this.tabRenewals = new System.Windows.Forms.TabPage();
            this.tabLeads = new System.Windows.Forms.TabPage();
            this.dgvRenewals = new System.Windows.Forms.DataGridView();
            this.lblRenewalsSummary = new System.Windows.Forms.Label();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnRefreshRenewals = new System.Windows.Forms.Button();
            this.chkMembershipsActiveOnly = new System.Windows.Forms.CheckBox();
            this.btnRefreshMemberships = new System.Windows.Forms.Button();
            this.lblMembershipsSummary = new System.Windows.Forms.Label();
            this.dgvMemberships = new System.Windows.Forms.DataGridView();
            this.tabRenewals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRenewals)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabProcessSale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.tabMemberships.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).BeginInit();
            this.SuspendLayout();
            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabProcessSale);
            this.tabControl.Controls.Add(this.tabMemberships);
            this.tabControl.Controls.Add(this.tabRenewals);
            this.tabControl.Controls.Add(this.tabLeads);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 600);
            this.tabControl.TabIndex = 0;
            //
            // tabProcessSale
            //
            this.tabProcessSale.Controls.Add(this.dgvRecentSales);
            this.tabProcessSale.Controls.Add(this.lblRecent);
            this.tabProcessSale.Controls.Add(this.btnClear);
            this.tabProcessSale.Controls.Add(this.btnProcessSale);
            this.tabProcessSale.Controls.Add(this.dtpSaleDate);
            this.tabProcessSale.Controls.Add(this.lblSaleDate);
            this.tabProcessSale.Controls.Add(this.numAmount);
            this.tabProcessSale.Controls.Add(this.lblAmount);
            this.tabProcessSale.Controls.Add(this.cmbPlan);
            this.tabProcessSale.Controls.Add(this.lblPlan);
            this.tabProcessSale.Controls.Add(this.cmbCustomer);
            this.tabProcessSale.Controls.Add(this.lblCustomer);
            this.tabProcessSale.Location = new System.Drawing.Point(4, 24);
            this.tabProcessSale.Name = "tabProcessSale";
            this.tabProcessSale.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcessSale.Size = new System.Drawing.Size(792, 572);
            this.tabProcessSale.TabIndex = 0;
            this.tabProcessSale.Text = "Process Sale";
            this.tabProcessSale.UseVisualStyleBackColor = true;
            //
            // dgvRecentSales
            //
            this.dgvRecentSales.AllowUserToAddRows = false;
            this.dgvRecentSales.AllowUserToDeleteRows = false;
            this.dgvRecentSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentSales.Location = new System.Drawing.Point(20, 215);
            this.dgvRecentSales.Name = "dgvRecentSales";
            this.dgvRecentSales.ReadOnly = true;
            this.dgvRecentSales.RowHeadersWidth = 51;
            this.dgvRecentSales.Size = new System.Drawing.Size(750, 340);
            this.dgvRecentSales.TabIndex = 11;
            //
            // lblRecent
            //
            this.lblRecent.AutoSize = true;
            this.lblRecent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecent.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblRecent.Location = new System.Drawing.Point(20, 190);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(200, 19);
            this.lblRecent.TabIndex = 10;
            this.lblRecent.Text = "Recent Sales";
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(170, 140);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 32);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            //
            // btnProcessSale
            //
            this.btnProcessSale.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnProcessSale.FlatAppearance.BorderSize = 0;
            this.btnProcessSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessSale.ForeColor = System.Drawing.Color.White;
            this.btnProcessSale.Location = new System.Drawing.Point(20, 140);
            this.btnProcessSale.Name = "btnProcessSale";
            this.btnProcessSale.Size = new System.Drawing.Size(140, 32);
            this.btnProcessSale.TabIndex = 8;
            this.btnProcessSale.Text = "Process Sale";
            this.btnProcessSale.UseVisualStyleBackColor = false;
            //
            // dtpSaleDate
            //
            this.dtpSaleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSaleDate.Location = new System.Drawing.Point(220, 100);
            this.dtpSaleDate.Name = "dtpSaleDate";
            this.dtpSaleDate.Size = new System.Drawing.Size(180, 23);
            this.dtpSaleDate.TabIndex = 7;
            //
            // lblSaleDate
            //
            this.lblSaleDate.AutoSize = true;
            this.lblSaleDate.Location = new System.Drawing.Point(220, 80);
            this.lblSaleDate.Name = "lblSaleDate";
            this.lblSaleDate.Size = new System.Drawing.Size(55, 15);
            this.lblSaleDate.TabIndex = 6;
            this.lblSaleDate.Text = "Sale Date";
            //
            // numAmount
            //
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Location = new System.Drawing.Point(20, 100);
            this.numAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(180, 23);
            this.numAmount.TabIndex = 5;
            //
            // lblAmount
            //
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(20, 80);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 15);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "Amount";
            //
            // cmbPlan
            //
            this.cmbPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlan.Location = new System.Drawing.Point(320, 40);
            this.cmbPlan.Name = "cmbPlan";
            this.cmbPlan.Size = new System.Drawing.Size(280, 23);
            this.cmbPlan.TabIndex = 3;
            //
            // lblPlan
            //
            this.lblPlan.AutoSize = true;
            this.lblPlan.Location = new System.Drawing.Point(320, 20);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(31, 15);
            this.lblPlan.TabIndex = 2;
            this.lblPlan.Text = "Plan";
            //
            // cmbCustomer
            //
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Location = new System.Drawing.Point(20, 40);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(280, 23);
            this.cmbCustomer.TabIndex = 1;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(20, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(59, 15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            //
            // tabMemberships
            //
            this.tabMemberships.Controls.Add(this.dgvMemberships);
            this.tabMemberships.Controls.Add(this.lblMembershipsSummary);
            this.tabMemberships.Controls.Add(this.btnRefreshMemberships);
            this.tabMemberships.Controls.Add(this.chkMembershipsActiveOnly);
            this.tabMemberships.Location = new System.Drawing.Point(4, 24);
            this.tabMemberships.Name = "tabMemberships";
            this.tabMemberships.Padding = new System.Windows.Forms.Padding(3);
            this.tabMemberships.Size = new System.Drawing.Size(792, 572);
            this.tabMemberships.TabIndex = 1;
            this.tabMemberships.Text = "Memberships";
            this.tabMemberships.UseVisualStyleBackColor = true;
            //
            // chkMembershipsActiveOnly
            //
            this.chkMembershipsActiveOnly.AutoSize = true;
            this.chkMembershipsActiveOnly.Checked = true;
            this.chkMembershipsActiveOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMembershipsActiveOnly.Location = new System.Drawing.Point(20, 20);
            this.chkMembershipsActiveOnly.Name = "chkMembershipsActiveOnly";
            this.chkMembershipsActiveOnly.Size = new System.Drawing.Size(150, 19);
            this.chkMembershipsActiveOnly.TabIndex = 0;
            this.chkMembershipsActiveOnly.Text = "Active memberships only";
            this.chkMembershipsActiveOnly.UseVisualStyleBackColor = true;
            //
            // btnRefreshMemberships
            //
            this.btnRefreshMemberships.Location = new System.Drawing.Point(190, 16);
            this.btnRefreshMemberships.Name = "btnRefreshMemberships";
            this.btnRefreshMemberships.Size = new System.Drawing.Size(100, 26);
            this.btnRefreshMemberships.TabIndex = 1;
            this.btnRefreshMemberships.Text = "Refresh";
            this.btnRefreshMemberships.UseVisualStyleBackColor = true;
            //
            // lblMembershipsSummary
            //
            this.lblMembershipsSummary.AutoSize = true;
            this.lblMembershipsSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMembershipsSummary.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblMembershipsSummary.Location = new System.Drawing.Point(20, 55);
            this.lblMembershipsSummary.Name = "lblMembershipsSummary";
            this.lblMembershipsSummary.Size = new System.Drawing.Size(300, 19);
            this.lblMembershipsSummary.TabIndex = 2;
            this.lblMembershipsSummary.Text = "Click Refresh to load memberships.";
            //
            // dgvMemberships
            //
            this.dgvMemberships.AllowUserToAddRows = false;
            this.dgvMemberships.AllowUserToDeleteRows = false;
            this.dgvMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemberships.Location = new System.Drawing.Point(20, 85);
            this.dgvMemberships.Name = "dgvMemberships";
            this.dgvMemberships.ReadOnly = true;
            this.dgvMemberships.RowHeadersWidth = 51;
            this.dgvMemberships.Size = new System.Drawing.Size(750, 470);
            this.dgvMemberships.TabIndex = 3;
            //
            // tabRenewals
            //
            this.tabRenewals.Controls.Add(this.dgvRenewals);
            this.tabRenewals.Controls.Add(this.lblRenewalsSummary);
            this.tabRenewals.Controls.Add(this.btnRenew);
            this.tabRenewals.Controls.Add(this.btnRefreshRenewals);
            this.tabRenewals.Location = new System.Drawing.Point(4, 24);
            this.tabRenewals.Name = "tabRenewals";
            this.tabRenewals.Padding = new System.Windows.Forms.Padding(3);
            this.tabRenewals.Size = new System.Drawing.Size(792, 572);
            this.tabRenewals.TabIndex = 2;
            this.tabRenewals.Text = "Renewals";
            this.tabRenewals.UseVisualStyleBackColor = true;
            //
            // btnRefreshRenewals
            //
            this.btnRefreshRenewals.Location = new System.Drawing.Point(20, 16);
            this.btnRefreshRenewals.Name = "btnRefreshRenewals";
            this.btnRefreshRenewals.Size = new System.Drawing.Size(100, 26);
            this.btnRefreshRenewals.TabIndex = 0;
            this.btnRefreshRenewals.Text = "Refresh";
            this.btnRefreshRenewals.UseVisualStyleBackColor = true;
            //
            // btnRenew
            //
            this.btnRenew.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnRenew.FlatAppearance.BorderSize = 0;
            this.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenew.ForeColor = System.Drawing.Color.White;
            this.btnRenew.Location = new System.Drawing.Point(130, 16);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(140, 26);
            this.btnRenew.TabIndex = 1;
            this.btnRenew.Text = "Renew Selected";
            this.btnRenew.UseVisualStyleBackColor = false;
            //
            // lblRenewalsSummary
            //
            this.lblRenewalsSummary.AutoSize = true;
            this.lblRenewalsSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRenewalsSummary.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblRenewalsSummary.Location = new System.Drawing.Point(20, 55);
            this.lblRenewalsSummary.Name = "lblRenewalsSummary";
            this.lblRenewalsSummary.Size = new System.Drawing.Size(400, 19);
            this.lblRenewalsSummary.TabIndex = 2;
            this.lblRenewalsSummary.Text = "Members expiring in the next 30 days will appear here.";
            //
            // dgvRenewals
            //
            this.dgvRenewals.AllowUserToAddRows = false;
            this.dgvRenewals.AllowUserToDeleteRows = false;
            this.dgvRenewals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRenewals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRenewals.Location = new System.Drawing.Point(20, 85);
            this.dgvRenewals.Name = "dgvRenewals";
            this.dgvRenewals.ReadOnly = true;
            this.dgvRenewals.RowHeadersWidth = 51;
            this.dgvRenewals.Size = new System.Drawing.Size(750, 470);
            this.dgvRenewals.TabIndex = 3;
            //
            // tabLeads
            //
            this.tabLeads.Location = new System.Drawing.Point(4, 24);
            this.tabLeads.Name = "tabLeads";
            this.tabLeads.Padding = new System.Windows.Forms.Padding(3);
            this.tabLeads.Size = new System.Drawing.Size(792, 572);
            this.tabLeads.TabIndex = 3;
            this.tabLeads.Text = "Leads";
            this.tabLeads.UseVisualStyleBackColor = true;
            //
            // SalesForceControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "SalesForceControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.tabControl.ResumeLayout(false);
            this.tabProcessSale.ResumeLayout(false);
            this.tabProcessSale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.tabMemberships.ResumeLayout(false);
            this.tabMemberships.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).EndInit();
            this.tabRenewals.ResumeLayout(false);
            this.tabRenewals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRenewals)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProcessSale;
        private System.Windows.Forms.DataGridView dgvRecentSales;
        private System.Windows.Forms.Label lblRecent;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnProcessSale;
        private System.Windows.Forms.DateTimePicker dtpSaleDate;
        private System.Windows.Forms.Label lblSaleDate;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.ComboBox cmbPlan;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TabPage tabMemberships;
        private System.Windows.Forms.CheckBox chkMembershipsActiveOnly;
        private System.Windows.Forms.Button btnRefreshMemberships;
        private System.Windows.Forms.Label lblMembershipsSummary;
        private System.Windows.Forms.DataGridView dgvMemberships;
        private System.Windows.Forms.TabPage tabRenewals;
        private System.Windows.Forms.DataGridView dgvRenewals;
        private System.Windows.Forms.Label lblRenewalsSummary;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnRefreshRenewals;
        private System.Windows.Forms.TabPage tabLeads;
    }
}