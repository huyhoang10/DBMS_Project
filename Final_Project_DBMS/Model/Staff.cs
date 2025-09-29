using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class Staff
    {
        int idStaff;
        string nameStaff;
        string cccd;
        string sex;
        DateTime birthDay;
        int idUser;
        string userName;
        string password;
        int idRole;

        public int IdStaff { get => idStaff; set => idStaff = value; }
        public string NameStaff { get => nameStaff; set => nameStaff = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public int IdRole { get => idRole; set => idRole = value; }

    }
}
