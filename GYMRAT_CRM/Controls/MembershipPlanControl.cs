using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class MembershipPlanControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedPlanId = null; // null = "Add" mode, set = "Update/Deactivate" mode

        public MembershipPlanControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            _ = LoadPlansAsync();
        }

        private void StyleGrid()
        {
            dgvPlans.BorderStyle = BorderStyle.None;
            dgvPlans.RowHeadersVisible = false;
            dgvPlans.AllowUserToAddRows = false;
            dgvPlans.AllowUserToDeleteRows = false;
            dgvPlans.ReadOnly = true;
            dgvPlans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlans.MultiSelect = false;
            dgvPlans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPlans.EnableHeadersVisualStyles = false;
            dgvPlans.GridColor = Color.FromArgb(229, 231, 235); // Border
            dgvPlans.ColumnHeadersHeight = 40;
            dgvPlans.RowTemplate.Height = 36;

            dgvPlans.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255); // Tints.AlmostWhiteBlue
            dgvPlans.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvPlans.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvPlans.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); // Text Primary — explicit, since dark-mode defaults can render invisible-on-white
            dgvPlans.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255); // Primary
            dgvPlans.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPlans.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPlans.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvPlans.Columns.Add("PlanCode", "Code");
            dgvPlans.Columns.Add("PlanName", "Name");
            dgvPlans.Columns.Add("Description", "Description");
            dgvPlans.Columns.Add("Price", "Price");
            dgvPlans.Columns.Add("DurationInDays", "Duration (Days)");
            dgvPlans.Columns.Add("Status", "Status");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddPlanAsync();
            btnUpdate.Click += async (s, e) => await UpdatePlanAsync();
            btnDeactivate.Click += async (s, e) => await DeactivatePlanAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadPlansAsync(); };
            dgvPlans.SelectionChanged += DgvPlans_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadPlansAsync()
        {
            try
            {
                var plans = await _api.GetMembershipPlansAsync();
                dgvPlans.Rows.Clear();

                foreach (var p in plans.OrderBy(x => x.MembershipPlanId))
                {
                    // Tag set BEFORE Rows.Add — see CustomerControl for why.
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvPlans,
                        p.PlanCode, p.PlanName, p.Description,
                        p.Price.ToString("C2"), p.DurationInDays,
                        p.IsActive ? "Active" : "Inactive");
                    row.Tag = p.MembershipPlanId; // must be set before Add — see CustomerControl

                    int rowIndex = dgvPlans.Rows.Add(row);
                    // Cells[...] by NAME needs row.DataGridView set — after Add only.
                    dgvPlans.Rows[rowIndex].Cells["Status"].Style.ForeColor =
                        p.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load membership plans: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPlans.SelectedRows.Count == 0) return;

            var row = dgvPlans.SelectedRows[0];
            if (row.Tag is not int planId) return; // defensive: Tag not set yet
            _selectedPlanId = planId;

            txtCode.Text = row.Cells["PlanCode"].Value?.ToString();
            txtName.Text = row.Cells["PlanName"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            // Strip currency symbol back out for editing
            txtPrice.Text = row.Cells["Price"].Value?.ToString()?.TrimStart('$', '\u20B1');
            txtDuration.Text = row.Cells["DurationInDays"].Value?.ToString();
            chkActive.Checked = row.Cells["Status"].Value?.ToString() == "Active";
        }

        private async System.Threading.Tasks.Task AddPlanAsync()
        {
            if (!ValidateForm(out var plan)) return;

            try
            {
                await _api.CreateMembershipPlanAsync(plan);
                ClearForm();
                await LoadPlansAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task UpdatePlanAsync()
        {
            if (_selectedPlanId == null)
            {
                MessageBox.Show("Select a plan from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateForm(out var plan)) return;

            try
            {
                plan.MembershipPlanId = _selectedPlanId.Value;
                await _api.UpdateMembershipPlanAsync(_selectedPlanId.Value, plan);
                ClearForm();
                await LoadPlansAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task DeactivatePlanAsync()
        {
            if (_selectedPlanId == null)
            {
                MessageBox.Show("Select a plan from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this membership plan?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivateMembershipPlanAsync(_selectedPlanId.Value);
            ClearForm();
            await LoadPlansAsync();
        }

        // Validates required fields and numeric fields, and builds the MembershipPlan
        // in one pass so callers don't duplicate parsing logic.
        private bool ValidateForm(out MembershipPlan plan)
        {
            plan = null;

            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Plan Code and Plan Name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var price))
            {
                MessageBox.Show("Price must be a valid number.", "Invalid Price",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtDuration.Text, out var duration))
            {
                MessageBox.Show("Duration (Days) must be a whole number.", "Invalid Duration",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            plan = new MembershipPlan
            {
                PlanCode = txtCode.Text.Trim(),
                PlanName = txtName.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                Price = price,
                DurationInDays = duration,
                IsActive = chkActive.Checked,
            };
            return true;
        }

        private void ClearForm()
        {
            _selectedPlanId = null;
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtDuration.Clear();
            chkActive.Checked = true;
            dgvPlans.ClearSelection();
        }
    }
}