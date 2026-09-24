using CRM.domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class AttendanceHistoryControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private List<Customer> _customers = new();

        public AttendanceHistoryControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            SetDefaultRange();
            _ = LoadCustomersAsync();
        }

        private void SetDefaultRange()
        {
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
        }

        private void StyleGrid()
        {
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.ReadOnly = true;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.GridColor = Color.FromArgb(229, 231, 235);
            dgvHistory.ColumnHeadersHeight = 40;
            dgvHistory.RowTemplate.Height = 36;

            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvHistory.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvHistory.Columns.Add("Code", "Code");
            dgvHistory.Columns.Add("Name", "Name");
            dgvHistory.Columns.Add("Date", "Date");
            dgvHistory.Columns.Add("CheckIn", "Check-In");
            dgvHistory.Columns.Add("CheckOut", "Check-Out");
            dgvHistory.Columns.Add("Notes", "Notes");
        }

        private void WireEvents()
        {
            btnLoad.Click += async (s, e) => await LoadHistoryAsync();
            btnClear.Click += async (s, e) =>
            {
                SetDefaultRange();
                cmbCustomer.SelectedIndex = 0;
                await LoadHistoryAsync();
            };
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            try
            {
                _customers = await _api.GetCustomersAsync();

                var items = new List<object> { new { CustomerId = 0, Display = "(All customers)" } };
                items.AddRange(_customers.Select(c => (object)new { c.CustomerId, Display = $"{c.CustomerCode} — {c.CustomerName}" }));

                cmbCustomer.DataSource = items;
                cmbCustomer.DisplayMember = "Display";
                cmbCustomer.ValueMember = "CustomerId";
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load customers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadHistoryAsync()
        {
            try
            {
                int customerId = 0;
                if (cmbCustomer.SelectedValue is int id) customerId = id;

                var records = await _api.GetAttendanceHistoryAsync(
                    customerId > 0 ? customerId : (int?)null,
                    dtpFrom.Value.Date,
                    dtpTo.Value.Date);

                dgvHistory.Rows.Clear();

                foreach (var a in records)
                {
                    var localIn = a.CheckInTime.ToLocalTime();
                    var localOut = a.CheckOutTime?.ToLocalTime();

                    var row = new DataGridViewRow();
                    row.CreateCells(dgvHistory,
                        a.Customer?.CustomerCode ?? $"#{a.CustomerId}",
                        a.Customer?.CustomerName ?? "—",
                        localIn.ToString("yyyy-MM-dd"),
                        localIn.ToString("HH:mm:ss"),
                        localOut?.ToString("HH:mm:ss") ?? "—",
                        a.Notes ?? "—");
                    row.Tag = a;
                    dgvHistory.Rows.Add(row);
                }

                lblSummary.Text = $"{records.Count} attendance record(s) from {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd}.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load attendance history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}