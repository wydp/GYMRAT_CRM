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
            tabControl = new TabControl();
            tabProcessSale = new TabPage();
            dgvRecentSales = new DataGridView();
            lblRecent = new Label();
            btnClear = new Button();
            btnProcessSale = new Button();
            dtpSaleDate = new DateTimePicker();
            lblSaleDate = new Label();
            numAmount = new NumericUpDown();
            lblAmount = new Label();
            cmbPlan = new ComboBox();
            lblPlan = new Label();
            cmbCustomer = new ComboBox();
            lblCustomer = new Label();
            tabMemberships = new TabPage();
            dgvMemberships = new DataGridView();
            lblMembershipsSummary = new Label();
            btnRefreshMemberships = new Button();
            chkMembershipsActiveOnly = new CheckBox();
            tabRenewals = new TabPage();
            dgvRenewals = new DataGridView();
            lblRenewalsSummary = new Label();
            btnRenew = new Button();
            btnRefreshRenewals = new Button();
            tabLeads = new TabPage();
            tabCheckIn = new TabPage();
            tabHistory = new TabPage();
            tabControl.SuspendLayout();
            tabProcessSale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentSales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            tabMemberships.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemberships).BeginInit();
            tabRenewals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRenewals).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabProcessSale);
            tabControl.Controls.Add(tabMemberships);
            tabControl.Controls.Add(tabRenewals);
            tabControl.Controls.Add(tabLeads);
            tabControl.Controls.Add(tabCheckIn);
            tabControl.Controls.Add(tabHistory);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1143, 1000);
            tabControl.TabIndex = 0;
            // 
            // tabProcessSale
            // 
            tabProcessSale.Controls.Add(dgvRecentSales);
            tabProcessSale.Controls.Add(lblRecent);
            tabProcessSale.Controls.Add(btnClear);
            tabProcessSale.Controls.Add(btnProcessSale);
            tabProcessSale.Controls.Add(dtpSaleDate);
            tabProcessSale.Controls.Add(lblSaleDate);
            tabProcessSale.Controls.Add(numAmount);
            tabProcessSale.Controls.Add(lblAmount);
            tabProcessSale.Controls.Add(cmbPlan);
            tabProcessSale.Controls.Add(lblPlan);
            tabProcessSale.Controls.Add(cmbCustomer);
            tabProcessSale.Controls.Add(lblCustomer);
            tabProcessSale.Location = new Point(4, 34);
            tabProcessSale.Margin = new Padding(4, 5, 4, 5);
            tabProcessSale.Name = "tabProcessSale";
            tabProcessSale.Padding = new Padding(4, 5, 4, 5);
            tabProcessSale.Size = new Size(1135, 962);
            tabProcessSale.TabIndex = 0;
            tabProcessSale.Text = "Process Sale";
            tabProcessSale.UseVisualStyleBackColor = true;
            // 
            // dgvRecentSales
            // 
            dgvRecentSales.AllowUserToAddRows = false;
            dgvRecentSales.AllowUserToDeleteRows = false;
            dgvRecentSales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRecentSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentSales.Location = new Point(29, 358);
            dgvRecentSales.Margin = new Padding(4, 5, 4, 5);
            dgvRecentSales.Name = "dgvRecentSales";
            dgvRecentSales.ReadOnly = true;
            dgvRecentSales.RowHeadersWidth = 51;
            dgvRecentSales.Size = new Size(1071, 567);
            dgvRecentSales.TabIndex = 11;
            // 
            // lblRecent
            // 
            lblRecent.AutoSize = true;
            lblRecent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRecent.ForeColor = Color.FromArgb(31, 41, 55);
            lblRecent.Location = new Point(29, 317);
            lblRecent.Margin = new Padding(4, 0, 4, 0);
            lblRecent.Name = "lblRecent";
            lblRecent.Size = new Size(131, 28);
            lblRecent.TabIndex = 10;
            lblRecent.Text = "Recent Sales";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(243, 233);
            btnClear.Margin = new Padding(4, 5, 4, 5);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(143, 53);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnProcessSale
            // 
            btnProcessSale.BackColor = Color.FromArgb(21, 128, 61);
            btnProcessSale.FlatAppearance.BorderSize = 0;
            btnProcessSale.FlatStyle = FlatStyle.Flat;
            btnProcessSale.ForeColor = Color.White;
            btnProcessSale.Location = new Point(29, 233);
            btnProcessSale.Margin = new Padding(4, 5, 4, 5);
            btnProcessSale.Name = "btnProcessSale";
            btnProcessSale.Size = new Size(200, 53);
            btnProcessSale.TabIndex = 8;
            btnProcessSale.Text = "Process Sale";
            btnProcessSale.UseVisualStyleBackColor = false;
            // 
            // dtpSaleDate
            // 
            dtpSaleDate.Format = DateTimePickerFormat.Short;
            dtpSaleDate.Location = new Point(314, 167);
            dtpSaleDate.Margin = new Padding(4, 5, 4, 5);
            dtpSaleDate.Name = "dtpSaleDate";
            dtpSaleDate.Size = new Size(255, 31);
            dtpSaleDate.TabIndex = 7;
            // 
            // lblSaleDate
            // 
            lblSaleDate.AutoSize = true;
            lblSaleDate.Location = new Point(314, 133);
            lblSaleDate.Margin = new Padding(4, 0, 4, 0);
            lblSaleDate.Name = "lblSaleDate";
            lblSaleDate.Size = new Size(86, 25);
            lblSaleDate.TabIndex = 6;
            lblSaleDate.Text = "Sale Date";
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Location = new Point(29, 167);
            numAmount.Margin = new Padding(4, 5, 4, 5);
            numAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(257, 31);
            numAmount.TabIndex = 5;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(29, 133);
            lblAmount.Margin = new Padding(4, 0, 4, 0);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(77, 25);
            lblAmount.TabIndex = 4;
            lblAmount.Text = "Amount";
            // 
            // cmbPlan
            // 
            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Location = new Point(457, 67);
            cmbPlan.Margin = new Padding(4, 5, 4, 5);
            cmbPlan.Name = "cmbPlan";
            cmbPlan.Size = new Size(398, 33);
            cmbPlan.TabIndex = 3;
            // 
            // lblPlan
            // 
            lblPlan.AutoSize = true;
            lblPlan.Location = new Point(457, 33);
            lblPlan.Margin = new Padding(4, 0, 4, 0);
            lblPlan.Name = "lblPlan";
            lblPlan.Size = new Size(45, 25);
            lblPlan.TabIndex = 2;
            lblPlan.Text = "Plan";
            // 
            // cmbCustomer
            // 
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomer.Location = new Point(29, 67);
            cmbCustomer.Margin = new Padding(4, 5, 4, 5);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(398, 33);
            cmbCustomer.TabIndex = 1;
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(29, 33);
            lblCustomer.Margin = new Padding(4, 0, 4, 0);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(89, 25);
            lblCustomer.TabIndex = 0;
            lblCustomer.Text = "Customer";
            // 
            // tabMemberships
            // 
            tabMemberships.Controls.Add(dgvMemberships);
            tabMemberships.Controls.Add(lblMembershipsSummary);
            tabMemberships.Controls.Add(btnRefreshMemberships);
            tabMemberships.Controls.Add(chkMembershipsActiveOnly);
            tabMemberships.Location = new Point(4, 34);
            tabMemberships.Margin = new Padding(4, 5, 4, 5);
            tabMemberships.Name = "tabMemberships";
            tabMemberships.Padding = new Padding(4, 5, 4, 5);
            tabMemberships.Size = new Size(1135, 962);
            tabMemberships.TabIndex = 1;
            tabMemberships.Text = "Memberships";
            tabMemberships.UseVisualStyleBackColor = true;
            // 
            // dgvMemberships
            // 
            dgvMemberships.AllowUserToAddRows = false;
            dgvMemberships.AllowUserToDeleteRows = false;
            dgvMemberships.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMemberships.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMemberships.Location = new Point(29, 142);
            dgvMemberships.Margin = new Padding(4, 5, 4, 5);
            dgvMemberships.Name = "dgvMemberships";
            dgvMemberships.ReadOnly = true;
            dgvMemberships.RowHeadersWidth = 51;
            dgvMemberships.Size = new Size(1071, 783);
            dgvMemberships.TabIndex = 3;
            // 
            // lblMembershipsSummary
            // 
            lblMembershipsSummary.AutoSize = true;
            lblMembershipsSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMembershipsSummary.ForeColor = Color.FromArgb(31, 41, 55);
            lblMembershipsSummary.Location = new Point(29, 92);
            lblMembershipsSummary.Margin = new Padding(4, 0, 4, 0);
            lblMembershipsSummary.Name = "lblMembershipsSummary";
            lblMembershipsSummary.Size = new Size(345, 28);
            lblMembershipsSummary.TabIndex = 2;
            lblMembershipsSummary.Text = "Click Refresh to load memberships.";
            // 
            // btnRefreshMemberships
            // 
            btnRefreshMemberships.Location = new Point(271, 27);
            btnRefreshMemberships.Margin = new Padding(4, 5, 4, 5);
            btnRefreshMemberships.Name = "btnRefreshMemberships";
            btnRefreshMemberships.Size = new Size(143, 43);
            btnRefreshMemberships.TabIndex = 1;
            btnRefreshMemberships.Text = "Refresh";
            btnRefreshMemberships.UseVisualStyleBackColor = true;
            // 
            // chkMembershipsActiveOnly
            // 
            chkMembershipsActiveOnly.AutoSize = true;
            chkMembershipsActiveOnly.Checked = true;
            chkMembershipsActiveOnly.CheckState = CheckState.Checked;
            chkMembershipsActiveOnly.Location = new Point(29, 33);
            chkMembershipsActiveOnly.Margin = new Padding(4, 5, 4, 5);
            chkMembershipsActiveOnly.Name = "chkMembershipsActiveOnly";
            chkMembershipsActiveOnly.Size = new Size(238, 29);
            chkMembershipsActiveOnly.TabIndex = 0;
            chkMembershipsActiveOnly.Text = "Active memberships only";
            chkMembershipsActiveOnly.UseVisualStyleBackColor = true;
            // 
            // tabRenewals
            // 
            tabRenewals.Controls.Add(dgvRenewals);
            tabRenewals.Controls.Add(lblRenewalsSummary);
            tabRenewals.Controls.Add(btnRenew);
            tabRenewals.Controls.Add(btnRefreshRenewals);
            tabRenewals.Location = new Point(4, 34);
            tabRenewals.Margin = new Padding(4, 5, 4, 5);
            tabRenewals.Name = "tabRenewals";
            tabRenewals.Padding = new Padding(4, 5, 4, 5);
            tabRenewals.Size = new Size(1135, 962);
            tabRenewals.TabIndex = 2;
            tabRenewals.Text = "Renewals";
            tabRenewals.UseVisualStyleBackColor = true;
            // 
            // dgvRenewals
            // 
            dgvRenewals.AllowUserToAddRows = false;
            dgvRenewals.AllowUserToDeleteRows = false;
            dgvRenewals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRenewals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRenewals.Location = new Point(29, 142);
            dgvRenewals.Margin = new Padding(4, 5, 4, 5);
            dgvRenewals.Name = "dgvRenewals";
            dgvRenewals.ReadOnly = true;
            dgvRenewals.RowHeadersWidth = 51;
            dgvRenewals.Size = new Size(1071, 783);
            dgvRenewals.TabIndex = 3;
            // 
            // lblRenewalsSummary
            // 
            lblRenewalsSummary.AutoSize = true;
            lblRenewalsSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRenewalsSummary.ForeColor = Color.FromArgb(31, 41, 55);
            lblRenewalsSummary.Location = new Point(29, 92);
            lblRenewalsSummary.Margin = new Padding(4, 0, 4, 0);
            lblRenewalsSummary.Name = "lblRenewalsSummary";
            lblRenewalsSummary.Size = new Size(536, 28);
            lblRenewalsSummary.TabIndex = 2;
            lblRenewalsSummary.Text = "Members expiring in the next 30 days will appear here.";
            // 
            // btnRenew
            // 
            btnRenew.BackColor = Color.FromArgb(21, 128, 61);
            btnRenew.FlatAppearance.BorderSize = 0;
            btnRenew.FlatStyle = FlatStyle.Flat;
            btnRenew.ForeColor = Color.White;
            btnRenew.Location = new Point(186, 27);
            btnRenew.Margin = new Padding(4, 5, 4, 5);
            btnRenew.Name = "btnRenew";
            btnRenew.Size = new Size(200, 43);
            btnRenew.TabIndex = 1;
            btnRenew.Text = "Renew Selected";
            btnRenew.UseVisualStyleBackColor = false;
            // 
            // btnRefreshRenewals
            // 
            btnRefreshRenewals.Location = new Point(29, 27);
            btnRefreshRenewals.Margin = new Padding(4, 5, 4, 5);
            btnRefreshRenewals.Name = "btnRefreshRenewals";
            btnRefreshRenewals.Size = new Size(143, 43);
            btnRefreshRenewals.TabIndex = 0;
            btnRefreshRenewals.Text = "Refresh";
            btnRefreshRenewals.UseVisualStyleBackColor = true;
            // 
            // tabLeads
            // 
            tabLeads.Location = new Point(4, 34);
            tabLeads.Margin = new Padding(4, 5, 4, 5);
            tabLeads.Name = "tabLeads";
            tabLeads.Padding = new Padding(4, 5, 4, 5);
            tabLeads.Size = new Size(1135, 962);
            tabLeads.TabIndex = 3;
            tabLeads.Text = "Leads";
            tabLeads.UseVisualStyleBackColor = true;
            // 
            // tabCheckIn
            // 
            tabCheckIn.Location = new Point(4, 34);
            tabCheckIn.Margin = new Padding(4, 5, 4, 5);
            tabCheckIn.Name = "tabCheckIn";
            tabCheckIn.Padding = new Padding(4, 5, 4, 5);
            tabCheckIn.Size = new Size(1135, 962);
            tabCheckIn.TabIndex = 4;
            tabCheckIn.Text = "Check-In";
            tabCheckIn.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            tabHistory.Location = new Point(4, 34);
            tabHistory.Margin = new Padding(4, 5, 4, 5);
            tabHistory.Name = "tabHistory";
            tabHistory.Padding = new Padding(4, 5, 4, 5);
            tabHistory.Size = new Size(1135, 962);
            tabHistory.TabIndex = 5;
            tabHistory.Text = "Attendance History";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // SalesForceControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl);
            Margin = new Padding(4, 5, 4, 5);
            Name = "SalesForceControl";
            Size = new Size(1143, 1000);
            tabControl.ResumeLayout(false);
            tabProcessSale.ResumeLayout(false);
            tabProcessSale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentSales).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            tabMemberships.ResumeLayout(false);
            tabMemberships.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemberships).EndInit();
            tabRenewals.ResumeLayout(false);
            tabRenewals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRenewals).EndInit();
            ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabCheckIn;
        private System.Windows.Forms.TabPage tabHistory;
    }
}