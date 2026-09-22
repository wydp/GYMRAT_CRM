using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class ReportsControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private static readonly CultureInfo PesoCulture = CultureInfo.GetCultureInfo("en-PH");

        public ReportsControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            SetDefaultDateRange();
        }

        private void SetDefaultDateRange()
        {
            dtpFrom.Value = DateTime.Today.AddMonths(-6);
            dtpTo.Value = DateTime.Today;
        }

        private void WireEvents()
        {
            btnGenerate.Click += async (s, e) => await GenerateReportAsync();
        }

        private void StyleGrid()
        {
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.RowHeadersVisible = false;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.ReadOnly = true;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.MultiSelect = false;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.GridColor = Color.FromArgb(229, 231, 235);
            dgvReport.ColumnHeadersHeight = 40;
            dgvReport.RowTemplate.Height = 36;

            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvReport.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvReport.Columns.Add("SaleDate", "Sale Date");
            dgvReport.Columns.Add("CustomerName", "Customer");
            dgvReport.Columns.Add("PlanName", "Plan");
            dgvReport.Columns.Add("AmountPaid", "Amount Paid");

            // Right-align the currency column
            dgvReport.Columns["AmountPaid"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private async System.Threading.Tasks.Task GenerateReportAsync()
        {
            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                MessageBox.Show("'To' date cannot be earlier than 'From' date.", "Invalid Range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sales = await _api.GetMembershipSalesReportAsync(dtpFrom.Value, dtpTo.Value);
                dgvReport.Rows.Clear();

                decimal total = 0m;
                int count = 0;

                foreach (var s in sales.OrderBy(x => x.SaleDate))
                {
                    var customerName = s.Customer?.CustomerName ?? "(unknown)";
                    var planName = s.MembershipPlan?.PlanName ?? "(unknown)";

                    var row = new DataGridViewRow();
                    row.CreateCells(dgvReport,
                        s.SaleDate.ToString("yyyy-MM-dd"),
                        customerName,
                        planName,
                        s.AmountPaid.ToString("C2", PesoCulture));
                    row.Tag = s.MembershipSaleId;

                    dgvReport.Rows.Add(row);

                    total += s.AmountPaid;
                    count++;
                }

                // Add a totals row at the bottom, styled bold.
                var totalsRow = new DataGridViewRow();
                totalsRow.CreateCells(dgvReport,
                    "",
                    "",
                    $"TOTAL ({count} sales)",
                    total.ToString("C2", PesoCulture));
                totalsRow.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                totalsRow.DefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
                totalsRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 242, 255);
                totalsRow.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
                totalsRow.ReadOnly = true;
                dgvReport.Rows.Add(totalsRow);

                // Summary label above the grid
                lblSummary.Text = $"{count} sales from {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd} — Total: {total.ToString("C2", PesoCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not generate report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}