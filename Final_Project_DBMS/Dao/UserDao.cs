using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Final_Project_DBMS.Dao
{
    internal class UserDao
    {
        String connectionString = DBConnect.connectionString;
        public int Login(string username, string password)
        {
            int roleID;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select dbo.fn_CheckRoleId(@username,@password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                roleID = (int)cmd.ExecuteScalar();
            }
            return roleID;
           
        }
    }
}
