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

        private static readonly Color Accent = Color.FromArgb(72, 128, 255);
        private static readonly Color TextDark = Color.FromArgb(31, 41, 55);
        private static readonly Color GridLine = Color.FromArgb(229, 231, 235);
        private static readonly Color DangerRed = Color.FromArgb(185, 28, 28);
        private static readonly Color SuccessGreen = Color.FromArgb(21, 128, 61);
        private static readonly Color Amber = Color.FromArgb(180, 83, 9);

        public ReportsControl()
        {
            InitializeComponent();
            StyleGrid(dgvSalesReport);
            StyleGrid(dgvMembershipReport);
            StyleGrid(dgvAttendanceReport);
            StyleGrid(dgvLeadReport);
            StyleGrid(dgvRetentionReport);
            StyleGrid(dgvPromoReport);
            StyleCharts();
            StyleAttendanceCharts();
            StyleLeadConversionCharts();
            StyleRetentionCharts();
            StylePromoUsageCharts();
            SetDefaultDateRange();
            WireEvents();
        }

        private async System.Threading.Tasks.Task GenerateLeadConversionReportAsync()
        {
            try
            {
                var report = await _api.GetLeadConversionReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null) return;

                kpiLeadTotal.Value = report.Total.ToString("N0");
                kpiLeadConverted.Value = report.Converted.ToString("N0");
                kpiLeadLost.Value = report.Lost.ToString("N0");
                kpiLeadRate.Value = report.ConversionRate.ToString("F1") + "%";

                chartLeadBySource.Series[0].Points.Clear();
                int i = 0;
                foreach (var s in report.BySource)
                {
                    var idx = chartLeadBySource.Series[0].Points.AddXY(i++, s.Count);
                    var pt = chartLeadBySource.Series[0].Points[idx];
                    pt.AxisLabel = s.Name;
                    pt.ToolTip = $"{s.Name}: {s.Count} leads";
                }

                chartLeadByStatus.Series[0].Points.Clear();
                foreach (var s in report.StatusDistribution)
                {
                    var idx = chartLeadByStatus.Series[0].Points.AddXY(s.Name, s.Count);
                    var pt = chartLeadByStatus.Series[0].Points[idx];
                    pt.ToolTip = $"{s.Name}: {s.Count}";
                }

                if (dgvLeadReport.Columns.Count == 0)
                {
                    dgvLeadReport.Columns.Add("FullName", "Full Name");
                    dgvLeadReport.Columns.Add("Status", "Status");
                    dgvLeadReport.Columns.Add("Source", "Source");
                    dgvLeadReport.Columns.Add("CreatedAt", "Created At");
                    dgvLeadReport.Columns.Add("ConvertedAt", "Converted At");
                    dgvLeadReport.Columns.Add("ConvertedCustomerName", "Converted Customer");
                }

                dgvLeadReport.Rows.Clear();
                foreach (var l in report.Leads)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvLeadReport,
                        l.FullName,
                        l.Status,
                        l.Source,
                        l.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd"),
                        l.ConvertedAt.HasValue ? l.ConvertedAt.Value.ToLocalTime().ToString("yyyy-MM-dd") : "—",
                        l.ConvertedCustomerName ?? "");
                    dgvLeadReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lead Conversion report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task GenerateRetentionReportAsync()
        {
            try
            {
                var report = await _api.GetRetentionReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null) return;

                kpiRetCancellations.Value = report.Cancellations.ToString("N0");
                kpiRetFreezes.Value = report.Freezes.ToString("N0");
                kpiRetWinBacks.Value = report.WinBacks.ToString("N0");
                kpiRetRenewals.Value = report.Renewals.ToString("N0");

                chartRetentionByDay.Series[0].Points.Clear();
                int i = 0;
                foreach (var d in report.ByDay)
                {
                    var idx = chartRetentionByDay.Series[0].Points.AddXY(i++, d.Count);
                    var pt = chartRetentionByDay.Series[0].Points[idx];
                    pt.AxisLabel = d.Date;
                    pt.ToolTip = $"{d.Date}: {d.Count} actions";
                }

                chartRetentionByAction.Series[0].Points.Clear();
                foreach (var a in report.ByAction)
                {
                    var idx = chartRetentionByAction.Series[0].Points.AddXY(a.Name, a.Count);
                    var pt = chartRetentionByAction.Series[0].Points[idx];
                    pt.ToolTip = $"{a.Name}: {a.Count}";
                }

                if (dgvRetentionReport.Columns.Count == 0)
                {
                    dgvRetentionReport.Columns.Add("CustomerName", "Customer");
                    dgvRetentionReport.Columns.Add("ActionType", "Action");
                    dgvRetentionReport.Columns.Add("Outcome", "Outcome");
                    dgvRetentionReport.Columns.Add("Timestamp", "Timestamp");
                    dgvRetentionReport.Columns.Add("Notes", "Notes");
                }

                dgvRetentionReport.Rows.Clear();
                foreach (var r in report.Actions)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvRetentionReport,
                        r.CustomerName ?? "(unknown)",
                        r.ActionType,
                        r.Outcome,
                        r.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                        r.Notes ?? "");
                    dgvRetentionReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Retention report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task GeneratePromoUsageReportAsync()
        {
            try
            {
                var report = await _api.GetPromoUsageReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null) return;

                kpiPromoTotal.Value = report.TotalCodes.ToString("N0");
                kpiPromoActive.Value = report.ActiveCodes.ToString("N0");
                kpiPromoRedemptions.Value = report.TotalRedemptions.ToString("N0");
                kpiPromoAvgRate.Value = (report.AvgRate).ToString("P1");

                chartPromoByCode.Series[0].Points.Clear();
                int i = 0;
                foreach (var p in report.ByCode)
                {
                    var idx = chartPromoByCode.Series[0].Points.AddXY(i++, p.Redemptions);
                    var pt = chartPromoByCode.Series[0].Points[idx];
                    pt.AxisLabel = p.Code;
                    pt.ToolTip = $"{p.Code}: {p.Redemptions} redemptions";
                }

                chartPromoByRate.Series[0].Points.Clear();
                var active = report.ActiveCodes;
                var inactive = report.TotalCodes - report.ActiveCodes;
                chartPromoByRate.Series[0].Points.AddXY("Active", active);
                chartPromoByRate.Series[0].Points.AddXY("Inactive", inactive);

                if (dgvPromoReport.Columns.Count == 0)
                {
                    dgvPromoReport.Columns.Add("Code", "Code");
                    dgvPromoReport.Columns.Add("CurrentUses", "Current Uses");
                    dgvPromoReport.Columns.Add("MaxUses", "Max Uses");
                    dgvPromoReport.Columns.Add("ExpiresAt", "Expires At");
                    dgvPromoReport.Columns.Add("PromotionName", "Promotion");
                }

                dgvPromoReport.Rows.Clear();
                foreach (var p in report.PromoCodes)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvPromoReport,
                        p.Code,
                        p.CurrentUses,
                        p.MaxUses.HasValue ? p.MaxUses.Value.ToString() : "—",
                        p.ExpiresAt.HasValue ? p.ExpiresAt.Value.ToLocalTime().ToString("yyyy-MM-dd") : "—",
                        p.PromotionName ?? "");
                    dgvPromoReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Promo Usage report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDateRange()
        {
            dtpFrom.Value = DateTime.Today.AddMonths(-6);
            dtpTo.Value = DateTime.Today;
        }

        private void StyleLeadConversionCharts()
        {
            foreach (var chart in new[] { chartLeadBySource, chartLeadByStatus })
            {
                chart.BackColor = Color.White;
                chart.BorderlineColor = GridLine;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;

                var area = chart.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.LineColor = GridLine;
                area.AxisY.MajorGrid.LineColor = GridLine;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisX.LineColor = GridLine;
                area.AxisY.LineColor = GridLine;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.Interval = 1;
            }

            chartLeadBySource.Series[0].Color = Accent;
            chartLeadBySource.Titles.Clear();
            chartLeadBySource.Titles.Add(new Title("Leads by Source",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartLeadByStatus.Series[0].IsValueShownAsLabel = true;
            chartLeadByStatus.Series[0]["DoughnutRadius"] = "60";
            chartLeadByStatus.Titles.Clear();
            chartLeadByStatus.Titles.Add(new Title("Leads by Status",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));
        }

        private void StyleRetentionCharts()
        {
            foreach (var chart in new[] { chartRetentionByDay, chartRetentionByAction })
            {
                chart.BackColor = Color.White;
                chart.BorderlineColor = GridLine;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;

                var area = chart.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.LineColor = GridLine;
                area.AxisY.MajorGrid.LineColor = GridLine;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisX.LineColor = GridLine;
                area.AxisY.LineColor = GridLine;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.Interval = 1;
            }

            chartRetentionByDay.Series[0].Color = Accent;
            chartRetentionByDay.Titles.Clear();
            chartRetentionByDay.Titles.Add(new Title("Retention Actions by Day",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartRetentionByAction.Series[0].IsValueShownAsLabel = true;
            chartRetentionByAction.Series[0]["DoughnutRadius"] = "60";
            chartRetentionByAction.Titles.Clear();
            chartRetentionByAction.Titles.Add(new Title("Retention by Action Type",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));
        }

        private void StylePromoUsageCharts()
        {
            foreach (var chart in new[] { chartPromoByCode, chartPromoByRate })
            {
                chart.BackColor = Color.White;
                chart.BorderlineColor = GridLine;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;

                var area = chart.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.LineColor = GridLine;
                area.AxisY.MajorGrid.LineColor = GridLine;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisX.LineColor = GridLine;
                area.AxisY.LineColor = GridLine;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.Interval = 1;
            }

            chartPromoByCode.Series[0].Color = Accent;
            chartPromoByCode.Titles.Clear();
            chartPromoByCode.Titles.Add(new Title("Redemptions by Code",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartPromoByRate.Series[0].IsValueShownAsLabel = true;
            chartPromoByRate.Series[0]["DoughnutRadius"] = "60";
            chartPromoByRate.Titles.Clear();
            chartPromoByRate.Titles.Add(new Title("Active vs Inactive Promo Codes",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));
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
            foreach (var chart in new[] { chartRevenueByMonth, chartRevenueByPlan,
                                          chartMembershipStatus, chartMembershipByPlan })
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
                area.AxisX.Interval = 1;
            }

            chartRevenueByMonth.Series[0].Color = Accent;
            chartRevenueByMonth.Series[0].MarkerColor = Accent;
            chartRevenueByMonth.Titles.Clear();
            chartRevenueByMonth.Titles.Add(new Title("Revenue by Month",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartRevenueByPlan.Series[0].Color = Accent;
            chartRevenueByPlan.Titles.Clear();
            chartRevenueByPlan.Titles.Add(new Title("Revenue by Plan",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartMembershipStatus.Series[0].IsValueShownAsLabel = true;
            chartMembershipStatus.Series[0]["DoughnutRadius"] = "60";
            chartMembershipStatus.Titles.Clear();
            chartMembershipStatus.Titles.Add(new Title("Membership Status",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartMembershipByPlan.Series[0].Color = Accent;
            chartMembershipByPlan.Titles.Clear();
            chartMembershipByPlan.Titles.Add(new Title("Members by Plan",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));
        }

        private void StyleAttendanceCharts()
        {
            foreach (var chart in new[] { chartAttendanceByDay, chartAttendanceByHour })
            {
                chart.BackColor = Color.White;
                chart.BorderlineColor = GridLine;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;

                var area = chart.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.LineColor = GridLine;
                area.AxisY.MajorGrid.LineColor = GridLine;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisX.LineColor = GridLine;
                area.AxisY.LineColor = GridLine;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.Interval = 1;
            }

            chartAttendanceByDay.Series[0].Color = Accent;
            chartAttendanceByDay.Titles.Clear();
            chartAttendanceByDay.Titles.Add(new Title("Check-Ins by Day",
                Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), TextDark));

            chartAttendanceByHour.Series[0].Color = Amber;
            chartAttendanceByHour.Titles.Clear();
            chartAttendanceByHour.Titles.Add(new Title("Check-Ins by Hour",
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
            await GenerateMembershipReportAsync();
            await GenerateAttendanceReportAsync();
            await GenerateLeadConversionReportAsync();
            await GenerateRetentionReportAsync();
            await GeneratePromoUsageReportAsync();
        }

        // ==========================================================
        // TAB 1 — Revenue
        // ==========================================================

        private async System.Threading.Tasks.Task GenerateRevenueReportAsync()
        {
            try
            {
                var report = await _api.GetRevenueReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null) return;

                kpiRevenueTotal.Value = report.TotalRevenue.ToString("C2", PesoCulture);
                kpiRevenueCount.Value = report.SaleCount.ToString("N0");
                kpiRevenueAvg.Value = report.AvgSale.ToString("C2", PesoCulture);
                kpiRevenueCancelled.Value = report.CancelledCount.ToString("N0");

                chartRevenueByMonth.Series[0].Points.Clear();
                int m = 0;
                foreach (var x in report.ByMonth)
                {
                    var idx = chartRevenueByMonth.Series[0].Points.AddXY(m++, x.Revenue);
                    var pt = chartRevenueByMonth.Series[0].Points[idx];
                    pt.AxisLabel = x.Month;
                    pt.ToolTip = $"{x.Month}: {x.Revenue.ToString("C2", PesoCulture)} ({x.Count} sales)";
                }

                chartRevenueByPlan.Series[0].Points.Clear();
                int p = 0;
                foreach (var x in report.ByPlan)
                {
                    var idx = chartRevenueByPlan.Series[0].Points.AddXY(p++, x.Revenue);
                    var pt = chartRevenueByPlan.Series[0].Points[idx];
                    pt.AxisLabel = x.PlanName;
                    pt.ToolTip = $"{x.PlanName}: {x.Revenue.ToString("C2", PesoCulture)} ({x.Count} sales)";
                }

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
                    dgvSalesReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Revenue report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // TAB 2 — Membership
        // ==========================================================

        private async System.Threading.Tasks.Task GenerateMembershipReportAsync()
        {
            try
            {
                var report = await _api.GetMembershipReportAsync();
                if (report is null) return;

                kpiMemberActive.Value = report.ActiveCount.ToString("N0");
                kpiMemberExpiring.Value = report.ExpiringSoonCount.ToString("N0");
                kpiMemberExpired.Value = report.ExpiredCount.ToString("N0");
                kpiMemberFrozen.Value = report.FrozenCount.ToString("N0");

                chartMembershipStatus.Series[0].Points.Clear();
                foreach (var s in report.StatusDistribution)
                {
                    var idx = chartMembershipStatus.Series[0].Points.AddXY(s.Status, s.Count);
                    var pt = chartMembershipStatus.Series[0].Points[idx];
                    pt.ToolTip = $"{s.Status}: {s.Count}";
                    pt.Color = s.Status switch
                    {
                        "Active" => SuccessGreen,
                        "Expiring Soon" => Amber,
                        "Expired" => DangerRed,
                        "Frozen" => Accent,
                        _ => Color.Gray,
                    };
                }

                chartMembershipByPlan.Series[0].Points.Clear();
                int i = 0;
                foreach (var x in report.ByPlan)
                {
                    var idx = chartMembershipByPlan.Series[0].Points.AddXY(i++, x.Count);
                    var pt = chartMembershipByPlan.Series[0].Points[idx];
                    pt.AxisLabel = x.PlanName;
                    pt.ToolTip = $"{x.PlanName}: {x.Count} members";
                }

                if (dgvMembershipReport.Columns.Count == 0)
                {
                    dgvMembershipReport.Columns.Add("CustomerCode", "Code");
                    dgvMembershipReport.Columns.Add("CustomerName", "Customer");
                    dgvMembershipReport.Columns.Add("PlanName", "Plan");
                    dgvMembershipReport.Columns.Add("StartDate", "Start");
                    dgvMembershipReport.Columns.Add("ExpiryDate", "Expiry");
                    dgvMembershipReport.Columns.Add("DaysLeft", "Days Left");
                    dgvMembershipReport.Columns.Add("Status", "Status");
                }

                dgvMembershipReport.Rows.Clear();
                foreach (var x in report.Members)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvMembershipReport,
                        x.CustomerCode,
                        x.CustomerName,
                        x.PlanName,
                        x.StartDate.ToString("yyyy-MM-dd"),
                        x.ExpiryDate.ToString("yyyy-MM-dd"),
                        x.DaysLeft >= 0 ? x.DaysLeft.ToString() : "—",
                        x.Status);

                    int rowIndex = dgvMembershipReport.Rows.Add(row);
                    var sc = dgvMembershipReport.Rows[rowIndex].Cells["Status"];
                    sc.Style.ForeColor = x.Status switch
                    {
                        "Active" => SuccessGreen,
                        "Expiring Soon" => Amber,
                        "Expired" => DangerRed,
                        _ => Accent,
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Membership report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // TAB 3 — Attendance
        // ==========================================================

        private async System.Threading.Tasks.Task GenerateAttendanceReportAsync()
        {
            try
            {
                var report = await _api.GetAttendanceReportAsync(dtpFrom.Value, dtpTo.Value);
                if (report is null) return;

                kpiAttToday.Value = report.TodayCount.ToString("N0");
                kpiAttWeek.Value = report.WeekCount.ToString("N0");
                kpiAttMonth.Value = report.MonthCount.ToString("N0");
                kpiAttUnique.Value = report.UniqueMembers.ToString("N0");

                chartAttendanceByDay.Series[0].Points.Clear();
                int i = 0;
                foreach (var d in report.ByDay)
                {
                    var idx = chartAttendanceByDay.Series[0].Points.AddXY(i++, d.Count);
                    var pt = chartAttendanceByDay.Series[0].Points[idx];
                    pt.AxisLabel = d.Date;
                    pt.ToolTip = $"{d.Date}: {d.Count} check-ins";
                }

                chartAttendanceByHour.Series[0].Points.Clear();
                int j = 0;
                foreach (var h in report.ByHour)
                {
                    var idx = chartAttendanceByHour.Series[0].Points.AddXY(j++, h.Count);
                    var pt = chartAttendanceByHour.Series[0].Points[idx];
                    pt.AxisLabel = $"{h.Hour:D2}:00";
                    pt.ToolTip = $"{h.Hour:D2}:00 — {h.Count} check-ins";
                }

                if (dgvAttendanceReport.Columns.Count == 0)
                {
                    dgvAttendanceReport.Columns.Add("CustomerName", "Customer");
                    dgvAttendanceReport.Columns.Add("CheckInTime", "Check-In");
                    dgvAttendanceReport.Columns.Add("CheckOutTime", "Check-Out");
                    dgvAttendanceReport.Columns.Add("Notes", "Notes");
                }

                dgvAttendanceReport.Rows.Clear();
                foreach (var a in report.Attendance)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvAttendanceReport,
                        a.CustomerName,
                        a.CheckInTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                        a.CheckOutTime.HasValue
                            ? a.CheckOutTime.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                            : "—",
                        a.Notes ?? "");
                    dgvAttendanceReport.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Attendance report failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // EXPORT + PRINT
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
                int y = 40, x = 40, rowHeight = 22, colWidth = 180;

                g.DrawString(doc.DocumentName, new Font("Segoe UI", 14F, FontStyle.Bold), Brushes.Black, x, y);
                y += 30;
                g.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}", font, Brushes.Gray, x, y);
                y += 30;

                for (int c = 0; c < grid.Columns.Count; c++)
                    g.DrawString(grid.Columns[c].HeaderText, headerFont, Brushes.Black, x + (c * colWidth), y);

                y += rowHeight;
                g.DrawLine(Pens.Black, x, y, x + (grid.Columns.Count * colWidth), y);
                y += 4;

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

            using var preview = new PrintPreviewDialog { Document = doc, Width = 1000, Height = 700 };
            preview.ShowDialog(this);
        }

        private DataGridView? GetCurrentTabGrid()
        {
            if (tabs.SelectedTab == tabRevenue) return dgvSalesReport;
            if (tabs.SelectedTab == tabMembership) return dgvMembershipReport;
            if (tabs.SelectedTab == tabAttendance) return dgvAttendanceReport;
            if (tabs.SelectedTab == tabLeadConversion) return dgvLeadReport;
            if (tabs.SelectedTab == tabRetention) return dgvRetentionReport;
            if (tabs.SelectedTab == tabPromoUsage) return dgvPromoReport;
            return null;
        }
    }
}