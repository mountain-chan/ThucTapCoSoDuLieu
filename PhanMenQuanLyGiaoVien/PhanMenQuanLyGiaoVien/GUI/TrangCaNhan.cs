using PhanMenQuanLyGiaoVien.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PhanMenQuanLyGiaoVien.DAL;
using System.Drawing;
using PhanMenQuanLyGiaoVien.Entity;
using System.Data;

namespace PhanMenQuanLyGiaoVien.GUI
{
    public partial class TrangCaNhan : Form
    {
        private string maGV;
        private string namBD;
        private string namKT;
        private IList<LoaiHoiDong> listHoiDong;
        private IList<He> listHe;
        private IList<LoaiHuongDan> listHuongDan;
        private IList<HocVi> ttHocVi;
        private IList<ChucDanhNghienCuu> ttCDNCKH;
        private IList<ChucDanhNhaGiao> ttCDNhaGiao;
        private DateTimePicker[] dtpHocVi;
        private DateTimePicker[] dtpCDNhaGiao;
        private DateTimePicker[] dtpCDNCKH;
        private GiaoVien ttCaNhan;       
        private int top, stt;
        private int date, month, year;
        public TrangCaNhan(string maGiaoVien, string namBatDau, string namKeThuc)
        {
            InitializeComponent();
            maGV =  maGiaoVien;
            namBD = namBatDau;
            namKT = namKeThuc;

            LoadDuLieu();
            LuaChonNamHoc();
            LyLichKhoaHoc();
            ThongTinCaNhan();
        }

        private void LoadDuLieu()
        {
            dtpHocVi = new DateTimePicker[10];
            for(int i = 0; i< 10; i++)
            {
                dtpHocVi[i] = new DateTimePicker();
            }
            dtpCDNhaGiao = new DateTimePicker[10];
            for (int i = 0; i < 10; i++)
            {
                dtpCDNhaGiao[i] = new DateTimePicker();
            }
            dtpCDNCKH = new DateTimePicker[10];
            for (int i = 0; i < 10; i++)
            {
                dtpCDNCKH[i] = new DateTimePicker();
            }
            listHoiDong = new List<LoaiHoiDong>();
            listHuongDan = new List<LoaiHuongDan>();
            listHe = new List<He>();
            ttHocVi = new List<HocVi>();
            ttCDNhaGiao = new List<ChucDanhNhaGiao>();
            ttCDNCKH = new List<ChucDanhNghienCuu>();

            ttHocVi = new HocViDAL().LayHocViTheoGiaoVien(maGV);
            ttCDNhaGiao = new ChucDanhNhaGiaoDAL().LayChucDanhNhaGiaoTheoGiaoVien(maGV);
            ttCDNCKH = new ChucDanhNghienCuuDAL().LayChucDanhNCKHTheoGiaoVien(maGV);
            listHe = new HeDAL().LayTatCaHe();
            listHoiDong = new LoaiHoiDongDAL().LayTatCaLoaiHoiDong();
            listHuongDan = new LoaiHuongDanDAL().LayTatCaLoaiHuongDan();
            
            dtpNgayDen.MaxDate = DateTime.Today;
            dtpNgaySinh.MaxDate = DateTime.Today.AddYears(-10);
            date = DateTime.Today.Day;
            month = DateTime.Today.Month;
            year = DateTime.Today.Year;
                      
        }

        private void LuaChonNamHoc()
        {
            int inamBD = Int32.Parse(namBD);
            int inamKT = Int32.Parse(namKT);
            for (int i = inamBD; i < inamKT; i++)
            {
                string namHoc = i.ToString() + "-" + (i + 1).ToString();
                cbbNamHoc.Items.Add(namHoc);
            }

            pnTitleGiangDay.BackColor = Color.LightGray;
            pnTitleHoiDong.BackColor = Color.LightGray;
            pnTitleHuongDan.BackColor = Color.LightGray;
            pnTitleKhaoThi.BackColor = Color.LightGray;
            pnTitleNCKH.BackColor = Color.LightGray;
            pnTitleTongHop.BackColor = Color.LightGray;
        }

        private void newDTP(DateTimePicker dtp, int left, int t, string str, Panel pn)
        {         
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "MM/dd/yyyy";
            dtp.Width = 100;
            dtp.Height = 20;
            dtp.Top = t;
            dtp.Left = left;
            dtp.Parent = pn;
            dtp.Value = DateTime.Parse(str);
        }

