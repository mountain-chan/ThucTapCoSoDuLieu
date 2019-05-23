using PhanMenQuanLyGiaoVien.Entity;
using PhanMenQuanLyGiaoVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class LichSuHoatDongDAL
    {      
        public DataTable lichSuGD(string maGV)
        {          
            string str = "LichSuGiangDay " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable lichSuHuongDan(string maGV)
        {
            string str = "LichSuHuongDan " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable lichSuVietSach(string maGV)
        {
            string str = "LichSuVietSach " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable lichSuBaiBao(string maGV)
        {
            string str = "LichSuBaiBao " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable lichSuNghienCuuDeTai(string maGV)
        {
            string str = "LichSuNghienCuDeTai " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable CacSanPhamKHCN(string maGV)
        {
            string str = "CacSanPhamKHCN " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable VanBangSangChe(string maGV)
        {
            string str = "VanBangSangChe " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }

        public DataTable CacGiaiThuongKHCN(string maGV)
        {
            string str = "CacGiaiThuongKHCN " + maGV + "";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);

            return data;
        }
    }
}
