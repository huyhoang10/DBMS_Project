using Final_Project_DBMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Final_Project_DBMS.Dao
{
    internal class ProductDao
    {
        string connectString = DBConnect.connectionString;
        // Methods for Product CRUD operations would go here
        public List<Product> GetAllProduct()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "Select * from v_SanPham_Chitiet";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    product.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    product.Brand = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    product.Category = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                    product.Color = dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4);
                    product.Material = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                    product.Size = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                    product.Unit = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                    product.Status = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                    product.Location = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                    product.Description = dataReader.IsDBNull(10) ? null : dataReader.GetString(10);
                    products.Add(product);
                }
            }
            return products;
        }

        public List<Product> FindProductByName(string keyword)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "Select * from dbo.fn_TimSanPham(@keyword)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    product.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    product.Brand = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    product.Category = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                    product.Color = dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4);
                    product.Material = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                    product.Size = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                    product.Unit = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                    product.Status = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                    product.Location = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                    product.Description = dataReader.IsDBNull(10) ? null : dataReader.GetString(10);
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
