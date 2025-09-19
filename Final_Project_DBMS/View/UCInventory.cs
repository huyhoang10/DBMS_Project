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
        InventoryController inventoryController = new InventoryController();
        WarehouseController warehouseController = new WarehouseController();
        public UCInventory()
        {
            InitializeComponent();
        }

        public void UCWarehouse_Load(object sender, EventArgs e)
        {
            LoadCmbWarehouse();
            LoadDgvInventory();          
        }

        private void LoadDgvInventory()
        {
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.DataSource = inventoryController.GetInventoryByWarehouse(cmbWarehouse.Text);
            
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
            dgvInventory.DataSource = inventoryController.FindProductInventory(txtSearch.Text,cmbWarehouse.Text);
        }

        private void LoadCmbWarehouse()
        {
            cmbWarehouse.Items.Clear();
            List<Warehouse> warehouses = warehouseController.GetAllWarehouse();
            foreach(var item in warehouses)
            {
                cmbWarehouse.Items.Add(item.Name);
            }
            cmbWarehouse.SelectedIndex = 0;
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDgvInventory();
        }
    }
}
