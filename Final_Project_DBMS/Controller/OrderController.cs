using Final_Project_DBMS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Controller
{
    internal class OrderController
    {
        OrderDao orderDao = new OrderDao();
        public List<Order> GetAllOrderExpect()
        {
            return orderDao.GetAllOrderExpect();
        }

        public List<Order> GetOrderPending()
        {
            return orderDao.GetOrderPending();
        }
        public List<OrderDetail> FindOderDetailById(int id)
        {
            return orderDao.FindOderDetailById(id);
        }

        public void InsertOrderExpect(List<OrderDetail> details, Order order)
        {
            orderDao.InsertOrderExpect(details, order);
        }

        public void InsertOrderActual(int idOrderPending,List<OrderDetail> details, Order order)
        {
            orderDao.InsertOrderActual(idOrderPending,details, order);
        }

        public List<History> GetHistory()
        {
            return orderDao.GetHistory();
        }

        public Order FindOrderById(int id)
        {
            return orderDao.FindOrderById(id);
        }

        public void CanCelOrderExpect(int id) { 
            orderDao.CancelOrderExpect(id);
        }
    }
}
