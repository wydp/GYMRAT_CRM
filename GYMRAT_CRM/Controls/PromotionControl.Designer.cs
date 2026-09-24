namespace CRM.winforms.Controls
{
    partial class PromotionControl
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
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblAudience = new System.Windows.Forms.Label();
            this.chkTargetAll = new System.Windows.Forms.CheckBox();
            this.chkTargetPWD = new System.Windows.Forms.CheckBox();
            this.chkTargetSenior = new System.Windows.Forms.CheckBox();
            this.chkTargetStudent = new System.Windows.Forms.CheckBox();
            this.chkTargetCorporate = new System.Windows.Forms.CheckBox();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.cmbDiscountType = new System.Windows.Forms.ComboBox();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.numDiscountValue = new System.Windows.Forms.NumericUpDown();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPromotions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotions)).BeginInit();
            this.SuspendLayout();
            //
            // lblCode
            //
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(20, 20);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(35, 15);
            this.lblCode.Text = "Code";
            //
            // txtCode
            //
            this.txtCode.Location = new System.Drawing.Point(20, 40);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(200, 23);
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(240, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.Text = "Name";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(240, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 23);
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(67, 15);
            this.lblDescription.Text = "Description";
            //
            // txtDescription
            //
            this.txtDescription.Location = new System.Drawing.Point(20, 95);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(480, 50);
            //
            // lblReason
            //
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(520, 20);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(120, 15);
            this.lblReason.Text = "Reason / Justification";
            //
            // txtReason
            //
            this.txtReason.Location = new System.Drawing.Point(520, 40);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(260, 105);
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            //
            // lblAudience
            //
            this.lblAudience.AutoSize = true;
            this.lblAudience.Location = new System.Drawing.Point(20, 155);
            this.lblAudience.Name = "lblAudience";
            this.lblAudience.Size = new System.Drawing.Size(66, 15);
            this.lblAudience.Text = "Audience";
            //
            // chkTargetAll
            //
            this.chkTargetAll.AutoSize = true;
            this.chkTargetAll.Checked = true;
            this.chkTargetAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTargetAll.Location = new System.Drawing.Point(100, 155);
            this.chkTargetAll.Name = "chkTargetAll";
            this.chkTargetAll.Size = new System.Drawing.Size(95, 19);
            this.chkTargetAll.Text = "All Members";
            this.chkTargetAll.UseVisualStyleBackColor = true;
            //
            // chkTargetPWD
            //
            this.chkTargetPWD.AutoSize = true;
            this.chkTargetPWD.Location = new System.Drawing.Point(205, 155);
            this.chkTargetPWD.Name = "chkTargetPWD";
            this.chkTargetPWD.Size = new System.Drawing.Size(50, 19);
            this.chkTargetPWD.Text = "PWD";
            this.chkTargetPWD.UseVisualStyleBackColor = true;
            //
            // chkTargetSenior
            //
            this.chkTargetSenior.AutoSize = true;
            this.chkTargetSenior.Location = new System.Drawing.Point(265, 155);
            this.chkTargetSenior.Name = "chkTargetSenior";
            this.chkTargetSenior.Size = new System.Drawing.Size(60, 19);
            this.chkTargetSenior.Text = "Senior";
            this.chkTargetSenior.UseVisualStyleBackColor = true;
            //
            // chkTargetStudent
            //
            this.chkTargetStudent.AutoSize = true;
            this.chkTargetStudent.Location = new System.Drawing.Point(335, 155);
            this.chkTargetStudent.Name = "chkTargetStudent";
            this.chkTargetStudent.Size = new System.Drawing.Size(68, 19);
            this.chkTargetStudent.Text = "Student";
            this.chkTargetStudent.UseVisualStyleBackColor = true;
            //
            // chkTargetCorporate
            //
            this.chkTargetCorporate.AutoSize = true;
            this.chkTargetCorporate.Location = new System.Drawing.Point(413, 155);
            this.chkTargetCorporate.Name = "chkTargetCorporate";
            this.chkTargetCorporate.Size = new System.Drawing.Size(78, 19);
            this.chkTargetCorporate.Text = "Corporate";
            this.chkTargetCorporate.UseVisualStyleBackColor = true;
            //
            // lblDiscountType
            //
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.Location = new System.Drawing.Point(20, 185);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(84, 15);
            this.lblDiscountType.Text = "Discount Type";
            //
            // cmbDiscountType
            //
            this.cmbDiscountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiscountType.Location = new System.Drawing.Point(20, 205);
            this.cmbDiscountType.Name = "cmbDiscountType";
            this.cmbDiscountType.Size = new System.Drawing.Size(160, 23);
            //
            // lblDiscountValue
            //
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Location = new System.Drawing.Point(200, 185);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(87, 15);
            this.lblDiscountValue.Text = "Discount Value";
            //
            // numDiscountValue
            //
            this.numDiscountValue.DecimalPlaces = 2;
            this.numDiscountValue.Location = new System.Drawing.Point(200, 205);
            this.numDiscountValue.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numDiscountValue.Name = "numDiscountValue";
            this.numDiscountValue.Size = new System.Drawing.Size(160, 23);
            //
            // lblStart
            //
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(380, 185);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(61, 15);
            this.lblStart.Text = "Start Date";
            //
            // dtpStart
            //
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(380, 205);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(140, 23);
            //
            // lblEnd
            //
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(540, 185);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(55, 15);
            this.lblEnd.Text = "End Date";
            //
            // dtpEnd
            //
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(540, 205);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(140, 23);
            //
            // chkActive
            //
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(700, 208);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(59, 19);
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 250);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 32);
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            //
            // btnUpdate
            //
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(130, 250);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 32);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            //
            // btnDeactivate
            //
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(240, 250);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(100, 32);
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(350, 250);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            //
            // dgvPromotions
            //
            this.dgvPromotions.AllowUserToAddRows = false;
            this.dgvPromotions.AllowUserToDeleteRows = false;
            this.dgvPromotions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPromotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPromotions.Location = new System.Drawing.Point(20, 300);
            this.dgvPromotions.Name = "dgvPromotions";
            this.dgvPromotions.ReadOnly = true;
            this.dgvPromotions.RowHeadersWidth = 51;
            this.dgvPromotions.Size = new System.Drawing.Size(960, 280);
            //
            // PromotionControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPromotions);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDeactivate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.numDiscountValue);
            this.Controls.Add(this.lblDiscountValue);
            this.Controls.Add(this.cmbDiscountType);
            this.Controls.Add(this.lblDiscountType);
            this.Controls.Add(this.chkTargetCorporate);
            this.Controls.Add(this.chkTargetStudent);
            this.Controls.Add(this.chkTargetSenior);
            this.Controls.Add(this.chkTargetPWD);
            this.Controls.Add(this.chkTargetAll);
            this.Controls.Add(this.lblAudience);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Name = "PromotionControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblAudience;
        private System.Windows.Forms.CheckBox chkTargetAll;
        private System.Windows.Forms.CheckBox chkTargetPWD;
        private System.Windows.Forms.CheckBox chkTargetSenior;
        private System.Windows.Forms.CheckBox chkTargetStudent;
        private System.Windows.Forms.CheckBox chkTargetCorporate;
        private System.Windows.Forms.Label lblDiscountType;
        private System.Windows.Forms.ComboBox cmbDiscountType;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.NumericUpDown numDiscountValue;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPromotions;
    }
}