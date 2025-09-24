using System;
using System.Collections.Generic;
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

        public Order FindOderById(int id)
        {
            Order order = null;
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "select * from v_DonHangDuKien where IdOrder = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order = new Order();
                    order.IdOrder = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    order.OrderDate = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                    order.IdStaff = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    order.Total = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                    order.Supplier = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    order.Warehouse = reader.IsDBNull(5) ? "" : reader.GetString(5);
                }
            }
            return order;
        }
    }
}
