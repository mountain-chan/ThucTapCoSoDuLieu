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
    public class TrinhDoNgoaiNguDAL
    {
        private string[] ngay;
        public IList<TrinhDoNgoaiNgu> listTrinhDo(string maGV)
        {
            IList<TrinhDoNgoaiNgu> list = new List<TrinhDoNgoaiNgu>();
            string str = "TrinhDoNgoaiNguGiaoVien '" + maGV + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                ngay = item["NgayCapChungChi"].ToString().Split(' ');
                TrinhDoNgoaiNgu hv = new TrinhDoNgoaiNgu
                {
                    TenTrinhDo = item["TenTrinhDo"].ToString(),
                    NgayCapChungChi = ngay[0]
                };
                list.Add(hv);
            }
            return list;
        }
    }
}
