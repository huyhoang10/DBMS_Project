using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Dao
{
    internal class InventoryDao
    {
        String connectString = DBConnect.connectionString;

        public List<Inventory> GetInventoryByWarehouse(string nameWarehouse){
            List<Inventory> inventories = new List<Inventory>();
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.fn_LaySPTheoKho(@tenkho)", conn);
                cmd.Parameters.AddWithValue("@tenkho", nameWarehouse);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Inventory inventory = new Inventory();
                    inventory.NameWarehouse = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    inventory.IdProduct = reader.IsDBNull(1)? 0 : reader.GetInt32(1);
                    inventory.NameProduct = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    inventory.Brand = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    inventory.Unit = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    inventory.Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    inventory.CountProduct = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    inventories.Add(inventory);
                }
            }
            return inventories;
        }

        public List<Inventory> FindProductInventory(string nameProduct,string nameWarehouse)
        {
            List<Inventory> inventories = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.fn_TimSPTrongTonKho(@tensp,@tenkho)", conn);
                cmd.Parameters.AddWithValue("@tensp", nameProduct);
                cmd.Parameters.AddWithValue("@tenkho",nameWarehouse);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Inventory inventory = new Inventory();
                    inventory.NameWarehouse = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    inventory.IdProduct = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    inventory.NameProduct = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    inventory.Brand = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    inventory.Unit = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    inventory.Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    inventory.CountProduct = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    inventories.Add(inventory);
                }
            }
            return inventories;
        }

    }
}
