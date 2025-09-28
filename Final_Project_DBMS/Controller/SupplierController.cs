using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;
using Final_Project_DBMS.Dao;
using System.Windows.Forms;

namespace Final_Project_DBMS.Controller
{
    internal class SupplierController
    {
        SupplierDao supplierDao = new SupplierDao();
        public List<Supplier> GetAllSupllier()
        {
            return supplierDao.GetAllSupplier();
        }

        public List<Supplier> FindSupplier(string keyword)
        {
            return supplierDao.FindSupplier(keyword);
        }

        public void SaveSupplier(Supplier supplier)
        {
            if(supplier.Id == 0)
            {
                supplierDao.InsertSupplier(supplier);
                MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo");
            }
            else
            {
                supplierDao.UpdateSupplier(supplier);
                MessageBox.Show("Chỉnh sửa nhà cung cấp thành công.", "Thông báo");
            }
        }

        public void DeleteSupplier(int id)
        {
            supplierDao.DeleteSupplier(id);
        }
    }
}
