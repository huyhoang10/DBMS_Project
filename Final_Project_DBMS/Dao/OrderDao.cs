using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Dao
{
    internal class OrderDao
    {
        String connectString = DBConnect.connectionString;

        public List<Order> GetAllOrderExpect()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from v_DonHangDuKien";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    order.OrderDate = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                    order.IdStaff = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    order.Total = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                    order.Supplier = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    order.Warehouse = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    order.Status = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    order.Note = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    orders.Add(order);
                }

            }
            return orders;
        }

        public List<Order> GetOrderPending()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from v_DonHangCanXuLy";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    order.OrderDate = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                    order.IdStaff = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    order.Total = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                    order.Supplier = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    order.Warehouse = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    order.Status = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    order.Note = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    orders.Add(order);
                }
            }
            return orders;
        }

        public List<OrderDetail> FindOderDetailById(int id)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from fn_ChitietDonHang(@madon)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@madon", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.IdProduct = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    orderDetail.NameProduct = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    orderDetail.Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                    orderDetail.Unit = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    orderDetail.Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                    orderDetails.Add(orderDetail);
                }
            }
            return orderDetails;
        }

        public Order FindOrderById(int id)
        {
            Order order = null;
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from fn_LayDonHangTheoMa(@id)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order = new Order();
                    order.IdOrder = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    order.OrderDate = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                    order.IdStaff = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    order.Total = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                    order.Supplier = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    order.Warehouse = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    order.Status = reader.IsDBNull(6) ? "" : reader.GetString(5);
                }
            }
            return order;
        }

        private DataTable ToDataTable(List<OrderDetail> details)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ma_sp", typeof(int));
            table.Columns.Add("gia_dk", typeof(decimal));
            table.Columns.Add("soluong", typeof(int));

            foreach (OrderDetail d in details)
            {
                table.Rows.Add(d.IdProduct, d.Price, d.Quantity);
            }

            return table;
        }


        public void InsertOrderExpect(List<OrderDetail> orderDetails, Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("prc_ThemDonHangDuKien", conn, tran))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tham số đơn giản
                        
                        cmd.Parameters.AddWithValue("@makho", Int32.Parse(order.Warehouse));
                        cmd.Parameters.AddWithValue("@ma_nv", order.IdStaff);
                        cmd.Parameters.AddWithValue("@ma_ncc", Int32.Parse(order.Supplier));

                        // Table-Valued Parameter
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ChiTietPhieuNhap", ToDataTable(orderDetails));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.DanhSachSanPham"; // Tên UDT bạn tạo trong SQL

                        // Thực thi SP
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("InsertOrderExpect failed: " + ex.Message, ex);
                }
            }
        }

        public void CancelOrderExpect(int id) {
            using (SqlConnection conn = new SqlConnection(connectString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_HuyDonHangDuKien",conn);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma_don", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertOrderActual(int idOrderPending,List<OrderDetail> orderDetails, Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("prc_ThemDonHangThucTe", conn, tran))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tham số đơn giản
                        
                        cmd.Parameters.AddWithValue("@makho", Int32.Parse(order.Warehouse));
                        cmd.Parameters.AddWithValue("@ma_nv", order.IdStaff);
                        cmd.Parameters.AddWithValue("@ma_ncc", Int32.Parse(order.Supplier));
                        cmd.Parameters.AddWithValue("@ma_dhxuly", idOrderPending);

                        // Table-Valued Parameter
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ChiTietPhieuNhap", ToDataTable(orderDetails));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.DanhSachSanPham"; // Tên UDT bạn tạo trong SQL

                        // Thực thi SP
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("InsertOrderExpect failed: " + ex.Message, ex);
                }
            }
        }

        public List<History> GetHistory()
        {
            List<History> histories = new List<History>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from v_LichSu order by thoigian Desc";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    History history = new History();
                    history.Time = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0);
                    history.IdOrder = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    history.Note = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    histories.Add(history);
                }
            }
            return histories;
        }

    }
}
