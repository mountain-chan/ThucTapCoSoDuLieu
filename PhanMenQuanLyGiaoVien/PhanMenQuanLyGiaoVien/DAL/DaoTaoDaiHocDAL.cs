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
    public class DaoTaoDaiHocDAL
    {
        private DaoTaoDaiHoc dt;
        public DaoTaoDaiHoc QuaTrinhDaoTao(string maGV)
        {      
            string str = "QuaTrinhDaoTaoDaiHoc '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            if(data.Rows.Count > 0)
            {
                dt = new DaoTaoDaiHoc
                {
                    HeDaoTao = data.Rows[0]["HeDaoTao"].ToString(),
                    NoiDTao = data.Rows[0]["NoiDTao"].ToString(),
                    NganhHoc = data.Rows[0]["NganhHoc"].ToString(),
                    NuocDaoTao = data.Rows[0]["NuocDaoTao"].ToString(),
                    NamTotNghiep = data.Rows[0]["NamTotNghiem"].ToString()
                };
                return dt;
            }
            dt = new DaoTaoDaiHoc
            {
                HeDaoTao = "...................",
                NoiDTao = "...................",
                NganhHoc = "...................",
                NuocDaoTao = "...................",
                NamTotNghiep = "...................",
            };

            return dt;
        }
    }
}
