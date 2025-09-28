using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;

namespace Final_Project_DBMS.Controller
{
    internal class DashboardController
    {
        DashboardDao dashboardDao = new DashboardDao();

        public decimal SumMoneyThisMonth()
        {
            return dashboardDao.SumMoneyThisMonth();
        }
        public decimal SumMoneyThisWeek()
        {
            return dashboardDao.SumMoneyThisWeek();
        }
        public System.Data.DataTable GetMoneyBySupplier()
        {
            return dashboardDao.GetMoneyBySupplier();
        }
        public System.Data.DataTable GetMoneyByWarehouse()
        {
            return dashboardDao.GetMoneyByWarehouse();
        }
        public System.Data.DataTable GetMoneyEachMonthByYear(int year)
        {
            return dashboardDao.GetMoneyEachMonthByYear(year);
        }

        public List<decimal> ConvertDataTableToMoneyEachMonth(DataTable dt, int monthColumnIndex, int moneyColumnIndex)
        {
            // Khởi tạo list 12 tháng với giá trị 0
            List<decimal> moneyEachMonth = Enumerable.Repeat(0m, 12).ToList();

            foreach (DataRow row in dt.Rows)
            {
                // Lấy tháng (ví dụ 1-12)
                int month = Convert.ToInt32(row[monthColumnIndex]);

                // Lấy số tiền
                decimal money = Convert.ToDecimal(row[moneyColumnIndex]);

                // Map vào đúng vị trí trong list (index = month - 1)
                if (month >= 1 && month <= 12)
                {
                    moneyEachMonth[month - 1] = money;
                }
            }

            return moneyEachMonth;
        }

        public List<int> GetYear() { 
            List<int> years = new List<int>();
            DataTable dt = dashboardDao.GetYear();
            foreach (DataRow row in dt.Rows)
            {
                years.Add(int.Parse(row[0].ToString()));
            }
            return years;
        }
    }
}
