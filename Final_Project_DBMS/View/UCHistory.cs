using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project_DBMS.Controller;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.View
{
    public partial class UCHistory : UserControl
    {
        OrderController orderController = new OrderController();
        public UCHistory()
        {
            InitializeComponent();
            dgvHistory.ReadOnly = true;
            dgvDetailOder.ReadOnly = true;
            dgvHistory.AutoGenerateColumns = false;
            txtId.ReadOnly = true;
            txtStaff.ReadOnly = true;
            txtSupplier.ReadOnly = true;
            txtWarehouse.ReadOnly = true;
            txtTotalPrice.ReadOnly = true;
            txtDescription.ReadOnly = true;
            dtpDate.Enabled = false;

        }

        public void UCHistory_Load(object sender, EventArgs e)
        {
            dgvHistory.DataSource = orderController.GetHistory();
            txtDescription.Enabled = false;
            txtId.Enabled = false;
            txtStaff.Enabled = false;
            txtSupplier.Enabled = false;
            txtWarehouse.Enabled = false;
            txtTotalPrice.Enabled = false;
        }

        private void dgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idOrder = Int32.Parse(dgvHistory.CurrentRow.Cells[1].Value.ToString());
            Order order = orderController.FindOrderById(idOrder);
            dgvDetailOder.DataSource = orderController.FindOderDetailById(idOrder);
            foreach (DataGridViewRow row in dgvDetailOder.Rows)
            {
                row.Cells[0].Value = row.Index + 1;
                int quantity = Int32.Parse(row.Cells["col_quantity"].Value.ToString());
                decimal price = decimal.Parse(row.Cells["col_price"].Value.ToString());
                row.Cells["col_totalPrice"].Value = quantity * price;
            }
            txtId.Text = order.IdOrder.ToString();
            txtStaff.Text = order.IdStaff.ToString();
            txtSupplier.Text = order.Supplier;
            txtWarehouse.Text = order.Warehouse;
            txtTotalPrice.Text = order.Total.ToString();
            txtDescription.Text = dgvHistory.CurrentRow.Cells[2].Value.ToString();
            dtpDate.Value = order.OrderDate;

        }

        private void dgvDetailOder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
