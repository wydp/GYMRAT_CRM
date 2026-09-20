namespace CRM.winforms.Controls
{
    partial class FeedbackControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.formCard = new System.Windows.Forms.Panel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.dgvFeedback = new System.Windows.Forms.DataGridView();
            this.formCard.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).BeginInit();
            this.SuspendLayout();
            //
            // formCard
            //
            this.formCard.BackColor = System.Drawing.Color.White;
            this.formCard.Controls.Add(this.lblCustomer);
            this.formCard.Controls.Add(this.cboCustomer);
            this.formCard.Controls.Add(this.lblType);
            this.formCard.Controls.Add(this.cboType);
            this.formCard.Controls.Add(this.lblMessage);
            this.formCard.Controls.Add(this.txtMessage);
            this.formCard.Controls.Add(this.lblStatus);
            this.formCard.Controls.Add(this.cboStatus);
            this.formCard.Controls.Add(this.btnAdd);
            this.formCard.Controls.Add(this.btnUpdateStatus);
            this.formCard.Controls.Add(this.btnDeactivate);
            this.formCard.Controls.Add(this.btnRefresh);
            this.formCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.formCard.Location = new System.Drawing.Point(0, 0);
            this.formCard.Name = "formCard";
            this.formCard.Padding = new System.Windows.Forms.Padding(24, 12, 24, 0);
            this.formCard.Size = new System.Drawing.Size(948, 260);
            this.formCard.TabIndex = 0;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomer.Location = new System.Drawing.Point(24, 16);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(60, 15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            //
            // cboCustomer
            //
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCustomer.Location = new System.Drawing.Point(24, 34);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(280, 26);
            this.cboCustomer.TabIndex = 1;
            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblType.Location = new System.Drawing.Point(328, 16);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 15);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type";
            //
            // cboType
            //
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboType.Location = new System.Drawing.Point(328, 34);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(200, 26);
            this.cboType.TabIndex = 3;
            //
            // lblMessage
            //
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMessage.Location = new System.Drawing.Point(24, 74);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(60, 15);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Message";
            //
            // txtMessage
            //
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMessage.Location = new System.Drawing.Point(24, 92);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(900, 60);
            this.txtMessage.TabIndex = 5;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStatus.Location = new System.Drawing.Point(24, 164);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(90, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status (select row to update)";
            //
            // cboStatus
            //
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.Location = new System.Drawing.Point(24, 182);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(200, 26);
            this.cboStatus.TabIndex = 7;
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(260, 180);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 36);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            //
            // btnUpdateStatus
            //
            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(370, 180);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(130, 36);
            this.btnUpdateStatus.TabIndex = 9;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            //
            // btnDeactivate
            //
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(510, 180);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(110, 36);
            this.btnDeactivate.TabIndex = 10;
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
            this.btnRefresh.Location = new System.Drawing.Point(632, 180);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 36);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh / Clear";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            //
            // gridPanel
            //
            this.gridPanel.Controls.Add(this.dgvFeedback);
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Location = new System.Drawing.Point(0, 260);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Padding = new System.Windows.Forms.Padding(24, 12, 24, 24);
            this.gridPanel.Size = new System.Drawing.Size(948, 300);
            this.gridPanel.TabIndex = 1;
            //
            // dgvFeedback
            //
            this.dgvFeedback.BackgroundColor = System.Drawing.Color.White;
            this.dgvFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFeedback.Location = new System.Drawing.Point(24, 12);
            this.dgvFeedback.Name = "dgvFeedback";
            this.dgvFeedback.Size = new System.Drawing.Size(900, 264);
            this.dgvFeedback.TabIndex = 0;
            //
            // FeedbackControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.formCard);
            this.Name = "FeedbackControl";
            this.Size = new System.Drawing.Size(948, 560);
            this.formCard.ResumeLayout(false);
            this.formCard.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel formCard;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.DataGridView dgvFeedback;
    }
}
