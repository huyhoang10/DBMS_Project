using Final_Project_DBMS.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Final_Project_DBMS.View
{
    public partial class UCDashboard : UserControl
    {
        DashboardController dashboardController = new DashboardController();
        public UCDashboard()
        {
            InitializeComponent();
        }

        public void UCDashboard_Load(object sender, EventArgs e)
        {
            LoadcmbYear();
            int year = (int)cmbYear.SelectedItem;
            DataTable dt = dashboardController.GetMoneyEachMonthByYear(year);
            LoadChart(dt);
            txtMoneyMonth.Text = dashboardController.SumMoneyThisMonth().ToString("N0") + " VND";
            txtMoneyWeek.Text = dashboardController.SumMoneyThisWeek().ToString("N0") + " VND";
            LoadDgvSupplier();
            LoadDgvWarehouse(); 
        }

        private void LoadcmbYear()
        {
            List<int> years = dashboardController.GetYear();
            cmbYear.DataSource = years;
            cmbYear.SelectedItem = DateTime.Now.Year;
        }

        private void LoadChart(DataTable dt)
        {
            // Tạo ChartArea
            ChartArea chartArea = chartTotalMoneyByMonth.ChartAreas[0];
            chartArea.AxisX.Title = "Tháng";
            chartArea.AxisY.Title = "Số tiền (triệu VND)";
            chartArea.AxisX.Interval = 1; // Hiển thị từng tháng
            chartArea.AxisX.TitleFont = new Font("Arial", 12, FontStyle.Regular);
            chartArea.AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);



            // Tạo Series
            Series series = chartTotalMoneyByMonth.Series[0];
            series.ChartType = SeriesChartType.Column; // Biểu đồ cột
            series.IsValueShownAsLabel = true; // Hiển thị giá trị trên cột
            series.Color = Color.SteelBlue;
            series.BorderWidth = 2;
            series["PointWidth"] = "0.8";
            // Lấy list số tiền 12 tháng từ DataTable
            List<decimal> doanhThu = dashboardController.ConvertDataTableToMoneyEachMonth(dt, 0, 1);

            // Xóa dữ liệu cũ (nếu có)
            series.Points.Clear();

            // Thêm dữ liệu vào series
            for (int i = 0; i < 12; i++)
            {
                string thang = (i + 1).ToString();
                series.Points.AddXY(thang, doanhThu[i]);
            }


            chartTotalMoneyByMonth.Legends[0].Enabled = false;
            chartTotalMoneyByMonth.Series[0].IsVisibleInLegend = false;

        }

        


        private void LoadDgvSupplier()
        {
            DataTable dt = dashboardController.GetMoneyBySupplier();
            dgvSupllier.AutoGenerateColumns = false;
            dgvSupllier.Columns[0].DataPropertyName = dt.Columns[0].ColumnName;
            dgvSupllier.Columns[1].DataPropertyName = dt.Columns[1].ColumnName;
            dgvSupllier.DataSource = dt;
        }

        private void LoadDgvWarehouse()
        {
            DataTable dt = dashboardController.GetMoneyByWarehouse();
            dgvWarehouse.AutoGenerateColumns = false;
            dgvWarehouse.Columns[0].DataPropertyName = dt.Columns[0].ColumnName;
            dgvWarehouse.Columns[1].DataPropertyName = dt.Columns[1].ColumnName;
            dgvWarehouse.DataSource = dt;
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = (int)cmbYear.SelectedItem;
            LoadChart(dashboardController.GetMoneyEachMonthByYear(year));
        }

        private void grbTotalMoney_Click(object sender, EventArgs e)
        {

        }
    }
}
