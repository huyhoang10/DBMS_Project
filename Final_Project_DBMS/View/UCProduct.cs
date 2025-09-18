using Final_Project_DBMS.Controller;
using Final_Project_DBMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_DBMS.View
{
    public partial class UCProduct : UserControl
    {
        ProductController productController = new ProductController();
        public UCProduct()
        {
            InitializeComponent();
        }

        private void UCProduct_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            LoadDgvProduct();
        }

        private void LoadDgvProduct()
        {
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.DataSource = productController.GetAllProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FProduct fProduct = new FProduct();
            Constants.choosedProduct.Id = 0;
            fProduct.ShowDialog();
            UCProduct_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FProduct fProduct = new FProduct();
            fProduct.ShowDialog();
            UCProduct_Load(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProduct.DataSource = productController.FindProductByName(txtSearch.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            dgvProduct.DataSource = productController.FindProductByName(txtSearch.Text);
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvProduct.CurrentRow.Cells[0].Value.ToString());
                Constants.choosedProduct = productController.GetProductById(id);
            }
            btnEdit.Enabled = true;
        }
    }
}
