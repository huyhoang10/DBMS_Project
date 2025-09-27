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
using Final_Project_DBMS.Utils;

namespace Final_Project_DBMS.View
{
    public partial class FAttributes : Form
    {
        AttributesController attributesController = new AttributesController();
        public FAttributes()
        {
            InitializeComponent();
        }

        private void FAttributes_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            txtName.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            LoadCmbAttribute();
            cmbAttribute.SelectedIndex = 0;
            LoadDgvAttribute(Constants.dicAttribute[cmbAttribute.Text.ToString()]);
            
        }

        private void LoadDgvAttribute(string nameAttribute)
        {
            dgvAttribute.DataSource = attributesController.GetDataAttributes(nameAttribute);
        }
        private void LoadCmbAttribute()
        {
            foreach (var item in Constants.dicAttribute)
            {
                cmbAttribute.Items.Add(item.Key);
            }
        }


        private void cmbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
            LoadDgvAttribute(Constants.dicAttribute[cmbAttribute.Text.ToString()]);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            txtId.Text = "";
            txtName.Text = "";
            txtName.Enabled = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            else
            {
                if(txtId.Text == "")
                {
                    // Insert
                    Model.Attributes attributes = new Model.Attributes();
                    attributes.Name = txtName.Text;
                    try
                    {
                        attributesController.InsertAttributes(Constants.dicAttribute[cmbAttribute.Text.ToString()], attributes);
                        MessageBox.Show("Thêm thành công");
                        LoadDgvAttribute(Constants.dicAttribute[cmbAttribute.Text.ToString()]);
                        btnReset_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
                else
                {
                    // Update
                    Model.Attributes attributes = new Model.Attributes();
                    attributes.Id = Int32.Parse(txtId.Text.ToString());
                    attributes.Name = txtName.Text;
                    try
                    {
                        attributesController.UpdateAttributes(Constants.dicAttribute[cmbAttribute.Text.ToString()], attributes);
                        MessageBox.Show("Cập nhật thành công");
                        LoadDgvAttribute(Constants.dicAttribute[cmbAttribute.Text.ToString()]);
                        btnReset_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            txtId.Text = "";
            txtName.Text = "";
            txtName.Enabled = false;
        }

        private void dgvAttribute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                DataGridViewRow row = this.dgvAttribute.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtName.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn thuộc tính cần xóa");
                    return;
                }
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thuộc tính này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    attributesController.DeleteAttributes(Constants.dicAttribute[cmbAttribute.Text.ToString()], Int32.Parse(txtId.Text.ToString()));
                    MessageBox.Show("Xóa thành công");
                    LoadDgvAttribute(Constants.dicAttribute[cmbAttribute.Text.ToString()]);
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
