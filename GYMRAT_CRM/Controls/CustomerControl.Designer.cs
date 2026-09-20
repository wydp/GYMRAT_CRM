namespace CRM.winforms.Controls
{
    partial class CustomerControl
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.headerPanel.SuspendLayout();
            this.formCard.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            //
            // headerPanel
            // Dock=Top, added LAST to Controls (see bottom) so it sits at the very
            // top of the control, above formCard.
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
            this.titleLabel.Size = new System.Drawing.Size(150, 30);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Customers";
            //
            // formCard
            // Dock=Top, added SECOND to Controls — sits below headerPanel.
            //
            this.formCard.BackColor = System.Drawing.Color.White;
            this.formCard.Controls.Add(this.chkActive);
            this.formCard.Controls.Add(this.lblAddress);
            this.formCard.Controls.Add(this.txtAddress);
            this.formCard.Controls.Add(this.lblEmail);
            this.formCard.Controls.Add(this.txtEmail);
            this.formCard.Controls.Add(this.lblContact);
            this.formCard.Controls.Add(this.txtContact);
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
            this.lblCode.Size = new System.Drawing.Size(90, 15);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Customer Code";
            //
            // txtCode
            //
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.Location = new System.Drawing.Point(24, 34);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(240, 25);
            this.txtCode.TabIndex = 1;
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblName.Location = new System.Drawing.Point(288, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(90, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Customer Name";
            //
            // txtName
            //
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(288, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(240, 25);
            this.txtName.TabIndex = 3;
            //
            // lblContact
            //
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblContact.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblContact.Location = new System.Drawing.Point(552, 16);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(90, 15);
            this.lblContact.TabIndex = 4;
            this.lblContact.Text = "Contact Number";
            //
            // txtContact
            //
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContact.Location = new System.Drawing.Point(552, 34);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(240, 25);
            this.txtContact.TabIndex = 5;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEmail.Location = new System.Drawing.Point(24, 74);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(90, 15);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email Address";
            //
            // txtEmail
            //
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(24, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(240, 25);
            this.txtEmail.TabIndex = 7;
            //
            // lblAddress
            //
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAddress.Location = new System.Drawing.Point(288, 74);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(90, 15);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "Address";
            //
            // txtAddress
            //
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location = new System.Drawing.Point(288, 92);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(504, 25);
            this.txtAddress.TabIndex = 9;
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
            // Dock=Fill, added FIRST to Controls — takes whatever space is left
            // after headerPanel and formCard claim their Top space.
            //
            this.gridPanel.Controls.Add(this.dgvCustomers);
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Location = new System.Drawing.Point(0, 284);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Padding = new System.Windows.Forms.Padding(24, 0, 24, 24);
            this.gridPanel.Size = new System.Drawing.Size(948, 276);
            this.gridPanel.TabIndex = 2;
            //
            // dgvCustomers
            //
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.Location = new System.Drawing.Point(24, 0);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(900, 252);
            this.dgvCustomers.TabIndex = 0;
            //
            // CustomerControl
            // Add order: gridPanel (Fill) first, formCard (Top) second,
            // headerPanel (Top) last — this makes headerPanel sit at the very
            // top spanning full width, formCard directly below it, gridPanel
            // filling everything else. Same rule used for Form1's shell.
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.formCard);
            this.Controls.Add(this.headerPanel);
            this.Name = "CustomerControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.formCard.ResumeLayout(false);
            this.formCard.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
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
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.DataGridView dgvCustomers;
    }
}
