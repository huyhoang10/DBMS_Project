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
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
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
            txtName.ReadOnly = false;
            txtContact.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtNote.ReadOnly = false;
        }

        private void ReadOnlyTxt()
        {
            txtId.ReadOnly = true;
            txtName.ReadOnly = true;
            txtContact.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtNote.ReadOnly = true;

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
    }
}
