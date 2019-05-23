using PhanMenQuanLyGiaoVien.DAL;
using PhanMenQuanLyGiaoVien.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PhanMenQuanLyGiaoVien.Entity;
using PhanMenQuanLyGiaoVien.GUI;
using System.Data;

namespace PhanMenQuanLyGiaoVien
{
    public partial class TrangChu : Form
    {
        private int index;
        private List<Khoa> listKhoa;
        private IList<GiaoVien> listAllGV;
        public TrangChu()
        {
            InitializeComponent();
            

            TaiDuLieu();
            DanhMucKhoa();           
            dgvGiaoVien.RowHeaderMouseClick += DgvGiaoVien_RowHeaderMouseClick;
        }

        private void TaiDuLieu()
        {
            int inamBD = 2005;
            int inamKT = DateTime.Today.Year;
            for (int i = inamBD; i < inamKT; i++)
            {
                string namHoc = i.ToString() + "-" + (i + 1).ToString();
                cbNamHoc.Items.Add(namHoc);
            }

            dtpNgayXem.MaxDate = DateTime.Today;
            dtpNgayXem.Value = DateTime.Today;
            dtpThongNhanLuc.MaxDate = DateTime.Today;
            listKhoa = new KhoaDAL().LayTatKhoa();
            listAllGV = new GiaoVienDAL().LayTatCaGiaoVien(dtpNgayXem.Value.ToString());

            pnKhoa.BackColor = Color.WhiteSmoke;
            pnMenu.BackColor = Color.LightGray;
            pnTitleTKNhanLuc.BackColor = Color.LightGray;
            pnTitleTongHopTai.BackColor = Color.LightGray;
        }
        
        private void DanhMucKhoa()
        {
            int y = 90;         
            
            Label lbDM = new Label();
            lbDM.Top = 10;
            lbDM.Left = 10;
            lbDM.Font = ThamSo.fTitleB;
            lbDM.AutoSize = true;
            lbDM.Text = "Danh Mục Khoa";
            lbDM.Parent = pnKhoa;

            Label lbAll = new Label();
            lbAll.Top = 55;
            lbAll.Left = 10;
            lbAll.Cursor = Cursors.Hand;
            lbAll.Font = ThamSo.fTitle;
            lbAll.ForeColor = Color.DodgerBlue;
            lbAll.AutoSize = true;
            lbAll.Text = "Tất Cả Giáo Viên";
            lbAll.Parent = pnKhoa;
            lbAll.Click += LbAll_Click;

            index = 0;
            foreach (var item in listKhoa)
            {
                Label lb = new Label();
                lb.Top = y;
                lb.Left = 10;
                lb.Font = ThamSo.fTitle;
                lb.ForeColor = Color.DodgerBlue;
                lb.AutoSize = true;
                lb.Cursor = Cursors.Hand;
                lb.Text = item.TenKhoa;
                cbKhoa.Items.Add(item.TenKhoa);
                lb.Parent = pnKhoa;
                lb.Tag = index;
                index++;
                y += 35;

                lb.Click += Lb_Click;
            }
        }

        private void DoDuLieuDSGV(IList<GiaoVien> listGV)
        {
            BindingSource dts = new BindingSource();
            dts.DataSource = typeof(GiaoVien);
            dgvGiaoVien.DataSource = dts;
            foreach (var item in listGV)
            {
                dts.Add(item);
            }
            dgvGiaoVien.Columns["MaGiaoVien"].HeaderText = "Mã Giáo Viên";
            dgvGiaoVien.Columns["TenGiaoVien"].HeaderText = "Tên Giáo Viên";
            dgvGiaoVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvGiaoVien.Columns["DienThoai"].HeaderText = "Số Điện Thoại";
            dgvGiaoVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvGiaoVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvGiaoVien.Columns["QueQuan"].HeaderText = "Quê Quán";
            dgvGiaoVien.Columns["MaBoMon"].HeaderText = "Mã Bộ Môn";
            dgvGiaoVien.Columns["NgayChuyenDen"].HeaderText = "Ngày Chuyển Đến Bộ Môn";
        }

