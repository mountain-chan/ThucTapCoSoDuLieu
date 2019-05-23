using PhanMenQuanLyGiaoVien.Entity;
using System.Data;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class ThongTinHoatDongDAL
    {       

        public DataTable ThongTinGiangDay(string maGiaoVien, string namHoc, string kiHoc, string maHe)
        {
            string str = "ThongTinDayHoc '" + maGiaoVien + "', '" + namHoc + "','" + kiHoc + "', '" + maHe + "'";            
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }

        public DataTable ThongTinHuongDan(string maGiaoVien, string namHoc, string kiHoc, string maLoai)
        {
            string str = "select * from inCacDoAn('" + maGiaoVien + "', '" + namHoc + "', '" + kiHoc + "', '" + maLoai + "')";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }
        public DataTable ThongTinKhaoThi(string maGiaoVien, string namHoc, string kiHoc)
        {
            string str = "select * from inCongTacKhaoThi('" + maGiaoVien + "', '" + namHoc + "', '" + kiHoc + "')";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }
        public DataTable ThongTinNghienCuuDeTai(string maGiaoVien, string namHoc, string kiHoc)
        {
            string str = "ThongTinNghienCuuDeTai '" + maGiaoVien + "' , '" + namHoc + "' , '" + kiHoc + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }

        public DataTable ThongTinBaoKhoaHoc(string maGiaoVien, string namHoc, string kiHoc)
        {
            string str = "ThongTinBaoKhoaHoc '" + maGiaoVien + "' , '" + namHoc + "' , '" + kiHoc + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }

        public DataTable ThongTinVietSach(string maGiaoVien, string namHoc, string kiHoc)
        {
            string str = "ThongTinVietSach '" + maGiaoVien + "' , '" + namHoc + "' , '" + kiHoc + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }

        public DataTable ThongTinThamGiaHoiDong(string maGiaoVien, string namHoc, string kiHoc, string maLoai)
        {
            string str = "ThongTinThamGiaHoiDong '" + maGiaoVien + "' , '" + namHoc + "' , '" + kiHoc + "', '" + maLoai + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }
    }
}
