using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    // Small modal dialog asking "why are you cancelling this membership?"
    // Returns the reason string via the Reason property, or null if cancelled.
    public partial class CancelReasonDialog : Form
    {
        public string? Reason { get; private set; }

        private readonly TextBox _txtReason;

        public CancelReasonDialog(string customerName, string planName)
        {
            Text = "Cancel Membership";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(440, 220);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitle = new Label
            {
                Text = $"Cancel {customerName}'s {planName} membership?",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(20, 18),
                Size = new Size(400, 22),
            };

            var lblHint = new Label
            {
                Text = "Reason for cancellation (will be logged for retention):",
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(20, 48),
                Size = new Size(400, 20),
            };

            _txtReason = new TextBox
            {
                Multiline = true,
                Location = new Point(20, 72),
                Size = new Size(400, 90),
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle,
            };

            var btnCancel = new Button
            {
                Text = "Keep Membership",
                DialogResult = DialogResult.Cancel,
                Location = new Point(20, 174),
                Size = new Size(140, 32),
            };

            var btnConfirm = new Button
            {
                Text = "Confirm Cancellation",
                Location = new Point(280, 174),
                Size = new Size(140, 32),
                BackColor = Color.FromArgb(185, 28, 28),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += (s, e) =>
            {
                var text = _txtReason.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Please enter a reason.", "Reason Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Reason = text;
                DialogResult = DialogResult.OK;
                Close();
            };

            Controls.Add(lblTitle);
            Controls.Add(lblHint);
            Controls.Add(_txtReason);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);

            AcceptButton = btnConfirm;
            CancelButton = btnCancel;
        }
    }
}