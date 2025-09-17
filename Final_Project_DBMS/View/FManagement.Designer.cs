namespace Final_Project_DBMS
{
    partial class FManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.line = new Guna.UI2.WinForms.Guna2Button();
            this.btnExpectOrder = new Guna.UI2.WinForms.Guna2Button();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnHistory = new Guna.UI2.WinForms.Guna2Button();
            this.btnStocking = new Guna.UI2.WinForms.Guna2Button();
            this.btnSupplier = new Guna.UI2.WinForms.Guna2Button();
            this.btnWarehouse = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.ucWarehouse1 = new Final_Project_DBMS.View.UCWarehouse();
            this.ucSupplier1 = new Final_Project_DBMS.View.UCSupplier();
            this.ucExpectedGoodsRecieve1 = new Final_Project_DBMS.View.UCExpectedGoodsRecieve();
            this.ucStocking1 = new Final_Project_DBMS.View.UCStocking();
            this.ucHistory1 = new Final_Project_DBMS.View.UCHistory();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(211)))), ((int)(((byte)(195)))));
            this.guna2Panel1.Controls.Add(this.line);
            this.guna2Panel1.Controls.Add(this.btnExpectOrder);
            this.guna2Panel1.Controls.Add(this.btnLogout);
            this.guna2Panel1.Controls.Add(this.btnHistory);
            this.guna2Panel1.Controls.Add(this.btnStocking);
            this.guna2Panel1.Controls.Add(this.btnSupplier);
            this.guna2Panel1.Controls.Add(this.btnWarehouse);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(300, 1000);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // line
            // 
            this.line.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.line.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.line.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.line.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.line.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.line.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.line.ForeColor = System.Drawing.Color.White;
            this.line.Location = new System.Drawing.Point(0, 250);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(300, 3);
            this.line.TabIndex = 6;
            // 
            // btnExpectOrder
            // 
            this.btnExpectOrder.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExpectOrder.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExpectOrder.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExpectOrder.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExpectOrder.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.btnExpectOrder.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpectOrder.ForeColor = System.Drawing.Color.White;
            this.btnExpectOrder.Location = new System.Drawing.Point(0, 442);
            this.btnExpectOrder.Name = "btnExpectOrder";
            this.btnExpectOrder.Size = new System.Drawing.Size(300, 60);
            this.btnExpectOrder.TabIndex = 5;
            this.btnExpectOrder.Text = "Đơn hàng dự kiến";
            this.btnExpectOrder.Click += new System.EventHandler(this.btnExpectOrder_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 881);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(300, 60);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Đăng xuất";
            // 
            // btnHistory
            // 
            this.btnHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHistory.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location = new System.Drawing.Point(0, 584);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(300, 60);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnStocking
            // 
            this.btnStocking.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStocking.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStocking.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStocking.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStocking.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.btnStocking.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStocking.ForeColor = System.Drawing.Color.White;
            this.btnStocking.Location = new System.Drawing.Point(0, 513);
            this.btnStocking.Name = "btnStocking";
            this.btnStocking.Size = new System.Drawing.Size(300, 60);
            this.btnStocking.TabIndex = 2;
            this.btnStocking.Text = "Nhập kho";
            this.btnStocking.Click += new System.EventHandler(this.btnStocking_Click);
            // 
            // btnSupplier
            // 
            this.btnSupplier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSupplier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSupplier.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSupplier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSupplier.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(98)))), ((int)(((byte)(89)))));
            this.btnSupplier.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplier.ForeColor = System.Drawing.Color.White;
            this.btnSupplier.Location = new System.Drawing.Point(0, 371);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(300, 60);
            this.btnSupplier.TabIndex = 1;
            this.btnSupplier.Text = "Nhà cung cấp";
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // btnWarehouse
            // 
            this.btnWarehouse.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWarehouse.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWarehouse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWarehouse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWarehouse.FillColor = System.Drawing.Color.DarkRed;
            this.btnWarehouse.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWarehouse.ForeColor = System.Drawing.Color.White;
            this.btnWarehouse.Location = new System.Drawing.Point(0, 300);
            this.btnWarehouse.Name = "btnWarehouse";
            this.btnWarehouse.Size = new System.Drawing.Size(300, 60);
            this.btnWarehouse.TabIndex = 0;
            this.btnWarehouse.Text = "Kho hàng";
            this.btnWarehouse.Click += new System.EventHandler(this.btnWarehouse_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel2.Controls.Add(this.ucWarehouse1);
            this.guna2Panel2.Controls.Add(this.ucSupplier1);
            this.guna2Panel2.Controls.Add(this.ucExpectedGoodsRecieve1);
            this.guna2Panel2.Controls.Add(this.ucStocking1);
            this.guna2Panel2.Controls.Add(this.ucHistory1);
            this.guna2Panel2.Location = new System.Drawing.Point(300, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1500, 1200);
            this.guna2Panel2.TabIndex = 1;
            // 
            // ucWarehouse1
            // 
            this.ucWarehouse1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucWarehouse1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ucWarehouse1.Location = new System.Drawing.Point(0, 0);
            this.ucWarehouse1.MaximumSize = new System.Drawing.Size(1750, 1035);
            this.ucWarehouse1.Name = "ucWarehouse1";
            this.ucWarehouse1.Size = new System.Drawing.Size(1500, 1000);
            this.ucWarehouse1.TabIndex = 0;
            this.ucWarehouse1.Load += new System.EventHandler(this.ucWarehouse1_Load);
            // 
            // ucSupplier1
            // 
            this.ucSupplier1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSupplier1.Location = new System.Drawing.Point(0, 0);
            this.ucSupplier1.MaximumSize = new System.Drawing.Size(1750, 1035);
            this.ucSupplier1.Name = "ucSupplier1";
            this.ucSupplier1.Size = new System.Drawing.Size(1500, 1000);
            this.ucSupplier1.TabIndex = 1;
            // 
            // ucExpectedGoodsRecieve1
            // 
            this.ucExpectedGoodsRecieve1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucExpectedGoodsRecieve1.Location = new System.Drawing.Point(0, 0);
            this.ucExpectedGoodsRecieve1.MaximumSize = new System.Drawing.Size(1750, 1035);
            this.ucExpectedGoodsRecieve1.Name = "ucExpectedGoodsRecieve1";
            this.ucExpectedGoodsRecieve1.Size = new System.Drawing.Size(1500, 1000);
            this.ucExpectedGoodsRecieve1.TabIndex = 2;
            // 
            // ucStocking1
            // 
            this.ucStocking1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucStocking1.Location = new System.Drawing.Point(0, 0);
            this.ucStocking1.MaximumSize = new System.Drawing.Size(1750, 1035);
            this.ucStocking1.Name = "ucStocking1";
            this.ucStocking1.Size = new System.Drawing.Size(1500, 1000);
            this.ucStocking1.TabIndex = 3;
            // 
            // ucHistory1
            // 
            this.ucHistory1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucHistory1.Location = new System.Drawing.Point(0, 0);
            this.ucHistory1.MaximumSize = new System.Drawing.Size(1750, 1035);
            this.ucHistory1.Name = "ucHistory1";
            this.ucHistory1.Size = new System.Drawing.Size(1500, 1000);
            this.ucHistory1.TabIndex = 4;
            // 
            // FManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 953);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "FManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FManagement_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Button btnWarehouse;
        private Guna.UI2.WinForms.Guna2Button btnExpectOrder;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Button btnHistory;
        private Guna.UI2.WinForms.Guna2Button btnStocking;
        private Guna.UI2.WinForms.Guna2Button btnSupplier;
        private View.UCWarehouse ucWarehouse1;
        private Guna.UI2.WinForms.Guna2Button line;
        private View.UCSupplier ucSupplier1;
        private View.UCHistory ucHistory1;
        private View.UCStocking ucStocking1;
        private View.UCExpectedGoodsRecieve ucExpectedGoodsRecieve1;
    }
}

