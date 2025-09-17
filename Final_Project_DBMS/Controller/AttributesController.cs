using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;
namespace Final_Project_DBMS.Controller
{
    internal class AttributesController
    {
        AttributeDao attributeDao = new AttributeDao();
        public List<Attributes> GetDataAttributes(string nameAttribute)
        {
            return attributeDao.GetDataAttribute(nameAttribute);
        }
        public void InsertAttributes(string nameAttribute, Attributes attributes)
        {
            attributeDao.InsertAttribute(nameAttribute, attributes);
        }
        public void UpdateAttributes(string nameAttribute, Attributes attributes)
        {
            attributeDao.UpdateAttribute(nameAttribute, attributes);
        }
    }
}
