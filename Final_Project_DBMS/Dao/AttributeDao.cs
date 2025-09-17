using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Dao
{
    internal class AttributeDao
    {
        string connectString = DBConnect.connectionString;

        public List<Attributes> GetDataAttribute(string nameAttribute)
        {
            List<Attributes> attributes = new List<Attributes>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_LayDuLieuThuocTinh",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nametable", nameAttribute);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read()) {
                    Attributes attribute = new Attributes();
                    attribute.Id = dataReader.IsDBNull(0) ? 0 : dataReader.GetInt32(0);
                    attribute.Name = dataReader.IsDBNull(1) ? String.Empty : dataReader.GetString(1);
                    attributes.Add(attribute);
                } 
            }
            return attributes;
        }

        public void InsertAttribute(string nameAttribute,Attributes attributes)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_InsertThuocTinh", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nametable", nameAttribute);
                cmd.Parameters.AddWithValue("@ten", attributes.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAttribute(string nameAttribute, Attributes attributes)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("prc_UpdateThuocTinh", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nametable", nameAttribute);
                cmd.Parameters.AddWithValue("@ma", attributes.Id);
                cmd.Parameters.AddWithValue("@ten", attributes.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
