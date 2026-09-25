using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;



namespace CRM.winforms.Controls
{
    public partial class RetentionControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();

        public RetentionControl()
        {
            InitializeComponent();
            StyleAllGrids();
            WireEvents();
            _ = LoadInitialAsync();
        }

        private async System.Threading.Tasks.Task LoadInitialAsync()
        {
            await LoadAtRiskAsync();
            await LoadWinBackAsync();
            await LoadLogAsync();
            await LoadFrozenAsync();
        }

        // ==================================================================
        // GRID STYLING
        // ==================================================================

        private void StyleAllGrids()
        {
            StyleGrid(dgvAtRisk);
            StyleGrid(dgvWinBack);
            StyleGrid(dgvLog);
            StyleGrid(dgvFrozen);
        }

        private void StyleGrid(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.RowHeadersVisible = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.EnableHeadersVisualStyles = false;
            g.GridColor = Color.FromArgb(229, 231, 235);
            g.ColumnHeadersHeight = 40;
            g.RowTemplate.Height = 36;

            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            g.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            g.DefaultCellStyle.SelectionForeColor = Color.White;
            g.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        }

        // ==================================================================
        // WIRING
        // ==================================================================

        private void WireEvents()
        {
            // At-Risk tab
            btnReloadAtRisk.Click += async (s, e) => await LoadAtRiskAsync();
            btnSendCampaign.Click += async (s, e) => await SendCampaignAsync();
            btnScheduleFollowUp.Click += async (s, e) => await ScheduleFollowUpAsync();
            btnFreezeCustomer.Click += async (s, e) => await FreezeSelectedAsync();

            // Win-Back tab
            btnReloadWinBack.Click += async (s, e) => await LoadWinBackAsync();
            btnLogWinBack.Click += async (s, e) => await LogWinBackAsync();

            // Log tab
            btnReloadLog.Click += async (s, e) => await LoadLogAsync();
            btnUpdateOutcome.Click += async (s, e) => await UpdateOutcomeAsync();

            // Freeze tab
            btnReloadFrozen.Click += async (s, e) => await LoadFrozenAsync();
            btnUnfreeze.Click += async (s, e) => await UnfreezeSelectedAsync();

            // Auto-reload log when tab switches to it (so it stays fresh)
            tabControl.SelectedIndexChanged += async (s, e) =>
            {
                if (tabControl.SelectedTab == tabLog) await LoadLogAsync();
                if (tabControl.SelectedTab == tabFreeze) await LoadFrozenAsync();
            };
        }

        // ==================================================================
        // AT-RISK
        // ==================================================================

        private async System.Threading.Tasks.Task LoadAtRiskAsync()
        {
            try
            {
                var days = (int)numDays.Value;
                var members = await _api.GetAtRiskMembersAsync(days);

                dgvAtRisk.Rows.Clear();
                dgvAtRisk.Columns.Clear();
                dgvAtRisk.Columns.Add("CustomerCode", "Code");
                dgvAtRisk.Columns.Add("CustomerName", "Name");
                dgvAtRisk.Columns.Add("Contact", "Contact");
                dgvAtRisk.Columns.Add("PlanName", "Plan");
                dgvAtRisk.Columns.Add("ExpiryDate", "Expires");
                dgvAtRisk.Columns.Add("DaysLeft", "Days Left");
                dgvAtRisk.Columns.Add("Status", "Status");
                dgvAtRisk.Columns.Add("Frozen", "Frozen");

                foreach (var m in members)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvAtRisk,
                        m.CustomerCode,
                        m.CustomerName ?? string.Empty,
                        m.ContactNumber ?? "—",
                        m.PlanName ?? string.Empty,
                        m.ExpiryDate.ToString("yyyy-MM-dd"),
                        m.DaysLeft >= 0 ? m.DaysLeft.ToString() : $"{Math.Abs(m.DaysLeft)} overdue",
                        m.Status ?? string.Empty,
                        m.IsFrozen ? "Yes" : "No");
                    row.Tag = m;

                    int idx = dgvAtRisk.Rows.Add(row);

                    dgvAtRisk.Rows[idx].Cells["Status"].Style.ForeColor =
                        m.Status == "Expired" ? Color.FromArgb(185, 28, 28) : Color.FromArgb(180, 83, 9);

                    if (m.IsFrozen)
                        dgvAtRisk.Rows[idx].Cells["Frozen"].Style.ForeColor = Color.FromArgb(180, 83, 9);
                }

