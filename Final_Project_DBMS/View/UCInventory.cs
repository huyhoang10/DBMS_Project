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
            btnReset_Click(sender, e);
                     
        }

        private void LoadDgvInventory()
        {
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.ReadOnly = true;
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
            cmbWarehouse.DataSource = warehouseController.GetAllWarehouse(); 
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Id";

        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDgvInventory();
            txtIdProduct.Text = "";
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdProduct.Text = dgvInventory.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idWarehouse = int.Parse(cmbWarehouse.SelectedValue.ToString());
            try
            {
                if (txtIdProduct.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa");
                    return;
                }
                int idProduct = Int32.Parse(txtIdProduct.Text.ToString());
                var confirmResult = MessageBox.Show("Bạn có chắc chắn xóa sản phẩm này ra khỏi kho?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    inventoryController.DeleteProductInventory(idWarehouse,idProduct);
                    MessageBox.Show("Xóa thành công");
                    LoadDgvInventory();
                    btnReset_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadCmbWarehouse();
            cmbWarehouse.SelectedIndex = 0;
            LoadDgvInventory();
        }
    }
}
