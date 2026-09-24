namespace CRM.winforms.Controls
{
    partial class CheckInControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstMatches = new System.Windows.Forms.ListBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblSelectedValue = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblToday = new System.Windows.Forms.Label();
            this.btnRefreshToday = new System.Windows.Forms.Button();
            this.dgvToday = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).BeginInit();
            this.SuspendLayout();
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(20, 20);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(280, 19);
            this.lblSearch.Text = "Search Customer (name or code)";
            //
            // txtSearch
            //
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(20, 45);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(400, 27);
            //
            // lstMatches
            //
            this.lstMatches.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstMatches.FormattingEnabled = true;
            this.lstMatches.ItemHeight = 17;
            this.lstMatches.Location = new System.Drawing.Point(20, 80);
            this.lstMatches.Name = "lstMatches";
            this.lstMatches.Size = new System.Drawing.Size(400, 140);
            //
            // lblSelected
            //
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSelected.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSelected.Location = new System.Drawing.Point(20, 230);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(90, 15);
            this.lblSelected.Text = "Selected: (none)";
            //
            // lblSelectedValue
            //
            this.lblSelectedValue.AutoSize = true;
            this.lblSelectedValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectedValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblSelectedValue.Location = new System.Drawing.Point(20, 250);
            this.lblSelectedValue.Name = "lblSelectedValue";
            this.lblSelectedValue.Size = new System.Drawing.Size(60, 21);
            this.lblSelectedValue.Text = "—";
            //
            // lblNotes
            //
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(20, 290);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(110, 15);
            this.lblNotes.Text = "Notes (optional)";
            //
            // txtNotes
            //
            this.txtNotes.Location = new System.Drawing.Point(20, 310);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(400, 23);
            //
            // btnCheckIn
            //
            this.btnCheckIn.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnCheckIn.FlatAppearance.BorderSize = 0;
            this.btnCheckIn.Enabled = false;
            this.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(20, 350);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(200, 50);
            this.btnCheckIn.Text = "CHECK IN";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(230, 350);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 50);
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            //
            // lblToday
            //
            this.lblToday.AutoSize = true;
            this.lblToday.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblToday.Location = new System.Drawing.Point(480, 20);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(200, 20);
            this.lblToday.Text = "Today's Check-Ins";
            //
            // btnRefreshToday
            //
            this.btnRefreshToday.Location = new System.Drawing.Point(680, 16);
            this.btnRefreshToday.Name = "btnRefreshToday";
            this.btnRefreshToday.Size = new System.Drawing.Size(100, 26);
            this.btnRefreshToday.Text = "Refresh";
            this.btnRefreshToday.UseVisualStyleBackColor = true;
            //
            // dgvToday
            //
            this.dgvToday.AllowUserToAddRows = false;
            this.dgvToday.AllowUserToDeleteRows = false;
            this.dgvToday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToday.Location = new System.Drawing.Point(480, 55);
            this.dgvToday.Name = "dgvToday";
            this.dgvToday.ReadOnly = true;
            this.dgvToday.RowHeadersWidth = 51;
            this.dgvToday.Size = new System.Drawing.Size(490, 500);
            //
            // CheckInControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvToday);
            this.Controls.Add(this.btnRefreshToday);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblSelectedValue);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.lstMatches);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Name = "CheckInControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lstMatches;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Label lblSelectedValue;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Button btnRefreshToday;
        private System.Windows.Forms.DataGridView dgvToday;
    }
}