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
                bm = new BoMon(item["MaBoMon"].ToString(), item["TenBM"].ToString(), item["MaKhoa"].ToString(),
                    item["MaChuNhiem"].ToString());

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
                bm = new BoMon(item["MaBoMon"].ToString(), item["TenBM"].ToString(),
                    item["MaKhoa"].ToString(),
                    item["MaChuNhiem"].ToString());

                listBM.Add(bm);
            }
            return listBM;
        }

        public BoMon LayBoMonTheoMa(string maBM)
        {
            data = new DataTable();
            string str = "select * from BoMon where MaBoMon = '" + maBM + "'";
            data = Server.LayDuLieuBang(str);
            if(data.Rows.Count > 0)
            {
                bm = new BoMon(data.Rows[0]["MaBoMon"].ToString(), data.Rows[0]["TenBM"].ToString(), 
                    data.Rows[0]["MaKhoa"].ToString(), data.Rows[0]["MaChuNhiem"].ToString());
                return bm;
            }
            return null;
        }

    }
}
