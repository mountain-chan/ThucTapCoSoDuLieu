using PhanMenQuanLyGiaoVien.Entity;
using System.Data;
namespace PhanMenQuanLyGiaoVien.DAL
{
    public class TongHopTaiCacGiaoVienDAL
    {
        
        public DataTable tongHop(string maBM, string namHoc, string kiHoc)
        {
            string str = "select * from TongHopTaiCacGiaoVien('" + maBM + "', '" + namHoc + "', '" + kiHoc + "')";
            DataTable data = new DataTable();
            data = Server.LayDuLieuBang(str);
            return data;
        }
    }
}
