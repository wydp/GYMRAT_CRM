namespace CRM.winforms.Controls
{
    partial class RetentionControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();

            // At-Risk tab
            this.tabAtRisk = new System.Windows.Forms.TabPage();
            this.lblAtRiskDays = new System.Windows.Forms.Label();
            this.numDays = new System.Windows.Forms.NumericUpDown();
            this.btnReloadAtRisk = new System.Windows.Forms.Button();
            this.btnSendCampaign = new System.Windows.Forms.Button();
            this.btnScheduleFollowUp = new System.Windows.Forms.Button();
            this.btnFreezeCustomer = new System.Windows.Forms.Button();
            this.lblAtRiskHint = new System.Windows.Forms.Label();
            this.dgvAtRisk = new System.Windows.Forms.DataGridView();

            // Win-Back tab
            this.tabWinBack = new System.Windows.Forms.TabPage();
            this.btnReloadWinBack = new System.Windows.Forms.Button();
            this.btnLogWinBack = new System.Windows.Forms.Button();
            this.lblWinBackHint = new System.Windows.Forms.Label();
            this.dgvWinBack = new System.Windows.Forms.DataGridView();

            // Retention Log tab
            this.tabLog = new System.Windows.Forms.TabPage();
            this.btnReloadLog = new System.Windows.Forms.Button();
            this.btnUpdateOutcome = new System.Windows.Forms.Button();
            this.lblLogHint = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();

            // Freeze Management tab
            this.tabFreeze = new System.Windows.Forms.TabPage();
            this.btnReloadFrozen = new System.Windows.Forms.Button();
            this.btnUnfreeze = new System.Windows.Forms.Button();
            this.lblFreezeHint = new System.Windows.Forms.Label();
            this.dgvFrozen = new System.Windows.Forms.DataGridView();

            this.tabControl.SuspendLayout();
            this.tabAtRisk.SuspendLayout();
            this.tabWinBack.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabFreeze.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtRisk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWinBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFrozen)).BeginInit();
            this.SuspendLayout();

            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabAtRisk);
            this.tabControl.Controls.Add(this.tabWinBack);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Controls.Add(this.tabFreeze);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 600);
            this.tabControl.TabIndex = 0;

            // ============================================================
            // TAB 1 — AT RISK
            // ============================================================
            this.tabAtRisk.Controls.Add(this.dgvAtRisk);
            this.tabAtRisk.Controls.Add(this.lblAtRiskHint);
            this.tabAtRisk.Controls.Add(this.btnFreezeCustomer);
            this.tabAtRisk.Controls.Add(this.btnScheduleFollowUp);
            this.tabAtRisk.Controls.Add(this.btnSendCampaign);
            this.tabAtRisk.Controls.Add(this.btnReloadAtRisk);
            this.tabAtRisk.Controls.Add(this.numDays);
            this.tabAtRisk.Controls.Add(this.lblAtRiskDays);
            this.tabAtRisk.Location = new System.Drawing.Point(4, 24);
            this.tabAtRisk.Name = "tabAtRisk";
            this.tabAtRisk.Padding = new System.Windows.Forms.Padding(3);
            this.tabAtRisk.Size = new System.Drawing.Size(992, 572);
            this.tabAtRisk.TabIndex = 0;
            this.tabAtRisk.Text = "At-Risk Members";
            this.tabAtRisk.UseVisualStyleBackColor = true;
            //
            // lblAtRiskDays
            //
            this.lblAtRiskDays.AutoSize = true;
            this.lblAtRiskDays.Location = new System.Drawing.Point(20, 20);
            this.lblAtRiskDays.Name = "lblAtRiskDays";
            this.lblAtRiskDays.Size = new System.Drawing.Size(120, 15);
            this.lblAtRiskDays.Text = "Expiring within (days):";
            //
            // numDays
            //
            this.numDays.Location = new System.Drawing.Point(160, 17);
            this.numDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numDays.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.numDays.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.numDays.Name = "numDays";
            this.numDays.Size = new System.Drawing.Size(70, 23);
            //
            // btnReloadAtRisk
            //
            this.btnReloadAtRisk.Location = new System.Drawing.Point(240, 16);
            this.btnReloadAtRisk.Name = "btnReloadAtRisk";
            this.btnReloadAtRisk.Size = new System.Drawing.Size(100, 26);
            this.btnReloadAtRisk.Text = "Reload";
            this.btnReloadAtRisk.UseVisualStyleBackColor = true;
            //
            // btnSendCampaign
            //
            this.btnSendCampaign.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnSendCampaign.FlatAppearance.BorderSize = 0;
            this.btnSendCampaign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendCampaign.ForeColor = System.Drawing.Color.White;
            this.btnSendCampaign.Location = new System.Drawing.Point(360, 16);
            this.btnSendCampaign.Name = "btnSendCampaign";
            this.btnSendCampaign.Size = new System.Drawing.Size(150, 26);
            this.btnSendCampaign.Text = "Send Retention Campaign";
            this.btnSendCampaign.UseVisualStyleBackColor = false;
            //
            // btnScheduleFollowUp
            //
            this.btnScheduleFollowUp.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnScheduleFollowUp.FlatAppearance.BorderSize = 0;
            this.btnScheduleFollowUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleFollowUp.ForeColor = System.Drawing.Color.White;
            this.btnScheduleFollowUp.Location = new System.Drawing.Point(520, 16);
            this.btnScheduleFollowUp.Name = "btnScheduleFollowUp";
            this.btnScheduleFollowUp.Size = new System.Drawing.Size(150, 26);
            this.btnScheduleFollowUp.Text = "Schedule Follow-Up";
            this.btnScheduleFollowUp.UseVisualStyleBackColor = false;
            //
            // btnFreezeCustomer
            //
            this.btnFreezeCustomer.BackColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.btnFreezeCustomer.FlatAppearance.BorderSize = 0;
            this.btnFreezeCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreezeCustomer.ForeColor = System.Drawing.Color.White;
            this.btnFreezeCustomer.Location = new System.Drawing.Point(680, 16);
            this.btnFreezeCustomer.Name = "btnFreezeCustomer";
            this.btnFreezeCustomer.Size = new System.Drawing.Size(120, 26);
            this.btnFreezeCustomer.Text = "Freeze";
            this.btnFreezeCustomer.UseVisualStyleBackColor = false;
            //
            // lblAtRiskHint
            //
            this.lblAtRiskHint.AutoSize = true;
            this.lblAtRiskHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblAtRiskHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAtRiskHint.Location = new System.Drawing.Point(20, 50);
            this.lblAtRiskHint.Name = "lblAtRiskHint";
            this.lblAtRiskHint.Size = new System.Drawing.Size(400, 15);
            this.lblAtRiskHint.Text = "Members whose latest membership expires within the selected window.";
            //
            // dgvAtRisk
            //
            this.dgvAtRisk.AllowUserToAddRows = false;
            this.dgvAtRisk.AllowUserToDeleteRows = false;
            this.dgvAtRisk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAtRisk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAtRisk.Location = new System.Drawing.Point(20, 75);
            this.dgvAtRisk.Name = "dgvAtRisk";
            this.dgvAtRisk.ReadOnly = true;
            this.dgvAtRisk.RowHeadersWidth = 51;
            this.dgvAtRisk.Size = new System.Drawing.Size(950, 480);
            this.dgvAtRisk.TabIndex = 10;

            // ============================================================
            // TAB 2 — WIN-BACK
            // ============================================================
            this.tabWinBack.Controls.Add(this.dgvWinBack);
            this.tabWinBack.Controls.Add(this.lblWinBackHint);
            this.tabWinBack.Controls.Add(this.btnLogWinBack);
            this.tabWinBack.Controls.Add(this.btnReloadWinBack);
            this.tabWinBack.Location = new System.Drawing.Point(4, 24);
            this.tabWinBack.Name = "tabWinBack";
            this.tabWinBack.Padding = new System.Windows.Forms.Padding(3);
            this.tabWinBack.Size = new System.Drawing.Size(992, 572);
            this.tabWinBack.TabIndex = 1;
            this.tabWinBack.Text = "Win-Back Pipeline";
            this.tabWinBack.UseVisualStyleBackColor = true;
            //
            // btnReloadWinBack
            //
            this.btnReloadWinBack.Location = new System.Drawing.Point(20, 16);
            this.btnReloadWinBack.Name = "btnReloadWinBack";
            this.btnReloadWinBack.Size = new System.Drawing.Size(100, 26);
            this.btnReloadWinBack.Text = "Reload";
            this.btnReloadWinBack.UseVisualStyleBackColor = true;
            //
            // btnLogWinBack
            //
            this.btnLogWinBack.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnLogWinBack.FlatAppearance.BorderSize = 0;
            this.btnLogWinBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogWinBack.ForeColor = System.Drawing.Color.White;
            this.btnLogWinBack.Location = new System.Drawing.Point(140, 16);
            this.btnLogWinBack.Name = "btnLogWinBack";
            this.btnLogWinBack.Size = new System.Drawing.Size(150, 26);
            this.btnLogWinBack.Text = "Log Win-Back Attempt";
            this.btnLogWinBack.UseVisualStyleBackColor = false;
            //
            // lblWinBackHint
            //
            this.lblWinBackHint.AutoSize = true;
            this.lblWinBackHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblWinBackHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblWinBackHint.Location = new System.Drawing.Point(310, 21);
            this.lblWinBackHint.Name = "lblWinBackHint";
            this.lblWinBackHint.Size = new System.Drawing.Size(500, 15);
            this.lblWinBackHint.Text = "Members whose membership expired 90+ days ago and haven't renewed.";
            //
            // dgvWinBack
            //
            this.dgvWinBack.AllowUserToAddRows = false;
            this.dgvWinBack.AllowUserToDeleteRows = false;
            this.dgvWinBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWinBack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWinBack.Location = new System.Drawing.Point(20, 55);
            this.dgvWinBack.Name = "dgvWinBack";
            this.dgvWinBack.ReadOnly = true;
            this.dgvWinBack.RowHeadersWidth = 51;
            this.dgvWinBack.Size = new System.Drawing.Size(950, 500);
            this.dgvWinBack.TabIndex = 10;

            // ============================================================
            // TAB 3 — RETENTION LOG
            // ============================================================
            this.tabLog.Controls.Add(this.dgvLog);
            this.tabLog.Controls.Add(this.lblLogHint);
            this.tabLog.Controls.Add(this.btnUpdateOutcome);
            this.tabLog.Controls.Add(this.btnReloadLog);
            this.tabLog.Location = new System.Drawing.Point(4, 24);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(992, 572);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Retention Log";
            this.tabLog.UseVisualStyleBackColor = true;
            //
            // btnReloadLog
            //
            this.btnReloadLog.Location = new System.Drawing.Point(20, 16);
            this.btnReloadLog.Name = "btnReloadLog";
            this.btnReloadLog.Size = new System.Drawing.Size(100, 26);
            this.btnReloadLog.Text = "Reload";
            this.btnReloadLog.UseVisualStyleBackColor = true;
            //
            // btnUpdateOutcome
            //
            this.btnUpdateOutcome.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnUpdateOutcome.FlatAppearance.BorderSize = 0;
            this.btnUpdateOutcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateOutcome.ForeColor = System.Drawing.Color.White;
            this.btnUpdateOutcome.Location = new System.Drawing.Point(140, 16);
            this.btnUpdateOutcome.Name = "btnUpdateOutcome";
            this.btnUpdateOutcome.Size = new System.Drawing.Size(150, 26);
            this.btnUpdateOutcome.Text = "Update Outcome";
            this.btnUpdateOutcome.UseVisualStyleBackColor = false;
            //
            // lblLogHint
            //
            this.lblLogHint.AutoSize = true;
            this.lblLogHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblLogHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblLogHint.Location = new System.Drawing.Point(310, 21);
            this.lblLogHint.Name = "lblLogHint";
            this.lblLogHint.Size = new System.Drawing.Size(500, 15);
            this.lblLogHint.Text = "Every retention action logged, newest first. Select a row to update its outcome.";
            //
            // dgvLog
            //
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Location = new System.Drawing.Point(20, 55);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersWidth = 51;
            this.dgvLog.Size = new System.Drawing.Size(950, 500);
            this.dgvLog.TabIndex = 10;

            // ============================================================
            // TAB 4 — FREEZE MANAGEMENT
            // ============================================================
            this.tabFreeze.Controls.Add(this.dgvFrozen);
            this.tabFreeze.Controls.Add(this.lblFreezeHint);
            this.tabFreeze.Controls.Add(this.btnUnfreeze);
            this.tabFreeze.Controls.Add(this.btnReloadFrozen);
            this.tabFreeze.Location = new System.Drawing.Point(4, 24);
            this.tabFreeze.Name = "tabFreeze";
            this.tabFreeze.Padding = new System.Windows.Forms.Padding(3);
            this.tabFreeze.Size = new System.Drawing.Size(992, 572);
            this.tabFreeze.TabIndex = 3;
            this.tabFreeze.Text = "Freeze Management";
            this.tabFreeze.UseVisualStyleBackColor = true;
            //
            // btnReloadFrozen
            //
            this.btnReloadFrozen.Location = new System.Drawing.Point(20, 16);
            this.btnReloadFrozen.Name = "btnReloadFrozen";
            this.btnReloadFrozen.Size = new System.Drawing.Size(100, 26);
            this.btnReloadFrozen.Text = "Reload";
            this.btnReloadFrozen.UseVisualStyleBackColor = true;
            //
            // btnUnfreeze
            //
            this.btnUnfreeze.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnUnfreeze.FlatAppearance.BorderSize = 0;
            this.btnUnfreeze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnfreeze.ForeColor = System.Drawing.Color.White;
            this.btnUnfreeze.Location = new System.Drawing.Point(140, 16);
            this.btnUnfreeze.Name = "btnUnfreeze";
            this.btnUnfreeze.Size = new System.Drawing.Size(150, 26);
            this.btnUnfreeze.Text = "Unfreeze Selected";
            this.btnUnfreeze.UseVisualStyleBackColor = false;
            //
            // lblFreezeHint
            //
            this.lblFreezeHint.AutoSize = true;
            this.lblFreezeHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblFreezeHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFreezeHint.Location = new System.Drawing.Point(310, 21);
            this.lblFreezeHint.Name = "lblFreezeHint";
            this.lblFreezeHint.Size = new System.Drawing.Size(500, 15);
            this.lblFreezeHint.Text = "Members with frozen memberships. Unfreezing restores normal status.";
            //
            // dgvFrozen
            //
            this.dgvFrozen.AllowUserToAddRows = false;
            this.dgvFrozen.AllowUserToDeleteRows = false;
            this.dgvFrozen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFrozen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFrozen.Location = new System.Drawing.Point(20, 55);
            this.dgvFrozen.Name = "dgvFrozen";
            this.dgvFrozen.ReadOnly = true;
            this.dgvFrozen.RowHeadersWidth = 51;
            this.dgvFrozen.Size = new System.Drawing.Size(950, 500);
            this.dgvFrozen.TabIndex = 10;

            // ============================================================
            // RetentionControl
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "RetentionControl";
            this.Size = new System.Drawing.Size(1000, 600);

            this.tabControl.ResumeLayout(false);
            this.tabAtRisk.ResumeLayout(false);
            this.tabAtRisk.PerformLayout();
            this.tabWinBack.ResumeLayout(false);
            this.tabWinBack.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabFreeze.ResumeLayout(false);
            this.tabFreeze.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtRisk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWinBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFrozen)).EndInit();
            this.ResumeLayout(false);
        }

        // Control declarations
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAtRisk;
        private System.Windows.Forms.Label lblAtRiskDays;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnReloadAtRisk;
        private System.Windows.Forms.Button btnSendCampaign;
        private System.Windows.Forms.Button btnScheduleFollowUp;
        private System.Windows.Forms.Button btnFreezeCustomer;
        private System.Windows.Forms.Label lblAtRiskHint;
        private System.Windows.Forms.DataGridView dgvAtRisk;

        private System.Windows.Forms.TabPage tabWinBack;
        private System.Windows.Forms.Button btnReloadWinBack;
        private System.Windows.Forms.Button btnLogWinBack;
        private System.Windows.Forms.Label lblWinBackHint;
        private System.Windows.Forms.DataGridView dgvWinBack;

        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.Button btnReloadLog;
        private System.Windows.Forms.Button btnUpdateOutcome;
        private System.Windows.Forms.Label lblLogHint;
        private System.Windows.Forms.DataGridView dgvLog;

        private System.Windows.Forms.TabPage tabFreeze;
        private System.Windows.Forms.Button btnReloadFrozen;
        private System.Windows.Forms.Button btnUnfreeze;
        private System.Windows.Forms.Label lblFreezeHint;
        private System.Windows.Forms.DataGridView dgvFrozen;
    }
}