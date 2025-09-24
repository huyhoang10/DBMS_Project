using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class Order
    {
        int idOrder;
        DateTime orderDate;
        int idStaff;
        string warehouse;
        string supplier;
        string status;
        decimal total;
        string note;


        public int IdOrder { get => idOrder; set => idOrder = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public int IdStaff { get => idStaff; set => idStaff = value; }

        public string Warehouse { get => warehouse; set => warehouse = value; }
        public string Supplier { get => supplier; set => supplier = value; }

        public string Status { get => status; set => status = value; }

        public decimal Total { get => total; set => total = value; }

        public String Note { get => note; set => note = value; }

    }
}
