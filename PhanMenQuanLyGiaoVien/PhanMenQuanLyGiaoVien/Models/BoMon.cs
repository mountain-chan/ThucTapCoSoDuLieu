
namespace PhanMenQuanLyGiaoVien.Models
{
    public class BoMon
    {
        private string maBoMon;
        private string tenBoMon;
        private string maChuNhiem;
        private string maKhoa;

        public BoMon(string maBoMon, string tenBoMon, string maKhoa,  string maChuNhiem)
        {
            this.maBoMon = maBoMon;
            this.maKhoa = maKhoa;
            this.tenBoMon = tenBoMon;
            this.maChuNhiem = maChuNhiem;
        }

        public string MaBoMon
        {
            get => maBoMon;
            set => maBoMon = value;
        }

        public string MaKhoa
        {
            get => maKhoa;
            set => maKhoa = value;
        }

        public string TenBoMon
        {
            get => tenBoMon;
            set => tenBoMon = value;
        }

        public string MaChuNhiem
        {
            get => maChuNhiem;
            set => maChuNhiem = value;
        }

        public override string ToString()
        {
            return tenBoMon;
        }
    }
}
