namespace CRM.winforms.Controls
{
    partial class PromoCodeControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblPromotion = new System.Windows.Forms.Label();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.lblMaxUses = new System.Windows.Forms.Label();
            this.numMaxUses = new System.Windows.Forms.NumericUpDown();
            this.chkUnlimited = new System.Windows.Forms.CheckBox();
            this.lblExpiresAt = new System.Windows.Forms.Label();
            this.dtpExpiresAt = new System.Windows.Forms.DateTimePicker();
            this.chkNoExpiry = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPromoCodes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxUses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromoCodes)).BeginInit();
            this.SuspendLayout();
            //
            // lblCode
            //
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(20, 20);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 15);
            this.lblCode.Text = "Code";
            //
            // txtCode
            //
            this.txtCode.Location = new System.Drawing.Point(20, 40);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(200, 23);
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            //
            // lblPromotion
            //
            this.lblPromotion.AutoSize = true;
            this.lblPromotion.Location = new System.Drawing.Point(240, 20);
            this.lblPromotion.Name = "lblPromotion";
            this.lblPromotion.Size = new System.Drawing.Size(60, 15);
            this.lblPromotion.Text = "Promotion";
            //
            // cmbPromotion
            //
            this.cmbPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotion.Location = new System.Drawing.Point(240, 40);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(320, 23);
            //
            // lblMaxUses
            //
            this.lblMaxUses.AutoSize = true;
            this.lblMaxUses.Location = new System.Drawing.Point(580, 20);
            this.lblMaxUses.Name = "lblMaxUses";
            this.lblMaxUses.Size = new System.Drawing.Size(60, 15);
            this.lblMaxUses.Text = "Max Uses";
            //
            // numMaxUses
            //
            this.numMaxUses.Location = new System.Drawing.Point(580, 40);
            this.numMaxUses.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMaxUses.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numMaxUses.Value = new decimal(new int[] { 100, 0, 0, 0 });
            this.numMaxUses.Name = "numMaxUses";
            this.numMaxUses.Size = new System.Drawing.Size(120, 23);
            //
            // chkUnlimited
            //
            this.chkUnlimited.AutoSize = true;
            this.chkUnlimited.Location = new System.Drawing.Point(710, 42);
            this.chkUnlimited.Name = "chkUnlimited";
            this.chkUnlimited.Size = new System.Drawing.Size(80, 19);
            this.chkUnlimited.Text = "Unlimited";
            this.chkUnlimited.UseVisualStyleBackColor = true;
            //
            // lblExpiresAt
            //
            this.lblExpiresAt.AutoSize = true;
            this.lblExpiresAt.Location = new System.Drawing.Point(20, 80);
            this.lblExpiresAt.Name = "lblExpiresAt";
            this.lblExpiresAt.Size = new System.Drawing.Size(66, 15);
            this.lblExpiresAt.Text = "Expires At";
            //
            // dtpExpiresAt
            //
            this.dtpExpiresAt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiresAt.Location = new System.Drawing.Point(20, 100);
            this.dtpExpiresAt.Name = "dtpExpiresAt";
            this.dtpExpiresAt.Size = new System.Drawing.Size(180, 23);
            //
            // chkNoExpiry
            //
            this.chkNoExpiry.AutoSize = true;
            this.chkNoExpiry.Location = new System.Drawing.Point(210, 102);
            this.chkNoExpiry.Name = "chkNoExpiry";
            this.chkNoExpiry.Size = new System.Drawing.Size(130, 19);
            this.chkNoExpiry.Text = "No expiry (follows promotion)";
            this.chkNoExpiry.UseVisualStyleBackColor = true;
            //
            // chkActive
            //
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(580, 102);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(59, 19);
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            //
            // lblHint
            //
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblHint.Location = new System.Drawing.Point(20, 135);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(400, 15);
            this.lblHint.Text = "Codes are uppercase. Duplicate codes are rejected.";
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 165);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 32);
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            //
            // btnDeactivate
            //
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(150, 165);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(120, 32);
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(280, 165);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            //
            // dgvPromoCodes
            //
            this.dgvPromoCodes.AllowUserToAddRows = false;
            this.dgvPromoCodes.AllowUserToDeleteRows = false;
            this.dgvPromoCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPromoCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPromoCodes.Location = new System.Drawing.Point(20, 215);
            this.dgvPromoCodes.Name = "dgvPromoCodes";
            this.dgvPromoCodes.ReadOnly = true;
            this.dgvPromoCodes.RowHeadersWidth = 51;
            this.dgvPromoCodes.Size = new System.Drawing.Size(960, 365);
            //
            // PromoCodeControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPromoCodes);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDeactivate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.chkNoExpiry);
            this.Controls.Add(this.dtpExpiresAt);
            this.Controls.Add(this.lblExpiresAt);
            this.Controls.Add(this.chkUnlimited);
            this.Controls.Add(this.numMaxUses);
            this.Controls.Add(this.lblMaxUses);
            this.Controls.Add(this.cmbPromotion);
            this.Controls.Add(this.lblPromotion);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Name = "PromoCodeControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxUses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromoCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblPromotion;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.Label lblMaxUses;
        private System.Windows.Forms.NumericUpDown numMaxUses;
        private System.Windows.Forms.CheckBox chkUnlimited;
        private System.Windows.Forms.Label lblExpiresAt;
        private System.Windows.Forms.DateTimePicker dtpExpiresAt;
        private System.Windows.Forms.CheckBox chkNoExpiry;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPromoCodes;
    }
}