                lblAtRiskHint.Text = $"{members.Count} member(s) match the {days}-day window.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load at-risk members: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ApiClient.AtRiskMemberDto? GetSelectedAtRisk()
        {
            if (dgvAtRisk.SelectedRows.Count == 0) return null;
            return dgvAtRisk.SelectedRows[0].Tag as ApiClient.AtRiskMemberDto;
        }

        private async System.Threading.Tasks.Task SendCampaignAsync()
        {
            var m = GetSelectedAtRisk();
            if (m is null)
            {
                MessageBox.Show("Select a member from the At-Risk list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var notes = PromptForText(
                "Send Retention Campaign",
                $"Send a retention campaign to {m.CustomerName}?\n\nEnter notes (optional):");
            if (notes is null) return; // cancelled

            try
            {
                await _api.LogRetentionActionAsync(new ApiClient.LogRetentionActionRequest
                {
                    CustomerId = m.CustomerId,
                    ActionType = "CampaignSent",
                    Notes = string.IsNullOrWhiteSpace(notes) ? $"Retention campaign sent to {m.CustomerName}" : notes,
                    Outcome = "Pending",
                });

                MessageBox.Show($"Retention campaign logged for {m.CustomerName}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadAtRiskAsync();
                await LoadLogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not log campaign: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task ScheduleFollowUpAsync()
        {
            var m = GetSelectedAtRisk();
            if (m is null)
            {
                MessageBox.Show("Select a member from the At-Risk list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var followUpDate = PromptForDate(
                "Schedule Follow-Up Call",
                $"Schedule a follow-up with {m.CustomerName}?",
                DateTime.Today.AddDays(3));
            if (followUpDate is null) return;

            try
            {
                await _api.LogRetentionActionAsync(new ApiClient.LogRetentionActionRequest
                {
                    CustomerId = m.CustomerId,
                    ActionType = "FollowUpScheduled",
                    Notes = $"Follow-up call scheduled for {m.CustomerName}",
                    FollowUpDate = followUpDate,
                    Outcome = "Pending",
                });

                MessageBox.Show($"Follow-up scheduled for {followUpDate:yyyy-MM-dd}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadAtRiskAsync();
                await LoadLogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not schedule follow-up: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task FreezeSelectedAsync()
        {
            var m = GetSelectedAtRisk();
            if (m is null)
            {
                MessageBox.Show("Select a member from the At-Risk list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (m.IsFrozen)
            {
                MessageBox.Show($"{m.CustomerName}'s membership is already frozen.", "Already Frozen",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var until = PromptForDate(
                "Freeze Membership",
                $"Freeze {m.CustomerName}'s membership?\nSelect the date it should unfreeze:",
                DateTime.Today.AddDays(30));
            if (until is null) return;

            try
            {
                await _api.FreezeCustomerAsync(m.CustomerId, until, $"Frozen until {until:yyyy-MM-dd}");

                MessageBox.Show($"{m.CustomerName}'s membership frozen until {until:yyyy-MM-dd}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadAtRiskAsync();
                await LoadFrozenAsync();
                await LoadLogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not freeze membership: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================
        // WIN-BACK
        // ==================================================================

        private async System.Threading.Tasks.Task LoadWinBackAsync()
        {
            try
            {
                var members = await _api.GetWinBackMembersAsync();

                dgvWinBack.Rows.Clear();
                dgvWinBack.Columns.Clear();
                dgvWinBack.Columns.Add("CustomerCode", "Code");
                dgvWinBack.Columns.Add("CustomerName", "Name");
                dgvWinBack.Columns.Add("Contact", "Contact");
                dgvWinBack.Columns.Add("LastPlanName", "Last Plan");
                dgvWinBack.Columns.Add("LastExpiry", "Last Expired");
                dgvWinBack.Columns.Add("DaysExpired", "Days Expired");

                foreach (var m in members)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvWinBack,
                        m.CustomerCode,
                        m.CustomerName,
                        m.ContactNumber ?? "—",
                        m.LastPlanName,
                        m.LastExpiry.ToString("yyyy-MM-dd"),
                        m.DaysExpired.ToString());
                    row.Tag = m;

                    int idx = dgvWinBack.Rows.Add(row);
                    dgvWinBack.Rows[idx].Cells["DaysExpired"].Style.ForeColor = Color.FromArgb(185, 28, 28);
                }

                lblWinBackHint.Text = $"{members.Count} member(s) in the win-back pipeline (expired 90+ days ago).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load win-back pipeline: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task LogWinBackAsync()
        {
            if (dgvWinBack.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a member from the Win-Back list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var m = dgvWinBack.SelectedRows[0].Tag as ApiClient.WinBackMemberDto;
            if (m is null) return;

            var notes = PromptForText(
                "Log Win-Back Attempt",
                $"Log a win-back attempt for {m.CustomerName}?\n\nEnter notes (optional):");
            if (notes is null) return;

            try
            {
                await _api.LogRetentionActionAsync(new ApiClient.LogRetentionActionRequest
                {
                    CustomerId = m.CustomerId,
                    ActionType = "WinBackAttempt",
                    Notes = string.IsNullOrWhiteSpace(notes) ? $"Win-back attempt for {m.CustomerName}" : notes,
                    Outcome = "Pending",
                });

                MessageBox.Show($"Win-back attempt logged for {m.CustomerName}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadLogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not log win-back: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================
        // LOG
        // ==================================================================

        private async System.Threading.Tasks.Task LoadLogAsync()
        {
            try
            {
                var actions = await _api.GetRetentionActionsAsync();

                dgvLog.Rows.Clear();
                dgvLog.Columns.Clear();
                dgvLog.Columns.Add("When", "When");
                dgvLog.Columns.Add("Customer", "Customer");
                dgvLog.Columns.Add("ActionType", "Action");
                dgvLog.Columns.Add("Notes", "Notes");
                dgvLog.Columns.Add("FollowUp", "Follow-Up");
                dgvLog.Columns.Add("Outcome", "Outcome");

                foreach (var a in actions)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvLog,
                        a.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                        a.Customer?.CustomerName ?? $"Customer #{a.CustomerId}",
                        a.ActionType,
                        a.Notes ?? "—",
                        a.FollowUpDate?.ToString("yyyy-MM-dd") ?? "—",
                        a.Outcome);
                    row.Tag = a;

                    int idx = dgvLog.Rows.Add(row);

                    dgvLog.Rows[idx].Cells["Outcome"].Style.ForeColor = a.Outcome switch
                    {
                        "Renewed" => Color.FromArgb(21, 128, 61),
                        "Lost" => Color.FromArgb(185, 28, 28),
                        "Ignored" => Color.FromArgb(107, 114, 128),
                        _ => Color.FromArgb(180, 83, 9),
                    };
                }

                lblLogHint.Text = $"{actions.Count} retention action(s) logged (newest first).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load retention log: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdateOutcomeAsync()
        {
            if (dgvLog.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a retention action from the log first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var a = dgvLog.SelectedRows[0].Tag as ApiClient.RetentionActionDto;
            if (a is null) return;

            var outcome = PromptForChoice(
                "Update Outcome",
                $"Update outcome for {a.Customer?.CustomerName ?? $"Customer #{a.CustomerId}"}?",
                new[] { "Pending", "Renewed", "Lost", "Ignored" },
                a.Outcome);
            if (outcome is null) return;

            try
            {
                await _api.UpdateRetentionOutcomeAsync(a.RetentionActionId, outcome, null);
                MessageBox.Show($"Outcome updated to {outcome}.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadLogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update outcome: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================
        // FREEZE
        // ==================================================================

        private async System.Threading.Tasks.Task LoadFrozenAsync()
        {
            try
            {
                var frozen = await _api.GetFrozenCustomersAsync();

                dgvFrozen.Rows.Clear();
                dgvFrozen.Columns.Clear();
                dgvFrozen.Columns.Add("CustomerCode", "Code");
                dgvFrozen.Columns.Add("CustomerName", "Name");
                dgvFrozen.Columns.Add("Contact", "Contact");
                dgvFrozen.Columns.Add("Email", "Email");
                dgvFrozen.Columns.Add("FrozenUntil", "Frozen Until");

                foreach (var c in frozen)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvFrozen,
                        c.CustomerCode,
                        c.CustomerName,
                        c.ContactNumber ?? "—",
                        c.EmailAddress ?? "—",
                        c.FrozenUntil?.ToString("yyyy-MM-dd") ?? "No date set");
                    row.Tag = c;
                    dgvFrozen.Rows.Add(row);
                }

                lblFreezeHint.Text = $"{frozen.Count} member(s) with frozen memberships.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load frozen members: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UnfreezeSelectedAsync()
        {
            if (dgvFrozen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a frozen member first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var c = dgvFrozen.SelectedRows[0].Tag as Customer;
            if (c is null) return;

            var confirm = MessageBox.Show($"Unfreeze {c.CustomerName}'s membership?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.UnfreezeCustomerAsync(c.CustomerId);
                MessageBox.Show($"{c.CustomerName}'s membership unfrozen.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadFrozenAsync();
                await LoadLogAsync();
                await LoadAtRiskAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not unfreeze: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================================================================
        // PROMPT DIALOGS
        // ==================================================================

        private string? PromptForText(string title, string prompt)
        {
            using var dlg = new Form
            {
                Text = title,
                Size = new Size(420, 220),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
            };
            var lbl = new Label { Text = prompt, Location = new Point(20, 15), AutoSize = false, Size = new Size(370, 60) };
            var txt = new TextBox { Location = new Point(20, 80), Width = 370, Multiline = true, Height = 60 };
            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(230, 150), Width = 75 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(315, 150), Width = 75 };

            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }

        private DateTime? PromptForDate(string title, string prompt, DateTime defaultValue)
        {
            using var dlg = new Form
            {
                Text = title,
                Size = new Size(420, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
            };
            var lbl = new Label { Text = prompt, Location = new Point(20, 15), AutoSize = false, Size = new Size(370, 60) };
            var dtp = new DateTimePicker
            {
                Location = new Point(20, 80),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Value = defaultValue,
            };
            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(230, 130), Width = 75 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(315, 130), Width = 75 };

            dlg.Controls.AddRange(new Control[] { lbl, dtp, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? dtp.Value : null;
        }

        private string? PromptForChoice(string title, string prompt, string[] options, string current)
        {
            using var dlg = new Form
            {
                Text = title,
                Size = new Size(420, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
            };
            var lbl = new Label { Text = prompt, Location = new Point(20, 15), AutoSize = false, Size = new Size(370, 40) };
            var cmb = new ComboBox
            {
                Location = new Point(20, 60),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            cmb.Items.AddRange(options);
            cmb.SelectedItem = current;
            if (cmb.SelectedIndex < 0 && cmb.Items.Count > 0) cmb.SelectedIndex = 0;

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(230, 110), Width = 75 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(315, 110), Width = 75 };

            dlg.Controls.AddRange(new Control[] { lbl, cmb, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? cmb.SelectedItem?.ToString() : null;
        }
    }
}