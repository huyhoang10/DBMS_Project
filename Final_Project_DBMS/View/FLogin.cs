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

namespace Final_Project_DBMS.View
{
    public partial class FLogin : Form
    {
        UserController userControl = new UserController();
        public FLogin()
        {
            InitializeComponent();
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi đăng nhập");
            }
            else { 
                int role = userControl.Login(txtUsername.Text, txtPassword.Text);
                if (role == 0) { 
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập");
                }
                if(role == 1 || role == 2)
                {
                    this.Hide();
                    FManagement fManagement = new FManagement();
                    fManagement.ShowDialog();
                    this.Close();
                }
            }
            
        }
    }
}
