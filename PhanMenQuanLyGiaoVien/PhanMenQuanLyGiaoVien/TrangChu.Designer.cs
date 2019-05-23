namespace PhanMenQuanLyGiaoVien
{
    partial class TrangChu
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDanhSachGiaoVien = new System.Windows.Forms.TabPage();
            this.pnDataGr = new System.Windows.Forms.Panel();
            this.dgvGiaoVien = new System.Windows.Forms.DataGridView();
            this.pnKhoa = new System.Windows.Forms.Panel();
            this.pnMenu = new System.Windows.Forms.Panel();
            this.dtpNgayXem = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.tabTongHopTai = new System.Windows.Forms.TabPage();
            this.pnTongHopTai = new System.Windows.Forms.Panel();
            this.pnTitleTongHopTai = new System.Windows.Forms.Panel();
            this.cbKiHoc = new System.Windows.Forms.ComboBox();
            this.cbNamHoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbKhoa = new System.Windows.Forms.ComboBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabNhanLuc = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvTKNhanLuc = new System.Windows.Forms.DataGridView();
            this.pnTitleTKNhanLuc = new System.Windows.Forms.Panel();
            this.dtpThongNhanLuc = new System.Windows.Forms.DateTimePicker();
            this.btnTKNhanLuc = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabDanhSachGiaoVien.SuspendLayout();
            this.pnDataGr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoVien)).BeginInit();
            this.pnMenu.SuspendLayout();
            this.tabTongHopTai.SuspendLayout();
            this.pnTitleTongHopTai.SuspendLayout();
            this.tabNhanLuc.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTKNhanLuc)).BeginInit();
            this.pnTitleTKNhanLuc.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDanhSachGiaoVien);
            this.tabControl1.Controls.Add(this.tabTongHopTai);
            this.tabControl1.Controls.Add(this.tabNhanLuc);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1019, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDanhSachGiaoVien
            // 
            this.tabDanhSachGiaoVien.Controls.Add(this.pnDataGr);
            this.tabDanhSachGiaoVien.Controls.Add(this.pnKhoa);
            this.tabDanhSachGiaoVien.Controls.Add(this.pnMenu);
            this.tabDanhSachGiaoVien.Location = new System.Drawing.Point(4, 22);
            this.tabDanhSachGiaoVien.Name = "tabDanhSachGiaoVien";
            this.tabDanhSachGiaoVien.Padding = new System.Windows.Forms.Padding(3);
            this.tabDanhSachGiaoVien.Size = new System.Drawing.Size(1011, 424);
            this.tabDanhSachGiaoVien.TabIndex = 0;
            this.tabDanhSachGiaoVien.Text = "Danh Sách Giáo Vien";
            this.tabDanhSachGiaoVien.UseVisualStyleBackColor = true;
            // 
            // pnDataGr
            // 
            this.pnDataGr.Controls.Add(this.dgvGiaoVien);
            this.pnDataGr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDataGr.Location = new System.Drawing.Point(223, 32);
            this.pnDataGr.Name = "pnDataGr";
            this.pnDataGr.Size = new System.Drawing.Size(785, 389);
            this.pnDataGr.TabIndex = 2;
            // 
            // dgvGiaoVien
            // 
            this.dgvGiaoVien.AllowUserToAddRows = false;
            this.dgvGiaoVien.AllowUserToDeleteRows = false;
            this.dgvGiaoVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiaoVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiaoVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGiaoVien.Location = new System.Drawing.Point(0, 0);
            this.dgvGiaoVien.Name = "dgvGiaoVien";
            this.dgvGiaoVien.ReadOnly = true;
            this.dgvGiaoVien.Size = new System.Drawing.Size(785, 389);
            this.dgvGiaoVien.TabIndex = 0;
            // 
            // pnKhoa
            // 
            this.pnKhoa.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnKhoa.Location = new System.Drawing.Point(3, 32);
            this.pnKhoa.Name = "pnKhoa";
            this.pnKhoa.Size = new System.Drawing.Size(220, 389);
            this.pnKhoa.TabIndex = 1;
            // 
            // pnMenu
            // 
            this.pnMenu.Controls.Add(this.dtpNgayXem);
            this.pnMenu.Controls.Add(this.label1);
            this.pnMenu.Controls.Add(this.btnTimKiem);
            this.pnMenu.Controls.Add(this.txtTimKiem);
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnMenu.Location = new System.Drawing.Point(3, 3);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(1005, 29);
            this.pnMenu.TabIndex = 0;
            // 
            // dtpNgayXem
            // 
            this.dtpNgayXem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayXem.Location = new System.Drawing.Point(97, 5);
            this.dtpNgayXem.MaxDate = new System.DateTime(2019, 5, 20, 0, 0, 0, 0);
            this.dtpNgayXem.Name = "dtpNgayXem";
            this.dtpNgayXem.Size = new System.Drawing.Size(112, 20);
            this.dtpNgayXem.TabIndex = 4;
            this.dtpNgayXem.Value = new System.DateTime(2019, 5, 20, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(27, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chọn Ngày";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(917, 3);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(40, 20);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(504, 3);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(409, 20);
            this.txtTimKiem.TabIndex = 0;
            // 
            // tabTongHopTai
            // 
            this.tabTongHopTai.Controls.Add(this.pnTongHopTai);
            this.tabTongHopTai.Controls.Add(this.pnTitleTongHopTai);
            this.tabTongHopTai.Location = new System.Drawing.Point(4, 22);
            this.tabTongHopTai.Name = "tabTongHopTai";
            this.tabTongHopTai.Padding = new System.Windows.Forms.Padding(3);
            this.tabTongHopTai.Size = new System.Drawing.Size(1011, 424);
            this.tabTongHopTai.TabIndex = 1;
            this.tabTongHopTai.Text = "Tổng Hợp Tải";
            this.tabTongHopTai.UseVisualStyleBackColor = true;
            // 
            // pnTongHopTai
            // 
            this.pnTongHopTai.AutoScroll = true;
            this.pnTongHopTai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTongHopTai.Location = new System.Drawing.Point(3, 53);
            this.pnTongHopTai.Name = "pnTongHopTai";
            this.pnTongHopTai.Size = new System.Drawing.Size(1005, 368);
            this.pnTongHopTai.TabIndex = 1;
            // 
            // pnTitleTongHopTai
            // 
            this.pnTitleTongHopTai.Controls.Add(this.cbKiHoc);
            this.pnTitleTongHopTai.Controls.Add(this.cbNamHoc);
            this.pnTitleTongHopTai.Controls.Add(this.label4);
            this.pnTitleTongHopTai.Controls.Add(this.cbKhoa);
            this.pnTitleTongHopTai.Controls.Add(this.btnThongKe);
            this.pnTitleTongHopTai.Controls.Add(this.label3);
            this.pnTitleTongHopTai.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitleTongHopTai.Location = new System.Drawing.Point(3, 3);
            this.pnTitleTongHopTai.Name = "pnTitleTongHopTai";
            this.pnTitleTongHopTai.Size = new System.Drawing.Size(1005, 50);
            this.pnTitleTongHopTai.TabIndex = 0;
            // 
            // cbKiHoc
            // 
            this.cbKiHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKiHoc.FormattingEnabled = true;
            this.cbKiHoc.Items.AddRange(new object[] {
            "Kì 1",
            "Kì 2"});
            this.cbKiHoc.Location = new System.Drawing.Point(861, 9);
            this.cbKiHoc.Name = "cbKiHoc";
            this.cbKiHoc.Size = new System.Drawing.Size(61, 28);
            this.cbKiHoc.TabIndex = 6;
            // 
            // cbNamHoc
            // 
            this.cbNamHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNamHoc.FormattingEnabled = true;
            this.cbNamHoc.Items.AddRange(new object[] {
            "2005-2006",
            "2006-2007",
            "2007-2008",
            "2008-2009",
            "2009-2010",
            "2010-2011"});
            this.cbNamHoc.Location = new System.Drawing.Point(753, 9);
            this.cbNamHoc.Name = "cbNamHoc";
            this.cbNamHoc.Size = new System.Drawing.Size(100, 28);
            this.cbNamHoc.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(635, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "NĂM HỌC:";
            // 
            // cbKhoa
            // 
            this.cbKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhoa.FormattingEnabled = true;
            this.cbKhoa.Location = new System.Drawing.Point(508, 8);
            this.cbKhoa.Name = "cbKhoa";
            this.cbKhoa.Size = new System.Drawing.Size(121, 28);
            this.cbKhoa.TabIndex = 5;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(929, 11);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(63, 23);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(498, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "TỔNG HỢP TẢI CÔNG TÁC CỦA GIÁO VIÊN KHOA:";
            // 
            // tabNhanLuc
            // 
            this.tabNhanLuc.Controls.Add(this.panel3);
            this.tabNhanLuc.Controls.Add(this.pnTitleTKNhanLuc);
            this.tabNhanLuc.Location = new System.Drawing.Point(4, 22);
            this.tabNhanLuc.Name = "tabNhanLuc";
            this.tabNhanLuc.Padding = new System.Windows.Forms.Padding(3);
            this.tabNhanLuc.Size = new System.Drawing.Size(1011, 424);
            this.tabNhanLuc.TabIndex = 2;
            this.tabNhanLuc.Text = "Thống Kê Nhân Lực";
            this.tabNhanLuc.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvTKNhanLuc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1005, 370);
            this.panel3.TabIndex = 2;
            // 
            // dgvTKNhanLuc
            // 
            this.dgvTKNhanLuc.AllowUserToAddRows = false;
            this.dgvTKNhanLuc.AllowUserToDeleteRows = false;
            this.dgvTKNhanLuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTKNhanLuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTKNhanLuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTKNhanLuc.Location = new System.Drawing.Point(0, 0);
            this.dgvTKNhanLuc.Name = "dgvTKNhanLuc";
            this.dgvTKNhanLuc.ReadOnly = true;
            this.dgvTKNhanLuc.Size = new System.Drawing.Size(1005, 370);
            this.dgvTKNhanLuc.TabIndex = 0;
            // 
            // pnTitleTKNhanLuc
            // 
            this.pnTitleTKNhanLuc.Controls.Add(this.dtpThongNhanLuc);
            this.pnTitleTKNhanLuc.Controls.Add(this.btnTKNhanLuc);
            this.pnTitleTKNhanLuc.Controls.Add(this.label2);
            this.pnTitleTKNhanLuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitleTKNhanLuc.Location = new System.Drawing.Point(3, 3);
            this.pnTitleTKNhanLuc.Name = "pnTitleTKNhanLuc";
            this.pnTitleTKNhanLuc.Size = new System.Drawing.Size(1005, 48);
            this.pnTitleTKNhanLuc.TabIndex = 1;
            // 
            // dtpThongNhanLuc
            // 
            this.dtpThongNhanLuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThongNhanLuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThongNhanLuc.Location = new System.Drawing.Point(563, 11);
            this.dtpThongNhanLuc.Name = "dtpThongNhanLuc";
            this.dtpThongNhanLuc.Size = new System.Drawing.Size(136, 26);
            this.dtpThongNhanLuc.TabIndex = 5;
            // 
            // btnTKNhanLuc
            // 
            this.btnTKNhanLuc.Location = new System.Drawing.Point(705, 13);
            this.btnTKNhanLuc.Name = "btnTKNhanLuc";
            this.btnTKNhanLuc.Size = new System.Drawing.Size(75, 23);
            this.btnTKNhanLuc.TabIndex = 4;
            this.btnTKNhanLuc.Text = "Thống Kê";
            this.btnTKNhanLuc.UseVisualStyleBackColor = true;
            this.btnTKNhanLuc.Click += new System.EventHandler(this.btnTKNhanLuc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "THỐNG KÊ NHÂN LỰC ĐẾN HẾT NGÀY";
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "TrangChu";
            this.Text = "Trang Chủ";
            this.tabControl1.ResumeLayout(false);
            this.tabDanhSachGiaoVien.ResumeLayout(false);
            this.pnDataGr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoVien)).EndInit();
            this.pnMenu.ResumeLayout(false);
            this.pnMenu.PerformLayout();
            this.tabTongHopTai.ResumeLayout(false);
            this.pnTitleTongHopTai.ResumeLayout(false);
            this.pnTitleTongHopTai.PerformLayout();
            this.tabNhanLuc.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTKNhanLuc)).EndInit();
            this.pnTitleTKNhanLuc.ResumeLayout(false);
            this.pnTitleTKNhanLuc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDanhSachGiaoVien;
        private System.Windows.Forms.TabPage tabTongHopTai;
        private System.Windows.Forms.TabPage tabNhanLuc;
        private System.Windows.Forms.Panel pnDataGr;
        private System.Windows.Forms.DataGridView dgvGiaoVien;
        private System.Windows.Forms.Panel pnKhoa;
        private System.Windows.Forms.Panel pnMenu;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnTitleTongHopTai;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnTitleTKNhanLuc;
        private System.Windows.Forms.Button btnTKNhanLuc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvTKNhanLuc;
        private System.Windows.Forms.DateTimePicker dtpNgayXem;
        private System.Windows.Forms.DateTimePicker dtpThongNhanLuc;
        private System.Windows.Forms.ComboBox cbKiHoc;
        private System.Windows.Forms.ComboBox cbNamHoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbKhoa;
        private System.Windows.Forms.Panel pnTongHopTai;
    }
}

