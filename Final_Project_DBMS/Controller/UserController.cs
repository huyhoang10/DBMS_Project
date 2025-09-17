using Final_Project_DBMS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Controller
{
    internal class UserController
    {
        UserDao userDao = new UserDao();
        public int Login(string username, string password)
        {
            return userDao.Login(username, password);
        }
    }
}
