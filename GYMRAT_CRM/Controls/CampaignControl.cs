using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class CampaignControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedCampaignId = null;

        public CampaignControl()
        {
            InitializeComponent();
            PopulateStatusCombo();
            StyleGrid();
            WireEvents();
            WireAudienceCheckboxes();
            _ = LoadCampaignsAsync();
        }

        private void PopulateStatusCombo()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(CampaignStatus));
            cmbStatus.SelectedIndex = 0;
        }

        // "All Members" and specific audiences are mutually exclusive.
        private void WireAudienceCheckboxes()
        {
            chkTargetAll.CheckedChanged += (s, e) =>
            {
                if (!chkTargetAll.Checked) return;
                chkTargetPWD.Checked = false;
                chkTargetSenior.Checked = false;
                chkTargetStudent.Checked = false;
                chkTargetCorporate.Checked = false;
            };

            void UncheckAllIfSpecific(CheckBox cb)
            {
                cb.CheckedChanged += (s, e) =>
                {
                    if (!cb.Checked) return;
                    chkTargetAll.Checked = false;
                };
            }

            UncheckAllIfSpecific(chkTargetPWD);
            UncheckAllIfSpecific(chkTargetSenior);
            UncheckAllIfSpecific(chkTargetStudent);
            UncheckAllIfSpecific(chkTargetCorporate);
        }

        private void StyleGrid()
        {
            dgvCampaigns.BorderStyle = BorderStyle.None;
            dgvCampaigns.RowHeadersVisible = false;
            dgvCampaigns.AllowUserToAddRows = false;
            dgvCampaigns.AllowUserToDeleteRows = false;
            dgvCampaigns.ReadOnly = true;
            dgvCampaigns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCampaigns.MultiSelect = false;
            dgvCampaigns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCampaigns.EnableHeadersVisualStyles = false;
            dgvCampaigns.GridColor = Color.FromArgb(229, 231, 235);
            dgvCampaigns.ColumnHeadersHeight = 40;
            dgvCampaigns.RowTemplate.Height = 36;

            dgvCampaigns.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvCampaigns.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvCampaigns.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvCampaigns.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvCampaigns.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvCampaigns.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCampaigns.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvCampaigns.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvCampaigns.Columns.Add("CampaignCode", "Code");
            dgvCampaigns.Columns.Add("CampaignName", "Name");
            dgvCampaigns.Columns.Add("Description", "Description");
            dgvCampaigns.Columns.Add("Audience", "Audience");
            dgvCampaigns.Columns.Add("StartDate", "Start");
            dgvCampaigns.Columns.Add("EndDate", "End");
            dgvCampaigns.Columns.Add("Status", "Status");
            dgvCampaigns.Columns.Add("Active", "Active");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddCampaignAsync();
            btnUpdate.Click += async (s, e) => await UpdateCampaignAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateCampaignAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadCampaignsAsync(); };
            dgvCampaigns.SelectionChanged += DgvCampaigns_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadCampaignsAsync()
        {
            try
            {
                var campaigns = await _api.GetCampaignsAsync();
                dgvCampaigns.Rows.Clear();

                foreach (var c in campaigns.OrderBy(x => x.CampaignId))
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvCampaigns,
                        c.CampaignCode,
                        c.CampaignName,
                        c.Description,
                        FormatAudience(c),
                        c.StartDate.ToString("yyyy-MM-dd"),
                        c.EndDate.ToString("yyyy-MM-dd"),
                        c.Status.ToString(),
                        c.IsActive ? "Active" : "Inactive");
                    row.Tag = c;

                    int rowIndex = dgvCampaigns.Rows.Add(row);

                    var statusCell = dgvCampaigns.Rows[rowIndex].Cells["Status"];
                    statusCell.Style.ForeColor = c.Status switch
                    {
                        CampaignStatus.Active => Color.FromArgb(21, 128, 61),
                        CampaignStatus.Completed => Color.FromArgb(72, 128, 255),
                        CampaignStatus.Cancelled => Color.FromArgb(185, 28, 28),
                        _ => Color.FromArgb(180, 83, 9),
                    };

                    dgvCampaigns.Rows[rowIndex].Cells["Active"].Style.ForeColor =
                        c.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load campaigns: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatAudience(Campaign c)
        {
            var parts = new System.Collections.Generic.List<string>();
            if (c.TargetAllMembers) parts.Add("All Members");
            if (c.TargetPWD) parts.Add("PWD");
            if (c.TargetSenior) parts.Add("Senior");
            if (c.TargetStudent) parts.Add("Student");
            if (c.TargetCorporate) parts.Add("Corporate");
            return parts.Count == 0 ? "—" : string.Join(", ", parts);
        }

        private void DgvCampaigns_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCampaigns.SelectedRows.Count == 0) return;

            var row = dgvCampaigns.SelectedRows[0];
            if (row.Tag is not Campaign c) return;
            _selectedCampaignId = c.CampaignId;

            txtCode.Text = c.CampaignCode;
            txtName.Text = c.CampaignName;
            txtDescription.Text = c.Description;
            txtReason.Text = c.Reason ?? string.Empty;

            chkTargetAll.Checked = c.TargetAllMembers;
            chkTargetPWD.Checked = c.TargetPWD;
            chkTargetSenior.Checked = c.TargetSenior;
            chkTargetStudent.Checked = c.TargetStudent;
            chkTargetCorporate.Checked = c.TargetCorporate;

            dtpStart.Value = c.StartDate;
            dtpEnd.Value = c.EndDate;
            cmbStatus.SelectedItem = c.Status;
            chkActive.Checked = c.IsActive;
        }

        private async System.Threading.Tasks.Task AddCampaignAsync()
        {
            if (!ValidateForm()) return;

            try
            {
                await _api.CreateCampaignAsync(BuildCampaignFromForm());
                ClearForm();
                await LoadCampaignsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add campaign: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdateCampaignAsync()
        {
            if (_selectedCampaignId == null)
            {
                MessageBox.Show("Select a campaign from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateForm()) return;

            try
            {
                var campaign = BuildCampaignFromForm();
                campaign.CampaignId = _selectedCampaignId.Value;
                await _api.UpdateCampaignAsync(_selectedCampaignId.Value, campaign);
                ClearForm();
                await LoadCampaignsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update campaign: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeactivateCampaignAsync()
        {
            if (_selectedCampaignId == null)
            {
                MessageBox.Show("Select a campaign from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this campaign?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivateCampaignAsync(_selectedCampaignId.Value);
            ClearForm();
            await LoadCampaignsAsync();
        }

        private Campaign BuildCampaignFromForm() => new Campaign
        {
            CampaignCode = txtCode.Text.Trim(),
            CampaignName = txtName.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            Reason = string.IsNullOrWhiteSpace(txtReason.Text) ? null : txtReason.Text.Trim(),
            TargetAllMembers = chkTargetAll.Checked,
            TargetPWD = chkTargetPWD.Checked,
            TargetSenior = chkTargetSenior.Checked,
            TargetStudent = chkTargetStudent.Checked,
            TargetCorporate = chkTargetCorporate.Checked,
            StartDate = dtpStart.Value,
            EndDate = dtpEnd.Value,
            Status = (CampaignStatus)cmbStatus.SelectedItem!,
            IsActive = chkActive.Checked,
        };

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Campaign Code and Campaign Name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason/justification for this campaign.", "Missing Reason",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!chkTargetAll.Checked && !chkTargetPWD.Checked && !chkTargetSenior.Checked
                && !chkTargetStudent.Checked && !chkTargetCorporate.Checked)
            {
                MessageBox.Show("Select at least one target audience.", "Missing Audience",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpEnd.Value < dtpStart.Value)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "Invalid Dates",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _selectedCampaignId = null;
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtReason.Clear();
            chkTargetAll.Checked = true;
            chkTargetPWD.Checked = false;
            chkTargetSenior.Checked = false;
            chkTargetStudent.Checked = false;
            chkTargetCorporate.Checked = false;
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            cmbStatus.SelectedIndex = 0;
            chkActive.Checked = true;
            dgvCampaigns.ClearSelection();
        }
    }
}