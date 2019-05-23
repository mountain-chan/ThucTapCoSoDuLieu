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
    public class TongHopDAL
    {
        private TongHop th;
        public TongHop TongHopKetQuaCongTac(string maGV, string namHoc, string kiHoc)
        {
            DataTable data = new DataTable();
            string str = "select * from TongHop('"+maGV+"', '"+namHoc+"', '"+kiHoc+"')";
            data = Server.LayDuLieuBang(str);
            if (data.Rows.Count > 0)
            {
                th = new TongHop{TenGiaoVien = data.Rows[0]["TenGiaoVien"].ToString(),
                    DonVi = data.Rows[0]["DonVi"].ToString(),
                    ChucVu = data.Rows[0]["ChucVu"].ToString(),
                    HocHam = data.Rows[0]["HocHam"].ToString(),
                    HocVi = data.Rows[0]["HocVi"].ToString(),
                    ChuNhiemBoMon = data.Rows[0]["ChuNhiemBoMon"].ToString(),
                    DinhMucTaiDaoTao = data.Rows[0]["DinhMucTaiDaoTao"].ToString(),
                    DinhMucTaiNghienCuu = data.Rows[0]["DinhMucTaiNghienCuu"].ToString(),
                    TyLeMienGiam = data.Rows[0]["TyLeMienGiam"].ToString(),
                    TaiDaoTaoYeuCau = data.Rows[0]["TaiDaoTaoYeuCau"].ToString(),
                    TaiNghienCuuYeuCau = data.Rows[0]["TaiNghienCuuYeuCau"].ToString(),
                    TaiDaoTao = data.Rows[0]["TaiDaoTao"].ToString(),
                    TaiGiangDay = data.Rows[0]["TaiGiangDay"].ToString(),
                    TaiDayCaoDang = data.Rows[0]["TaiDayCaoDang"].ToString(),
                    TaiDayDaiHoc = data.Rows[0]["TaiDayDaiHoc"].ToString(),
                    TaiDayCaoHoc = data.Rows[0]["TaiDayCaoHoc"].ToString(),
                    TaiHuongDan = data.Rows[0]["TaiHuongDan"].ToString(),
                    TaiKhaoThi = data.Rows[0]["TaiKhaoThi"].ToString(),
                    TaiHoiDong = data.Rows[0]["TaiHoiDong"].ToString(),
                    TaiNCKH = data.Rows[0]["TaiNCKH"].ToString(),
                    TaiDeTai = data.Rows[0]["TaiDeTai"].ToString(),
                    TaiBaiBao = data.Rows[0]["TaiBaiBao"].ToString(),
                    TaiBienSoan = data.Rows[0]["TaiBienSoan"].ToString(),
                    TongTai = data.Rows[0]["TongTai"].ToString(),
                    PhanTramTaiDaoTao = data.Rows[0]["PhanTramTaiDaoTao"].ToString(),
                    phanTramTaiNCKH = data.Rows[0]["phanTramTaiNCKH"].ToString()
                };
                return th;
            }
            return null;
        }
    }
}
