using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Utils
{
    internal class MethodStatic
    {
        public static String GenerateID(string prefix)
        {
            Random random = new Random();
            int number = random.Next(10000, 99999);
            return prefix + number.ToString();
        }
    }
}
