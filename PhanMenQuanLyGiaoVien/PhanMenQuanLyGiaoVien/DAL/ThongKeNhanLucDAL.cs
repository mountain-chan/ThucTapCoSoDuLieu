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
    public class ThongKeNhanLucDAL
    {
        private ThongKeNhanLuc tt;
        public ThongKeNhanLuc ThongKe(string MaKhoa, string ngayThongKe)
        {
            string str = "select * from ThongKeNhanLuc('" + ngayThongKe + "', '" + MaKhoa + "')";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                tt = new ThongKeNhanLuc
                {
                    TenKhoa = data.Rows[0]["TenKhoa"].ToString(),
                    TongSo = data.Rows[0]["TongSo"].ToString(),
                    GiaoSu = data.Rows[0]["GiaoSu"].ToString(),
                    PhoGiaoSu = data.Rows[0]["PhoGiaoSu"].ToString(),
                    TienSyKH = data.Rows[0]["TienSyKH"].ToString(),
                    TienSy = data.Rows[0]["TienSy"].ToString(),
                    ThacSy = data.Rows[0]["ThacSy"].ToString(),
                    DaiHoc = data.Rows[0]["DaiHoc"].ToString(),
                    Khac = data.Rows[0]["Khac"].ToString()
                };
                return tt;
            }
                return null;
        }

        public DataTable ThongKes(string MaKhoa, string ngayThongKe)
        {
            string str = "select * from ThongKeNhanLuc('" + ngayThongKe + "', '" + MaKhoa + "')";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }

    }
}
