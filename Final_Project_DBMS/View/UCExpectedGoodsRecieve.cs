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
using Final_Project_DBMS.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Final_Project_DBMS.Dao;
using System.Globalization;
using Final_Project_DBMS.Utils;

namespace Final_Project_DBMS.View
{
    public partial class UCExpectedGoodsRecieve : UserControl
    {
        OrderController orderController = new OrderController();
        WarehouseController warehouseController = new WarehouseController();
        SupplierController supplierController = new SupplierController();
        ProductController productController = new ProductController();
        public UCExpectedGoodsRecieve()
        {
            InitializeComponent();
        }

        List<Product> allProducts;
        List<Product> availableProducts;

        public void UCExpectedGoodsRecieve_Load(object sender, EventArgs e)
        {
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            allProducts = productController.GetAllProduct();
            availableProducts = new List<Product>(allProducts); // clone danh sách

            dgvDetailOder.UserDeletingRow += dgvDetailOder_UserDeletingRow;
            dgvDetailOder.CellValueChanged += dgvDetailOder_CellValueChanged;
            dgvDetailOder.DefaultValuesNeeded += dgvDetailOder_DefaultValuesNeeded;

            btnReset_Click(sender, e);

            txtIdStaff.Text = Constants.staffLogin.IdStaff.ToString();

            txtIdStaff.Enabled = false;
            txtIdOder.Enabled = false;
            txtTotal.Enabled = false;
            btnSave.Enabled = false;

            DataGridViewColumn col_index = dgvDetailOder.Columns[0];
            col_index.ReadOnly = true;
            DataGridViewColumn col_unit = dgvDetailOder.Columns[4];
            col_unit.ReadOnly = true;
        }


        private void dgvDetailOder_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // Gán STT
            e.Row.Cells["col_Index"].Value = (dgvDetailOder.Rows.Count).ToString();

            // Setup datasource riêng cho combobox mỗi row
            var cellcmb = (DataGridViewComboBoxCell)e.Row.Cells["col_name"];
            cellcmb.DataSource = new List<Product>(availableProducts);
            cellcmb.DisplayMember = "Name";
            cellcmb.ValueMember = "Id";
            cellcmb.Value = null; // reset
        }

        private void LoadDgvOrderExpect()
        {
            dgvOrderExpect.AutoGenerateColumns = false;
            dgvOrderExpect.DataSource = orderController.GetAllOrderExpect();

        }

        

        private void LoadCmb()
        {
            cmbSupplier.DataSource = supplierController.GetAllSupllier();
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "Id";

            cmbWarehouse.DataSource = warehouseController.GetAllWarehouse();
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Id";
        }



        private void UpdateSTT()
        {
            for (int i = 0; i < dgvDetailOder.Rows.Count; i++)
            {
                dgvDetailOder.Rows[i].Cells["col_Index"].Value = (i + 1).ToString();
            }
        }

