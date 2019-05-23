using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhanMenQuanLyGiaoVien.Entity
{
    public class ThamSo
    {       
        public static readonly Font fBody = new Font("Time New Roman", 11);
        public static readonly Font fNormal = new Font("Microsoft Sans Serif", 8);
        public static readonly Font fSign = new Font("Time New Roman", 11, FontStyle.Italic);
        public static readonly Font fTitle = new Font("Arial", 13, FontStyle.Bold);
        public static readonly Font fTitleSmall = new Font("Time New Roman", 12, FontStyle.Bold);
        public static readonly Font fTitleB = new Font("Arial", 15, FontStyle.Bold);
        public static readonly int leftTitle = 80;
        public static readonly int leftBody = 90;
        public static readonly int pdHMax = 20;
        public static readonly int pdHNormal = 10;
        public static readonly int pdHMin = 0;
        public static readonly int lbHeight = 25;      
        public static readonly int tableW = 800;
    }
}