        private void newLable(int left, int them, string str, Font font, Panel pn)
        {
            Label lb = new Label();
            lb.AutoSize = true;
            lb.Left = left;
            lb.Top = top + them;
            lb.Font = font;
            lb.Text = str;
            lb.Parent = pn;
            top += ThamSo.lbHeight + them;
        }

        private void newCheckBox(CheckBox cb, int l, string str)
        {
            cb.Top = top;
            cb.Left = l;
            cb.Parent = pnLyLichKH;
            cb.Text = str;
            cb.AutoSize = true;
            cb.Font = ThamSo.fBody;

        }

        private void taiTTHocVi()
        {
            pnHocVi.Controls.Clear();
            top = 10;
            stt = 0;
            foreach (var item in ttHocVi)
            {
                string str = item.TenHocVi + "         Ngày nhận:";
                newDTP(dtpHocVi[stt] ,250, top, item.NgayNhan, pnHocVi);
                Button btn = new Button();
                btn.Parent = pnHocVi;
                btn.Width = 48;
                btn.Height = 20;
                btn.Top = top;
                btn.Left = 400;
                btn.Text = "Sửa";
                btn.Tag = stt;
                newLable(40, 0, str, ThamSo.fNormal, pnHocVi);
                stt++;
                btn.Click += Btn_Click;
            }
        }

        private void taiTTCDNhaGiao()
        {
            pnCDNhaGiao.Controls.Clear();
            top = 10;
            stt = 0;
            foreach (var item in ttCDNhaGiao)
            {
                string str = item.TenChucVu + "         Ngày nhận:";
                newDTP(dtpCDNhaGiao[stt], 250, top, item.NgayNhan, pnCDNhaGiao);
                Button btn = new Button();
                btn.Parent = pnCDNhaGiao;
                btn.Width = 48;
                btn.Height = 20;
                btn.Top = top;
                btn.Left = 400;
                btn.Text = "Sửa";
                btn.Tag = stt;
                newLable(40, 0, str, ThamSo.fNormal, pnCDNhaGiao);
                stt++;
                btn.Click += Btn_Click1;
            }
        }

        private void taiTTCDNCKH()
        {
            pnCDNCKH.Controls.Clear();
            top = 10;
            stt = 0;
            foreach (var item in ttCDNCKH)
            {
                string str = item.TenChucDanh + "         Ngày nhận:";
                newDTP(dtpCDNCKH[stt], 250, top, item.NgayNhan, pnCDNCKH);
                Button btn = new Button();
                btn.Parent = pnCDNCKH;
                btn.Width = 48;
                btn.Height = 20;
                btn.Top = top;
                btn.Left = 400;
                btn.Text = "Sửa";
                btn.Tag = stt;
                newLable(40, 0, str, ThamSo.fNormal, pnCDNCKH);
                stt++;
                btn.Click += Btn_Click2;
            }
        }

