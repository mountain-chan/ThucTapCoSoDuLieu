using PhanMenQuanLyGiaoVien.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMenQuanLyGiaoVien.DAL
{
    public class DoiTuongMienGiamDAL
    {
        public List<string> DanhSachDoiTuongMienGiam(string maGV, string namHoc, string kiHoc)
        {
            List<string> list = new List<string>();
            string str = "DoiTuongMienGiamChucVuCQ '" + maGV + "','" + namHoc + "','" + kiHoc + "'";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach(DataRow row in data.Rows)
            {
                list.Add(row["TenChucVu"].ToString());
            }
            str = "DoiTuongMienGiamChucVuDang '" + maGV + "','" + namHoc + "','" + kiHoc + "'";
            data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow row in data.Rows)
            {
                list.Add(row["TenChucVuDang"].ToString());
            }
            if (list.Count == 0) list.Add("Không có");
            return list;
        }
    }
}
