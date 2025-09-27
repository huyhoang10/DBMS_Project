using Final_Project_DBMS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Dao
{
    internal class WarehouseDao
    {
        String connectionString = DBConnect.connectionString;
        public List<Warehouse> GetAllWarehouse()
        {
            List<Warehouse> warehouses = new List<Warehouse>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select * from v_Kho;";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    warehouse.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    warehouse.Address = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    warehouses.Add(warehouse);
                }
            }
            return warehouses;
        }

        public void InsertWarehouse(Warehouse warehouse)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
               
                SqlCommand cmd = new SqlCommand("prc_InsertKho", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ten", warehouse.Name);
                cmd.Parameters.AddWithValue("@diachi", warehouse.Address);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateWarehouse(Warehouse warehouse)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_UpdateKho", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma", warehouse.Id);
                cmd.Parameters.AddWithValue("@ten", warehouse.Name);
                cmd.Parameters.AddWithValue("@diachi", warehouse.Address);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteWarehouse(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_XoaKho", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
