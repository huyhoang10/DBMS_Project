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
            txtId.Enabled = false;
            LoadCmb();
            if (Constants.choosedProduct.Id != 0)
            {
                LoadDataProduct(Constants.choosedProduct);
            }
        }

        private void LoadCmb()
        {
            List<Attributes> categorys = attributesController.GetDataAttributes(Constants.dicAttribute["Phân loại"]);
            cmbCategory.Items.Clear();
            foreach (Attributes category in categorys) {
                Constants.dicCategory[category.Name] = category.Id;
                cmbCategory.Items.Add(category.Name);
            }
            List<Attributes> brands = attributesController.GetDataAttributes(Constants.dicAttribute["Thương hiệu"]);
            cmbBrand.Items.Clear();
            foreach (Attributes brand in brands)
            {
                Constants.dicBrand[brand.Name] = brand.Id;
                cmbBrand.Items.Add(brand.Name);
            }
            List<Attributes> colors = attributesController.GetDataAttributes(Constants.dicAttribute["Màu sắc"]);
            cmbColor.Items.Clear();
            foreach (Attributes color in colors)
            {
                Constants.dicColor[color.Name] = color.Id;
                cmbColor.Items.Add(color.Name);
            }
            List<Attributes> units = attributesController.GetDataAttributes(Constants.dicAttribute["Đơn vị tính"]);
            cmbUnit.Items.Clear();
            foreach (Attributes unit in units)
            {
                Constants.dicUnit[unit.Name] = unit.Id;
                cmbUnit.Items.Add(unit.Name);
            }
            List<Attributes> materials = attributesController.GetDataAttributes(Constants.dicAttribute["Chất liệu"]);
            cmbMaterial.Items.Clear();
            foreach (Attributes material in materials)
            {
                Constants.dicMaterial[material.Name] = material.Id;
                cmbMaterial.Items.Add(material.Name);
            }
            cmbCategory.SelectedIndex = 0;
            cmbBrand.SelectedIndex = 0;
            cmbColor.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 0;
            cmbMaterial.SelectedIndex = 0;


        }

        private void LoadDataProduct(Product product)
        {
            txtId.Text = product.Id.ToString();
            txtName.Text = product.Name;
            txtDescription.Text = product.Description;
            cmbBrand.SelectedItem = product.Brand;
            cmbCategory.SelectedItem = product.Category;
            cmbColor.SelectedItem = product.Color;
            cmbMaterial.SelectedItem = product.Material;
            txtSize.Text = product.Size;
            cmbUnit.SelectedItem = product.Unit;
            //txtQuantity.Text = product.Quantity.ToString();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            FAttributes fAttributes = new FAttributes();
            fAttributes.ShowDialog();
            LoadCmb();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Product product = new Product();
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;
                product.Brand = cmbBrand.Text;
                product.Category = cmbCategory.Text;
                product.Color = cmbColor.Text;
                product.Material = cmbMaterial.Text;
                product.Size = txtSize.Text;
                product.Unit = cmbUnit.Text;
                //product.Quantity = int.Parse(txtQuantity.Text);
                if (txtId.Text == "")
                {
                    // Insert
                    ProductController productController = new ProductController();
                    try
                    {
                        productController.InsertProduct(product);
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Update
                    product.Id = int.Parse(txtId.Text);
                    ProductController productController = new ProductController();
                    try
                    {
                        productController.UpdateProduct(product);
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
