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
    public partial class UCWarehouse : UserControl
    {
        WarehouseController warehouseController = new WarehouseController();
        public UCWarehouse()
        {
            InitializeComponent();
        }

        private void UCWarehouse_Load(object sender, EventArgs e)
        {
            LoadDgvWarehouse();
        }

        private void LoadDgvWarehouse()
        {
            dgvWarehouse.DataSource = warehouseController.GetAllWarehouse();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse();
            if(txtId.Text == "")
            {
                warehouse.Name = txtName.Text;
                warehouse.Address = txtAddress.Text;
                warehouseController.InsertWarehouse(warehouse);
                LoadDgvWarehouse();
            }
            else
            {
                warehouse.Id = int.Parse(txtId.Text);
                warehouse.Name = txtName.Text;
                warehouse.Address = txtAddress.Text;
                warehouseController.UpdateWarehouse(warehouse);
                LoadDgvWarehouse();
            }
        }
    }
}
