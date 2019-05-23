
namespace PhanMenQuanLyGiaoVien.Models
{
    public class BoMon
    {
        public string MaBoMon;
        public string TenBoMon;
        public string MaChuNhiem;
        public string MaKhoa;
        public string NgayChuyenDen { get; set; }
        public string NgayChuyenDi { get; set; }

        public override string ToString()
        {
            return TenBoMon;
        }
    }
}
