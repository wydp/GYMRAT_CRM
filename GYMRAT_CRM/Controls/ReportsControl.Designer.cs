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
            this.tabAttendance = new System.Windows.Forms.TabPage();
            this.tabLeadConversion = new System.Windows.Forms.TabPage();
            this.tabRetention = new System.Windows.Forms.TabPage();
            this.tabPromoUsage = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.tabRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
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
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "From";
            //
            // dtpFrom
            //
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(20, 110);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(140, 23);
            this.dtpFrom.TabIndex = 2;
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(180, 92);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 15);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "To";
            //
            // dtpTo
            //
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(180, 110);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(140, 23);
            this.dtpTo.TabIndex = 4;
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
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            //
            // btnExport
            //
            this.btnExport.Location = new System.Drawing.Point(460, 108);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 28);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            //
            // btnPrint
            //
            this.btnPrint.Location = new System.Drawing.Point(580, 108);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 28);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
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
            // KPI cards row
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
            // Grid is intentionally taller than the visible tab so the tab's
            // AutoScroll kicks in and users can scroll to see all rows.
            // It also has its own internal scrollbar for rows beyond its height.
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
            // Other tabs (empty for now)
            //
            this.tabMembership.AutoScroll = true;
            this.tabMembership.Location = new System.Drawing.Point(4, 24);
            this.tabMembership.Name = "tabMembership";
            this.tabMembership.Padding = new System.Windows.Forms.Padding(3);
            this.tabMembership.Size = new System.Drawing.Size(852, 517);
            this.tabMembership.TabIndex = 1;
            this.tabMembership.Text = "Membership";
            this.tabMembership.UseVisualStyleBackColor = true;

            this.tabAttendance.AutoScroll = true;
            this.tabAttendance.Location = new System.Drawing.Point(4, 24);
            this.tabAttendance.Name = "tabAttendance";
            this.tabAttendance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttendance.Size = new System.Drawing.Size(852, 517);
            this.tabAttendance.TabIndex = 2;
            this.tabAttendance.Text = "Attendance";
            this.tabAttendance.UseVisualStyleBackColor = true;

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
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
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
    }
}