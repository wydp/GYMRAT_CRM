using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class LeadControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedLeadId = null;

        public LeadControl()
        {
            InitializeComponent();
            PopulateComboBoxes();
            StyleGrid();
            WireEvents();
            _ = LoadLeadsAsync();
        }

        private void PopulateComboBoxes()
        {
            cmbSource.Items.Clear();
            foreach (var s in Enum.GetValues(typeof(CRM.domain.Entities.LeadSource)))
                cmbSource.Items.Add(s);
            cmbSource.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            foreach (var s in Enum.GetValues(typeof(CRM.domain.Entities.LeadStatus)))
                cmbStatus.Items.Add(s);
            cmbStatus.SelectedIndex = 0;
        }

        private void StyleGrid()
        {
            dgvLeads.BorderStyle = BorderStyle.None;
            dgvLeads.RowHeadersVisible = false;
            dgvLeads.AllowUserToAddRows = false;
            dgvLeads.AllowUserToDeleteRows = false;
            dgvLeads.ReadOnly = true;
            dgvLeads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeads.MultiSelect = false;
            dgvLeads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLeads.EnableHeadersVisualStyles = false;
            dgvLeads.GridColor = Color.FromArgb(229, 231, 235);
            dgvLeads.ColumnHeadersHeight = 40;
            dgvLeads.RowTemplate.Height = 36;

            dgvLeads.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvLeads.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvLeads.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvLeads.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvLeads.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvLeads.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvLeads.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvLeads.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvLeads.Columns.Add("LeadCode", "Code");
            dgvLeads.Columns.Add("FullName", "Full Name");
            dgvLeads.Columns.Add("Contact", "Contact");
            dgvLeads.Columns.Add("Email", "Email");
            dgvLeads.Columns.Add("Source", "Source");
            dgvLeads.Columns.Add("Status", "Status");
            dgvLeads.Columns.Add("ConvertedTo", "Converted To");
            dgvLeads.Columns.Add("Created", "Created");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddLeadAsync();
            btnUpdate.Click += async (s, e) => await UpdateLeadAsync();
            btnConvert.Click += async (s, e) => await ConvertLeadAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateLeadAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadLeadsAsync(); };
            dgvLeads.SelectionChanged += DgvLeads_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadLeadsAsync()
        {
            try
            {
                var leads = await _api.GetLeadsAsync();
                dgvLeads.Rows.Clear();

                foreach (var l in leads)
                {
                    var convertedText = l.ConvertedCustomer != null
                        ? $"{l.ConvertedCustomer.CustomerCode} ({l.ConvertedCustomer.CustomerName})"
                        : "—";

                    var row = new DataGridViewRow();
                    row.CreateCells(dgvLeads,
                        l.LeadCode,
                        l.FullName,
                        l.ContactNumber ?? "—",
                        l.EmailAddress ?? "—",
                        l.Source,
                        l.Status,
                        convertedText,
                        l.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd"));
                    row.Tag = l;

                    int idx = dgvLeads.Rows.Add(row);

                    var statusCell = dgvLeads.Rows[idx].Cells["Status"];
                    statusCell.Style.ForeColor = l.Status switch
                    {
                        "Converted" => Color.FromArgb(21, 128, 61),
                        "Lost" => Color.FromArgb(185, 28, 28),
                        "Interested" => Color.FromArgb(72, 128, 255),
                        "Contacted" => Color.FromArgb(180, 83, 9),
                        _ => Color.FromArgb(31, 41, 55),
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load leads: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvLeads_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvLeads.SelectedRows.Count == 0) return;

            var row = dgvLeads.SelectedRows[0];
            if (row.Tag is not ApiClient.LeadDto l) return;
            _selectedLeadId = l.LeadId;

            txtCode.Text = l.LeadCode;
            txtFullName.Text = l.FullName;
            txtContact.Text = l.ContactNumber ?? "";
            txtEmail.Text = l.EmailAddress ?? "";
            txtAddress.Text = l.Address ?? "";
            txtNotes.Text = l.Notes ?? "";

            if (Enum.TryParse<CRM.domain.Entities.LeadSource>(l.Source, out var src))
                cmbSource.SelectedItem = src;

            if (Enum.TryParse<CRM.domain.Entities.LeadStatus>(l.Status, out var stat))
                cmbStatus.SelectedItem = stat;

            // Convert button only usable when status != Converted
            btnConvert.Enabled = l.Status != "Converted";
        }

        private async System.Threading.Tasks.Task AddLeadAsync()
        {
            if (!ValidateForm()) return;

            var lead = BuildLeadFromForm();

            try
            {
                await _api.CreateLeadAsync(lead);
                ClearForm();
                await LoadLeadsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot add lead", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add lead: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdateLeadAsync()
        {
            if (_selectedLeadId == null)
            {
                MessageBox.Show("Select a lead from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateForm()) return;

            var lead = BuildLeadFromForm();
            lead.LeadId = _selectedLeadId.Value;

            try
            {
                await _api.UpdateLeadAsync(_selectedLeadId.Value, lead);
                ClearForm();
                await LoadLeadsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot update lead", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update lead: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task ConvertLeadAsync()
        {
            if (_selectedLeadId == null)
            {
                MessageBox.Show("Select a lead first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Simple dialog: enter customer code
            var customerCode = PromptForText(
                "Convert Lead to Customer",
                $"Enter a unique Customer Code for this new customer\n(e.g. CUST-0100):",
                "CUST-");
            if (string.IsNullOrWhiteSpace(customerCode)) return;

            try
            {
                var converted = await _api.ConvertLeadAsync(_selectedLeadId.Value,
                    new ApiClient.ConvertLeadRequest { CustomerCode = customerCode.Trim() });

                MessageBox.Show($"Lead converted. Customer {customerCode} created.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                await LoadLeadsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot convert lead", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not convert lead: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeactivateLeadAsync()
        {
            if (_selectedLeadId == null)
            {
                MessageBox.Show("Select a lead from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this lead?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeactivateLeadAsync(_selectedLeadId.Value);
                ClearForm();
                await LoadLeadsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not deactivate lead: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ApiClient.LeadDto BuildLeadFromForm() => new ApiClient.LeadDto
        {
            LeadCode = txtCode.Text.Trim(),
            FullName = txtFullName.Text.Trim(),
            ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
            EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
            Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
            Source = cmbSource.SelectedItem?.ToString() ?? "Other",
            Status = cmbStatus.SelectedItem?.ToString() ?? "New",
            IsActive = true,
        };

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Lead Code and Full Name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            _selectedLeadId = null;
            txtCode.Clear();
            txtFullName.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtNotes.Clear();
            cmbSource.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            btnConvert.Enabled = true;
            dgvLeads.ClearSelection();
        }

        private string? PromptForText(string title, string prompt, string defaultText)
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
            var txt = new TextBox { Location = new Point(20, 80), Width = 370, Text = defaultText };
            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(230, 130), Width = 75 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(315, 130), Width = 75 };

            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }
    }
}