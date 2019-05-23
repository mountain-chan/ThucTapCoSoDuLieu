using PhanMenQuanLyGiaoVien.Entity;
using PhanMenQuanLyGiaoVien.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class ChucDanhNghienCuuDAL
    {
        private ChucDanhNghienCuu ChucDanhNCKH;
        public IList<ChucDanhNghienCuu> LayTatCaChucDanhNCKH(string maGV)
        {
            IList<ChucDanhNghienCuu> listChucDanhNCKH = new List<ChucDanhNghienCuu>();
            string str = "LayChucDanhNCKHChuaNhan '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                ChucDanhNCKH = new ChucDanhNghienCuu
                {
                    MaChucDanh = item["MaChucDanh"].ToString(),
                    TenChucDanh = item["TenChucDanh"].ToString()
                };
                listChucDanhNCKH.Add(ChucDanhNCKH);
            }
            return listChucDanhNCKH;
        }

        public IList<ChucDanhNghienCuu> LayChucDanhNCKHTheoGiaoVien(string maGV)
        {
            IList<ChucDanhNghienCuu> listChucDanhNCKH = new List<ChucDanhNghienCuu>();
            string str = "LayChucDanhNCKHTheoGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                ChucDanhNCKH = new ChucDanhNghienCuu
                {
                    MaChucDanh = item["MaChucDanh"].ToString(),
                    TenChucDanh = item["TenChucDanh"].ToString(),
                    NgayNhan = item["NgayNhan"].ToString()
                };
                listChucDanhNCKH.Add(ChucDanhNCKH);
            }
            return listChucDanhNCKH;
        }

        public IList<ChucDanhNghienCuu> listChucDanh(string maGV)
        {
            IList<ChucDanhNghienCuu> list = new List<ChucDanhNghienCuu>();
            string str = "ChucDanhNghienCuuCuaGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                ChucDanhNCKH = new ChucDanhNghienCuu
                {
                    TenChucDanh = item["TenChucDanh"].ToString(),
                    Nam = item["Nam"].ToString(),
                    NoiBoNhiem = item["NoiBoNhiem"].ToString()
                };
                list.Add(ChucDanhNCKH);
            }
            return list;
        }
    }
}
