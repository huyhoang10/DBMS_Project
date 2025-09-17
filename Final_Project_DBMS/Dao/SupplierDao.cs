using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;
using System.Data.SqlClient;

namespace Final_Project_DBMS.Dao
{
    
    internal class SupplierDao
    {
        string connectString = DBConnect.connectionString;

        public List<Supplier> GetAllSupplier()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using(SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "Select * from NhaCungCap";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dataReader =  cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Supplier supplier = new Supplier();

                    supplier.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    supplier.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    supplier.Address = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    supplier.Contact = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                    supplier.Note = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);

                    suppliers.Add(supplier);
                }

            }
            return suppliers;
        }

        public List<Supplier> FindSupplier(string keyword)
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string query = "Select * from dbo.fn_TimNhaCC(@keyword)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    supplier.Name = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                    supplier.Address = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                    supplier.Contact = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                    supplier.Note = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }
        public void InsertSupplier(Supplier supplier)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_InsertNhaCC", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ten_ncc", supplier.Name);
                cmd.Parameters.AddWithValue("@diachi", supplier.Address);
                cmd.Parameters.AddWithValue("@lienhe", supplier.Contact);
                cmd.Parameters.AddWithValue("@ghichu", supplier.Note);
                cmd.ExecuteNonQuery();
      
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_UpdateNhaCC", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma_ncc", supplier.Id);
                cmd.Parameters.AddWithValue("@ten_ncc", supplier.Name);
                cmd.Parameters.AddWithValue("@diachi", supplier.Address);
                cmd.Parameters.AddWithValue("@lienhe", supplier.Contact);
                cmd.Parameters.AddWithValue("@ghichu", supplier.Note);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
