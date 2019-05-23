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
    public class ChucDanhNhaGiaoDAL
    {
        private string ten;
        public IList<ChucDanhNhaGiao> listChucDanh(string maGV)
        {
            IList<ChucDanhNhaGiao> list = new List<ChucDanhNhaGiao>();
            string str = "ChucDanhNhaGiaoCuaGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                if (item["TenChucVu"].ToString() == "Giảng viên cao cấp") ten = "Giáo Sư";
                else if (item["TenChucVu"].ToString() == "Giảng viên chính") ten = "Phó Giáo Sư";
                else ten = item["TenChucVu"].ToString();
                ChucDanhNhaGiao hv = new ChucDanhNhaGiao
                {
                    TenChucVu = ten,
                    Nam = item["Nam"].ToString(),
                    NoiBoNhiem = item["NoiBoNhiem"].ToString()
                };
                list.Add(hv);
            }
            return list;
        }

        public IList<ChucDanhNhaGiao> LayTatCaChucDanhNhaGiao(string maGV)
        {
            IList<ChucDanhNhaGiao> listHocVi = new List<ChucDanhNhaGiao>();
            string str = "LayChucDanhNhaGiaoChuaNhan '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                if (item["TenChucVu"].ToString() == "Giảng viên cao cấp") ten = "Giáo Sư";
                else if (item["TenChucVu"].ToString() == "Giảng viên chính") ten = "Phó Giáo Sư";
                else ten = item["TenChucVu"].ToString();
                ChucDanhNhaGiao HocVi = new ChucDanhNhaGiao
                {
                    MaChucVu = item["MaChucVu"].ToString(),
                    TenChucVu = ten
                };
                listHocVi.Add(HocVi);
            }
            return listHocVi;
        }

        public IList<ChucDanhNhaGiao> LayChucDanhNhaGiaoTheoGiaoVien(string maGV)
        {
            IList<ChucDanhNhaGiao> listHocVi = new List<ChucDanhNhaGiao>();
            string str = "LayChucVuCMKTTheoGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                if (item["TenChucVu"].ToString() == "Giảng viên cao cấp") ten = "Giáo Sư";
                else if (item["TenChucVu"].ToString() == "Giảng viên chính") ten = "Phó Giáo Sư";
                else ten = item["TenChucVu"].ToString();
                ChucDanhNhaGiao cd = new ChucDanhNhaGiao
                {
                    MaChucVu = item["MaChucVu"].ToString(),
                    TenChucVu = ten,
                    NgayNhan = item["NgayNhan"].ToString()
                };
                listHocVi.Add(cd);
            }
            return listHocVi;
        }


    }
}
