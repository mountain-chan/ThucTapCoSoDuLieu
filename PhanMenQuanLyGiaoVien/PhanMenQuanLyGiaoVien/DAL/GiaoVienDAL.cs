using PhanMenQuanLyGiaoVien.Models;
using System.Collections.Generic;
using System.Data;
using PhanMenQuanLyGiaoVien.Entity;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class GiaoVienDAL
    {
        private GiaoVien tmp;
        private DataTable data;
        private string gt;
        private string[] ngaySinh;
        private string[] ngayDen;

        public bool ThemHocVi(string maGV, string maHV, string ngayNhan)
        {
            string str = "insert into GV_HocVi values('" + maGV + "', '" + maHV + "', '" + ngayNhan + "')";       
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool ThemCDNhaGiao(string maGV, string maCD, string ngayNhan, string noiBoNhiem)
        {
            string str = "insert into GV_ChucVuChMKT values('" + maGV + "', '" + maCD + "'," +
                " '" + ngayNhan + "',N'" + noiBoNhiem + "')";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool ThemCDNCKH(string maGV, string maCD, string ngayNhan, string noiBoNhiem)
        {
            string str = "insert into GV_ChucDanhChMNV values('" + maGV + "', '" + maCD + "'," +
                " '" + ngayNhan + "',N'" + noiBoNhiem + "')";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool capNhatHocVi(string maGV, string maHV, string ngayNhan)
        {
            string str = "update GV_HocVi set NgayNhan = '" + ngayNhan + "' " +
                "where MaGiaoVien = '" + maGV + "' and MaHocVi = '" + maHV + "' ";           
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool capNhatCDNhaGiao(string maGV, string maCD, string ngayNhan)
        {
            string str = "update GV_ChucVuChMKT set NgayNhan = '" + ngayNhan + "' " +
                "where MaGiaoVien = '" + maGV + "' and MaChucVu = '" + maCD + "' ";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool capNhatCDNCKH(string maGV, string maCD, string ngayNhan)
        {
            string str = "update GV_ChucDanhChMNV set NgayNhan = '" + ngayNhan + "' " +
                "where MaGiaoVien = '" + maGV + "' and MaChucDanh = '" + maCD + "' ";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool capNhatBoMon(string maGV, string maBM, string ngayden, string ngaydi)
        {
            string str = "updateGV_BoMon '" + maGV + "', '" + maBM + "', '" + ngayden + "', '" + ngaydi + "'";
            if(ngaydi == null)
                str = "updateGV_BoMon '" + maGV + "', '" + maBM + "', '" + ngayden + "', null";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }

        public bool updateGiaoVien(string maGV, string tenGV, string gioiTinh, string ngaysinh,
            string queQuan, string diaChi, string dienthoai, string email)
        {
            gt = "1";
            if (gioiTinh != "Nam") gt = "0";
            string str = "updateGiaoVien '" + maGV + "', N'" + tenGV + "', " + gt + ", '" + ngaysinh + "', N'" + queQuan + "'," +
                " N'" + diaChi + "','" + dienthoai + "', '" + email + "'";
            if (Server.ThucHienCauLenh(str))
                return true;
            return false;
        }


        public List<GiaoVien> LayTatCaGiaoVien(string ngayLay)
        {
            List<GiaoVien> listGV = new List<GiaoVien>();
            data = new DataTable();
            string str = "layTatCaGiaoVien '" + ngayLay + "'";
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                if (item["GioiTinh"].ToString() == "True") gt = "Nam";
                else gt = "Nữ";
                ngaySinh = (item["NgaySinh"].ToString()).Split(' ');
                ngayDen = (item["NgayChuyenDen"].ToString()).Split(' ');
                tmp = new GiaoVien
                {
                    MaGiaoVien = item["MaGiaoVien"].ToString(),
                    TenGiaoVien = item["TenGiaoVien"].ToString(),
                    GioiTinh = gt,
                    NgaySinh = ngaySinh[0],
                    QueQuan = item["QueQuan"].ToString(),
                    DiaChi = item["DiaChi"].ToString(),
                    DienThoai = item["DienThoai"].ToString(),
                    Email = item["Email"].ToString(),
                    MaBoMon = item["MaBoMon"].ToString(),
                    NgayChuyenDen = ngayDen[0]
                };

                listGV.Add(tmp);
            }
            return listGV;
        }

        public List<GiaoVien> LayGiaoVienTheoKhoa(string maK, string ngayLay)
        {
            List<GiaoVien> listGV = new List<GiaoVien>();
            data = new DataTable();
            string str = "LayGiaoVienTheoKhoa '" + maK + "','" + ngayLay + "'";
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                if (item["GioiTinh"].ToString() == "True") gt = "Nam";
                else gt = "Nữ";
                ngaySinh = (item["NgaySinh"].ToString()).Split(' ');
                ngayDen = (item["NgayChuyenDen"].ToString()).Split(' ');
                tmp = new GiaoVien
                {
                    MaGiaoVien = item["MaGiaoVien"].ToString(),
                    TenGiaoVien = item["TenGiaoVien"].ToString(),
                    GioiTinh = gt,
                    NgaySinh = ngaySinh[0],
                    QueQuan = item["QueQuan"].ToString(),
                    DiaChi = item["DiaChi"].ToString(),
                    DienThoai = item["DienThoai"].ToString(),
                    Email = item["Email"].ToString(),
                    MaBoMon = item["MaBoMon"].ToString(),
                    NgayChuyenDen = ngayDen[0]
                };

                listGV.Add(tmp);
            }
            return listGV;
        }

        public GiaoVien LayGiaoVienTheoMa(string maGV)
        {
            data = new DataTable();
            string str = "layGiaoVienTheoMa '" + maGV + "'";
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                if (data.Rows[0]["GioiTinh"].ToString() == "True") gt = "Nam";
                else gt = "Nữ";
                ngaySinh = (data.Rows[0]["NgaySinh"].ToString()).Split(' ');
                tmp = new GiaoVien
                {
                    MaGiaoVien = data.Rows[0]["MaGiaoVien"].ToString(),
                    TenGiaoVien = data.Rows[0]["TenGiaoVien"].ToString(),
                    GioiTinh = gt,
                    NgaySinh = ngaySinh[0],
                    QueQuan = data.Rows[0]["QueQuan"].ToString(),
                    DiaChi = data.Rows[0]["DiaChi"].ToString(),
                    DienThoai = data.Rows[0]["DienThoai"].ToString(),
                    Email = data.Rows[0]["Email"].ToString()                    
                };
                return tmp;
            }
            return null;
        }
    }
}
