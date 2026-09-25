namespace CRM.winforms.Controls
{
    partial class ReportsControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.presetsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPresetToday = new System.Windows.Forms.Button();
            this.btnPresetThisWeek = new System.Windows.Forms.Button();
            this.btnPresetThisMonth = new System.Windows.Forms.Button();
            this.btnPresetLast3 = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.kpiRevenueTotal = new CRM.winforms.Controls.KpiCard();
            this.kpiRevenueCount = new CRM.winforms.Controls.KpiCard();
            this.kpiRevenueAvg = new CRM.winforms.Controls.KpiCard();
            this.kpiRevenueCancelled = new CRM.winforms.Controls.KpiCard();
            this.chartRevenueByMonth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRevenueByPlan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.tabMembership = new System.Windows.Forms.TabPage();
            this.kpiMemberActive = new CRM.winforms.Controls.KpiCard();
            this.kpiMemberExpiring = new CRM.winforms.Controls.KpiCard();
            this.kpiMemberExpired = new CRM.winforms.Controls.KpiCard();
            this.kpiMemberFrozen = new CRM.winforms.Controls.KpiCard();
            this.chartMembershipStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMembershipByPlan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvMembershipReport = new System.Windows.Forms.DataGridView();
            this.tabAttendance = new System.Windows.Forms.TabPage();
            this.kpiAttToday = new CRM.winforms.Controls.KpiCard();
            this.kpiAttWeek = new CRM.winforms.Controls.KpiCard();
            this.kpiAttMonth = new CRM.winforms.Controls.KpiCard();
            this.kpiAttUnique = new CRM.winforms.Controls.KpiCard();
            this.chartAttendanceByDay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAttendanceByHour = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvAttendanceReport = new System.Windows.Forms.DataGridView();
            // Lead Conversion controls
            this.kpiLeadTotal = new CRM.winforms.Controls.KpiCard();
            this.kpiLeadConverted = new CRM.winforms.Controls.KpiCard();
            this.kpiLeadLost = new CRM.winforms.Controls.KpiCard();
            this.kpiLeadRate = new CRM.winforms.Controls.KpiCard();
            this.chartLeadBySource = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartLeadByStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvLeadReport = new System.Windows.Forms.DataGridView();

            // Retention controls
            this.kpiRetCancellations = new CRM.winforms.Controls.KpiCard();
            this.kpiRetFreezes = new CRM.winforms.Controls.KpiCard();
            this.kpiRetWinBacks = new CRM.winforms.Controls.KpiCard();
            this.kpiRetRenewals = new CRM.winforms.Controls.KpiCard();
            this.chartRetentionByDay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRetentionByAction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvRetentionReport = new System.Windows.Forms.DataGridView();

            // Promo Usage controls
            this.kpiPromoTotal = new CRM.winforms.Controls.KpiCard();
            this.kpiPromoActive = new CRM.winforms.Controls.KpiCard();
            this.kpiPromoRedemptions = new CRM.winforms.Controls.KpiCard();
            this.kpiPromoAvgRate = new CRM.winforms.Controls.KpiCard();
            this.chartPromoByCode = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPromoByRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvPromoReport = new System.Windows.Forms.DataGridView();
            this.tabLeadConversion = new System.Windows.Forms.TabPage();
            this.tabRetention = new System.Windows.Forms.TabPage();
            this.tabPromoUsage = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.tabRevenue.SuspendLayout();
            this.tabMembership.SuspendLayout();
            this.tabAttendance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMembershipStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMembershipByPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembershipReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendanceByDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendanceByHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendanceReport)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(80, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reports";
            //
            // presetsPanel
            //
            this.presetsPanel.Location = new System.Drawing.Point(20, 50);
            this.presetsPanel.Size = new System.Drawing.Size(600, 30);
            this.presetsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.presetsPanel.WrapContents = false;
            this.presetsPanel.AutoSize = false;
            this.presetsPanel.Controls.Add(this.btnPresetToday);
            this.presetsPanel.Controls.Add(this.btnPresetThisWeek);
            this.presetsPanel.Controls.Add(this.btnPresetThisMonth);
            this.presetsPanel.Controls.Add(this.btnPresetLast3);
            //
            // preset buttons
            //
            this.btnPresetToday.Text = "Today";
            this.btnPresetToday.Size = new System.Drawing.Size(80, 26);
            this.btnPresetToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetToday.BackColor = System.Drawing.Color.White;
            this.btnPresetToday.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnPresetToday.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnPresetToday.Name = "btnPresetToday";

            this.btnPresetThisWeek.Text = "This Week";
            this.btnPresetThisWeek.Size = new System.Drawing.Size(90, 26);
            this.btnPresetThisWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetThisWeek.BackColor = System.Drawing.Color.White;
            this.btnPresetThisWeek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnPresetThisWeek.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnPresetThisWeek.Name = "btnPresetThisWeek";

            this.btnPresetThisMonth.Text = "This Month";
            this.btnPresetThisMonth.Size = new System.Drawing.Size(95, 26);
            this.btnPresetThisMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetThisMonth.BackColor = System.Drawing.Color.White;
            this.btnPresetThisMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnPresetThisMonth.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnPresetThisMonth.Name = "btnPresetThisMonth";

            this.btnPresetLast3.Text = "Last 3 Months";
            this.btnPresetLast3.Size = new System.Drawing.Size(110, 26);
            this.btnPresetLast3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresetLast3.BackColor = System.Drawing.Color.White;
            this.btnPresetLast3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnPresetLast3.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnPresetLast3.Name = "btnPresetLast3";
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 92);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(35, 15);
            this.lblFrom.Text = "From";
            //
            // dtpFrom
            //
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(20, 110);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(140, 23);
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(180, 92);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 15);
            this.lblTo.Text = "To";
            //
            // dtpTo
            //
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(180, 110);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(140, 23);
            //
            // btnGenerate
            //
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(72, 128, 255);
            this.btnGenerate.FlatAppearance.BorderSize = 0;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(340, 108);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(110, 28);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            //
            // btnExport
            //
            this.btnExport.Location = new System.Drawing.Point(460, 108);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 28);
            this.btnExport.Text = "Export CSV";
            //
            // btnPrint
            //
            this.btnPrint.Location = new System.Drawing.Point(580, 108);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 28);
            this.btnPrint.Text = "Print";
            //
            // tabs
            //
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabRevenue);
            this.tabs.Controls.Add(this.tabMembership);
            this.tabs.Controls.Add(this.tabAttendance);
            this.tabs.Controls.Add(this.tabLeadConversion);
            this.tabs.Controls.Add(this.tabRetention);
            this.tabs.Controls.Add(this.tabPromoUsage);
            this.tabs.Location = new System.Drawing.Point(20, 145);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(860, 545);
            this.tabs.TabIndex = 8;
            //
            // tabRevenue
            //
            this.tabRevenue.AutoScroll = true;
            this.tabRevenue.Controls.Add(this.dgvSalesReport);
            this.tabRevenue.Controls.Add(this.chartRevenueByPlan);
            this.tabRevenue.Controls.Add(this.chartRevenueByMonth);
            this.tabRevenue.Controls.Add(this.kpiRevenueCancelled);
            this.tabRevenue.Controls.Add(this.kpiRevenueAvg);
            this.tabRevenue.Controls.Add(this.kpiRevenueCount);
            this.tabRevenue.Controls.Add(this.kpiRevenueTotal);
            this.tabRevenue.Location = new System.Drawing.Point(4, 24);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(852, 517);
            this.tabRevenue.TabIndex = 0;
            this.tabRevenue.Text = "Revenue";
            this.tabRevenue.UseVisualStyleBackColor = true;
            //
            // KPI cards — Revenue
            //
            this.kpiRevenueTotal.Location = new System.Drawing.Point(15, 12);
            this.kpiRevenueTotal.Size = new System.Drawing.Size(195, 90);
            this.kpiRevenueTotal.Title = "Total Revenue";
            this.kpiRevenueTotal.Value = "₱0.00";
            this.kpiRevenueTotal.Subtitle = "excludes cancellations";

            this.kpiRevenueCount.Location = new System.Drawing.Point(220, 12);
            this.kpiRevenueCount.Size = new System.Drawing.Size(195, 90);
            this.kpiRevenueCount.Title = "Sale Count";
            this.kpiRevenueCount.Value = "0";
            this.kpiRevenueCount.Subtitle = "in the date range";

            this.kpiRevenueAvg.Location = new System.Drawing.Point(425, 12);
            this.kpiRevenueAvg.Size = new System.Drawing.Size(195, 90);
            this.kpiRevenueAvg.Title = "Average Sale";
            this.kpiRevenueAvg.Value = "₱0.00";
            this.kpiRevenueAvg.Subtitle = "revenue / sale";

            this.kpiRevenueCancelled.Location = new System.Drawing.Point(630, 12);
            this.kpiRevenueCancelled.Size = new System.Drawing.Size(195, 90);
            this.kpiRevenueCancelled.Title = "Cancellations";
            this.kpiRevenueCancelled.Value = "0";
            this.kpiRevenueCancelled.Subtitle = "in the date range";
            this.kpiRevenueCancelled.ValueColor = System.Drawing.Color.FromArgb(185, 28, 28);
            //
            // chartRevenueByMonth
            //
            chartArea1.Name = "ChartArea1";
            this.chartRevenueByMonth.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Enabled = false;
            this.chartRevenueByMonth.Legends.Add(legend1);
            this.chartRevenueByMonth.Location = new System.Drawing.Point(15, 115);
            this.chartRevenueByMonth.Name = "chartRevenueByMonth";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Revenue";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.BorderWidth = 3;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.MarkerSize = 8;
            this.chartRevenueByMonth.Series.Add(series1);
            this.chartRevenueByMonth.Size = new System.Drawing.Size(400, 180);
            this.chartRevenueByMonth.TabIndex = 1;
            //
            // chartRevenueByPlan
            //
            chartArea2.Name = "ChartArea1";
            this.chartRevenueByPlan.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend2";
            this.chartRevenueByPlan.Legends.Add(legend2);
            this.chartRevenueByPlan.Location = new System.Drawing.Point(425, 115);
            this.chartRevenueByPlan.Name = "chartRevenueByPlan";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend2";
            series2.Name = "Revenue";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartRevenueByPlan.Series.Add(series2);
            this.chartRevenueByPlan.Size = new System.Drawing.Size(400, 180);
            this.chartRevenueByPlan.TabIndex = 2;
            //
            // dgvSalesReport
            //
            this.dgvSalesReport.AllowUserToAddRows = false;
            this.dgvSalesReport.AllowUserToDeleteRows = false;
            this.dgvSalesReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(15, 305);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.ReadOnly = true;
            this.dgvSalesReport.RowHeadersWidth = 51;
            this.dgvSalesReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvSalesReport.Size = new System.Drawing.Size(810, 400);
            this.dgvSalesReport.TabIndex = 3;
            //
            // tabMembership
            //
            this.tabMembership.AutoScroll = true;
            this.tabMembership.Controls.Add(this.dgvMembershipReport);
            this.tabMembership.Controls.Add(this.chartMembershipByPlan);
            this.tabMembership.Controls.Add(this.chartMembershipStatus);
            this.tabMembership.Controls.Add(this.kpiMemberFrozen);
            this.tabMembership.Controls.Add(this.kpiMemberExpired);
            this.tabMembership.Controls.Add(this.kpiMemberExpiring);
            this.tabMembership.Controls.Add(this.kpiMemberActive);
            this.tabMembership.Location = new System.Drawing.Point(4, 24);
            this.tabMembership.Name = "tabMembership";
            this.tabMembership.Padding = new System.Windows.Forms.Padding(3);
            this.tabMembership.Size = new System.Drawing.Size(852, 517);
            this.tabMembership.TabIndex = 1;
            this.tabMembership.Text = "Membership";
            this.tabMembership.UseVisualStyleBackColor = true;
            //
            // KPI cards — Membership
            //
            this.kpiMemberActive.Location = new System.Drawing.Point(15, 12);
            this.kpiMemberActive.Size = new System.Drawing.Size(195, 90);
            this.kpiMemberActive.Title = "Active Members";
            this.kpiMemberActive.Value = "0";
            this.kpiMemberActive.Subtitle = "more than 30 days left";
            this.kpiMemberActive.ValueColor = System.Drawing.Color.FromArgb(21, 128, 61);

            this.kpiMemberExpiring.Location = new System.Drawing.Point(220, 12);
            this.kpiMemberExpiring.Size = new System.Drawing.Size(195, 90);
            this.kpiMemberExpiring.Title = "Expiring Soon";
            this.kpiMemberExpiring.Value = "0";
            this.kpiMemberExpiring.Subtitle = "within 30 days";
            this.kpiMemberExpiring.ValueColor = System.Drawing.Color.FromArgb(180, 83, 9);

            this.kpiMemberExpired.Location = new System.Drawing.Point(425, 12);
            this.kpiMemberExpired.Size = new System.Drawing.Size(195, 90);
            this.kpiMemberExpired.Title = "Expired";
            this.kpiMemberExpired.Value = "0";
            this.kpiMemberExpired.Subtitle = "needs renewal";
            this.kpiMemberExpired.ValueColor = System.Drawing.Color.FromArgb(185, 28, 28);

            this.kpiMemberFrozen.Location = new System.Drawing.Point(630, 12);
            this.kpiMemberFrozen.Size = new System.Drawing.Size(195, 90);
            this.kpiMemberFrozen.Title = "Frozen";
            this.kpiMemberFrozen.Value = "0";
            this.kpiMemberFrozen.Subtitle = "on hold";
            this.kpiMemberFrozen.ValueColor = System.Drawing.Color.FromArgb(72, 128, 255);
            //
            // chartMembershipStatus
            //
            chartArea3.Name = "ChartArea1";
            this.chartMembershipStatus.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend3";
            this.chartMembershipStatus.Legends.Add(legend3);
            this.chartMembershipStatus.Location = new System.Drawing.Point(15, 115);
            this.chartMembershipStatus.Name = "chartMembershipStatus";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend3";
            series3.Name = "Status";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            this.chartMembershipStatus.Series.Add(series3);
            this.chartMembershipStatus.Size = new System.Drawing.Size(400, 180);
            this.chartMembershipStatus.TabIndex = 4;
            //
            // chartMembershipByPlan
            //
            chartArea4.Name = "ChartArea1";
            this.chartMembershipByPlan.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend4";
            this.chartMembershipByPlan.Legends.Add(legend4);
            this.chartMembershipByPlan.Location = new System.Drawing.Point(425, 115);
            this.chartMembershipByPlan.Name = "chartMembershipByPlan";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend4";
            series4.Name = "Members";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartMembershipByPlan.Series.Add(series4);
            this.chartMembershipByPlan.Size = new System.Drawing.Size(400, 180);
            this.chartMembershipByPlan.TabIndex = 5;
            //
            // dgvMembershipReport
            //
            this.dgvMembershipReport.AllowUserToAddRows = false;
            this.dgvMembershipReport.AllowUserToDeleteRows = false;
            this.dgvMembershipReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMembershipReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembershipReport.Location = new System.Drawing.Point(15, 305);
            this.dgvMembershipReport.Name = "dgvMembershipReport";
            this.dgvMembershipReport.ReadOnly = true;
            this.dgvMembershipReport.RowHeadersWidth = 51;
            this.dgvMembershipReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvMembershipReport.Size = new System.Drawing.Size(810, 400);
            this.dgvMembershipReport.TabIndex = 6;
            //
            // tabAttendance
            //
            this.tabAttendance.AutoScroll = true;
            this.tabAttendance.Controls.Add(this.dgvAttendanceReport);
            this.tabAttendance.Controls.Add(this.chartAttendanceByHour);
            this.tabAttendance.Controls.Add(this.chartAttendanceByDay);
            this.tabAttendance.Controls.Add(this.kpiAttUnique);
            this.tabAttendance.Controls.Add(this.kpiAttMonth);
            this.tabAttendance.Controls.Add(this.kpiAttWeek);
            this.tabAttendance.Controls.Add(this.kpiAttToday);
            this.tabAttendance.Location = new System.Drawing.Point(4, 24);
            this.tabAttendance.Name = "tabAttendance";
            this.tabAttendance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttendance.Size = new System.Drawing.Size(852, 517);
            this.tabAttendance.TabIndex = 2;
            this.tabAttendance.Text = "Attendance";
            this.tabAttendance.UseVisualStyleBackColor = true;
            //
            // KPI cards — Attendance
            //
            this.kpiAttToday.Location = new System.Drawing.Point(15, 12);
            this.kpiAttToday.Size = new System.Drawing.Size(195, 90);
            this.kpiAttToday.Title = "Check-Ins Today";
            this.kpiAttToday.Value = "0";
            this.kpiAttToday.Subtitle = "since midnight";

            this.kpiAttWeek.Location = new System.Drawing.Point(220, 12);
            this.kpiAttWeek.Size = new System.Drawing.Size(195, 90);
            this.kpiAttWeek.Title = "This Week";
            this.kpiAttWeek.Value = "0";
            this.kpiAttWeek.Subtitle = "Monday to today";

            this.kpiAttMonth.Location = new System.Drawing.Point(425, 12);
            this.kpiAttMonth.Size = new System.Drawing.Size(195, 90);
            this.kpiAttMonth.Title = "This Month";
            this.kpiAttMonth.Value = "0";
            this.kpiAttMonth.Subtitle = "1st of month to today";

            this.kpiAttUnique.Location = new System.Drawing.Point(630, 12);
            this.kpiAttUnique.Size = new System.Drawing.Size(195, 90);
            this.kpiAttUnique.Title = "Unique Members";
            this.kpiAttUnique.Value = "0";
            this.kpiAttUnique.Subtitle = "in the date range";
            this.kpiAttUnique.ValueColor = System.Drawing.Color.FromArgb(72, 128, 255);
            //
            // chartAttendanceByDay
            //
            chartArea5.Name = "ChartArea1";
            this.chartAttendanceByDay.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend5";
            legend5.Enabled = false;
            this.chartAttendanceByDay.Legends.Add(legend5);
            this.chartAttendanceByDay.Location = new System.Drawing.Point(15, 115);
            this.chartAttendanceByDay.Name = "chartAttendanceByDay";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend5";
            series5.Name = "CheckIns";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartAttendanceByDay.Series.Add(series5);
            this.chartAttendanceByDay.Size = new System.Drawing.Size(400, 180);
            this.chartAttendanceByDay.TabIndex = 7;
            //
            // chartAttendanceByHour
            //
            chartArea6.Name = "ChartArea1";
            this.chartAttendanceByHour.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend6";
            legend6.Enabled = false;
            this.chartAttendanceByHour.Legends.Add(legend6);
            this.chartAttendanceByHour.Location = new System.Drawing.Point(425, 115);
            this.chartAttendanceByHour.Name = "chartAttendanceByHour";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend6";
            series6.Name = "CheckIns";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartAttendanceByHour.Series.Add(series6);
            this.chartAttendanceByHour.Size = new System.Drawing.Size(400, 180);
            this.chartAttendanceByHour.TabIndex = 8;
            //
            // dgvAttendanceReport
            //
            this.dgvAttendanceReport.AllowUserToAddRows = false;
            this.dgvAttendanceReport.AllowUserToDeleteRows = false;
            this.dgvAttendanceReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAttendanceReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendanceReport.Location = new System.Drawing.Point(15, 305);
            this.dgvAttendanceReport.Name = "dgvAttendanceReport";
            this.dgvAttendanceReport.ReadOnly = true;
            this.dgvAttendanceReport.RowHeadersWidth = 51;
            this.dgvAttendanceReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvAttendanceReport.Size = new System.Drawing.Size(810, 400);
            this.dgvAttendanceReport.TabIndex = 9;
            //
            // tabLeadConversion
            //
            this.tabLeadConversion.Controls.Add(this.dgvLeadReport);
            this.tabLeadConversion.Controls.Add(this.chartLeadByStatus);
            this.tabLeadConversion.Controls.Add(this.chartLeadBySource);
            this.tabLeadConversion.Controls.Add(this.kpiLeadRate);
            this.tabLeadConversion.Controls.Add(this.kpiLeadLost);
            this.tabLeadConversion.Controls.Add(this.kpiLeadConverted);
            this.tabLeadConversion.Controls.Add(this.kpiLeadTotal);
            this.tabLeadConversion.Location = new System.Drawing.Point(4, 24);
            this.tabLeadConversion.Name = "tabLeadConversion";
            this.tabLeadConversion.Padding = new System.Windows.Forms.Padding(3);
            this.tabLeadConversion.Size = new System.Drawing.Size(852, 517);
            this.tabLeadConversion.TabIndex = 3;
            this.tabLeadConversion.Text = "Lead Conversion";
            this.tabLeadConversion.UseVisualStyleBackColor = true;

            // KPI cards — Lead Conversion
            this.kpiLeadTotal.Location = new System.Drawing.Point(15, 12);
            this.kpiLeadTotal.Size = new System.Drawing.Size(195, 90);
            this.kpiLeadTotal.Title = "Total Leads";
            this.kpiLeadTotal.Value = "0";
            this.kpiLeadTotal.Subtitle = "in date range";

            this.kpiLeadConverted.Location = new System.Drawing.Point(220, 12);
            this.kpiLeadConverted.Size = new System.Drawing.Size(195, 90);
            this.kpiLeadConverted.Title = "Converted";
            this.kpiLeadConverted.Value = "0";
            this.kpiLeadConverted.Subtitle = "converted leads";

            this.kpiLeadLost.Location = new System.Drawing.Point(425, 12);
            this.kpiLeadLost.Size = new System.Drawing.Size(195, 90);
            this.kpiLeadLost.Title = "Lost";
            this.kpiLeadLost.Value = "0";
            this.kpiLeadLost.Subtitle = "lost leads";

            this.kpiLeadRate.Location = new System.Drawing.Point(630, 12);
            this.kpiLeadRate.Size = new System.Drawing.Size(195, 90);
            this.kpiLeadRate.Title = "Conversion Rate";
            this.kpiLeadRate.Value = "0%";
            this.kpiLeadRate.Subtitle = "Converted / Total";

            // chartLeadBySource
            chartArea1.Name = "ChartArea1";
            this.chartLeadBySource.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Enabled = false;
            this.chartLeadBySource.Legends.Add(legend1);
            this.chartLeadBySource.Location = new System.Drawing.Point(15, 115);
            this.chartLeadBySource.Name = "chartLeadBySource";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Leads";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartLeadBySource.Series.Add(series1);
            this.chartLeadBySource.Size = new System.Drawing.Size(400, 180);
            this.chartLeadBySource.TabIndex = 10;

            // chartLeadByStatus
            chartArea2.Name = "ChartArea1";
            this.chartLeadByStatus.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend2";
            legend2.Enabled = false;
            this.chartLeadByStatus.Legends.Add(legend2);
            this.chartLeadByStatus.Location = new System.Drawing.Point(425, 115);
            this.chartLeadByStatus.Name = "chartLeadByStatus";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend2";
            series2.Name = "Status";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            this.chartLeadByStatus.Series.Add(series2);
            this.chartLeadByStatus.Size = new System.Drawing.Size(400, 180);
            this.chartLeadByStatus.TabIndex = 11;

            // dgvLeadReport
            this.dgvLeadReport.AllowUserToAddRows = false;
            this.dgvLeadReport.AllowUserToDeleteRows = false;
            this.dgvLeadReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLeadReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeadReport.Location = new System.Drawing.Point(15, 305);
            this.dgvLeadReport.Name = "dgvLeadReport";
            this.dgvLeadReport.ReadOnly = true;
            this.dgvLeadReport.RowHeadersWidth = 51;
            this.dgvLeadReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvLeadReport.Size = new System.Drawing.Size(810, 400);
            this.dgvLeadReport.TabIndex = 12;

            //
            // tabRetention
            //
            this.tabRetention.Controls.Add(this.dgvRetentionReport);
            this.tabRetention.Controls.Add(this.chartRetentionByAction);
            this.tabRetention.Controls.Add(this.chartRetentionByDay);
            this.tabRetention.Controls.Add(this.kpiRetRenewals);
            this.tabRetention.Controls.Add(this.kpiRetWinBacks);
            this.tabRetention.Controls.Add(this.kpiRetFreezes);
            this.tabRetention.Controls.Add(this.kpiRetCancellations);
            this.tabRetention.Location = new System.Drawing.Point(4, 24);
            this.tabRetention.Name = "tabRetention";
            this.tabRetention.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetention.Size = new System.Drawing.Size(852, 517);
            this.tabRetention.TabIndex = 4;
            this.tabRetention.Text = "Retention";
            this.tabRetention.UseVisualStyleBackColor = true;

            // KPI cards — Retention
            this.kpiRetCancellations.Location = new System.Drawing.Point(15, 12);
            this.kpiRetCancellations.Size = new System.Drawing.Size(195, 90);
            this.kpiRetCancellations.Title = "Cancellations";
            this.kpiRetCancellations.Value = "0";
            this.kpiRetCancellations.Subtitle = "in range";

            this.kpiRetFreezes.Location = new System.Drawing.Point(220, 12);
            this.kpiRetFreezes.Size = new System.Drawing.Size(195, 90);
            this.kpiRetFreezes.Title = "Freezes";
            this.kpiRetFreezes.Value = "0";
            this.kpiRetFreezes.Subtitle = "freeze actions";

            this.kpiRetWinBacks.Location = new System.Drawing.Point(425, 12);
            this.kpiRetWinBacks.Size = new System.Drawing.Size(195, 90);
            this.kpiRetWinBacks.Title = "Win-Backs";
            this.kpiRetWinBacks.Value = "0";
            this.kpiRetWinBacks.Subtitle = "attempts";

            this.kpiRetRenewals.Location = new System.Drawing.Point(630, 12);
            this.kpiRetRenewals.Size = new System.Drawing.Size(195, 90);
            this.kpiRetRenewals.Title = "Renewals";
            this.kpiRetRenewals.Value = "0";
            this.kpiRetRenewals.Subtitle = "successful renewals";

            // chartRetentionByDay
            chartArea3.Name = "ChartArea1";
            this.chartRetentionByDay.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend3";
            legend3.Enabled = false;
            this.chartRetentionByDay.Legends.Add(legend3);
            this.chartRetentionByDay.Location = new System.Drawing.Point(15, 115);
            this.chartRetentionByDay.Name = "chartRetentionByDay";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend3";
            series3.Name = "Actions";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chartRetentionByDay.Series.Add(series3);
            this.chartRetentionByDay.Size = new System.Drawing.Size(400, 180);
            this.chartRetentionByDay.TabIndex = 13;

            // chartRetentionByAction
            chartArea4.Name = "ChartArea1";
            this.chartRetentionByAction.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend4";
            legend4.Enabled = false;
            this.chartRetentionByAction.Legends.Add(legend4);
            this.chartRetentionByAction.Location = new System.Drawing.Point(425, 115);
            this.chartRetentionByAction.Name = "chartRetentionByAction";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend4";
            series4.Name = "ActionType";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            this.chartRetentionByAction.Series.Add(series4);
            this.chartRetentionByAction.Size = new System.Drawing.Size(400, 180);
            this.chartRetentionByAction.TabIndex = 14;

            // dgvRetentionReport
            this.dgvRetentionReport.AllowUserToAddRows = false;
            this.dgvRetentionReport.AllowUserToDeleteRows = false;
            this.dgvRetentionReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRetentionReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetentionReport.Location = new System.Drawing.Point(15, 305);
            this.dgvRetentionReport.Name = "dgvRetentionReport";
            this.dgvRetentionReport.ReadOnly = true;
            this.dgvRetentionReport.RowHeadersWidth = 51;
            this.dgvRetentionReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvRetentionReport.Size = new System.Drawing.Size(810, 400);
            this.dgvRetentionReport.TabIndex = 15;

            //
            // tabPromoUsage
            //
            this.tabPromoUsage.Controls.Add(this.dgvPromoReport);
            this.tabPromoUsage.Controls.Add(this.chartPromoByRate);
            this.tabPromoUsage.Controls.Add(this.chartPromoByCode);
            this.tabPromoUsage.Controls.Add(this.kpiPromoAvgRate);
            this.tabPromoUsage.Controls.Add(this.kpiPromoRedemptions);
            this.tabPromoUsage.Controls.Add(this.kpiPromoActive);
            this.tabPromoUsage.Controls.Add(this.kpiPromoTotal);
            this.tabPromoUsage.Location = new System.Drawing.Point(4, 24);
            this.tabPromoUsage.Name = "tabPromoUsage";
            this.tabPromoUsage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPromoUsage.Size = new System.Drawing.Size(852, 517);
            this.tabPromoUsage.TabIndex = 5;
            this.tabPromoUsage.Text = "Promo Usage";
            this.tabPromoUsage.UseVisualStyleBackColor = true;

            // KPI cards — Promo Usage
            this.kpiPromoTotal.Location = new System.Drawing.Point(15, 12);
            this.kpiPromoTotal.Size = new System.Drawing.Size(195, 90);
            this.kpiPromoTotal.Title = "Total Codes";
            this.kpiPromoTotal.Value = "0";
            this.kpiPromoTotal.Subtitle = "in range";

            this.kpiPromoActive.Location = new System.Drawing.Point(220, 12);
            this.kpiPromoActive.Size = new System.Drawing.Size(195, 90);
            this.kpiPromoActive.Title = "Active";
            this.kpiPromoActive.Value = "0";
            this.kpiPromoActive.Subtitle = "currently active";

            this.kpiPromoRedemptions.Location = new System.Drawing.Point(425, 12);
            this.kpiPromoRedemptions.Size = new System.Drawing.Size(195, 90);
            this.kpiPromoRedemptions.Title = "Redemptions";
            this.kpiPromoRedemptions.Value = "0";
            this.kpiPromoRedemptions.Subtitle = "total uses";

            this.kpiPromoAvgRate.Location = new System.Drawing.Point(630, 12);
            this.kpiPromoAvgRate.Size = new System.Drawing.Size(195, 90);
            this.kpiPromoAvgRate.Title = "Avg Rate";
            this.kpiPromoAvgRate.Value = "0%";
            this.kpiPromoAvgRate.Subtitle = "avg redemptions";

            // chartPromoByCode
            chartArea5.Name = "ChartArea1";
            this.chartPromoByCode.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend5";
            legend5.Enabled = false;
            this.chartPromoByCode.Legends.Add(legend5);
            this.chartPromoByCode.Location = new System.Drawing.Point(15, 115);
            this.chartPromoByCode.Name = "chartPromoByCode";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend5";
            series5.Name = "Redemptions";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chartPromoByCode.Series.Add(series5);
            this.chartPromoByCode.Size = new System.Drawing.Size(400, 180);
            this.chartPromoByCode.TabIndex = 16;

            // chartPromoByRate
            chartArea6.Name = "ChartArea1";
            this.chartPromoByRate.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend6";
            legend6.Enabled = false;
            this.chartPromoByRate.Legends.Add(legend6);
            this.chartPromoByRate.Location = new System.Drawing.Point(425, 115);
            this.chartPromoByRate.Name = "chartPromoByRate";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend6";
            series6.Name = "Rate";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            this.chartPromoByRate.Series.Add(series6);
            this.chartPromoByRate.Size = new System.Drawing.Size(400, 180);
            this.chartPromoByRate.TabIndex = 17;

            // dgvPromoReport
            this.dgvPromoReport.AllowUserToAddRows = false;
            this.dgvPromoReport.AllowUserToDeleteRows = false;
            this.dgvPromoReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPromoReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPromoReport.Location = new System.Drawing.Point(15, 305);
            this.dgvPromoReport.Name = "dgvPromoReport";
            this.dgvPromoReport.ReadOnly = true;
            this.dgvPromoReport.RowHeadersWidth = 51;
            this.dgvPromoReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvPromoReport.Size = new System.Drawing.Size(810, 400);
            this.dgvPromoReport.TabIndex = 18;
            //
            // Empty tabs
            //
            this.tabLeadConversion.AutoScroll = true;
            this.tabLeadConversion.Location = new System.Drawing.Point(4, 24);
            this.tabLeadConversion.Name = "tabLeadConversion";
            this.tabLeadConversion.Padding = new System.Windows.Forms.Padding(3);
            this.tabLeadConversion.Size = new System.Drawing.Size(852, 517);
            this.tabLeadConversion.TabIndex = 3;
            this.tabLeadConversion.Text = "Lead Conversion";
            this.tabLeadConversion.UseVisualStyleBackColor = true;

            this.tabRetention.AutoScroll = true;
            this.tabRetention.Location = new System.Drawing.Point(4, 24);
            this.tabRetention.Name = "tabRetention";
            this.tabRetention.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetention.Size = new System.Drawing.Size(852, 517);
            this.tabRetention.TabIndex = 4;
            this.tabRetention.Text = "Retention";
            this.tabRetention.UseVisualStyleBackColor = true;

            this.tabPromoUsage.AutoScroll = true;
            this.tabPromoUsage.Location = new System.Drawing.Point(4, 24);
            this.tabPromoUsage.Name = "tabPromoUsage";
            this.tabPromoUsage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPromoUsage.Size = new System.Drawing.Size(852, 517);
            this.tabPromoUsage.TabIndex = 5;
            this.tabPromoUsage.Text = "Promo Usage";
            this.tabPromoUsage.UseVisualStyleBackColor = true;
            //
            // ReportsControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.presetsPanel);
            this.Controls.Add(this.lblTitle);
            this.Name = "ReportsControl";
            this.Size = new System.Drawing.Size(900, 710);
            this.tabs.ResumeLayout(false);
            this.tabRevenue.ResumeLayout(false);
            this.tabMembership.ResumeLayout(false);
            this.tabAttendance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMembershipStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMembershipByPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembershipReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendanceByDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendanceByHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendanceReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.FlowLayoutPanel presetsPanel;
        private System.Windows.Forms.Button btnPresetToday;
        private System.Windows.Forms.Button btnPresetThisWeek;
        private System.Windows.Forms.Button btnPresetThisMonth;
        private System.Windows.Forms.Button btnPresetLast3;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabRevenue;
        private System.Windows.Forms.TabPage tabMembership;
        private System.Windows.Forms.TabPage tabAttendance;
        private System.Windows.Forms.TabPage tabLeadConversion;
        private System.Windows.Forms.TabPage tabRetention;
        private System.Windows.Forms.TabPage tabPromoUsage;
        private KpiCard kpiRevenueTotal;
        private KpiCard kpiRevenueCount;
        private KpiCard kpiRevenueAvg;
        private KpiCard kpiRevenueCancelled;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueByMonth;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueByPlan;
        private System.Windows.Forms.DataGridView dgvSalesReport;
        private KpiCard kpiMemberActive;
        private KpiCard kpiMemberExpiring;
        private KpiCard kpiMemberExpired;
        private KpiCard kpiMemberFrozen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMembershipStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMembershipByPlan;
        private System.Windows.Forms.DataGridView dgvMembershipReport;
        private KpiCard kpiAttToday;
        private KpiCard kpiAttWeek;
        private KpiCard kpiAttMonth;
        private KpiCard kpiAttUnique;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttendanceByDay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttendanceByHour;
        private System.Windows.Forms.DataGridView dgvAttendanceReport;
        private KpiCard kpiLeadTotal;
        private KpiCard kpiLeadConverted;
        private KpiCard kpiLeadLost;
        private KpiCard kpiLeadRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLeadBySource;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLeadByStatus;
        private System.Windows.Forms.DataGridView dgvLeadReport;

        private KpiCard kpiRetCancellations;
        private KpiCard kpiRetFreezes;
        private KpiCard kpiRetWinBacks;
        private KpiCard kpiRetRenewals;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRetentionByDay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRetentionByAction;
        private System.Windows.Forms.DataGridView dgvRetentionReport;

        private KpiCard kpiPromoTotal;
        private KpiCard kpiPromoActive;
        private KpiCard kpiPromoRedemptions;
        private KpiCard kpiPromoAvgRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPromoByCode;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPromoByRate;
        private System.Windows.Forms.DataGridView dgvPromoReport;
    }
}