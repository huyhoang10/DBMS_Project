using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Dao
{
    internal class OrderDetail
    {
        int idProduct;
        string nameProduct;
        string unit;
        decimal price;
        int quantity;

        public int IdProduct { get => idProduct; set => idProduct = value; }
        public string NameProduct { get => nameProduct; set => nameProduct = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }

    }
}
