using CRM.domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace CRM.winforms.Controls
{
    public partial class CheckInControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private List<Customer> _allCustomers = new();
        private Customer? _selectedCustomer = null;

        public CheckInControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            _ = LoadInitialAsync();
        }

        private async System.Threading.Tasks.Task LoadInitialAsync()
        {
            await LoadCustomersAsync();
            await LoadTodayAsync();
        }

        private void StyleGrid()
        {
            dgvToday.BorderStyle = BorderStyle.None;
            dgvToday.RowHeadersVisible = false;
            dgvToday.AllowUserToAddRows = false;
            dgvToday.AllowUserToDeleteRows = false;
            dgvToday.ReadOnly = true;
            dgvToday.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvToday.MultiSelect = false;
            dgvToday.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvToday.EnableHeadersVisualStyles = false;
            dgvToday.GridColor = Color.FromArgb(229, 231, 235);
            dgvToday.ColumnHeadersHeight = 40;
            dgvToday.RowTemplate.Height = 36;

            dgvToday.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvToday.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvToday.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvToday.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvToday.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvToday.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvToday.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvToday.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvToday.Columns.Add("Code", "Code");
            dgvToday.Columns.Add("Name", "Name");
            dgvToday.Columns.Add("Time", "Check-In Time");
            dgvToday.Columns.Add("Notes", "Notes");
        }

        private void WireEvents()
        {
            txtSearch.TextChanged += (s, e) => UpdateMatches();
            lstMatches.SelectedIndexChanged += (s, e) => OnCustomerSelected();
            btnCheckIn.Click += async (s, e) => await PerformCheckInAsync();
            btnClear.Click += (s, e) => ClearForm();
            btnRefreshToday.Click += async (s, e) => await LoadTodayAsync();
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            try
            {
                _allCustomers = await _api.GetCustomersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load customers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filter customers by typed text — name or code, case-insensitive
        private void UpdateMatches()
        {
            var q = txtSearch.Text.Trim();
            lstMatches.Items.Clear();

            if (string.IsNullOrWhiteSpace(q))
            {
                _selectedCustomer = null;
                lblSelectedValue.Text = "—";
                btnCheckIn.Enabled = false;
                return;
            }

            var matches = _allCustomers
                .Where(c => c.CustomerName.Contains(q, StringComparison.OrdinalIgnoreCase)
                         || c.CustomerCode.Contains(q, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToList();

            foreach (var c in matches)
                lstMatches.Items.Add($"{c.CustomerCode} — {c.CustomerName}");
        }

        private void OnCustomerSelected()
        {
            if (lstMatches.SelectedIndex < 0)
            {
                _selectedCustomer = null;
                lblSelectedValue.Text = "—";
                btnCheckIn.Enabled = false;
                return;
            }

            var item = lstMatches.Items[lstMatches.SelectedIndex].ToString() ?? "";
            var code = item.Split('—')[0].Trim();

            _selectedCustomer = _allCustomers.FirstOrDefault(c => c.CustomerCode == code);
            if (_selectedCustomer != null)
            {
                lblSelectedValue.Text = $"{_selectedCustomer.CustomerCode} — {_selectedCustomer.CustomerName}";
                btnCheckIn.Enabled = true;
            }
            else
            {
                lblSelectedValue.Text = "—";
                btnCheckIn.Enabled = false;
            }
        }

        private async System.Threading.Tasks.Task PerformCheckInAsync()
        {
            if (_selectedCustomer is null)
            {
                MessageBox.Show("Search and select a customer first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnCheckIn.Enabled = false;
            btnCheckIn.Text = "Checking in...";

            try
            {
                var result = await _api.CheckInMemberAsync(new ApiClient.CheckInRequest
                {
                    CustomerId = _selectedCustomer.CustomerId,
                    BranchId = CRM.winforms.Model.AuthContext.BranchId,
                    Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                });

                MessageBox.Show($"{_selectedCustomer.CustomerName} checked in successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                await LoadTodayAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot Check In",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Check-in failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckIn.Text = "CHECK IN";
                btnCheckIn.Enabled = _selectedCustomer != null;
            }
        }

        private async System.Threading.Tasks.Task LoadTodayAsync()
        {
            try
            {
                var records = await _api.GetTodayAttendanceAsync();
                dgvToday.Rows.Clear();

                foreach (var a in records)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvToday,
                        a.Customer?.CustomerCode ?? $"#{a.CustomerId}",
                        a.Customer?.CustomerName ?? "—",
                        a.CheckInTime.ToLocalTime().ToString("HH:mm:ss"),
                        a.Notes ?? "—");
                    row.Tag = a;
                    dgvToday.Rows.Add(row);
                }

                lblToday.Text = $"Today's Check-Ins ({records.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load today's check-ins: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtSearch.Clear();
            lstMatches.Items.Clear();
            txtNotes.Clear();
            _selectedCustomer = null;
            lblSelectedValue.Text = "—";
            btnCheckIn.Enabled = false;
        }
    }
}