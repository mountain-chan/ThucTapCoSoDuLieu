using PhanMenQuanLyGiaoVien.Models;
using System.Collections.Generic;
using System.Data;
using PhanMenQuanLyGiaoVien.Entity;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class KhoaDAL
    {
        private Khoa bm;
        private DataTable data;

        public List<Khoa> LayTatKhoa()
        {
            List<Khoa> listKhoa = new List<Khoa>();
            data = new DataTable();
            string str = "select * from Khoa";
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                bm = new Khoa
                {
                    MaKhoa = item["MaKhoa"].ToString(),
                    TenKhoa = item["TenKhoa"].ToString(),
                    MaChuNhiem = item["MaChuNhiem"].ToString()
                };

                listKhoa.Add(bm);
            }
            return listKhoa;
        }

        public Khoa LayKhoaTheoMa(string maBM)
        {
            data = new DataTable();
            string str = "select * from Khoa where MaKhoa = '" + maBM + "'";
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                bm = new Khoa
                {
                    MaKhoa = data.Rows[0]["MaKhoa"].ToString(),
                    TenKhoa = data.Rows[0]["TenKhoa"].ToString(),
                    MaChuNhiem = data.Rows[0]["MaChuNhiem"].ToString()
                };
                return bm;
            }
            return null;
        }
    }
}
