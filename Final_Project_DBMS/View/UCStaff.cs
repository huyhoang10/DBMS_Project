using Final_Project_DBMS.Controller;
using Final_Project_DBMS.Model;
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
    public partial class UCStaff : UserControl
    {
        StaffController staffController = new StaffController();
        public UCStaff()
        {
            InitializeComponent();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        public void UCStaff_Load(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
            txtIdStaff.Enabled = false;
            txtIdUser.Enabled = false;
            
        }

        private void LoadDgv() { 
            dgvStaff.DataSource = staffController.GetAllStaff();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtCccd.Text == "" || txtUserName.Text == "" 
                || txtPassword.Text == "" || cmbSex.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            else
            {
                try
                {
                    Staff staff = new Staff();
                    staff.NameStaff = txtName.Text;
                    staff.Cccd = txtCccd.Text;
                    staff.BirthDay = dtpBirthday.Value;
                    staff.Sex = cmbSex.SelectedItem.ToString();
                    staff.UserName = txtUserName.Text;
                    staff.Password = txtPassword.Text;
                    //staff.IdRole = Int32.Parse(cmbRoleId.SelectedItem.ToString());
                    if(!rdbManager.Checked && !rdbStaff.Checked)
                    {
                        MessageBox.Show("Vui lòng chọn vai trò");
                        return;
                    }
                    if (rdbManager.Checked) staff.IdRole = 1;
                    if (rdbStaff.Checked) staff.IdRole = 2;
                    staffController.AddStaff(staff);
                    MessageBox.Show("Thêm nhân viên thành công");
                    btnReset_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtIdStaff.Text = "";
            txtIdUser.Text = "";
            txtName.Text = "";
            txtCccd.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";

            //txtCccd.Enabled = false;
            //txtName.Enabled = false;
            //txtUserName.Enabled = false;
            //txtPassword.Enabled = false;
            //cmbRoleId.Enabled = false;
            //cmbSex.Enabled = false;
            
            cmbSex.SelectedIndex = 0;
            dtpBirthday.Value = DateTime.Now;
            txtName.Text = "";
            LoadDgv();
        }
    }
}
