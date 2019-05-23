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
    public class DaoTaoSauDaiHocDAL
    {
        private DaoTaoSauDaiHoc dt;
        public DaoTaoSauDaiHoc QuaTrinhThacSy(string maGV)
        {
            string str = "QuaTrinhDaoTaoThacSi '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                dt = new DaoTaoSauDaiHoc
                {
                    ChuyenNganh = data.Rows[0]["ChuyenNganh"].ToString(),
                    NamCapBang = data.Rows[0]["NamCapBang"].ToString(),
                    NoiDaoTao = data.Rows[0]["NoiDaoTao"].ToString(),
                    TenLuan = data.Rows[0]["TenLuanVan"].ToString()             
                };
                return dt;
            }
            dt = new DaoTaoSauDaiHoc
            {
                ChuyenNganh = "...................",
                NamCapBang = "...................",
                NoiDaoTao = "...................",
                TenLuan = "...................",
            };

            return dt;
        }

        public DaoTaoSauDaiHoc QuaTrinhTienSi(string maGV)
        {
            string str = "QuaTrinhDaoTaoTienSi '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                DaoTaoSauDaiHoc dt = new DaoTaoSauDaiHoc
                {
                    ChuyenNganh = data.Rows[0]["ChuyenNganh"].ToString(),
                    NamCapBang = data.Rows[0]["NamCapBang"].ToString(),
                    NoiDaoTao = data.Rows[0]["NoiDaoTao"].ToString(),
                    TenLuan = data.Rows[0]["TenLuanAn"].ToString()
                };
                return dt;
            }
            dt = new DaoTaoSauDaiHoc
            {
                ChuyenNganh = "...................",
                NamCapBang = "...................",
                NoiDaoTao = "...................",
                TenLuan = "...................",
            };

            return dt;
        }
    }
}
