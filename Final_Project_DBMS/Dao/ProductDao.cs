using Final_Project_DBMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Configuration;
using Final_Project_DBMS.Utils;

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
                    product.Description = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
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
                    product.Description = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
                    products.Add(product);
                }
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                
                conn.Open();
                string query = "Select * from dbo.fn_TimSanPhamByID(@id)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    product.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    product.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    product.Brand = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    product.Category = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                    product.Color = dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4);
                    product.Material = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                    product.Size = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                    product.Unit = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                    product.Description = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
                    
                }
                
            }
            return product;

        }

        public void InsertProduct(Product product)
        {
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_InsertSanPham", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ten_sp", product.Name);
                cmd.Parameters.AddWithValue("@ma_th", Constants.dicBrand[product.Brand]);
                cmd.Parameters.AddWithValue("@ma_loai", Constants.dicCategory[product.Category]);
                cmd.Parameters.AddWithValue("@ma_mau", Constants.dicColor[product.Color]);
                cmd.Parameters.AddWithValue("@ma_chatlieu", Constants.dicMaterial[product.Material]);
                cmd.Parameters.AddWithValue("@kichthuoc", product.Size);
                cmd.Parameters.AddWithValue("@ma_dvt", Constants.dicUnit[product.Unit]);
                cmd.Parameters.AddWithValue("@mota", product.Description);
                cmd.ExecuteNonQuery();  

            }
        }

        public void UpdateProduct(Product product) {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_UpdateSanPham", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma_sp", product.Id);
                cmd.Parameters.AddWithValue("@ten_sp", product.Name);
                cmd.Parameters.AddWithValue("@ma_th", Constants.dicBrand[product.Brand]);
                cmd.Parameters.AddWithValue("@ma_loai", Constants.dicCategory[product.Category]);
                cmd.Parameters.AddWithValue("@ma_mau", Constants.dicColor[product.Color]);
                cmd.Parameters.AddWithValue("@ma_chatlieu", Constants.dicMaterial[product.Material]);
                cmd.Parameters.AddWithValue("@kichthuoc", product.Size);
                cmd.Parameters.AddWithValue("@ma_dvt", Constants.dicUnit[product.Unit]);
                cmd.Parameters.AddWithValue("@mota", product.Description);
                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_XoaSanPham", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma_sp", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
