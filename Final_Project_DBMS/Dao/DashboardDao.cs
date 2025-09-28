using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Dao
{
    internal class DashboardDao
    {
        string connectionString = DBConnect.connectionString;

        public decimal SumMoneyThisMonth()
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT dbo.fn_TongTienNhap_ThangNay() AS TongTienThangNay", conn);
                total = decimal.Parse(cmd.ExecuteScalar().ToString());
                return total;
            }
        }

        public decimal SumMoneyThisWeek()
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT dbo.fn_TongTienNhap_TuanNay() AS TongTienTuanNay", conn);
                total = decimal.Parse(cmd.ExecuteScalar().ToString());
                return total;
            }
        }

        public DataTable GetMoneyBySupplier() {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM v_TongTienNhapTheoNCC_ThangNay", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                
            }
            return dt;
        }

        public DataTable GetMoneyByWarehouse()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM v_TongTienNhapTheoKho_ThangNay", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

            }
            return dt;
        }

        public DataTable GetMoneyEachMonthByYear(int year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_TienNhapHangCacThangTheoNam (@year)", conn);
                cmd.Parameters.AddWithValue("@year", year);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            return dt;
        }

        public DataTable GetYear()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_LayNamNhapHang()", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            return dt;
        }

    }
}