        private void dgvDetailOder_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var cell = (DataGridViewComboBoxCell)e.Row.Cells["col_name"];
            if (cell.Value != null)
            {
                int id = Convert.ToInt32(cell.Value);
                // Lấy lại từ allProducts để tránh mất dữ liệu
                var product = allProducts.FirstOrDefault(p => p.Id == id);

                if (product != null && !availableProducts.Any(p => p.Id == id))
                {
                    availableProducts.Add(product);
                }
            }
        }



        private void dgvDetailOder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Xử lý ComboBox
            if (dgvDetailOder.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                var cell = (DataGridViewComboBoxCell)dgvDetailOder.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    int id;
                    if (int.TryParse(cell.Value.ToString(), out id))
                    {
                        var product = availableProducts.FirstOrDefault(p => p.Id == id);
                        if (product != null)
                        {
                            dgvDetailOder.Rows[e.RowIndex].Cells[1].Value = product.Id.ToString();
                            dgvDetailOder.Rows[e.RowIndex].Cells[4].Value = product.Unit;
                            availableProducts.Remove(product);
                        }
                    }
                }
            }


            // Xu ly kiem tra kieu du lieu so
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {

                var priceCell = dgvDetailOder.Rows[e.RowIndex].Cells[3];
                var quantityCell = dgvDetailOder.Rows[e.RowIndex].Cells[5];

                if (priceCell.Value == null || quantityCell.Value == null)
                {
                    dgvDetailOder.Rows[e.RowIndex].Cells[6].Value = null;
                }

                // Validate Price nếu người dùng vừa sửa cột 3
                if (e.ColumnIndex == 3)
                {
                    if (priceCell.Value != null && !string.IsNullOrWhiteSpace(priceCell.Value.ToString()))
                    {
                        if (!decimal.TryParse(priceCell.Value.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal priceVal))
                        {
                            MessageBox.Show("Giá trị ở cột Price phải là số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            priceCell.Value = null; // hoặc string.Empty
                            return;
                        }
                    }
                }

                // Validate Quantity nếu người dùng vừa sửa cột 5
                if (e.ColumnIndex == 5)
                {
                    if (quantityCell.Value != null && !string.IsNullOrWhiteSpace(quantityCell.Value.ToString()))
                    {
                        if (!int.TryParse(quantityCell.Value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int qtyVal))
                        {
                            MessageBox.Show("Giá trị ở cột Quantity phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            quantityCell.Value = null;
                            return;
                        }
                    }
                }
                if (priceCell.Value != null && quantityCell.Value != null)
                {
                    decimal price;
                    int quantity;

                    bool isPriceValid = decimal.TryParse(priceCell.Value?.ToString(), out price);
                    bool isQuantityValid = int.TryParse(quantityCell.Value?.ToString(), out quantity);

                    if (!isPriceValid)
                    {
                        MessageBox.Show("Giá trị ở cột Price phải là số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        priceCell.Value = null;
                        return;
                    }

                    if (!isQuantityValid)
                    {
                        MessageBox.Show("Giá trị ở cột Quantity phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        quantityCell.Value = null;
                        return;
                    }

                    // Nếu cả hai hợp lệ, tính tổng
                    dgvDetailOder.Rows[e.RowIndex].Cells[6].Value = price * quantity;
                }

            }

            if (e.ColumnIndex == 1)
            {
                if (dgvDetailOder.CurrentCell.Value == null)
                    return;
                var idProductCell = dgvDetailOder.Rows[e.RowIndex].Cells[1];

                if (!int.TryParse(idProductCell.Value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int qtyVal))
                {
                    MessageBox.Show("Giá trị ở cột Mã SP phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    idProductCell.Value = null;
                    return;
                }
                int idProduct = Int32.Parse(dgvDetailOder.Rows[e.RowIndex].Cells[1].Value.ToString());
                Product isProduct = allProducts.FirstOrDefault(p => p.Id == idProduct);
                if (isProduct == null)
                {
                    MessageBox.Show("Mã sản phẩm không tồn tại!", "Thông báo");
                    dgvDetailOder.CurrentCell.Value = null;
                    return;
                }
                foreach (DataGridViewRow row in dgvDetailOder.Rows)
                {
                    if (row.Index == e.RowIndex) break; // bỏ qua dòng hiện tại
                    int idExistProduct = Int32.Parse(row.Cells[1].Value.ToString());
                    if (idProduct == idExistProduct)
                    {
                        MessageBox.Show("Sản phẩm đã được chọn!", "Thông báo");
                        dgvDetailOder.CurrentCell.Value = null;
                        return;
                    }

                }

                var product = availableProducts.FirstOrDefault(p => p.Id == idProduct);
                if (product != null)
                {
                    var cellCmb = (DataGridViewComboBoxCell)dgvDetailOder.Rows[e.RowIndex].Cells["col_name"];
                    cellCmb.DataSource = allProducts;
                    cellCmb.DisplayMember = "Name";
                    cellCmb.ValueMember = "Id";
                    cellCmb.Value = product.Id;
                    dgvDetailOder.Rows[e.RowIndex].Cells[4].Value = product.Unit;
                    availableProducts.Remove(product);
                }
            }
            if (e.ColumnIndex == 6)
            {
                decimal total = 0;
                foreach (DataGridViewRow row in dgvDetailOder.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[6].Value != null)
                    {
                        total += decimal.Parse(row.Cells[6].Value.ToString());
                        txtTotal.Text = total.ToString();
                    }
                }
            }
        }



        private void RefreshAllComboBoxes()
        {
            foreach (DataGridViewRow row in dgvDetailOder.Rows)
            {
                if (row.IsNewRow) continue;

                var cellcmb = (DataGridViewComboBoxCell)row.Cells["col_name"];
                cellcmb.DataSource = new List<Product>(availableProducts);
                cellcmb.DisplayMember = "Name";
                cellcmb.ValueMember = "Id";

                // Nếu cell đã có giá trị thì giữ nguyên
                if (row.Cells["col_name"].Value != null)
                {
                    cellcmb.Value = row.Cells["col_name"].Value;
                }
            }
        }

        private void dgvOrderExpect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) return;
            Order order = (Order)dgvOrderExpect.Rows[e.RowIndex].DataBoundItem;
            txtIdOder.Text = order.IdOrder.ToString();
            txtIdStaff.Text = order.IdStaff.ToString();
            dtpDate.Value = order.OrderDate;
            cmbSupplier.SelectedItem = order.Supplier;
            cmbWarehouse.SelectedItem = order.Warehouse;
            txtTotal.Text = order.Total.ToString();

            LoadOrderDetails(order.IdOrder);
            cmbWarehouse.Enabled = false;
            cmbSupplier.Enabled = false;
            btnDelete.Enabled = true;

        }

        private void LoadOrderDetails(int orderId)
        {
            dgvDetailOder.Rows.Clear();
            var orderDetails = orderController.FindOderDetailById(orderId);

            int stt = 0;
            foreach(var detail in orderDetails)
            {
                stt++;
                dgvDetailOder.Rows.Add();
                dgvDetailOder.Rows[stt - 1].Cells["col_index"].Value = stt.ToString();
                dgvDetailOder.Rows[stt - 1].Cells["col_id"].Value = detail.IdProduct;
                
                dgvDetailOder.Rows[stt - 1].Cells["col_price"].Value = detail.Price;
                dgvDetailOder.Rows[stt - 1].Cells["col_unit"].Value = detail.Unit;
                dgvDetailOder.Rows[stt - 1].Cells["col_quantity"].Value = detail.Quantity;

                var cellCmb = (DataGridViewComboBoxCell)dgvDetailOder.Rows[stt - 1].Cells["col_name"];
                cellCmb.DataSource = allProducts;
                cellCmb.DisplayMember = "Name";
                cellCmb.ValueMember = "Id";
                cellCmb.Value = detail.IdProduct;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvDetailOder.Rows.Clear();
            dgvDetailOder.Enabled = true;
            availableProducts = new List<Product>(allProducts);
            cmbSupplier.Enabled = true;
            cmbWarehouse.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            dgvOrderExpect.Enabled = false;
            cmbSupplier.SelectedIndex = 0;
            cmbWarehouse.SelectedIndex = 0;
        }

        private void dgvDetailOder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void dgvDetailOder_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    var row = dgvDetailOder.Rows[e.RowIndex];
        //    if (row.IsNewRow) return; // bỏ qua dòng ảo

        //    // Ví dụ: cột 1 (ID), cột 3 (Price), cột 5 (Quantity) phải có dữ liệu
        //    if (row.Cells[1].Value == null || string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()) ||
        //        row.Cells[3].Value == null || string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()) ||
        //        row.Cells[5].Value == null || string.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
        //    {
        //        MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi sang dòng mới.",
        //                        "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        e.Cancel = true; // ngăn không cho rời dòng
        //    }
        //}

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvDetailOder.Rows.Clear();
            dgvDetailOder.Enabled = false;
            txtIdOder.Text = "";
            txtTotal.Text = "";
            txtIdOder.Enabled = false;
            txtIdStaff.Enabled = false;
            dtpDate.Value = DateTime.Now;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            cmbSupplier.Enabled = false;
            cmbWarehouse.Enabled = false;
            btnAdd.Enabled = true;
            dgvOrderExpect.Enabled = true;
            LoadDgvOrderExpect();
            LoadCmb();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (DataGridViewRow row in dgvDetailOder.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[1].Value == null || row.Cells[3].Value == null || row.Cells[5] == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đơn hàng", "Thông báo");
                    return;
                }
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.IdProduct = Int32.Parse(row.Cells[1].Value.ToString());
                orderDetail.Price = decimal.Parse(row.Cells[3].Value.ToString());
                orderDetail.Quantity = Int32.Parse(row.Cells[5].Value.ToString());
                orderDetails.Add(orderDetail);
            }
            Order order = new Order();
            dtpDate.Value = DateTime.Now;
            order.OrderDate = dtpDate.Value;
            order.IdStaff = Int32.Parse(txtIdStaff.Text);
            order.Warehouse = cmbWarehouse.SelectedValue.ToString();
            order.Supplier = cmbSupplier.SelectedValue.ToString();
            
            orderController.InsertOrderExpect(orderDetails, order);
            LoadDgvOrderExpect();
            btnReset_Click(sender, e);
        }

        private void dgvDetailOder_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            decimal total = 0;
            int stt = 1;
            foreach (DataGridViewRow row in dgvDetailOder.Rows)
            {
                row.Cells[0].Value = stt++;
                if (row.IsNewRow) continue;
                if (row.Cells[6].Value != null)
                {
                    total += decimal.Parse(row.Cells[6].Value.ToString());
                    txtTotal.Text = total.ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy đơn hàng này?",
                "Cảnh báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                int idOrder = Int32.Parse(txtIdOder.Text.Trim());
                orderController.CanCelOrderExpect(idOrder);
                LoadDgvOrderExpect();
                btnReset_Click(sender, e);
            }
        }

    }
}
