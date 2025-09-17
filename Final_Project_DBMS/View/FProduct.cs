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

namespace Final_Project_DBMS.View
{
    public partial class FProduct : Form
    {
        AttributesController attributesController = new AttributesController();
        public FProduct()
        {
            InitializeComponent();
        }

        private void FProduct_Load(object sender, EventArgs e)
        {
            LoadCmb();
        }

        private void LoadCmb()
        {
            List<Attributes> categorys = attributesController.GetDataAttributes(Constants.dicAttribute["Phân loại"]);
            foreach (Attributes category in categorys) {
                cmbCategory.Items.Add(category.Name+"-"+category.Id.ToString());
            }
            List<Attributes> brands = attributesController.GetDataAttributes(Constants.dicAttribute["Thương hiệu"]);
            foreach (Attributes brand in brands)
            {
                cmbBrand.Items.Add(brand.Name + "-" + brand.Id.ToString());
            }
            List<Attributes> colors = attributesController.GetDataAttributes(Constants.dicAttribute["Màu sắc"]);
            foreach (Attributes color in colors)
            {
                cmbColor.Items.Add(color.Name + "-" + color.Id.ToString());
            }
            List<Attributes> status = attributesController.GetDataAttributes(Constants.dicAttribute["Trạng thái"]);
            foreach (Attributes size in status)
            {
                cmbStatus.Items.Add(size.Name + "-" + size.Id.ToString());
            }
            List<Attributes> units = attributesController.GetDataAttributes(Constants.dicAttribute["Đơn vị tính"]);
            foreach (Attributes unit in units)
            {
                cmbUnit.Items.Add(unit.Name + "-" + unit.Id.ToString());
            }
            List<Attributes> materials = attributesController.GetDataAttributes(Constants.dicAttribute["Chất liệu"]);
            foreach (Attributes material in materials)
            {
                cmbMaterial.Items.Add(material.Name + "-" + material.Id.ToString());
            }
            cmbCategory.SelectedIndex = 0;
            cmbBrand.SelectedIndex = 0;
            cmbColor.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 0;
            cmbMaterial.SelectedIndex = 0;


        }
    }
}
