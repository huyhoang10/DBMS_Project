using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class Inventory
    {
        string nameWarehouse;
        int idProduct;
        string nameProduct;
        int countProduct;
        string unit;
        string brand;
        string status;
        public string NameWarehouse { get => nameWarehouse; set => nameWarehouse = value; }
        public int IdProduct { get => idProduct; set => idProduct = value; }
        public string NameProduct { get => nameProduct; set => nameProduct = value; }
        public int CountProduct { get => countProduct; set => countProduct = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Status { get => status; set => status = value; }
    }
}
