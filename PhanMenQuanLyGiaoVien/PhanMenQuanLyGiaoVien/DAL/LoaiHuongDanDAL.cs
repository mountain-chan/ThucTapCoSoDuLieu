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
    public class LoaiHuongDanDAL
    {
        private LoaiHuongDan LoaiHuongDan;
        public IList<LoaiHuongDan> LayTatCaLoaiHuongDan()
        {
            IList<LoaiHuongDan> listLoaiHuongDan = new List<LoaiHuongDan>();
            string str = "Select * from LoaiHuongDan";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                LoaiHuongDan = new LoaiHuongDan
                {
                    MaLoaiHuongDan = item["MaLoaiHuongDan"].ToString(),
                    TenLoaiHuongDan = item["TenLoaiHuongDan"].ToString()
                };
                listLoaiHuongDan.Add(LoaiHuongDan);
            }
            return listLoaiHuongDan;
        }
    }
}
