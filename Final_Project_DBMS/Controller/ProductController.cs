using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Controller
{
    internal class ProductController
    {
        ProductDao productDao = new ProductDao();
        public List<Product> GetAllProduct()
        {
            return productDao.GetAllProduct();
        }
        public List<Product> FindProductByName(string keyword)
        {
            return productDao.FindProductByName(keyword);
        }

        public Product GetProductById(int id)
        {
            return productDao.GetProductById(id);
        }

        public void InsertProduct(Product product)
        {
            productDao.InsertProduct(product);
        }
        public void UpdateProduct(Product product)
        {
            productDao.UpdateProduct(product);
        }
        public void DeleteProduct(int id)
        {
            productDao.DeleteProduct(id);
        }
    }
}
