using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class PromotionControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedPromotionId = null;

        private static readonly CultureInfo PesoCulture = CultureInfo.GetCultureInfo("en-PH");

        public PromotionControl()
        {
            InitializeComponent();
            PopulateDiscountTypeCombo();
            StyleGrid();
            WireEvents();
            WireAudienceCheckboxes();
            _ = LoadPromotionsAsync();
        }

        private void PopulateDiscountTypeCombo()
        {
            cmbDiscountType.DataSource = Enum.GetValues(typeof(DiscountType));
            cmbDiscountType.SelectedIndex = 0;
            cmbDiscountType.SelectedIndexChanged += (s, e) => UpdateDiscountValueLimit();
            UpdateDiscountValueLimit();
        }

        private void UpdateDiscountValueLimit()
        {
            if (cmbDiscountType.SelectedItem is DiscountType type && type == DiscountType.Percentage)
            {
                numDiscountValue.Maximum = 100m;
                if (numDiscountValue.Value > 100m) numDiscountValue.Value = 100m;
            }
            else
            {
                numDiscountValue.Maximum = 1000000m;
            }
        }

        // Ensures "All Members" and specific audiences are mutually exclusive.
        // Checking All Members unchecks the others, and vice versa.
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

            void UncheckAllIfAnySpecific(CheckBox cb)
            {
                cb.CheckedChanged += (s, e) =>
                {
                    if (!cb.Checked) return;
                    chkTargetAll.Checked = false;
                };
            }

            UncheckAllIfAnySpecific(chkTargetPWD);
            UncheckAllIfAnySpecific(chkTargetSenior);
            UncheckAllIfAnySpecific(chkTargetStudent);
            UncheckAllIfAnySpecific(chkTargetCorporate);
        }

        private void StyleGrid()
        {
            dgvPromotions.BorderStyle = BorderStyle.None;
            dgvPromotions.RowHeadersVisible = false;
            dgvPromotions.AllowUserToAddRows = false;
            dgvPromotions.AllowUserToDeleteRows = false;
            dgvPromotions.ReadOnly = true;
            dgvPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromotions.MultiSelect = false;
            dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromotions.EnableHeadersVisualStyles = false;
            dgvPromotions.GridColor = Color.FromArgb(229, 231, 235);
            dgvPromotions.ColumnHeadersHeight = 40;
            dgvPromotions.RowTemplate.Height = 36;

            dgvPromotions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvPromotions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvPromotions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvPromotions.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvPromotions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvPromotions.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPromotions.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPromotions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvPromotions.Columns.Add("PromotionCode", "Code");
            dgvPromotions.Columns.Add("PromotionName", "Name");
            dgvPromotions.Columns.Add("Description", "Description");
            dgvPromotions.Columns.Add("Audience", "Audience");
            dgvPromotions.Columns.Add("DiscountType", "Type");
            dgvPromotions.Columns.Add("DiscountValue", "Value");
            dgvPromotions.Columns.Add("StartDate", "Start");
            dgvPromotions.Columns.Add("EndDate", "End");
            dgvPromotions.Columns.Add("Active", "Active");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddPromotionAsync();
            btnUpdate.Click += async (s, e) => await UpdatePromotionAsync();
            btnDeactivate.Click += async (s, e) => await DeactivatePromotionAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadPromotionsAsync(); };
            dgvPromotions.SelectionChanged += DgvPromotions_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadPromotionsAsync()
        {
            try
            {
                var promotions = await _api.GetPromotionsAsync();
                dgvPromotions.Rows.Clear();

                foreach (var p in promotions.OrderBy(x => x.PromotionId))
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvPromotions,
                        p.PromotionCode,
                        p.PromotionName,
                        p.Description,
                        FormatAudience(p),
                        p.DiscountType.ToString(),
                        FormatDiscount(p.DiscountType, p.DiscountValue),
                        p.StartDate.ToString("yyyy-MM-dd"),
                        p.EndDate.ToString("yyyy-MM-dd"),
                        p.IsActive ? "Active" : "Inactive");
                    row.Tag = p;

                    int rowIndex = dgvPromotions.Rows.Add(row);

                    dgvPromotions.Rows[rowIndex].Cells["Active"].Style.ForeColor =
                        p.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load promotions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatAudience(Promotion p)
        {
            var parts = new System.Collections.Generic.List<string>();
            if (p.TargetAllMembers) parts.Add("All Members");
            if (p.TargetPWD) parts.Add("PWD");
            if (p.TargetSenior) parts.Add("Senior");
            if (p.TargetStudent) parts.Add("Student");
            if (p.TargetCorporate) parts.Add("Corporate");
            return parts.Count == 0 ? "—" : string.Join(", ", parts);
        }

        private static string FormatDiscount(DiscountType type, decimal value) =>
            type == DiscountType.Percentage
                ? $"{value:0.##}%"
                : value.ToString("C2", PesoCulture);

        private void DgvPromotions_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvPromotions.SelectedRows.Count == 0) return;

            var row = dgvPromotions.SelectedRows[0];
            if (row.Tag is not Promotion p) return;
            _selectedPromotionId = p.PromotionId;

            txtCode.Text = p.PromotionCode;
            txtName.Text = p.PromotionName;
            txtDescription.Text = p.Description;
            txtReason.Text = p.Reason ?? string.Empty;

            chkTargetAll.Checked = p.TargetAllMembers;
            chkTargetPWD.Checked = p.TargetPWD;
            chkTargetSenior.Checked = p.TargetSenior;
            chkTargetStudent.Checked = p.TargetStudent;
            chkTargetCorporate.Checked = p.TargetCorporate;

            cmbDiscountType.SelectedItem = p.DiscountType;
            UpdateDiscountValueLimit();

            var value = p.DiscountValue;
            if (value < numDiscountValue.Minimum) value = numDiscountValue.Minimum;
            if (value > numDiscountValue.Maximum) value = numDiscountValue.Maximum;
            numDiscountValue.Value = value;

            dtpStart.Value = p.StartDate;
            dtpEnd.Value = p.EndDate;
            chkActive.Checked = p.IsActive;
        }

        private async System.Threading.Tasks.Task AddPromotionAsync()
        {
            if (!ValidateForm()) return;

            try
            {
                await _api.CreatePromotionAsync(BuildPromotionFromForm());
                ClearForm();
                await LoadPromotionsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add promotion: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdatePromotionAsync()
        {
            if (_selectedPromotionId == null)
            {
                MessageBox.Show("Select a promotion from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateForm()) return;

            try
            {
                var promotion = BuildPromotionFromForm();
                promotion.PromotionId = _selectedPromotionId.Value;
                await _api.UpdatePromotionAsync(_selectedPromotionId.Value, promotion);
                ClearForm();
                await LoadPromotionsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update promotion: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeactivatePromotionAsync()
        {
            if (_selectedPromotionId == null)
            {
                MessageBox.Show("Select a promotion from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this promotion?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivatePromotionAsync(_selectedPromotionId.Value);
            ClearForm();
            await LoadPromotionsAsync();
        }

        private Promotion BuildPromotionFromForm() => new Promotion
        {
            PromotionCode = txtCode.Text.Trim(),
            PromotionName = txtName.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            Reason = string.IsNullOrWhiteSpace(txtReason.Text) ? null : txtReason.Text.Trim(),
            TargetAllMembers = chkTargetAll.Checked,
            TargetPWD = chkTargetPWD.Checked,
            TargetSenior = chkTargetSenior.Checked,
            TargetStudent = chkTargetStudent.Checked,
            TargetCorporate = chkTargetCorporate.Checked,
            DiscountType = (DiscountType)cmbDiscountType.SelectedItem!,
            DiscountValue = numDiscountValue.Value,
            StartDate = dtpStart.Value,
            EndDate = dtpEnd.Value,
            IsActive = chkActive.Checked,
        };

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Promotion Code and Promotion Name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason/justification for this promotion.", "Missing Reason",
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

            if ((DiscountType)cmbDiscountType.SelectedItem! == DiscountType.Percentage
                && numDiscountValue.Value > 100m)
            {
                MessageBox.Show("Percentage discounts cannot exceed 100%.", "Invalid Discount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numDiscountValue.Value <= 0m)
            {
                MessageBox.Show("Discount Value must be greater than zero.", "Invalid Discount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _selectedPromotionId = null;
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtReason.Clear();
            chkTargetAll.Checked = true;
            chkTargetPWD.Checked = false;
            chkTargetSenior.Checked = false;
            chkTargetStudent.Checked = false;
            chkTargetCorporate.Checked = false;
            cmbDiscountType.SelectedIndex = 0;
            numDiscountValue.Value = 0;
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            chkActive.Checked = true;
            dgvPromotions.ClearSelection();
        }
    }
}