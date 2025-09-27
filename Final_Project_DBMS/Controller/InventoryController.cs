using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Controller
{
    internal class InventoryController
    {
        InventoryDao inventoryDao = new InventoryDao();
        public List<Inventory> GetInventoryByWarehouse(string nameWarehouse)
        {
            return inventoryDao.GetInventoryByWarehouse(nameWarehouse);
        }

        public List<Inventory> FindProductInventory(string nameProduct,string nameWarehouse)
        {
            return inventoryDao.FindProductInventory(nameProduct,nameWarehouse);
        }
        public void DeleteProductInventory(int idWarehouse, int idProduct)
        {
            inventoryDao.DeleteProductInventory(idWarehouse, idProduct);
        }
    }
}
