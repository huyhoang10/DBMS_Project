using Final_Project_DBMS.Controller;
using Final_Project_DBMS.Dao;
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
    public partial class UCWarehouse : UserControl
    {
        ProductController productController = new ProductController();
        public UCWarehouse()
        {
            InitializeComponent();
        }

        private void UCWarehouse_Load(object sender, EventArgs e)
        {
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
            fProduct.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FProduct fProduct = new FProduct();
            fProduct.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProduct.DataSource = productController.FindProductByName(txtSearch.Text);
        }
    }
}
