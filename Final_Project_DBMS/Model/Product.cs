using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class Product
    {

        private int id;
        private string name;
        private string brand;
        private string category;
        private string color;
        private string material;
        private string size;
        private string unit;
        private string description;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public string Material
        {
            get { return material; }
            set { material = value; }
        }
        public string Size
        {
            get { return size; }
            set { size = value; }
        }
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
