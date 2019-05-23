using PhanMenQuanLyGiaoVien.Models;
using System.Collections.Generic;
using System.Data;
using PhanMenQuanLyGiaoVien.Entity;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class BoMonDAL
    {
        private BoMon bm;
        private DataTable data;

        public List<BoMon> LayTatCaBoMon()
        {
            List<BoMon> listBM = new List<BoMon>();
            data = new DataTable();
            string str = "select * from BoMon";
            data = Server.LayDuLieuBang(str);
            foreach(DataRow item in data.Rows)
            {
                bm = new BoMon
                {
                    MaBoMon = item["MaBoMon"].ToString(),
                    TenBoMon = item["TenBM"].ToString(),
                    MaKhoa = item["MaKhoa"].ToString(),
                    MaChuNhiem = item["MaChuNhiem"].ToString()
                };

                listBM.Add(bm);
            }
            return listBM;
        }

        public List<BoMon> LayBoMonTheoKhoa(string maK)
        {
            List<BoMon> listBM = new List<BoMon>();
            data = new DataTable();
            string str = "select * from BoMon where MaKhoa = '" + maK + "'";
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                bm = new BoMon
                {
                    MaBoMon = item["MaBoMon"].ToString(),
                    TenBoMon = item["TenBM"].ToString(),
                    MaKhoa = item["MaKhoa"].ToString(),
                    MaChuNhiem = item["MaChuNhiem"].ToString()
                };

                listBM.Add(bm);
            }
            return listBM;
        }

        public BoMon LayBoMonTheoGiaoVien(string maGV)
        {
            List<BoMon> listBM = new List<BoMon>();
            data = new DataTable();
            string str = "LayBoMonTheoGiaoVien '" + maGV + "'";
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                bm = new BoMon
                {                 
                    TenBoMon = data.Rows[0]["TenBM"].ToString(),
                    NgayChuyenDen = data.Rows[0]["NgayChuyenDen"].ToString()                 
                };
                return bm;
            }
            return null;
        }

        public BoMon LayBoMonTheoMa(string maBM)
        {
            if(maBM is null)
            {
                bm = new BoMon
                {
                    MaBoMon = null,
                    TenBoMon = "Không có bộ môn"       
                };
                return bm;
            }
            else
            {
                data = new DataTable();
                string str = "select * from BoMon where MaBoMon = '" + maBM + "'";
                data = Server.LayDuLieuBang(str);
                if (data.Rows.Count > 0)
                {
                    bm = new BoMon
                    {
                        MaBoMon = data.Rows[0]["MaBoMon"].ToString(),
                        TenBoMon = data.Rows[0]["TenBM"].ToString(),
                        MaKhoa = data.Rows[0]["MaKhoa"].ToString(),
                        MaChuNhiem = data.Rows[0]["MaChuNhiem"].ToString()
                    };
                    return bm;
                }
            }         
            return null;
        }

    }
}
