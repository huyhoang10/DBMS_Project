using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class User
    {
        private string username;
        private string password;
        private string role;
        public string Username { 
            get { return username; }
            set { username = value; }
        }
        public string Password {
            get { return password; }
            set { password = value; }
        }
        public string Role {
            get { return role; }
            set { role = value; }
        }
        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
        public User()
        {
        }
    }
}
