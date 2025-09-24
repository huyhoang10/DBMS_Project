using Final_Project_DBMS.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.View
{
    public partial class UCStocking : UserControl
    {
        OrderController orderController = new OrderController();
        List<Product> allProducts;
        public UCStocking()
        {
            InitializeComponent();
        }

        public void UCStocking_Load(object sender, EventArgs e)
        {
            LoadDgvOrderExpect();
        }

        private void LoadDgvOrderExpect()
        {
            dgvOrderExpect.AutoGenerateColumns = false;
            dgvOrderExpect.DataSource = orderController.GetAllOrderExpect();

        }

        private void LoadOrderDetails(int orderId)
        {
            dgvOrderDetailExpect.Rows.Clear();
            var orderDetails = orderController.FindOderDetailById(orderId);

            int stt = 0;
            foreach (var detail in orderDetails)
            {
                stt++;
                dgvOrderDetailExpect.Rows.Add();
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_indexExpect"].Value = stt.ToString();
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_idProductExpect"].Value = detail.IdProduct;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_nameProductExpect"].Value = detail.NameProduct;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_priceExpect"].Value = detail.Price;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_unitExpect"].Value = detail.Unit;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_quantityExpect"].Value = detail.Quantity;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_totalPriceExpect"].Value = detail.Quantity*detail.Price;
            }

        }

        private void dgvOrderExpect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Order order = (Order)dgvOrderExpect.Rows[e.RowIndex].DataBoundItem;
            LoadOrderDetails(order.IdOrder);
        }
    }
}
