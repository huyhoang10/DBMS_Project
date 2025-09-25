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
using System.Globalization;

namespace Final_Project_DBMS.View
{
    public partial class UCStocking : UserControl
    {
        OrderController orderController = new OrderController();
        ProductController productController = new ProductController();
        List<Product> allProducts;
        List<Product> availableProducts;
        public UCStocking()
        {
            InitializeComponent();
        }

        public void UCStocking_Load(object sender, EventArgs e)
        {
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            allProducts = productController.GetAllProduct();
            availableProducts = new List<Product>(allProducts); // clone danh sách
            LoadDgvOrderExpect();
            dgvDetailOder.UserDeletingRow += dgvDetailOder_UserDeletingRow;
            dgvDetailOder.CellValueChanged += dgvDetailOder_CellValueChanged;
            dgvDetailOder.DefaultValuesNeeded += dgvDetailOder_DefaultValuesNeeded;
            btnReset_Click(sender, e);
            dgvDetailOder.Enabled = true;
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
        private void LoadDgvOrderExpect()
        {
            dgvOrderExpect.AutoGenerateColumns = false;
            dgvOrderExpect.DataSource = orderController.GetAllOrderExpect();

        }

        private void LoadOrderDetails(int orderId)
        {
            dgvOrderDetailExpect.Rows.Clear();
            var orderDetails = orderController.FindOderDetailById(orderId);

            int stt = 0;
            foreach (var detail in orderDetails)
            {
                stt++;
                dgvOrderDetailExpect.Rows.Add();
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_indexExpect"].Value = stt.ToString();
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_idProductExpect"].Value = detail.IdProduct;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_nameProductExpect"].Value = detail.NameProduct;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_priceExpect"].Value = detail.Price;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_unitExpect"].Value = detail.Unit;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_quantityExpect"].Value = detail.Quantity;
                dgvOrderDetailExpect.Rows[stt - 1].Cells["col_totalPriceExpect"].Value = detail.Quantity*detail.Price;
            }

        }

        private void dgvOrderExpect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Order order = (Order)dgvOrderExpect.Rows[e.RowIndex].DataBoundItem;
            LoadOrderDetails(order.IdOrder);
            dtpDate.Text = order.OrderDate.ToString();
            dgvDetailOder.Enabled = true;
            btnAccept.Enabled = true;
            btnSame.Enabled = true;

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
                var cell = dgvDetailOder.Rows[e.RowIndex].Cells[1];
                if (cell.Value == null)
                    return;

                foreach (DataGridViewRow row in dgvDetailOder.Rows)
                {
                    if (row.Index == e.RowIndex) continue; // bỏ qua dòng hiện tại
                    if (row.Cells[1].Value != null && idProduct == Int32.Parse(row.Cells[1].Value.ToString()))
                    {
                        MessageBox.Show("Sản phẩm đã được chọn!", "Thông báo");
                        cell.Value = null;
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

        }

        private void dgvDetailOder_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = dgvDetailOder.Rows[e.RowIndex];
            if (row.IsNewRow) return; // bỏ qua dòng ảo

            // Ví dụ: cột 1 (ID), cột 3 (Price), cột 5 (Quantity) phải có dữ liệu
            if (row.Cells[1].Value == null || string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()) ||
                row.Cells[3].Value == null || string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()) ||
                row.Cells[5].Value == null || string.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi sang dòng mới.",
                                "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                e.Cancel = true; // ngăn không cho rời dòng
            }
        }

        private void btnSame_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvDetailOder.Rows.Clear();
            dgvDetailOder.Enabled = false;
            dgvOrderDetailExpect.Rows.Clear();
            dgvOrderDetailExpect.Enabled = false;
            btnAccept.Enabled = false;
            btnSame.Enabled = false;
            txtId.Text = "";
            txtId.Enabled = false;
            dtpDate.Value = DateTime.Now;
            dtpDate.Enabled = false;

        }
    }
}