        private void ThongTinCaNhan()
        {      
            var dsBM = new BoMonDAL().LayTatCaBoMon();
            foreach(var item in dsBM)
            {
                cbbBM.Items.Add(item);
            }

            var dsHV = new HocViDAL().LayTatCaHocVi(maGV);
            foreach(var item in dsHV)
            {
                cbbHocVi.Items.Add(item);
            }

            var dsCDNhaGiao = new ChucDanhNhaGiaoDAL().LayTatCaChucDanhNhaGiao(maGV);
            foreach (var item in dsCDNhaGiao)
            {
                cbbCDNhaGiao.Items.Add(item);
            }
           
            var dsCDNCKH = new ChucDanhNghienCuuDAL().LayTatCaChucDanhNCKH(maGV);
            foreach (var item in dsCDNCKH)
            {
                cbbCDNCKH.Items.Add(item);
            }
            var bm = new BoMonDAL().LayBoMonTheoGiaoVien(maGV);

            cbbBM.Text = bm.TenBoMon;
            cbbGioiTinh.Text = ttCaNhan.GioiTinh;
            txtMaGV.Text = ttCaNhan.MaGiaoVien;
            txtTenGV.Text = ttCaNhan.TenGiaoVien;
            txtQueQuan.Text = ttCaNhan.QueQuan;
            txtEmail.Text = ttCaNhan.Email;
            txtDiaChi.Text = ttCaNhan.DiaChi;
            txtDienThoai.Text = ttCaNhan.DienThoai;
            dtpNgaySinh.Value = DateTime.Parse(ttCaNhan.NgaySinh);
            dtpNgayDen.Value = DateTime.Parse(bm.NgayChuyenDen);

            taiTTCDNhaGiao();
            taiTTHocVi();
            taiTTCDNCKH();
        }

        
        private void checkBoxHocVi()
        {
            CheckBox[] cbHocVi = new CheckBox[5];
            for(int i = 0; i < 5; i++)
            {
                cbHocVi[i] = new CheckBox();
            }
            int leftCB = ThamSo.leftBody;
            newCheckBox(cbHocVi[0], leftCB, "Cử nhân");
            leftCB += 100;
            newCheckBox(cbHocVi[1], leftCB, "Kỹ sư");
            leftCB += 100;
            newCheckBox(cbHocVi[2], leftCB, "Thạc sỹ");
            leftCB += 100;
            newCheckBox(cbHocVi[3], leftCB, "Tiến sĩ");
            leftCB += 100;
            newCheckBox(cbHocVi[4], leftCB, "TSKH");
            top += ThamSo.lbHeight;
            var listHV = new HocViDAL().listHocVi(maGV);           
            for (int i = 0; i < 5; i++)
            {
                foreach (var item in listHV)
                {
                    if (cbHocVi[i].Text == item.TenHocVi)
                        cbHocVi[i].Checked = true;
                }
            }
        }

        private void tableLichSu(DataTable data)
        {
            Panel pn = new Panel();
            pn.Parent = pnLyLichKH;
            pn.Left = ThamSo.leftBody;
            pn.Top = top + 15;
            pn.Width = ThamSo.tableW;
            pn.Height = 200;
            DataGridView dgv = new DataGridView();
            dgv.Parent = pn;
            dgv.ReadOnly = true;
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DataSource = data;

            top += pn.Height + ThamSo.lbHeight;
        }

        private void LyLichKhoaHoc()
        {
            pnLyLichKH.Controls.Clear();
            ttCaNhan = new GiaoVienDAL().LayGiaoVienTheoMa(maGV);
            var bm = new BoMonDAL().LayBoMonTheoGiaoVien(maGV);
            var listCDNhaGiao = new ChucDanhNhaGiaoDAL().listChucDanh(maGV);
            var listCDNghienCuu = new ChucDanhNghienCuuDAL().listChucDanh(maGV);
            var listNgoaiNgu = new TrinhDoNgoaiNguDAL().listTrinhDo(maGV);
            var qtDaoTaoDaiDoc = new DaoTaoDaiHocDAL().QuaTrinhDaoTao(maGV);
            var qtDaoTaoThacSy = new DaoTaoSauDaiHocDAL().QuaTrinhThacSy(maGV);
            var qtDaoTaoTienSi = new DaoTaoSauDaiHocDAL().QuaTrinhTienSi(maGV);

            var lsGiangDay = new LichSuHoatDongDAL().lichSuGD(maGV);
            var lsHuongDan = new LichSuHoatDongDAL().lichSuHuongDan(maGV);
            var lsVietSach = new LichSuHoatDongDAL().lichSuVietSach(maGV);
            var lsBaiBao = new LichSuHoatDongDAL().lichSuBaiBao(maGV);
            var lsNghienCuuDeTai = new LichSuHoatDongDAL().lichSuNghienCuuDeTai(maGV);
            var listSanPhamKHCN = new LichSuHoatDongDAL().CacSanPhamKHCN(maGV);
            var listGiaThuong = new LichSuHoatDongDAL().CacGiaiThuongKHCN(maGV);
            var listVanBang = new LichSuHoatDongDAL().VanBangSangChe(maGV);
          
            top = 10;
            newLable(400, 0, "LÝ LỊCH KHOA HỌC", ThamSo.fTitleB, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMax, "I.THÔNG TIN CÁ NHÂN", ThamSo.fTitle, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "1. Họ Và Tên: " + ttCaNhan.TenGiaoVien + "     Giới tính: " + ttCaNhan.GioiTinh + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "2. Ngày, tháng, năm sinh: " + ttCaNhan.NgaySinh + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "3. Đơn vị: " + bm.TenBoMon + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "4. Quê quán: " + ttCaNhan.QueQuan + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "5. Địa chỉ liên hệ: " + ttCaNhan.DiaChi + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "6. Số điện thoai: " + ttCaNhan.DienThoai + "     Email: " + ttCaNhan.Email + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "7. Học vị:", ThamSo.fBody, pnLyLichKH);
            checkBoxHocVi();
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "8. Chức danh khoa học", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "8.1. Chức danh nhà giáo", ThamSo.fBody, pnLyLichKH);
            foreach(var item in listCDNhaGiao)
            {
                newLable(ThamSo.leftBody, ThamSo.pdHMin, "" + item.TenChucVu + "         Năm: " + item.Nam + "      Nơi bổ nhiệm: " + item.NoiBoNhiem + "", ThamSo.fBody, pnLyLichKH);
            }
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "8.2. Chức danh nghiên cứu", ThamSo.fBody, pnLyLichKH);
            foreach (var item in listCDNghienCuu)
            {
                newLable(ThamSo.leftBody, ThamSo.pdHMin, "" + item.TenChucDanh + "         Năm: " + item.Nam + "      Nơi bổ nhiệm: " + item.NoiBoNhiem + "", ThamSo.fBody, pnLyLichKH);
            }
            newLable(ThamSo.leftTitle, ThamSo.pdHMin, "9. Trinh độ ngoại ngữ", ThamSo.fBody, pnLyLichKH);
            foreach (var item in listNgoaiNgu)
            {
                newLable(ThamSo.leftBody, ThamSo.pdHMin, "Tên trình độ: " + item.TenTrinhDo + "     Ngày cấp: " + item.NgayCapChungChi + "", ThamSo.fBody, pnLyLichKH);
            }

