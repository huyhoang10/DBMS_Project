using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class History
    {
        DateTime time;
        int idOrder;
        string note;

        public DateTime Time { get => time; set => time = value; }
        public int IdOrder { get => idOrder; set => idOrder = value; }
        public string Note { get => note; set => note = value; }

    }
}
