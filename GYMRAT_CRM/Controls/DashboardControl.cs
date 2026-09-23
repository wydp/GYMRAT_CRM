using CRM.domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRM.winforms.Controls
{
    public partial class DashboardControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();

        private Chart revenueChart = null!;
        private Chart membershipChart = null!;

        private static readonly CultureInfo PhCulture = CultureInfo.GetCultureInfo("en-PH");

        // --- Colors ---
        private static readonly Color TextPrimary = Color.FromArgb(31, 41, 55);
        private static readonly Color TextSecondary = Color.FromArgb(107, 114, 128);
        private static readonly Color SuccessGreen = Color.FromArgb(21, 128, 61);
        private static readonly Color DangerRed = Color.FromArgb(185, 28, 28);
        private static readonly Color PrimaryBlue = Color.FromArgb(72, 128, 255);
        private static readonly Color Amber = Color.FromArgb(217, 119, 6);
        private static readonly Color CardBorder = Color.FromArgb(229, 231, 235);
        private static readonly Color GridLine = Color.FromArgb(243, 244, 246);
        private static readonly Color CardBack = Color.White;

        public event Action<string>? NavigateRequested;

        private Label totalCustomersValue = null!;
        private Label totalCustomersDelta = null!;
        private Label activeMembershipsValue = null!;
        private Label activeMembershipsDelta = null!;
        private Label totalRevenueValue = null!;
        private Label totalRevenueDelta = null!;
        private Label expiringSoonValue = null!;
        private Label expiringSoonDelta = null!;

        private Panel cardsContainer = null!;
        private Panel revenueCard = null!;
        private Panel membershipCard = null!;

        public DashboardControl()
        {
            InitializeComponent();

            BuildKpiCards();
            BuildCharts();

            cardsPanel.Resize += (s, e) => LayoutCards();
            chartsHost.Resize += (s, e) => LayoutChartCards();

            _ = LoadDataAsync();
        }

        // ==================================================================
        // KPI CARDS
        // ==================================================================

        private void BuildKpiCards()
        {
            var cards = new (string title, Color accent, string target)[]
            {
                ("Total Customers",     PrimaryBlue,  "Customers"),
                ("Active Memberships",  SuccessGreen, "Sales Force Automation"),
                ("Total Revenue",       Amber,        "Reports"),
                ("Expiring Soon",       DangerRed,    "Sales Force Automation"),
            };

            cardsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
            };
            cardsPanel.Controls.Add(cardsContainer);

            for (int i = 0; i < cards.Length; i++)
            {
                var (title, accent, target) = cards[i];

                var outer = BuildBorderedCard(accent, target);
                if (outer.Controls["innerCard"] is not Panel inner)
                    throw new InvalidOperationException("innerCard panel missing.");

                var (titleLbl, valueLbl, deltaLbl) = BuildCardContent(title, inner);
                inner.Controls.Add(deltaLbl);
                inner.Controls.Add(valueLbl);
                inner.Controls.Add(titleLbl);

                cardsContainer.Controls.Add(outer);

                switch (i)
                {
                    case 0: totalCustomersValue = valueLbl; totalCustomersDelta = deltaLbl; break;
                    case 1: activeMembershipsValue = valueLbl; activeMembershipsDelta = deltaLbl; break;
                    case 2: totalRevenueValue = valueLbl; totalRevenueDelta = deltaLbl; break;
                    case 3: expiringSoonValue = valueLbl; expiringSoonDelta = deltaLbl; break;
                }
            }
        }

        private Panel BuildBorderedCard(Color accent, string target)
        {
            // Outer panel — 1px border via background color + inner padding.
            var outer = new Panel
            {
                BackColor = CardBorder,
                Padding = new Padding(1),
                Tag = target,
                Cursor = Cursors.Hand,
            };

            var accentStrip = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = accent,
            };

            var inner = new Panel
            {
                Name = "innerCard",
                Dock = DockStyle.Fill,
                BackColor = CardBack,
            };

            outer.Controls.Add(inner);
            outer.Controls.Add(accentStrip);

            outer.Click += Card_Click;
            inner.Click += Card_Click;
            accentStrip.Click += Card_Click;

            return outer;
        }

        private (Label title, Label value, Label delta) BuildCardContent(string title, Panel innerCard)
        {
            const int leftPad = 20;
            const int rightPad = 16;

            var titleLbl = new Label
            {
                Name = "titleLbl",
                Text = title,
                Font = new Font("Segoe UI", 9F),
                ForeColor = TextSecondary,
                AutoSize = false,
                Height = 18,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var valueLbl = new Label
            {
                Name = "valueLbl",
                Text = "\u2014",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = TextPrimary,
                AutoSize = false,
                Height = 32,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
            };

            var deltaLbl = new Label
            {
                Name = "deltaLbl",
                Text = "",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = TextSecondary,
                AutoSize = false,
                Height = 16,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            void Reposition()
            {
                const int blockHeight = 18 + 2 + 32 + 2 + 16;
                int top = (innerCard.ClientSize.Height - blockHeight) / 2;
                if (top < 8) top = 8;

                titleLbl.Location = new Point(leftPad, top);
                valueLbl.Location = new Point(leftPad, top + 18 + 2);
                deltaLbl.Location = new Point(leftPad, top + 18 + 2 + 32 + 2);
            }

            void ResizeWidths()
            {
                int w = innerCard.ClientSize.Width - leftPad - rightPad;
                if (w < 40) w = 40;
                titleLbl.Width = w;
                valueLbl.Width = w;
                deltaLbl.Width = w;

                // Adaptive font: shrink from 18pt down to 12pt if the text is
                // wider than the available width. Prevents clipping on long values.
                if (string.IsNullOrEmpty(valueLbl.Text)) return;

                float size = 18F;
                while (size > 12F)
                {
                    var testFont = new Font("Segoe UI", size, FontStyle.Bold);
                    var measured = TextRenderer.MeasureText(valueLbl.Text, testFont);
                    testFont.Dispose();
                    if (measured.Width <= w) break;
                    size -= 0.5F;
                }
                if (Math.Abs(valueLbl.Font.Size - size) > 0.1F)
                    valueLbl.Font = new Font("Segoe UI", size, FontStyle.Bold);
            }


            innerCard.Resize += (s, e) => { Reposition(); ResizeWidths(); };
            innerCard.Controls.Add(titleLbl);

            Reposition();
            ResizeWidths();

            return (titleLbl, valueLbl, deltaLbl);
        }

        private void Card_Click(object? sender, EventArgs e)
        {
            var ctrl = sender as Control;
            while (ctrl != null && ctrl.Tag is not string)
                ctrl = ctrl.Parent;

            if (ctrl?.Tag is string target && !string.IsNullOrEmpty(target))
                NavigateRequested?.Invoke(target);
        }

        private void LayoutCards()
        {
            if (cardsContainer == null || cardsContainer.Controls.Count == 0) return;

            const int gap = 16;
            int count = cardsContainer.Controls.Count;
            int totalGap = gap * (count - 1);
            int availableWidth = cardsContainer.ClientSize.Width - totalGap;
            if (availableWidth <= 0) return;

            int cardWidth = availableWidth / count;
            int cardHeight = cardsContainer.ClientSize.Height;

            for (int i = 0; i < count; i++)
            {
                var card = cardsContainer.Controls[i];
                card.Size = new Size(cardWidth, cardHeight);
                card.Location = new Point(i * (cardWidth + gap), 0);
            }
        }

        // ==================================================================
        // CHARTS — two cards side by side, both with the same border style
        // as the KPI cards (via nested-panel trick).
        // ==================================================================

        private void BuildCharts()
        {
            revenueCard = BuildChartCard("Revenue \u2014 Last 6 Months", out revenueChart, out var revenueInner);
            membershipCard = BuildChartCard("Membership Status", out membershipChart, out var membershipInner);

            ConfigureRevenueChart();
            ConfigureMembershipChart();

            chartsHost.Controls.Add(membershipCard);
            chartsHost.Controls.Add(revenueCard);
        }

        // Creates one bordered chart card: outer border + inner white panel
        // with a title docked top and a Chart filling the rest.
        private Panel BuildChartCard(string title, out Chart chart, out Panel innerPanel)
        {
            var outer = new Panel
            {
                BackColor = CardBorder,
                Padding = new Padding(1),
            };

            innerPanel = new Panel
            {
                Name = "chartInner",
                Dock = DockStyle.Fill,
                BackColor = CardBack,
                Padding = new Padding(18, 12, 18, 18),
            };

            var titleLbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = TextPrimary,
                Dock = DockStyle.Top,
                Height = 28,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            chart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = CardBack,
            };

            var area = new ChartArea("Main");
            area.BackColor = CardBack;
            area.AxisX.MajorGrid.LineColor = GridLine;
            area.AxisY.MajorGrid.LineColor = GridLine;
            area.AxisX.LineColor = CardBorder;
            area.AxisY.LineColor = CardBorder;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LabelStyle.ForeColor = TextSecondary;
            area.AxisY.LabelStyle.ForeColor = TextSecondary;
            chart.ChartAreas.Add(area);

            innerPanel.Controls.Add(chart);     // fill
            innerPanel.Controls.Add(titleLbl);  // top

            outer.Controls.Add(innerPanel);
            return outer;
        }

        private void LayoutChartCards()
        {
            if (revenueCard == null || membershipCard == null) return;

            int hostWidth = chartsHost.ClientSize.Width
                            - chartsHost.Padding.Left
                            - chartsHost.Padding.Right;
            int hostHeight = chartsHost.ClientSize.Height
                            - chartsHost.Padding.Top
                            - chartsHost.Padding.Bottom;
            if (hostWidth <= 0 || hostHeight <= 0) return;

            const int gap = 16;
            // Revenue chart takes 60%, donut takes the rest.
            int leftWidth = (int)((hostWidth - gap) * 0.60);
            int rightWidth = hostWidth - gap - leftWidth;

            int top = chartsHost.Padding.Top;
            int left = chartsHost.Padding.Left;

            revenueCard.Location = new Point(left, top);
            revenueCard.Size = new Size(leftWidth, hostHeight);

            membershipCard.Location = new Point(left + leftWidth + gap, top);
            membershipCard.Size = new Size(rightWidth, hostHeight);
        }

        private void ConfigureRevenueChart()
        {
            // Done in BuildChart — series rebuilt per data load.
        }

        private void ConfigureMembershipChart()
        {
            // Done in BuildMembershipChart — series rebuilt per data load.
        }

        // ==================================================================
        // DATA
        // ==================================================================

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                var sales = await _api.GetMembershipSalesAsync();

                var latestSalePerCustomer = sales
                    .Where(s => s.MembershipPlan != null)
                    .GroupBy(s => s.CustomerId)
                    .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.SaleDate).First());

                var now = DateTime.UtcNow;
                var monthStart = new DateTime(now.Year, now.Month, 1);
                var today = now.Date;

                // --- KPI 1: Total Customers ---
                int activeCustomers = customers.Count(c => c.IsActive);
                int newThisMonth = customers.Count(c => c.CreatedAt >= monthStart);
                totalCustomersValue.Text = activeCustomers.ToString("N0");
                SetDelta(totalCustomersDelta, $"+{newThisMonth} this month", newThisMonth > 0);

                // --- KPI 2: Active Memberships ---
                int activeMemberships = 0;
                int expiringSoon = 0;
                int expired = 0;

                foreach (var c in customers.Where(c => c.IsActive))
                {
                    if (!latestSalePerCustomer.TryGetValue(c.CustomerId, out var sale)) continue;
                    if (sale.MembershipPlan == null) continue;
                    var expiry = sale.SaleDate.Date.AddDays(sale.MembershipPlan.DurationInDays);
                    var daysLeft = (expiry - today).Days;

                    if (daysLeft < 0) expired++;
                    else if (daysLeft <= 30) { expiringSoon++; activeMemberships++; }
                    else activeMemberships++;
                }
                activeMembershipsValue.Text = activeMemberships.ToString("N0");

                int newMembershipsThisMonth = sales
                    .GroupBy(s => s.CustomerId)
                    .Count(g => g.OrderBy(s => s.SaleDate).First().SaleDate >= monthStart);
                SetDelta(activeMembershipsDelta, $"+{newMembershipsThisMonth} this month", newMembershipsThisMonth > 0);

                // --- KPI 3: Total Revenue ---
                decimal totalRevenue = sales.Sum(s => s.AmountPaid);
                totalRevenueValue.Text = "\u20B1 " + totalRevenue.ToString("N0", PhCulture);

                decimal thisMonthRevenue = sales
                    .Where(s => s.SaleDate >= monthStart)
                    .Sum(s => s.AmountPaid);
                SetDelta(totalRevenueDelta, $"+\u20B1 {thisMonthRevenue.ToString("N0", PhCulture)} this month", thisMonthRevenue > 0);

                // --- KPI 4: Expiring Soon ---
                expiringSoonValue.Text = expiringSoon.ToString("N0");
                expiringSoonValue.ForeColor = expiringSoon > 0 ? DangerRed : TextPrimary;

                if (expiringSoon > 0)
                {
                    expiringSoonDelta.Text = "\u26A0 Needs attention";
                    expiringSoonDelta.ForeColor = DangerRed;
                }
                else
                {
                    expiringSoonDelta.Text = "\u2713 All clear";
                    expiringSoonDelta.ForeColor = SuccessGreen;
                }

                BuildRevenueChart(sales);
                BuildMembershipChart(activeMemberships, expiringSoon, expired);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SetDelta(Label lbl, string text, bool positive)
        {
            lbl.Text = text;
            lbl.ForeColor = positive ? SuccessGreen : TextSecondary;
        }

        // ==================================================================
        // REVENUE CHART
        // ==================================================================

        private void BuildRevenueChart(List<MembershipSale> sales)
        {
            var chartArea = revenueChart.ChartAreas["Main"];
            revenueChart.Series.Clear();
            chartArea.AxisX.CustomLabels.Clear();

            var series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = PrimaryBlue,
                BorderWidth = 0,
                IsXValueIndexed = true,
                ToolTip = "₱#VALY{N0}",
            };

            var now = DateTime.UtcNow;
            var months = new List<(string label, decimal revenue)>();
            for (int i = 5; i >= 0; i--)
            {
                var month = new DateTime(now.Year, now.Month, 1).AddMonths(-i);
                var monthRevenue = sales
                    .Where(s => s.SaleDate.Year == month.Year && s.SaleDate.Month == month.Month)
                    .Sum(s => s.AmountPaid);
                months.Add((month.ToString("MMM"), monthRevenue));
            }

            for (int i = 0; i < months.Count; i++)
                series.Points.AddY((double)months[i].revenue);

            chartArea.AxisY.LabelStyle.Format = "\u20B1#,##0";
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Interval = 1;
            chartArea.AxisX.Minimum = 0.5;
            chartArea.AxisX.Maximum = months.Count + 0.5;
            for (int i = 0; i < months.Count; i++)
                chartArea.AxisX.CustomLabels.Add(i + 0.5, i + 1.5, months[i].label);

            revenueChart.Series.Add(series);
        }

        // ==================================================================
        // MEMBERSHIP STATUS DONUT
        // ==================================================================

        private void BuildMembershipChart(int active, int expiring, int expired)
        {
            var chartArea = membershipChart.ChartAreas["Main"];
            membershipChart.Series.Clear();
            membershipChart.Legends.Clear();

            var legend = new Legend
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8.5F),
                BackColor = Color.Transparent,
                ForeColor = TextSecondary,
            };
            membershipChart.Legends.Add(legend);

            var series = new Series
            {
                ChartType = SeriesChartType.Doughnut,
                Font = new Font("Segoe UI", 8.5F),
                IsValueShownAsLabel = false,
            };

            series["DoughnutRadius"] = "55";
            series["PieStartAngle"] = "270";

            if (active > 0) series.Points.Add(new DataPoint { YValues = new[] { (double)active }, LegendText = $"Active ({active})", Color = SuccessGreen });
            if (expiring > 0) series.Points.Add(new DataPoint { YValues = new[] { (double)expiring }, LegendText = $"Expiring ({expiring})", Color = Amber });
            if (expired > 0) series.Points.Add(new DataPoint { YValues = new[] { (double)expired }, LegendText = $"Expired ({expired})", Color = DangerRed });

            if (series.Points.Count == 0)
            {
                series.Points.Add(new DataPoint { YValues = new[] { 1.0 }, LegendText = "No data", Color = CardBorder });
            }

            chartArea.AxisX.Enabled = AxisEnabled.False;
            chartArea.AxisY.Enabled = AxisEnabled.False;

            membershipChart.Series.Add(series);
        }
    }
}