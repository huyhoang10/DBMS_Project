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
using Final_Project_DBMS.Dao;
using Final_Project_DBMS.Utils;

namespace Final_Project_DBMS.View
{
    public partial class FLogin : Form
    {
        
        public FLogin()
        {
            InitializeComponent();
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi đăng nhập");
            }
            else {
                DBConnect.connectionString = "Data Source=.;Initial Catalog=NhapHang;User Id=" + txtUsername.Text + ";Password=" + txtPassword.Text;
                UserController userControl = new UserController();
                StaffController staffControl = new StaffController();
                int role = userControl.Login(txtUsername.Text, txtPassword.Text);
                if (role == 0) { 
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập");
                }
                if(role == 1)
                {
                    Constants.staffLogin = staffControl.GetStaffByUserName(txtUsername.Text);
                    this.Hide();
                    FManagement fManagement = new FManagement();
                    fManagement.ShowDialog();
                    this.Close();
                }
                if(role == 2)
                {
                    Constants.staffLogin = staffControl.GetStaffByUserName(txtUsername.Text);
                    this.Hide();
                    FStaff fStaff = new FStaff();
                    fStaff.ShowDialog();
                    this.Close();
                }
            }
            
        }
    }
}
