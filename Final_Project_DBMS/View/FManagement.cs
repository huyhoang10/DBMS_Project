using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_DBMS
{
    public partial class FManagement : Form
    {
        public FManagement()
        {
            InitializeComponent();
        }


        private void btnSupplier_Click(object sender, EventArgs e)
        {
            ChooseBtn(btnSupplier);
            ucSupplier1.BringToFront();
        }


        private void btnStocking_Click(object sender, EventArgs e)
        {
            ChooseBtn(btnStocking);
            ucStocking1.BringToFront();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            ChooseBtn(btnHistory);
            ucHistory1.BringToFront();
        }

        private void btnExpectOrder_Click(object sender, EventArgs e)
        {
            ChooseBtn(btnExpectOrder);
            ucExpectedGoodsRecieve1.BringToFront();
        }

        private void FManagement_Load(object sender, EventArgs e)
        {
            btnHome_Click(sender, e);
        }


        private void ChooseBtn(Guna2Button btn)
        {
            btnInventory.FillColor = Color.FromArgb(107, 98, 89);
            btnExpectOrder.FillColor = Color.FromArgb(107, 98, 89);
            btnSupplier.FillColor = Color.FromArgb(107, 98, 89);
            btnStocking.FillColor = Color.FromArgb(107, 98, 89);
            btnHistory.FillColor = Color.FromArgb(107, 98, 89);
            btnWarehouse.FillColor = Color.FromArgb(107, 98, 89);
            btnProduct.FillColor = Color.FromArgb(107, 98, 89);
            btnHome.FillColor = Color.FromArgb(107, 98, 89);
            btnStaff.FillColor = Color.FromArgb(107, 98, 89);
            btn.FillColor = Color.DarkRed;

        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            ucWarehouse1.BringToFront();
            ChooseBtn(btnWarehouse);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ucProduct1.BringToFront();
            ChooseBtn(btnProduct);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            ucInventory1.UCWarehouse_Load(sender, e);
            ucInventory1.BringToFront();
            ChooseBtn(btnInventory);
            
        }

        private void ucProduct1_Load(object sender, EventArgs e)
        {

        }

        private void ucInventory1_Load(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ucDashboard1.UCDashboard_Load(sender, e);
            ucDashboard1.BringToFront();
            ChooseBtn(btnHome);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ucStaff1.UCStaff_Load(sender, e);
            ucStaff1.BringToFront();
            ChooseBtn(btnStaff);
        }
    }
}
