using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMenQuanLyGiaoVien.Models
{
    public class Khoa
    {
        private string maKhoa;
        private string tenKhoa;
        private string maChuNhiem;

        public Khoa(string maKhoa, string tenKhoa, string maChuNhiem)
        {
            this.maKhoa = maKhoa;
            this.tenKhoa = tenKhoa;
            this.maChuNhiem = maChuNhiem;
        }

        public string MaKhoa
        {
            get => maKhoa;
            set => maKhoa = value;
        }

        public string TenKhoa
        {
            get => tenKhoa;
            set => tenKhoa = value;
        }

        public string MaChuNhiem
        {
            get => maChuNhiem;
            set => maChuNhiem = value;
        }
    }
}
