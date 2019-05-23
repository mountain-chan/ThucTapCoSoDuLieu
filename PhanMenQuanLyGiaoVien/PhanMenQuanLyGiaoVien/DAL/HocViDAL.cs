using PhanMenQuanLyGiaoVien.Entity;
using PhanMenQuanLyGiaoVien.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PhanMenQuanLyGiaoVien.DAL
{
    class HocViDAL
    {
        private HocVi HocVi;

        public IList<HocVi> listHocVi(string maGV)
        {
            IList<HocVi> list = new List<HocVi>();
            string str = "HocViGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                HocVi = new HocVi
                {
                    MaHocVi = item["MaHocVi"].ToString(),
                    TenHocVi = item["TenHocVi"].ToString()
                };
                list.Add(HocVi);
            }
            return list;
        }

        public IList<HocVi> LayTatCaHocVi(string maGV)
        {
            IList<HocVi> listHocVi = new List<HocVi>();
            string str = "LayHocViChuaNhan '"+ maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                HocVi = new HocVi
                {
                    MaHocVi = item["MaHocVi"].ToString(),
                    TenHocVi = item["TenHocVi"].ToString()
                };
                listHocVi.Add(HocVi);
            }
            return listHocVi;
        }

        public IList<HocVi> LayHocViTheoGiaoVien(string maGV)
        {
            IList<HocVi> listHocVi = new List<HocVi>();
            string str = "LayHocViTheoGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                HocVi = new HocVi
                {
                    MaHocVi = item["MaHocVi"].ToString(),
                    TenHocVi = item["TenHocVi"].ToString(),
                    NgayNhan = item["NgayNhan"].ToString()
                };
                listHocVi.Add(HocVi);
            }
            return listHocVi;
        }

    }
}
