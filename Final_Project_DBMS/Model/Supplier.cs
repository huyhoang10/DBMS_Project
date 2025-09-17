using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_DBMS.Model
{
    internal class Supplier
    {
        private int id = 0;
        private string name;
        private string address;
        private string contact;
        private string note;

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
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        public Supplier() { }



    }
}
