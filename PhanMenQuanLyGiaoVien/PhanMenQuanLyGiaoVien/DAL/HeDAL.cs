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
    public class HeDAL
    {
        private He he;
        public IList<He> LayTatCaHe()
        {
            IList<He> listHe = new List<He>();
            string str = "Select * from He";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach(DataRow item in data.Rows)
            {
                he = new He
                {
                    Mahe = item["MaHe"].ToString(),
                    TenHe = item["TenHe"].ToString()
                };
                listHe.Add(he);
            }
            return listHe;
        }
    }
}
