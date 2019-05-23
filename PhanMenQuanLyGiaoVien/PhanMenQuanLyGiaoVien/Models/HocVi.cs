using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMenQuanLyGiaoVien.Models
{
    public class HocVi
    {
        public string MaHocVi { get; set; }
        public string TenHocVi { get; set; }
        public string NgayNhan { get; set; }
        public override string ToString()
        {
            return TenHocVi;
        }
    }
}
