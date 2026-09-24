using CRM.domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class PromoCodeControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedPromoCodeId = null;
        private List<Promotion> _promotions = new();

        public PromoCodeControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            _ = LoadInitialAsync();
        }

        private async System.Threading.Tasks.Task LoadInitialAsync()
        {
            await LoadPromotionsAsync();
            await LoadPromoCodesAsync();
        }

        private void StyleGrid()
        {
            dgvPromoCodes.BorderStyle = BorderStyle.None;
            dgvPromoCodes.RowHeadersVisible = false;
            dgvPromoCodes.AllowUserToAddRows = false;
            dgvPromoCodes.AllowUserToDeleteRows = false;
            dgvPromoCodes.ReadOnly = true;
            dgvPromoCodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromoCodes.MultiSelect = false;
            dgvPromoCodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromoCodes.EnableHeadersVisualStyles = false;
            dgvPromoCodes.GridColor = Color.FromArgb(229, 231, 235);
            dgvPromoCodes.ColumnHeadersHeight = 40;
            dgvPromoCodes.RowTemplate.Height = 36;

            dgvPromoCodes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvPromoCodes.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvPromoCodes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvPromoCodes.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvPromoCodes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvPromoCodes.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPromoCodes.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPromoCodes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvPromoCodes.Columns.Add("Code", "Code");
            dgvPromoCodes.Columns.Add("Promotion", "Promotion");
            dgvPromoCodes.Columns.Add("Uses", "Uses");
            dgvPromoCodes.Columns.Add("ExpiresAt", "Expires");
            dgvPromoCodes.Columns.Add("Status", "Status");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddPromoCodeAsync();
            btnDeactivate.Click += async (s, e) => await DeactivatePromoCodeAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadPromoCodesAsync(); };
            dgvPromoCodes.SelectionChanged += DgvPromoCodes_SelectionChanged;

            chkUnlimited.CheckedChanged += (s, e) =>
            {
                numMaxUses.Enabled = !chkUnlimited.Checked;
            };
            chkNoExpiry.CheckedChanged += (s, e) =>
            {
                dtpExpiresAt.Enabled = !chkNoExpiry.Checked;
            };
        }

        private async System.Threading.Tasks.Task LoadPromotionsAsync()
        {
            try
            {
                _promotions = await _api.GetPromotionsAsync();
                cmbPromotion.DataSource = _promotions;
                cmbPromotion.DisplayMember = "PromotionName";
                cmbPromotion.ValueMember = "PromotionId";
                cmbPromotion.SelectedIndex = _promotions.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load promotions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadPromoCodesAsync()
        {
            try
            {
                var codes = await _api.GetPromoCodesAsync();
                dgvPromoCodes.Rows.Clear();

                foreach (var c in codes.OrderBy(x => x.PromoCodeId))
                {
                    var usesText = c.MaxUses.HasValue
                        ? $"{c.CurrentUses} / {c.MaxUses}"
                        : $"{c.CurrentUses} / ∞";

                    var expiresText = c.ExpiresAt.HasValue
                        ? c.ExpiresAt.Value.ToString("yyyy-MM-dd")
                        : "—";

                    var promoName = c.Promotion?.PromotionName ?? $"Promotion #{c.PromotionId}";

                    var row = new DataGridViewRow();
                    row.CreateCells(dgvPromoCodes,
                        c.Code,
                        promoName,
                        usesText,
                        expiresText,
                        c.IsActive ? "Active" : "Inactive");
                    row.Tag = c;

                    int idx = dgvPromoCodes.Rows.Add(row);

                    dgvPromoCodes.Rows[idx].Cells["Status"].Style.ForeColor =
                        c.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load promo codes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPromoCodes_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvPromoCodes.SelectedRows.Count == 0)
            {
                _selectedPromoCodeId = null;
                return;
            }

            var row = dgvPromoCodes.SelectedRows[0];
            if (row.Tag is not ApiClient.PromoCodeDto c) return;
            _selectedPromoCodeId = c.PromoCodeId;

            txtCode.Text = c.Code;

            if (_promotions.Any(p => p.PromotionId == c.PromotionId))
                cmbPromotion.SelectedValue = c.PromotionId;

            if (c.MaxUses.HasValue)
            {
                chkUnlimited.Checked = false;
                numMaxUses.Enabled = true;
                numMaxUses.Value = Math.Min(c.MaxUses.Value, (int)numMaxUses.Maximum);
            }
            else
            {
                chkUnlimited.Checked = true;
                numMaxUses.Enabled = false;
            }

            if (c.ExpiresAt.HasValue)
            {
                chkNoExpiry.Checked = false;
                dtpExpiresAt.Enabled = true;
                dtpExpiresAt.Value = c.ExpiresAt.Value;
            }
            else
            {
                chkNoExpiry.Checked = true;
                dtpExpiresAt.Enabled = false;
            }

            chkActive.Checked = c.IsActive;

            // Disable Add when editing existing (only deactivate allowed)
            btnAdd.Enabled = false;
        }

        private async System.Threading.Tasks.Task AddPromoCodeAsync()
        {
            if (!ValidateForm()) return;

            var dto = new ApiClient.PromoCodeDto
            {
                Code = txtCode.Text.Trim().ToUpperInvariant(),
                PromotionId = cmbPromotion.SelectedValue as int? ?? 0,
                MaxUses = chkUnlimited.Checked ? (int?)null : (int)numMaxUses.Value,
                ExpiresAt = chkNoExpiry.Checked ? (DateTime?)null : dtpExpiresAt.Value,
                IsActive = chkActive.Checked,
            };

            try
            {
                await _api.CreatePromoCodeAsync(dto);
                MessageBox.Show($"Promo code created: {dto.Code}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadPromoCodesAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot create promo code",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create promo code: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeactivatePromoCodeAsync()
        {
            if (_selectedPromoCodeId == null)
            {
                MessageBox.Show("Select a promo code from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this promo code?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeactivatePromoCodeAsync(_selectedPromoCodeId.Value);
                ClearForm();
                await LoadPromoCodesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not deactivate promo code: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Code is required.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbPromotion.SelectedValue == null)
            {
                MessageBox.Show("Select a promotion for this code.", "Missing Promotion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _selectedPromoCodeId = null;
            txtCode.Clear();
            if (cmbPromotion.Items.Count > 0) cmbPromotion.SelectedIndex = 0;
            chkUnlimited.Checked = false;
            numMaxUses.Enabled = true;
            numMaxUses.Value = 100;
            chkNoExpiry.Checked = true;
            dtpExpiresAt.Enabled = false;
            dtpExpiresAt.Value = DateTime.Today.AddMonths(1);
            chkActive.Checked = true;
            btnAdd.Enabled = true;
            dgvPromoCodes.ClearSelection();
        }
    }
}