        private void doDuLieuHopTai(DataTable data, string TenBM)
        {          
            GroupBox gb = new GroupBox();
            gb.Text = TenBM;
            gb.Height = 200;
            gb.Dock = DockStyle.Top;
            gb.Parent = pnTongHopTai;
            DataGridView dgv = new DataGridView();
            dgv.Parent = gb;
            dgv.Dock = DockStyle.Fill;           
            dgv.DataSource = data;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

        }


        private void LbAll_Click(object sender, EventArgs e)
        {
            listAllGV = new GiaoVienDAL().LayTatCaGiaoVien(dtpNgayXem.Value.ToString());
            DoDuLieuDSGV(listAllGV);
        }

        private void Lb_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            int i = (int)lb.Tag;
            string maK = listKhoa[i].MaKhoa;
            var listGV = new GiaoVienDAL().LayGiaoVienTheoKhoa(maK, dtpNgayXem.Text);
            DoDuLieuDSGV(listGV);
        }

        private void DgvGiaoVien_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = dgvGiaoVien.CurrentRow.Index;
            string magv = dgvGiaoVien.Rows[i].Cells["MaGiaoVien"].Value.ToString();
            string[] namBD = (dgvGiaoVien.Rows[i].Cells["NgayChuyenDen"].Value.ToString()).Split('/');
            string[] namKT = dtpNgayXem.Text.Split('/');
            TrangCaNhan f = new TrangCaNhan(magv, namBD[2], namKT[2]);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnTKNhanLuc_Click(object sender, EventArgs e)
        {          
            var listTT = new List<ThongKeNhanLuc>();
            foreach (var item in listKhoa)
            {
                var tt = new ThongKeNhanLucDAL().ThongKe(item.MaKhoa, dtpThongNhanLuc.Text);
                listTT.Add(tt);              
            }
            BindingSource dts = new BindingSource();
            dts.DataSource = typeof(ThongKeNhanLuc);
            dgvTKNhanLuc.DataSource = dts;
            foreach (var item in listTT)
            {
                dts.Add(item);
            }
            
            dgvTKNhanLuc.Columns["TenKhoa"].HeaderText = "Tên Khoa";           
            dgvTKNhanLuc.Columns["TongSo"].HeaderText = "Tổng Số";
            dgvTKNhanLuc.Columns["GiaoSu"].HeaderText = "Giáo Sư";
            dgvTKNhanLuc.Columns["PhoGiaoSu"].HeaderText = "Phó Giáo Sư";
            dgvTKNhanLuc.Columns["TienSyKH"].HeaderText = "Tiến Sỹ Khoa Học";
            dgvTKNhanLuc.Columns["TienSy"].HeaderText = "Tiến Sỹ";
            dgvTKNhanLuc.Columns["ThacSy"].HeaderText = "Thạc Sỹ";
            dgvTKNhanLuc.Columns["DaiHoc"].HeaderText = "Đại Học";
            dgvTKNhanLuc.Columns["Khac"].HeaderText = "Khác";
        }

       
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            pnTongHopTai.Controls.Clear();
            if(cbKhoa.SelectedItem == null || cbNamHoc.SelectedItem == null || cbKiHoc.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn đủ thông tin để thống kê");
            }
            else
            {
                int sttK = cbKhoa.SelectedIndex;
                string kiHoc = "1";
                if (cbKiHoc.SelectedItem.ToString() != "Kì 1") kiHoc = "2";
                var listBM = new BoMonDAL().LayBoMonTheoKhoa(listKhoa[sttK].MaKhoa);
                foreach(var item in listBM)
                {
                    var data = new TongHopTaiCacGiaoVienDAL().tongHop(item.MaBoMon, cbNamHoc.SelectedItem.ToString(), kiHoc);
                    doDuLieuHopTai(data, item.TenBoMon);
                }
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập từ cần tìm kiếm");
            }
            else
            {
                IList<GiaoVien> listTK = new List<GiaoVien>();
                foreach(var item in listAllGV)
                {
                    if (item.TenGiaoVien.Contains(txtTimKiem.Text))
                        listTK.Add(item);
                }
                foreach (var item in listAllGV)
                {
                    if (item.MaGiaoVien.Contains(txtTimKiem.Text))
                        listTK.Add(item);
                }
                foreach (var item in listAllGV)
                {
                    if (item.MaBoMon.Contains(txtTimKiem.Text))
                        listTK.Add(item);
                }
                DoDuLieuDSGV(listTK);
            }

        }
    }
}
