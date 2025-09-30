using Final_Project_DBMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace Final_Project_DBMS.Dao
{
    internal class StaffDao
    {
        string connectionString = DBConnect.connectionString;

        public List<Staff> GetAllStaff()
        {
            List<Staff> staffs = new List<Staff>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from v_NhanVienChiTiet", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) { 
                    Staff staff = new Staff();
                    staff.IdStaff = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    staff.NameStaff = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    staff.Cccd = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    staff.BirthDay = reader.IsDBNull(3) ? DateTime.Today :reader.GetDateTime(3);
                    staff.Sex = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    staff.IdUser = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                    staff.UserName = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    staff.Password = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    staff.IdRole = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                    staffs.Add(staff);
                }
            }
                return staffs;
        }
        public void AddStaff(Staff staff)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                conn.Open();
                if (staff.IdRole == 2)
                {   
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.prc_ThemNhanVienVaTaiKhoan_NvKho", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@HoTen", staff.NameStaff);
                        cmd.Parameters.AddWithValue("@CCCD", staff.Cccd);
                        cmd.Parameters.AddWithValue("@GioiTinh", staff.Sex);
                        cmd.Parameters.AddWithValue("@NgaySinh", staff.BirthDay);
                        cmd.Parameters.AddWithValue("@Tuoi", DateTime.Now.Year - staff.BirthDay.Year);
                        cmd.Parameters.AddWithValue("@TenDangNhap", staff.UserName);
                        cmd.Parameters.AddWithValue("@MatKhau", staff.Password);
                        cmd.Parameters.AddWithValue("@RoleId", staff.IdRole);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error adding staff and account: " + ex.Message);
                    }
                }
                else if(staff.IdRole == 1)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("Exec prc_ThemNhanVienVaTaiKhoan_QuanLy @HoTen,@CCCD," +
                            "@GioiTinh,@NgaySinh,@Tuoi,@TenDangNhap,@MatKhau,@RoleId", conn);
                        cmd.Parameters.AddWithValue("@HoTen", staff.NameStaff);
                        cmd.Parameters.AddWithValue("@CCCD", staff.Cccd);
                        cmd.Parameters.AddWithValue("@GioiTinh", staff.Sex);
                        cmd.Parameters.AddWithValue("@NgaySinh", staff.BirthDay);
                        cmd.Parameters.AddWithValue("@Tuoi", DateTime.Now.Year - staff.BirthDay.Year);
                        cmd.Parameters.AddWithValue("@TenDangNhap", staff.UserName);
                        cmd.Parameters.AddWithValue("@MatKhau", staff.Password);
                        cmd.Parameters.AddWithValue("@RoleId", staff.IdRole);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error adding staff and account: " + ex.Message);
                    }
                }
                
            }


        }

        public Staff GetStaffByUserName(string username) { 
            Staff staff = new Staff();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from fn_TimNhanVienTheoTenTaiKhoan(@username)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    staff.IdStaff = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    staff.NameStaff = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    staff.Cccd = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    staff.BirthDay = reader.IsDBNull(3) ? DateTime.Today : reader.GetDateTime(3);
                    staff.Sex = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    staff.IdUser = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                    staff.UserName = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    staff.Password = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    staff.IdRole = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                }
            }
            return staff;
        }
    }
}