            newLable(ThamSo.leftTitle, ThamSo.pdHMax, "II. QÚA TRÌNH ĐƯƠC ĐÀO TẠO VÀ TỰ ĐÀO TẠO ", ThamSo.fTitle, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "14. Đại học", ThamSo.fTitleSmall, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Hệ đào tạo: "+qtDaoTaoDaiDoc.HeDaoTao+"", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Nơi đào tạo: " + qtDaoTaoDaiDoc.NoiDTao + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Ngành học: " + qtDaoTaoDaiDoc.NganhHoc + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Nước đào tạo: " + qtDaoTaoDaiDoc.NuocDaoTao + "       Năm tốt nghiệp: "+qtDaoTaoDaiDoc.NamTotNghiep+"", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "15. Sau đại học", ThamSo.fTitleSmall, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Thạc sỹ chuyên ngành: " + qtDaoTaoThacSy.ChuyenNganh + "      Năm cấp bằng: "+qtDaoTaoThacSy.NamCapBang+"", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Nơi đào tạo: " + qtDaoTaoThacSy.NoiDaoTao + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Tên luận văn TN: " + qtDaoTaoThacSy.TenLuan + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Thạc sĩ chuyên ngành: " + qtDaoTaoTienSi.ChuyenNganh + "      Năm cấp bằng: " + qtDaoTaoTienSi.NamCapBang + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Nơi đào tạo: " + qtDaoTaoTienSi.NoiDaoTao + "", ThamSo.fBody, pnLyLichKH);
            newLable(ThamSo.leftBody, ThamSo.pdHMin, "- Tên luận án: " + qtDaoTaoTienSi.TenLuan + "", ThamSo.fBody, pnLyLichKH);

            newLable(ThamSo.leftTitle, ThamSo.pdHMax, "III. THÂM NIÊN, KINH NGHIỆP VÀ THÀNH TÍCH TRONG HOẠT ĐỘNG ĐÀO TẠO ", ThamSo.fTitle, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "18. Giảng dạy", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(lsGiangDay);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "20. Hướng dẫn luận văn Cao học, luận án Tiến sĩ", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(lsHuongDan);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "21. Sách chuyên khảo, giáo trình và sách tham khảo  đã viết hoặc tham gia", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(lsVietSach);

            newLable(ThamSo.leftTitle, ThamSo.pdHMax, "IV. KINH NGHIỆM VÀ THÀNH TÍCH TRONG HOẠT ĐỘNG NGHIÊN CỨU KHOA HỌC", ThamSo.fTitle, pnLyLichKH);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "22. Các bài báo, báo cáo khoa học", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(lsBaiBao);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "23. Các đề tài, dự án, nhiệm vụ KHCN các cấp đã chủ trì hoặc tham gia", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(lsNghienCuuDeTai);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "24. Sản phẩm KHCN được án dụng", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(listSanPhamKHCN);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "25. Giải thưởng về KHCN trong và ngoài nước", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(listGiaThuong);
            newLable(ThamSo.leftTitle, ThamSo.pdHNormal, "26. Số lượng phát minh, sáng chế, văn bằng sở hữu trí tuệ đã được cấp", ThamSo.fTitleSmall, pnLyLichKH);
            tableLichSu(listVanBang);

            newLable(600, 20, "Hà Nội, ngày "+date.ToString()+" tháng "+month.ToString()+" năm "+year.ToString()+"", ThamSo.fBody, pnLyLichKH);
            newLable(630, 8, "Người Khai", ThamSo.fBody, pnLyLichKH);
            newLable(625, 8, ttCaNhan.TenGiaoVien, ThamSo.fSign, pnLyLichKH);

        }
     
        private void DoDuLieu(DataTable data, Panel pn, string nameGB)
        {
            GroupBox gb = new GroupBox();
            gb.Text = nameGB;
            gb.Height = 200;
            gb.Dock = DockStyle.Top;
            gb.Parent = pn;
            DataGridView dgv = new DataGridView();
            dgv.Parent = gb;
            dgv.Dock = DockStyle.Fill;          
            dgv.DataSource = data;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void CapNhatThongTinKhaoThi(string maGiaoVien, string namHoc, string kiHoc)
        {
            var dataTable = new ThongTinHoatDongDAL().ThongTinKhaoThi(maGiaoVien, namHoc, kiHoc);
            DoDuLieu(dataTable, pnKhaoThi, "Thông Tin Khảo Thí");
            
        }

        private void CapNhatThongTinNCKH(string maGiaoVien, string namHoc, string kiHoc)
        {
            var dataTable = new ThongTinHoatDongDAL().ThongTinBaoKhoaHoc(maGiaoVien, namHoc, kiHoc);
            DoDuLieu(dataTable, pnNCKH, "Bài Báo, Bài Báo Khoa Học");

            dataTable = new ThongTinHoatDongDAL().ThongTinVietSach(maGiaoVien, namHoc, kiHoc);
            DoDuLieu(dataTable, pnNCKH, "Biên Soạn Sách Chuyên Khảo, Giáo Trình");

            dataTable = new ThongTinHoatDongDAL().ThongTinNghienCuuDeTai(maGiaoVien, namHoc, kiHoc);
            DoDuLieu(dataTable, pnNCKH, "Đề Tài Nghiên Cứu");
        }

        private void CapNhatThongTinHuongDan(string maGiaoVien, string namHoc, string kiHoc)
        {
            foreach(var item in listHuongDan)
            {
                var dataTable = new ThongTinHoatDongDAL().ThongTinHuongDan(maGiaoVien, namHoc, kiHoc, item.MaLoaiHuongDan);
                DoDuLieu(dataTable, pnHuongDan, item.TenLoaiHuongDan);
            }               
        }

        private void CapNhatThongTinTGHoiDong(string maGiaoVien, string namHoc, string kiHoc)
        {
            foreach(var item in listHoiDong)
            {
                var dataTable = new ThongTinHoatDongDAL().ThongTinThamGiaHoiDong(maGiaoVien, namHoc, kiHoc, item.MaLoaiHoiDong);
                DoDuLieu(dataTable, pnHoiDong, item.TenLoaiHoiDong);
            }
            
      
        }

        private void CapNhatThongTinGiangDay(string maGiaoVien, string namHoc, string kiHoc)
        {
            foreach (var item in listHe)
            {
                var dataTable = new ThongTinHoatDongDAL().ThongTinGiangDay(maGiaoVien, namHoc, kiHoc, item.Mahe);
                DoDuLieu(dataTable, pnGiangDay, item.TenHe);
            }
        }


        private void btnXem_Click(object sender, EventArgs e)
        {
            string namHoc, kiHoc;
            if (cbbNamHoc.SelectedItem != null && cbbKiHoc.SelectedItem != null)
            {
                namHoc = cbbNamHoc.SelectedItem.ToString();
                kiHoc = cbbKiHoc.SelectedItem.ToString();
                if (kiHoc == "Kì 1") kiHoc = "1";
                else kiHoc = "2";
                TongHop th = new TongHopDAL().TongHopKetQuaCongTac(maGV, namHoc, kiHoc);

                lbTenGiaoVien.Text = th.TenGiaoVien;
                lbTenGVGiangDay.Text = th.TenGiaoVien;
                lbTenGVHuongDan.Text = th.TenGiaoVien;
                lbTenGVKhaoThi.Text = th.TenGiaoVien;
                lbTenGVNCKH.Text = th.TenGiaoVien;
                lbTenGVHoiDong.Text = th.TenGiaoVien;
                lbDonVi.Text = th.DonVi;
                lbChucVu.Text = th.ChucVu;
                lbHocHam.Text = th.HocHam;
                lbHocVi.Text = th.HocVi;
                lbChuNhiemBoMon.Text = th.ChuNhiemBoMon;
                lbTongTaiDaoTao.Text = th.TaiDaoTao;
                lbTaiGiangDay.Text = th.TaiGiangDay;
                lbTaiCaoDang.Text = th.TaiDayCaoDang;
                lbTaiDaiHoc.Text = th.TaiDayDaiHoc;
                lbTaiCaoHoc.Text = th.TaiDayCaoHoc;
                lbTaiHuongDan.Text = th.TaiHuongDan;
                lbTaiKhaoThi.Text = th.TaiKhaoThi;
                lbTaiHoiDong.Text = th.TaiHoiDong;
                lbTongTaiNCKH.Text = th.TaiNCKH;
                lbTaiDeTai.Text = th.TaiDeTai;
                lbTaiBaiBao.Text = th.TaiBaiBao;
                lbTaiBienSoan.Text = th.TaiBienSoan;
                lbTongGioChuan.Text = th.TongTai;
                lbDinhMucTaiDaoTao.Text = th.DinhMucTaiDaoTao;
                lbPhanTramDatTaiDT.Text = th.PhanTramTaiDaoTao + " %";
                lbDinhMucTaiNCKH.Text = th.DinhMucTaiNghienCuu;
                lbTaiNCKHYeuCau.Text = th.TaiNghienCuuYeuCau;
                lbPhanTramDatTaiNCKH.Text = th.phanTramTaiNCKH + " %";
                lbTaiDaoTaoYeuCau.Text = th.TaiDaoTaoYeuCau;

                pnDoiTuongMienGiam1.Controls.Clear();
                pnDoiTuongMienGiam2.Controls.Clear();
                int i = 5;
                List<string> listDoiTuongMienGiam = new DoiTuongMienGiamDAL().DanhSachDoiTuongMienGiam(maGV, namHoc, kiHoc);
                foreach(string dt in listDoiTuongMienGiam)
                {
                    Label lb = new Label(), lb2 = new Label();
                    lb.Top = i;
                    lb.Left = 5;
                    lb.AutoSize = true;
                    lb.Text = dt;
                    lb.Parent = pnDoiTuongMienGiam1;
                    lb2.Top = i;
                    lb2.Left = 5;
                    lb2.AutoSize = true;
                    lb2.Text = dt;                  
                    lb2.Parent = pnDoiTuongMienGiam2;
                    i += 15;
                }

                pnGiangDay.Controls.Clear();
                pnHoiDong.Controls.Clear();
                pnHuongDan.Controls.Clear();
                pnNCKH.Controls.Clear();
                pnKhaoThi.Controls.Clear();
                CapNhatThongTinGiangDay(maGV, namHoc, kiHoc);
                CapNhatThongTinHuongDan(maGV, namHoc, kiHoc);
                CapNhatThongTinKhaoThi(maGV, namHoc, kiHoc);
                CapNhatThongTinTGHoiDong(maGV, namHoc, kiHoc);
                CapNhatThongTinNCKH(maGV, namHoc, kiHoc);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn năm học hoặc kì học");
            }           
        }

        

        private void btnCapNhatThongTinCaNhan_Click(object sender, EventArgs e)
        {
            if (new GiaoVienDAL().updateGiaoVien(txtMaGV.Text, txtTenGV.Text,
                cbbGioiTinh.Text, dtpNgaySinh.Text, txtQueQuan.Text, txtDiaChi.Text,
                txtDienThoai.Text, txtEmail.Text))
            {
                LyLichKhoaHoc();
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Lỗi! Cập nhật thất bại");
            }
        }

        private void btnCapNhatDonVi_Click(object sender, EventArgs e)
        {
            string maBM = ((BoMon)cbbBM.SelectedItem).MaBoMon;
            string ngaydi;
            if (dtpNgayDi.Checked)
                ngaydi = dtpNgayDi.Text;
            else ngaydi = null;
            if(new GiaoVienDAL().capNhatBoMon(maGV, maBM, dtpNgayDen.Text, ngaydi))
            {
                LyLichKhoaHoc();
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Lỗi! Cập nhật thất bại");
            }

        }

        private void btnThenHV_Click(object sender, EventArgs e)
        {
            if (dtpNgayNhanHV.Checked && cbbHocVi.Text != "")
            {
                string maHV = ((HocVi)cbbHocVi.SelectedItem).MaHocVi;            
                if (new GiaoVienDAL().ThemHocVi(maGV, maHV, dtpNgayNhanHV.Text))
                {
                    HocVi hv = new HocVi
                    {
                        MaHocVi = maHV,
                        TenHocVi = cbbHocVi.Text,
                        NgayNhan = dtpNgayNhanHV.Text
                    };
                    ttHocVi.Add(hv);
                    taiTTHocVi();
                    LyLichKhoaHoc();
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Lỗi! Cập nhật thất bại");
                }
            }
        }

        private void btnThemCDNhaGiao_Click(object sender, EventArgs e)
        {
            if (dtpNgayNhanCDNG.Checked && cbbCDNhaGiao.Text != "" && txtNoiBNCDNhaGiao.Text != "")
            {
                string maCD = ((ChucDanhNhaGiao)cbbCDNhaGiao.SelectedItem).MaChucVu;
                if (new GiaoVienDAL().ThemCDNhaGiao(maGV, maCD, dtpNgayNhanCDNG.Text, txtNoiBNCDNhaGiao.Text))
                {
                    ChucDanhNhaGiao cd = new ChucDanhNhaGiao
                    {
                        MaChucVu = maCD,
                        TenChucVu = cbbCDNhaGiao.Text,
                        NgayNhan = dtpNgayNhanCDNG.Text
                    };
                    ttCDNhaGiao.Add(cd);
                    taiTTCDNhaGiao();
                    LyLichKhoaHoc();
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Lỗi! Cập nhật thất bại");
                }
            }
        }

        private void btnThemCDNCKH_Click(object sender, EventArgs e)
        {
            if (dtpNgayNhanCDNCKH.Checked && cbbCDNCKH.Text != "" && txtNoiBNCDNCKH.Text != "")
            {
                string maCD = ((ChucDanhNghienCuu)cbbCDNCKH.SelectedItem).MaChucDanh;
                if (new GiaoVienDAL().ThemCDNCKH(maGV, maCD, dtpNgayNhanCDNCKH.Text, txtNoiBNCDNCKH.Text))
                {
                    ChucDanhNghienCuu cd = new ChucDanhNghienCuu
                    {
                        MaChucDanh = maCD,
                        TenChucDanh = cbbCDNCKH.Text,
                        NgayNhan = dtpNgayNhanCDNCKH.Text
                    };
                    ttCDNCKH.Add(cd);
                    taiTTCDNCKH();
                    LyLichKhoaHoc();
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Lỗi! Cập nhật thất bại");
                }
            }
        }

        private void Btn_Click2(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int i = (int)btn.Tag;
            if (new GiaoVienDAL().capNhatCDNCKH(maGV, ttCDNCKH[i].MaChucDanh, dtpCDNCKH[i].Text))
            {
                LyLichKhoaHoc();
                MessageBox.Show("Cập nhật thành công!");
            }               
            else
                MessageBox.Show("Lỗi!, Cập nhật thất bại");
        }

        private void Btn_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int i = (int)btn.Tag;
            if (new GiaoVienDAL().capNhatCDNhaGiao(maGV, ttCDNhaGiao[i].MaChucVu, dtpCDNhaGiao[i].Text))
            {
                LyLichKhoaHoc();
                MessageBox.Show("Cập nhật thành công!");
            }
            else
                MessageBox.Show("Lỗi!, Cập nhật thất bại");
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int i = (int)btn.Tag;

            if (new GiaoVienDAL().capNhatHocVi(maGV, ttHocVi[i].MaHocVi, dtpHocVi[i].Text))
            {              
                MessageBox.Show("Cập nhật thành công!");
            }
            else
                MessageBox.Show("Lỗi!, Cập nhật thất bại");
        }
    }
}
