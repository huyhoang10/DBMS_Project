using Final_Project_DBMS.Controller;
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Model;
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
    public partial class UCInventory : UserControl
    {
        ProductController productController = new ProductController();
        WarehouseController warehouseController = new WarehouseController();
        public UCInventory()
        {
            InitializeComponent();
        }

        private void UCWarehouse_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            LoadDgvProduct();
            LoadCmbWarehouse();
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
            UCWarehouse_Load(sender,e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FProduct fProduct = new FProduct();
            fProduct.ShowDialog();
            UCWarehouse_Load(sender,e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProduct.DataSource = productController.FindProductByName(txtSearch.Text);
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex>=0 && e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvProduct.CurrentRow.Cells[0].Value.ToString());
                Constants.choosedProduct = productController.GetProductById(id);
            }
            btnEdit.Enabled = true;
        }

        private void LoadCmbWarehouse()
        {
            cmbWarehouse.Items.Clear();
            List<Warehouse> warehouses = warehouseController.GetAllWarehouse();
            foreach(var item in warehouses)
            {
                cmbWarehouse.Items.Add(item.Name);
            }
        }
    }
}
