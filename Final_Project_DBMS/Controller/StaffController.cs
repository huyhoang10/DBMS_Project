using Final_Project_DBMS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;
using System.Windows.Forms;

namespace Final_Project_DBMS.Controller
{
    internal class StaffController
    {
        StaffDao staffDao = new StaffDao();
        public List<Staff> GetAllStaff()
        {
            return staffDao.GetAllStaff();
        }
        public void AddStaff(Staff staff)
        {
            staffDao.AddStaff(staff);
        }

        public Staff GetStaffByUserName(string username)
        {
            return staffDao.GetStaffByUserName(username);
        }
    }
}
