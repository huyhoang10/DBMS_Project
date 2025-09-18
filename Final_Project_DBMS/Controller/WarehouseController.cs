using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;


namespace Final_Project_DBMS.Controller
{
    internal class WarehouseController
    {
        WarehouseDao warehouseDao = new WarehouseDao();
        public List<Warehouse> GetAllWarehouse()
        {
            return warehouseDao.GetAllWarehouse();
        }

        public void InsertWarehouse(Warehouse warehouse)
        {
            warehouseDao.InsertWarehouse(warehouse);
        }
        public void UpdateWarehouse(Warehouse warehouse)
        {
            warehouseDao.UpdateWarehouse(warehouse);
        }
    }
}
