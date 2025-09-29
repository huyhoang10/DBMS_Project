using Final_Project_DBMS.Controller;
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
using System.Xml;

namespace Final_Project_DBMS.View
{
    public partial class UCWarehouse : UserControl
    {
        WarehouseController warehouseController = new WarehouseController();
        public UCWarehouse()
        {
            InitializeComponent();
        }

        public void UCWarehouse_Load(object sender, EventArgs e)
        {
            LoadDgvWarehouse();
            txtId.Enabled = false;
            btnReset_Click(sender, e);
        }

        private void LoadDgvWarehouse()
        {
            dgvWarehouse.DataSource = warehouseController.GetAllWarehouse();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtId.Text = "";
            txtName.Text = "";
            txtAddress.Enabled = true;
            txtName.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin");
                return;
            }
            Warehouse warehouse = new Warehouse();
            if(txtId.Text == "")
            {
                warehouse.Name = txtName.Text;
                warehouse.Address = txtAddress.Text;
                warehouseController.InsertWarehouse(warehouse);
                MessageBox.Show("Thêm nhà kho thành công.", "Thông báo");
                LoadDgvWarehouse();
            }
            else
            {
                warehouse.Id = int.Parse(txtId.Text);
                warehouse.Name = txtName.Text;
                warehouse.Address = txtAddress.Text;
                warehouseController.UpdateWarehouse(warehouse);
                MessageBox.Show("Cập nhật nhà kho thành công.", "Thông báo");
                LoadDgvWarehouse();
            }
            btnReset_Click(sender,e);
        }

        private void dgvWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0 && e.ColumnIndex >= 0)
            {
                txtId.Text = dgvWarehouse.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dgvWarehouse.CurrentRow.Cells[1].Value.ToString();
                txtAddress.Text = dgvWarehouse.CurrentRow.Cells[2].Value.ToString();
                txtName.Enabled = true;
                txtAddress.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnAdd.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtAddress.Enabled = false;
            txtId.Text = "";
            txtId.Enabled = false;
            txtName.Text = "";
            txtName.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            LoadDgvWarehouse();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn thuộc tính cần xóa");
                    return;
                }
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thuộc tính này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    warehouseController.DeleteWarehouse(Int32.Parse(txtId.Text.ToString()));
                    MessageBox.Show("Xóa thành công");
                    LoadDgvWarehouse();
                    btnReset_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }
    }
}
