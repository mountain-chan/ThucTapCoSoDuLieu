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
    public class LoaiHoiDongDAL
    {
        private LoaiHoiDong LoaiHoiDong;
        public IList<LoaiHoiDong> LayTatCaLoaiHoiDong()
        {
            IList<LoaiHoiDong> listLoaiHoiDong = new List<LoaiHoiDong>();
            string str = "Select * from LoaiHoiDong";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            foreach (DataRow item in data.Rows)
            {
                LoaiHoiDong = new LoaiHoiDong
                {
                    MaLoaiHoiDong = item["MaLoaiHoiDong"].ToString(),
                    TenLoaiHoiDong = item["TenLoaiHoiDong"].ToString()
                };
                listLoaiHoiDong.Add(LoaiHoiDong);
            }
            return listLoaiHoiDong;
        }
    }
}
