using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMenQuanLyGiaoVien.Models
{
    public class ChucDanhNhaGiao
    {
        public string TenChucVu { get; set; }
        public string Nam { get; set; }
        public string NoiBoNhiem { get; set; }
        public string MaChucVu { get; set; }
        public string NgayNhan { get; set; }

        public override string ToString()
        {
            return TenChucVu;
        }
    }
}
