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
    public partial class UCSupplier : UserControl
    {
        SupplierController supplierController = new SupplierController();
        public UCSupplier()
        {
            InitializeComponent();
        }

        public void UCSupplier_Load(object sender, EventArgs e)
        {
            ReadOnlyTxt();
            btnReset_Click(sender, e);
            LoadDgvSupllier();
        }

        private void LoadDgvSupllier()
        {
            dgvSupplier.AutoGenerateColumns = false;
            dgvSupplier.DataSource = supplierController.GetAllSupllier();
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtId.Text = dgvSupplier.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dgvSupplier.CurrentRow.Cells[1].Value.ToString();
                txtContact.Text = dgvSupplier.CurrentRow.Cells[2].Value.ToString();
                txtAddress.Text = dgvSupplier.CurrentRow.Cells[3].Value.ToString();
                txtNote.Text = dgvSupplier.CurrentRow.Cells[4].Value.ToString();
            }
            EnableTxt();
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void EnableTxt()
        {
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtContact.Enabled = true;
            txtNote.Enabled = true;
        }

        private void ReadOnlyTxt()
        {
            txtName.Enabled = false;
            txtId.Enabled = false;
            txtAddress.Enabled = false;
            txtContact.Enabled = false;
            txtNote.Enabled = false;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            txtNote.Clear();
            
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
            LoadDgvSupllier();
            ReadOnlyTxt();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
            EnableTxt();
            btnSave.Enabled = true;
            btnAdd.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtAddress.Text == "" || txtContact.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            else
            {
                Supplier supplier = new Supplier();
                supplier.Name = txtName.Text;
                supplier.Address = txtAddress.Text;
                supplier.Contact = txtContact.Text;
                supplier.Note = txtNote.Text;
                if (txtId.Text != "")
                {
                    supplier.Id = int.Parse(txtId.Text);
                }
                supplierController.SaveSupplier(supplier);
                LoadDgvSupllier();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvSupplier.DataSource = supplierController.FindSupplier(txtSearch.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa");
                    return;
                }
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    supplierController.DeleteSupplier(Int32.Parse(txtId.Text.ToString()));
                    MessageBox.Show("Xóa thành công");
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
