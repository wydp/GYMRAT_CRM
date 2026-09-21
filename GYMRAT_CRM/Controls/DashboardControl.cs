using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRM.winforms.Controls
{
    public partial class DashboardControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private readonly Chart revenueChart;

        // Explicit Philippine Peso formatting — "C0"/"C2" alone follow whatever
        // currency the OS's regional settings default to, which isn't reliably ₱.
        private static readonly CultureInfo PhCulture = CultureInfo.GetCultureInfo("en-PH");

        // Card value labels, kept as fields so LoadDataAsync can update them
        // after the cards are built once in the constructor.
        private Label totalCustomersValue;
        private Label totalSalesValue;
        private Label totalRevenueValue;
        private Label thisMonthValue;

        public DashboardControl()
        {
            InitializeComponent();

            BuildKpiCards();

            // The Chart control comes from the WinForms.DataVisualization package —
            // built in code here rather than the Designer, since it doesn't need
            // any properties set at design time.
            revenueChart = new Chart { Dock = DockStyle.Fill };
            var chartArea = new ChartArea("Main");
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(229, 231, 235);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(229, 231, 235);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            revenueChart.ChartAreas.Add(chartArea);
            chartCard.Controls.Add(revenueChart);
            revenueChart.BringToFront(); // sits below chartTitleLabel (Dock=Top), fills the rest

            _ = LoadDataAsync();
        }

        // Creates the 4 KPI cards side by side inside cardsPanel. Built in code
        // rather than the Designer since all 4 share the same shape — only the
        // label text, value, and accent color differ.
        private void BuildKpiCards()
        {
            var cards = new (string title, Color accent)[]
            {
                ("Total Customers", Color.FromArgb(72, 128, 255)),   // Primary
                ("Total Sales", Color.FromArgb(34, 197, 94)),        // Success
                ("Total Revenue", Color.FromArgb(245, 158, 11)),     // Warning
                ("This Month's Revenue", Color.FromArgb(156, 163, 175)), // Neutral
            };

            int cardWidth = 210;
            int gap = 16;
            int x = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                var (title, accent) = cards[i];

                var card = new Panel
                {
                    Location = new Point(x, 0),
                    Size = new Size(cardWidth, 110),
                    BackColor = Color.White,
                };

                var accentStrip = new Panel
                {
                    Dock = DockStyle.Left,
                    Width = 4,
                    BackColor = accent,
                };

                var titleLbl = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(107, 114, 128), // Text Secondary
                    AutoSize = false,
                    Location = new Point(16, 16),
                    Size = new Size(cardWidth - 32, 20),
                };

                var valueLbl = new Label
                {
                    Text = "—",
                    Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(31, 41, 55), // Text Primary
                    AutoSize = false,
                    Location = new Point(16, 44),
                    Size = new Size(cardWidth - 32, 40),
                };

                card.Controls.Add(valueLbl);
                card.Controls.Add(titleLbl);
                card.Controls.Add(accentStrip);
                cardsPanel.Controls.Add(card);

                // Stash references to the value labels so LoadDataAsync can update them.
                switch (i)
                {
                    case 0: totalCustomersValue = valueLbl; break;
                    case 1: totalSalesValue = valueLbl; break;
                    case 2: totalRevenueValue = valueLbl; break;
                    case 3: thisMonthValue = valueLbl; break;
                }

                x += cardWidth + gap;
            }
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                var sales = await _api.GetMembershipSalesAsync();

                totalCustomersValue.Text = customers.Count(c => c.IsActive).ToString("N0");
                totalSalesValue.Text = sales.Count.ToString("N0");
                totalRevenueValue.Text = sales.Sum(s => s.AmountPaid).ToString("C0", PhCulture);

                var now = DateTime.UtcNow;
                var thisMonthRevenue = sales
                    .Where(s => s.SaleDate.Year == now.Year && s.SaleDate.Month == now.Month)
                    .Sum(s => s.AmountPaid);
                thisMonthValue.Text = thisMonthRevenue.ToString("C0", PhCulture);

                BuildChart(sales);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Groups sales into the last 6 calendar months (oldest to newest) and
        // plots total revenue per month as a column chart.
        //
        // NOTE: deliberately NOT using Points.AddXY("Apr", value) — plotting bars
        // by string category directly is a known source of category/spacing bugs
        // in this charting library. Plotting by plain index (AddY) and attaching
        // month names via CustomLabels is the reliable pattern.
        private void BuildChart(System.Collections.Generic.List<MembershipSale> sales)
        {
            var chartArea = revenueChart.ChartAreas["Main"];
            revenueChart.Series.Clear();
            chartArea.AxisX.CustomLabels.Clear();

            var series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(72, 128, 255), // Primary
                BorderWidth = 0,
                IsXValueIndexed = true,
            };

            var now = DateTime.UtcNow;
            var months = new System.Collections.Generic.List<(string label, decimal revenue)>();
            for (int i = 5; i >= 0; i--)
            {
                var month = new DateTime(now.Year, now.Month, 1).AddMonths(-i);
                var monthRevenue = sales
                    .Where(s => s.SaleDate.Year == month.Year && s.SaleDate.Month == month.Month)
                    .Sum(s => s.AmountPaid);
                months.Add((month.ToString("MMM"), monthRevenue));
            }

            for (int i = 0; i < months.Count; i++)
            {
                series.Points.AddY((double)months[i].revenue);
            }

            chartArea.AxisY.LabelStyle.Format = "\u20B1#,##0"; // ₱ prefix on Y-axis labels
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Interval = 1;
            chartArea.AxisX.Minimum = 0.5;
            chartArea.AxisX.Maximum = months.Count + 0.5;
            for (int i = 0; i < months.Count; i++)
            {
                chartArea.AxisX.CustomLabels.Add(i + 0.5, i + 1.5, months[i].label);
            }

            revenueChart.Series.Add(series);
        }
    }
}