namespace CRM.winforms.Controls
{
    partial class CampaignControl
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
            lblCode = new Label();
            txtCode = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblReason = new Label();
            txtReason = new TextBox();
            lblAudience = new Label();
            chkTargetAll = new CheckBox();
            chkTargetPWD = new CheckBox();
            chkTargetSenior = new CheckBox();
            chkTargetStudent = new CheckBox();
            chkTargetCorporate = new CheckBox();
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            lblEnd = new Label();
            dtpEnd = new DateTimePicker();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            chkActive = new CheckBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDeactivate = new Button();
            btnRefresh = new Button();
            dgvCampaigns = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCampaigns).BeginInit();
            SuspendLayout();
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(29, 33);
            lblCode.Margin = new Padding(4, 0, 4, 0);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(54, 25);
            lblCode.TabIndex = 0;
            lblCode.Text = "Code";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(29, 67);
            txtCode.Margin = new Padding(4, 5, 4, 5);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(284, 31);
            txtCode.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(343, 33);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(59, 25);
            lblName.TabIndex = 2;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(343, 67);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(370, 31);
            txtName.TabIndex = 3;
            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.Location = new Point(745, 33);
            lblReason.Margin = new Padding(4, 0, 4, 0);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(176, 25);
            lblReason.TabIndex = 4;
            lblReason.Text = "Reason / Justification";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(745, 67);
            txtReason.Margin = new Padding(4, 5, 4, 5);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.ScrollBars = ScrollBars.Vertical;
            txtReason.Size = new Size(370, 180);
            txtReason.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(29, 125);
            lblDescription.Margin = new Padding(4, 0, 4, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(102, 25);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(29, 158);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(684, 89);
            txtDescription.TabIndex = 7;
            // 
            // lblAudience
            // 
            lblAudience.AutoSize = true;
            lblAudience.Location = new Point(29, 275);
            lblAudience.Margin = new Padding(4, 0, 4, 0);
            lblAudience.Name = "lblAudience";
            lblAudience.Size = new Size(100, 25);
            lblAudience.TabIndex = 8;
            lblAudience.Text = "Audience";
            // 
            // chkTargetAll
            // 
            chkTargetAll.AutoSize = true;
            chkTargetAll.Checked = true;
            chkTargetAll.CheckState = CheckState.Checked;
            chkTargetAll.Location = new Point(150, 273);
            chkTargetAll.Margin = new Padding(4, 5, 4, 5);
            chkTargetAll.Name = "chkTargetAll";
            chkTargetAll.Size = new Size(146, 29);
            chkTargetAll.TabIndex = 9;
            chkTargetAll.Text = "All Members";
            chkTargetAll.UseVisualStyleBackColor = true;
            // 
            // chkTargetPWD
            // 
            chkTargetPWD.AutoSize = true;
            chkTargetPWD.Location = new Point(310, 273);
            chkTargetPWD.Margin = new Padding(4, 5, 4, 5);
            chkTargetPWD.Name = "chkTargetPWD";
            chkTargetPWD.Size = new Size(79, 29);
            chkTargetPWD.TabIndex = 10;
            chkTargetPWD.Text = "PWD";
            chkTargetPWD.UseVisualStyleBackColor = true;
            // 
            // chkTargetSenior
            // 
            chkTargetSenior.AutoSize = true;
            chkTargetSenior.Location = new Point(400, 273);
            chkTargetSenior.Margin = new Padding(4, 5, 4, 5);
            chkTargetSenior.Name = "chkTargetSenior";
            chkTargetSenior.Size = new Size(94, 29);
            chkTargetSenior.TabIndex = 11;
            chkTargetSenior.Text = "Senior";
            chkTargetSenior.UseVisualStyleBackColor = true;
            // 
            // chkTargetStudent
            // 
            chkTargetStudent.AutoSize = true;
            chkTargetStudent.Location = new Point(505, 273);
            chkTargetStudent.Margin = new Padding(4, 5, 4, 5);
            chkTargetStudent.Name = "chkTargetStudent";
            chkTargetStudent.Size = new Size(106, 29);
            chkTargetStudent.TabIndex = 12;
            chkTargetStudent.Text = "Student";
            chkTargetStudent.UseVisualStyleBackColor = true;
            // 
            // chkTargetCorporate
            // 
            chkTargetCorporate.AutoSize = true;
            chkTargetCorporate.Location = new Point(625, 273);
            chkTargetCorporate.Margin = new Padding(4, 5, 4, 5);
            chkTargetCorporate.Name = "chkTargetCorporate";
            chkTargetCorporate.Size = new Size(127, 29);
            chkTargetCorporate.TabIndex = 13;
            chkTargetCorporate.Text = "Corporate";
            chkTargetCorporate.UseVisualStyleBackColor = true;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(29, 325);
            lblStart.Margin = new Padding(4, 0, 4, 0);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(90, 25);
            lblStart.TabIndex = 14;
            lblStart.Text = "Start Date";
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(29, 358);
            dtpStart.Margin = new Padding(4, 5, 4, 5);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(198, 31);
            dtpStart.TabIndex = 15;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(257, 325);
            lblEnd.Margin = new Padding(4, 0, 4, 0);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(84, 25);
            lblEnd.TabIndex = 16;
            lblEnd.Text = "End Date";
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(257, 358);
            dtpEnd.Margin = new Padding(4, 5, 4, 5);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(198, 31);
            dtpEnd.TabIndex = 17;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(486, 325);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 25);
            lblStatus.TabIndex = 18;
            lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(486, 358);
            cmbStatus.Margin = new Padding(4, 5, 4, 5);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(227, 33);
            cmbStatus.TabIndex = 19;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Location = new Point(745, 358);
            chkActive.Margin = new Padding(4, 5, 4, 5);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(86, 29);
            chkActive.TabIndex = 20;
            chkActive.Text = "Active";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(29, 425);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 53);
            btnAdd.TabIndex = 21;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(186, 425);
            btnUpdate.Margin = new Padding(4, 5, 4, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(143, 53);
            btnUpdate.TabIndex = 22;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDeactivate
            // 
            btnDeactivate.Location = new Point(343, 425);
            btnDeactivate.Margin = new Padding(4, 5, 4, 5);
            btnDeactivate.Name = "btnDeactivate";
            btnDeactivate.Size = new Size(143, 53);
            btnDeactivate.TabIndex = 23;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(500, 425);
            btnRefresh.Margin = new Padding(4, 5, 4, 5);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(143, 53);
            btnRefresh.TabIndex = 24;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvCampaigns
            // 
            dgvCampaigns.AllowUserToAddRows = false;
            dgvCampaigns.AllowUserToDeleteRows = false;
            dgvCampaigns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCampaigns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCampaigns.Location = new Point(29, 508);
            dgvCampaigns.Margin = new Padding(4, 5, 4, 5);
            dgvCampaigns.Name = "dgvCampaigns";
            dgvCampaigns.ReadOnly = true;
            dgvCampaigns.RowHeadersWidth = 51;
            dgvCampaigns.Size = new Size(1086, 433);
            dgvCampaigns.TabIndex = 25;
            // 
            // CampaignControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvCampaigns);
            Controls.Add(btnRefresh);
            Controls.Add(btnDeactivate);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(chkActive);
            Controls.Add(cmbStatus);
            Controls.Add(lblStatus);
            Controls.Add(dtpEnd);
            Controls.Add(lblEnd);
            Controls.Add(dtpStart);
            Controls.Add(lblStart);
            Controls.Add(chkTargetCorporate);
            Controls.Add(chkTargetStudent);
            Controls.Add(chkTargetSenior);
            Controls.Add(chkTargetPWD);
            Controls.Add(chkTargetAll);
            Controls.Add(lblAudience);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtReason);
            Controls.Add(lblReason);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(txtCode);
            Controls.Add(lblCode);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CampaignControl";
            Size = new Size(1143, 1000);
            ((System.ComponentModel.ISupportInitialize)dgvCampaigns).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvCampaigns;
    }
}