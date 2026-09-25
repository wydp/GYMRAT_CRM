using CRM.winforms.Helpers;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRM.winforms.Controls
{
    public partial class ReportsControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private static readonly CultureInfo PesoCulture = CultureInfo.GetCultureInfo("en-PH");

        // Palette
        private static readonly Color Accent = Color.FromArgb(72, 128, 255);
        private static readonly Color TextDark = Color.FromArgb(31, 41, 55);
        private static readonly Color GridLine = Color.FromArgb(229, 231, 235);
        private static readonly Color DangerRed = Color.FromArgb(185, 28, 28);
        private static readonly Color SuccessGreen = Color.FromArgb(21, 128, 61);

        public ReportsControl()
        {
            InitializeComponent();
            StyleGrid(dgvSalesReport);
            StyleCharts();
            SetDefaultDateRange();
            WireEvents();
        }

        private void SetDefaultDateRange()
        {
            dtpFrom.Value = DateTime.Today.AddMonths(-6);
            dtpTo.Value = DateTime.Today;
        }

        private void WireEvents()
        {
            btnGenerate.Click += async (s, e) => await GenerateAllReportsAsync();
            btnExport.Click += (s, e) => ExportCurrentTab();
            btnPrint.Click += (s, e) => PrintCurrentTab();

            btnPresetToday.Click += (s, e) => ApplyPreset("today");
            btnPresetThisWeek.Click += (s, e) => ApplyPreset("week");
            btnPresetThisMonth.Click += (s, e) => ApplyPreset("month");
            btnPresetLast3.Click += (s, e) => ApplyPreset("last3");
        }

        private void ApplyPreset(string preset)
        {
            var today = DateTime.Today;
            switch (preset)
            {
                case "today":
                    dtpFrom.Value = today;
                    dtpTo.Value = today;
                    break;
                case "week":
                    // Week starting Monday
                    int diff = ((int)today.DayOfWeek + 6) % 7;
                    dtpFrom.Value = today.AddDays(-diff);
                    dtpTo.Value = today;
                    break;
                case "month":
                    dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
                    dtpTo.Value = today;
                    break;
                case "last3":
                    dtpFrom.Value = today.AddMonths(-3);
                    dtpTo.Value = today;
                    break;
            }
        }

        private void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = GridLine;
            grid.ColumnHeadersHeight = 40;
            grid.RowTemplate.Height = 32;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = TextDark;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            grid.DefaultCellStyle.ForeColor = TextDark;
            grid.DefaultCellStyle.SelectionBackColor = Accent;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        }

        private void StyleCharts()
        {
            foreach (var chart in new[] { chartRevenueByMonth, chartRevenueByPlan })
            {
                chart.BackColor = Color.White;
                chart.BorderlineColor = GridLine;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;
                chart.Palette = ChartColorPalette.BrightPastel;

                var area = chart.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.LineColor = GridLine;
                area.AxisY.MajorGrid.LineColor = GridLine;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisX.LineColor = GridLine;
                area.AxisY.LineColor = GridLine;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.Interval = 1;                          // ← this is the key fix
            }

            // Line chart color
            chartRevenueByMonth.Series[0].Color = Accent;
            chartRevenueByMonth.Series[0].MarkerColor = Accent;
            chartRevenueByMonth.Titles.Clear();
            chartRevenueByMonth.Titles.Add(new Title("Revenue by Month",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            // Bar chart
            chartRevenueByPlan.Series[0].Color = Accent;
            chartRevenueByPlan.Titles.Clear();
            chartRevenueByPlan.Titles.Add(new Title("Revenue by Plan",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));
        }

        private async System.Threading.Tasks.Task GenerateAllReportsAsync()
        {
            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                MessageBox.Show("'To' date cannot be earlier than 'From' date.", "Invalid Range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await GenerateRevenueReportAsync();
            // Later: await GenerateMembershipReportAsync(); etc.
        }

        // ==========================================================
        // TAB 1 — Revenue
        // ==========================================================

        private async System.Threading.Tasks.Task GenerateRevenueReportAsync()
        {
            try
            {
                var report = await _api.GetRevenueReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null)
                {
                    MessageBox.Show("Report endpoint returned no data.", "Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- KPI cards ---
                kpiRevenueTotal.Value = report.TotalRevenue.ToString("C2", PesoCulture);
                kpiRevenueCount.Value = report.SaleCount.ToString("N0");
                kpiRevenueAvg.Value = report.AvgSale.ToString("C2", PesoCulture);
                kpiRevenueCancelled.Value = report.CancelledCount.ToString("N0");

                // --- Line chart: revenue by month ---
                chartRevenueByMonth.Series[0].Points.Clear();
                int monthIndex = 0;
                foreach (var m in report.ByMonth)
                {
                    var idx = chartRevenueByMonth.Series[0].Points.AddXY(monthIndex, m.Revenue);
                    var point = chartRevenueByMonth.Series[0].Points[idx];
                    point.AxisLabel = m.Month;                       // use month string as the label
                    point.ToolTip = $"{m.Month}: {m.Revenue.ToString("C2", PesoCulture)} ({m.Count} sales)";
                    monthIndex++;
                }

                // Force categorical-style rendering so the labels are respected
                chartRevenueByMonth.ChartAreas[0].AxisX.Interval = 1;
                chartRevenueByMonth.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartRevenueByMonth.ChartAreas[0].AxisX.IsMarginVisible = false;

                // --- Bar chart: revenue by plan ---
                chartRevenueByPlan.Series[0].Points.Clear();
                int planIndex = 0;
                foreach (var p in report.ByPlan)
                {
                    var idx = chartRevenueByPlan.Series[0].Points.AddXY(planIndex, p.Revenue);
                    var point = chartRevenueByPlan.Series[0].Points[idx];
                    point.AxisLabel = p.PlanName;
                    point.ToolTip = $"{p.PlanName}: {p.Revenue.ToString("C2", PesoCulture)} ({p.Count} sales)";
                    planIndex++;
                }

                chartRevenueByPlan.ChartAreas[0].AxisX.Interval = 1;

                // --- Grid ---
                if (dgvSalesReport.Columns.Count == 0)
                {
                    dgvSalesReport.Columns.Add("SaleDate", "Sale Date");
                    dgvSalesReport.Columns.Add("CustomerName", "Customer");
                    dgvSalesReport.Columns.Add("PlanName", "Plan");
                    dgvSalesReport.Columns.Add("AmountPaid", "Amount Paid");
                    dgvSalesReport.Columns["AmountPaid"]!.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }

                dgvSalesReport.Rows.Clear();
                foreach (var s in report.Sales)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvSalesReport,
                        s.SaleDate.ToString("yyyy-MM-dd"),
                        s.CustomerName,
                        s.PlanName,
                        s.AmountPaid.ToString("C2", PesoCulture));
                    row.Tag = s.MembershipSaleId;
                    dgvSalesReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not generate revenue report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // EXPORT + PRINT (shared across tabs)
        // ==========================================================

        private void ExportCurrentTab()
        {
            var grid = GetCurrentTabGrid();
            if (grid is null)
            {
                MessageBox.Show("Nothing to export on this tab yet.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fileName = $"GymRat_{tabs.SelectedTab?.Text?.Replace(" ", "")}_{DateTime.Now:yyyyMMdd}.csv";
            CsvExporter.ExportGrid(grid, fileName);
        }

        private void PrintCurrentTab()
        {
            var grid = GetCurrentTabGrid();
            if (grid is null || grid.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to print on this tab yet.", "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var doc = new PrintDocument();
            doc.DocumentName = $"GymRat Report — {tabs.SelectedTab?.Text}";

            int rowIndex = 0;

            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics!;
                var font = new Font("Segoe UI", 9F);
                var headerFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                int y = 40;
                int x = 40;
                int rowHeight = 22;
                int colWidth = 180;

                // Title
                g.DrawString(doc.DocumentName, new Font("Segoe UI", 14F, FontStyle.Bold),
                    Brushes.Black, x, y);
                y += 30;
                g.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}",
                    font, Brushes.Gray, x, y);
                y += 30;

                // Header row
                for (int c = 0; c < grid.Columns.Count; c++)
                {
                    g.DrawString(grid.Columns[c].HeaderText, headerFont, Brushes.Black,
                        x + (c * colWidth), y);
                }
                y += rowHeight;
                g.DrawLine(Pens.Black, x, y, x + (grid.Columns.Count * colWidth), y);
                y += 4;

                // Data rows
                while (rowIndex < grid.Rows.Count && y < e.MarginBounds.Bottom - rowHeight)
                {
                    var row = grid.Rows[rowIndex];
                    if (!row.IsNewRow)
                    {
                        for (int c = 0; c < grid.Columns.Count; c++)
                        {
                            var val = row.Cells[c].Value?.ToString() ?? "";
                            g.DrawString(val, font, Brushes.Black, x + (c * colWidth), y);
                        }
                        y += rowHeight;
                    }
                    rowIndex++;
                }

                e.HasMorePages = rowIndex < grid.Rows.Count;
            };

            using var preview = new PrintPreviewDialog
            {
                Document = doc,
                Width = 1000,
                Height = 700,
            };
            preview.ShowDialog(this);
        }

        private DataGridView GetCurrentTabGrid()
        {
            if (tabs.SelectedTab == tabRevenue) return dgvSalesReport;
            return null;
        }
    }
}