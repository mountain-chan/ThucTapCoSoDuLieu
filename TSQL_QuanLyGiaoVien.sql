
USE QuanLyGV
GO


-------------------------------Thông tin cá nhân của giáo viên----------------------------------------
----Dịnh mức giảng dạy
Create function DinhMucTaiDaoTao(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @dinhMuc int, @dinhMucHocHam int, @ketQua int
	select top 1 @dinhMuc = QuyDinhChung from DinhMucGiangDay, ChucVu_ChMonKT, GV_ChucVuChMKT 
	where ChucVu_ChMonKT.MaChucVu = GV_ChucVuChMKT.MaChucVu and DinhMucGiangDay.MaChucVu = ChucVu_ChMonKT.MaChucVu
	and GV_ChucVuChMKT.MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by NgayNhan desc

	select top 1 @dinhMucHocHam = QuyDinhChung from DinhMucGiangDay, HocHam, GV_HocHam 
	where HocHam.MaHocHam = GV_HocHam.MaHocHam and DinhMucGiangDay.MaHocHam = HocHam.MaHocHam
	and MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by NgayNhan desc

	if(@dinhMuc is null) set @dinhMuc = 0
	if(@dinhMucHocHam is null) set @dinhMucHocHam = 0
	if(@dinhMucHocHam > @dinhMuc) set @ketQua = @dinhMucHocHam
	else set @ketQua = @dinhMuc
	return @ketQua/2
end
go
select dinhmuc = dbo.DinhMucTaiDaoTao('GV04', '2017-2018', '1')

----Đinh mức nghiên cứu khoa học
create function DinhMucTaiNghienCuu(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @dinhMuc int, @dinhMucHocHam int, @ketQua int
	select top 1 @dinhMuc = DinhMucGioChuan from DinhMucNghienCuu, ChucDanh_ChMonNV, GV_ChucDanhChMNV 
	where ChucDanh_ChMonNV.MaChucDanh = GV_ChucDanhChMNV.MaChucDanh and DinhMucNghienCuu.MaChucDanh = ChucDanh_ChMonNV.MaChucDanh
	and MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by NgayNhan desc

	select top 1 @dinhMucHocHam = DinhMucGioChuan from DinhMucNghienCuu, HocHam, GV_HocHam 
	where HocHam.MaHocHam = GV_HocHam.MaHocHam and DinhMucNghienCuu.MaHocHam = HocHam.MaHocHam
	and MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by NgayNhan desc

	if(@dinhMuc is null) set @dinhMuc = 0
	if(@dinhMucHocHam is null) set @dinhMucHocHam = 0
	if(@dinhMucHocHam > @dinhMuc) set @ketQua = @dinhMucHocHam
	else set @ketQua = @dinhMuc
	return @ketQua/2
end
go

select dinhmuc = dbo.DinhMucTaiNghienCuu('GV02', '2017-2018', '1')


----Tỷ lệ miễn giảm
alter function TyLeMienGiam(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @MienGiamChucVuCQ float, @MienGiamChucVuDang float, @tong float
	
	select @MienGiamChucVuCQ = Sum(TyLeMienGiam) from ChucVuChinhQuyen, GV_ChucVuChQ 
	where ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu and GV_ChucVuChQ.MaGiaoVien = @MaGiaoVien
	and (
	(dbo.CheckTime(@namHoc, @kiHoc, NgayNhan, NgayKetThuc) = 1)
	or
	(NgayKetThuc is null and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1))

	select @MienGiamChucVuDang = Sum(TyLeMienGiam) from ChucVuDang, GV_ChucVuDang 
	where ChucVuDang.MaChucVuDang = GV_ChucVuDang.MaChucVuDang and GV_ChucVuDang.MaGiaoVien = @MaGiaoVien
	and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1

	if(@MienGiamChucVuCQ is null) set @MienGiamChucVuCQ = 0
	if(@MienGiamChucVuCQ > 50) set @MienGiamChucVuCQ = 50
	if(@MienGiamChucVuDang is null) set @MienGiamChucVuDang = 0
	if(@MienGiamChucVuDang > 50) set @MienGiamChucVuDang = 50
	
	if(@MienGiamChucVuCQ > @MienGiamChucVuDang)
		set @tong = @MienGiamChucVuCQ
	else
		set @tong = @MienGiamChucVuDang

	return @tong
end
go
select dinhmuc = dbo.TyLeMienGiam('GV02', '2018-2019', '1')

------------------------------------------------Ham
create function CheckTimeBegin(@namHoc varchar(10), @kiHoc nvarchar(10), @ngayNhan date) returns bit
as
begin
	declare @result bit
	
	set @result = 0
	if(@kiHoc = '1')
		if(datepart(year, @ngayNhan)<LEFT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)=LEFT(@namHoc, 4) and datepart(month, @ngayNhan) <= 8) 
			set @result = 1
	if(@kiHoc = '2')
		if(datepart(year, @ngayNhan)<RIGHT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)=RIGHT(@namHoc, 4) and datepart(month, @ngayNhan) <= 1)
			set @result = 1

	return @result
end
go
----------------------
alter function CheckTime(@namHoc varchar(10), @kiHoc nvarchar(10), @ngayNhan date, @ngayKetThuc date) returns bit
as
begin
	declare @result bit
	
	set @result = 0
	if(@kiHoc = '1')
		if(datepart(year, @ngayNhan)<LEFT(@namHoc, 4) and datepart(year, @ngayKetThuc)>LEFT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)=LEFT(@namHoc, 4) and datepart(month, @ngayNhan) <= 8 
		and datepart(year, @ngayKetThuc)>LEFT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)<LEFT(@namHoc, 4)
		and datepart(year, @ngayKetThuc)=LEFT(@namHoc, 4) and datepart(month, @ngayKetThuc) = 12)
			set @result = 1
		else if(datepart(year, @ngayNhan)=LEFT(@namHoc, 4) and datepart(month, @ngayNhan) <= 8
		and datepart(year, @ngayKetThuc)=LEFT(@namHoc, 4) and datepart(month, @ngayKetThuc) = 12)
			set @result = 1
	if(@kiHoc = '2')
		if(datepart(year, @ngayNhan)<RIGHT(@namHoc, 4) and datepart(year, @ngayKetThuc)>RIGHT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)=RIGHT(@namHoc, 4) and datepart(month, @ngayNhan) <= 1
		and datepart(year, @ngayKetThuc)>RIGHT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)<RIGHT(@namHoc, 4)
		and datepart(year, @ngayKetThuc)=RIGHT(@namHoc, 4) and datepart(month, @ngayKetThuc) >= 5)
			set @result = 1
		else if(datepart(year, @ngayNhan)=RIGHT(@namHoc, 4) and datepart(month, @ngayNhan) <= 1
		and datepart(year, @ngayKetThuc)=RIGHT(@namHoc, 4) and datepart(month, @ngayKetThuc) >= 5)
			set @result = 1

	return @result
end
go
-----------------------------------------------------
----=============================Tải yêu cầu===========================---
alter function ThongTinCaNhanVaTaiYeucau(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns @table table
(
	TenGiaoVien nvarchar(40),
	DonVi nvarchar(40),
	ChucVu nvarchar(40),
	HocHam nvarchar(40),
	HocVi nvarchar(40),
	DinhMucTaiDaoTao float,
	DinhMucTaiNghienCuu float,
	TyLeMienGiam float,
	TaiDaoTaoYeuCau float,
	TaiNghienCuuYeuCau float
)
as
begin
	declare @dinhMucTaiDaoTao float, @dinhMucTaiNghienCuu float, @tyLeMienGiam float, 
	@taiDaoTaoYeuCau float, @taiNghienCuuYeuCau float, @tenGV nvarchar(40), @donVi nvarchar(40),
	@chucVu nvarchar(40), @hocHam nvarchar(40), @hocVi nvarchar(40)

	set @dinhMucTaiDaoTao = dbo.DinhMucTaiDaoTao(@MaGiaoVien, @namHoc, @kiHoc)
	set @dinhMucTaiNghienCuu = dbo.DinhMucTaiNghienCuu(@MaGiaoVien, @namHoc, @kiHoc)
	set @tyLeMienGiam = dbo.TyLeMienGiam(@MaGiaoVien, @namHoc, @kiHoc)

	set @taiDaoTaoYeuCau = @dinhMucTaiDaoTao - @dinhMucTaiDaoTao*@tyLeMienGiam/100
	set @taiNghienCuuYeuCau = @dinhMucTaiNghienCuu - @dinhMucTaiNghienCuu*@tyLeMienGiam/100

	select @tenGV = TenGiaoVien from GiaoVien where MaGiaoVien = @MaGiaoVien
	select @donVi = tenBM  from GiaoVien join BoMon on GiaoVien.maBoMon=BoMon.MaBoMon where MaGiaoVien = @MaGiaoVien

	select top 1 @chucVu = TenChucVu  from GV_ChucVuChQ join ChucVuChinhQuyen on ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_ChucVuChQ.NgayNhan desc

	select top 1 @hocHam = TenHocHam  from GV_HocHam join HocHam on HocHam.MaHocHam = GV_HocHam.MaHocHam 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_HocHam.NgayNhan desc

	select top 1 @hocVi = TenHocVi  from GV_HocVi join HocVi on HocVi.MaHocVi = GV_HocVi.MaHocVi 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_HocVi.NgayNhan desc

	insert into @table values(@tenGV, @donVi, @chucVu, @hocHam, @hocVi, @dinhMucTaiDaoTao, @dinhMucTaiNghienCuu, 
	@tyLeMienGiam, @taiDaoTaoYeuCau, @taiNghienCuuYeuCau)

	return 
end
go

select * from ThongTinCaNhanVaTaiYeuCau('GV02', '2018-2019', '1')
go
DoiTuongMienGiam 'GV02','2018-2019','2'

----------------------
create proc DoiTuongMienGiam
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10)
as
begin
	select TenChucVu from ChucVuChinhQuyen, GV_ChucVuChQ 
	where ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu and GV_ChucVuChQ.MaGiaoVien = @MaGiaoVien
	and ((dbo.CheckTime(@namHoc, @kiHoc, NgayNhan, NgayKetThuc) = 1)
	or
	(NgayKetThuc is null and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1))

	select TenChucVuDang from ChucVuDang, GV_ChucVuDang 
	where ChucVuDang.MaChucVuDang = GV_ChucVuDang.MaChucVuDang and GV_ChucVuDang.MaGiaoVien = @MaGiaoVien
	and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
end
go


-------------------------------------Tải Dạy Học--------------------------------------------
alter function taiDayHoc(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10), @maDoiTuong varchar(10)) returns float
as
begin
	declare @Tai_Day_LThuyet float, @Tai_Day_NgoaiNgu float, @Tai_Day_TDuc float, @tai_DayVKT float,
	@tai_DayPhuDao float, @tai_HDKT float, @Tong_Tai float
	
	select @Tai_Day_LThuyet = Sum(GV_LopHocPhan.SoTiet*DayLiThuyet.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayLiThuyet on HocPhan.MaDayLiThuyet=DayLiThuyet.MaDayLiThuyet where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	select @Tai_Day_NgoaiNgu = Sum(GV_LopHocPhan.SoTiet*DayNgoaiNgu.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayNgoaiNgu on HocPhan.MaDayNgoaiNgu=DayNgoaiNgu.MaDayNgoaiNgu where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	select @Tai_Day_TDuc = Sum(GV_LopHocPhan.SoTiet*DayTheDucTheThao.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayTheDucTheThao on HocPhan.MaDayTheDuc=DayTheDucTheThao.MaDayTheDuc where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	select @tai_DayVKT = Sum(GV_LopHocPhan.SoTiet*DayVeKiThuat.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayVeKiThuat on HocPhan.MaDayVeKiThuat=DayVeKiThuat.MaDayVeKiThuat where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	select @tai_DayPhuDao = Sum(GV_LopHocPhan.SoTiet*DayPhuDao.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayPhuDao on HocPhan.MaDayPhuDao=DayPhuDao.MaDayPhuDao where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	select @tai_HDKT = Sum(GV_LopHocPhan.SoTiet*DayBaiTapKiemTraThucHannh.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join DayBaiTapKiemTraThucHannh on HocPhan.MaDayBaiTapKiemTraThucHannh=DayBaiTapKiemTraThucHannh.MaDayBaiTapKiemTraThucHannh
	where NamHoc=@namHoc and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	if(@Tai_Day_TDuc is NULL) set @Tai_Day_TDuc = 0 
	if(@Tai_Day_LThuyet is NULL) set @Tai_Day_LThuyet = 0 
	if(@Tai_Day_NgoaiNgu is NULL) set @Tai_Day_NgoaiNgu = 0
	if(@tai_DayVKT is NULL) set @tai_DayVKT = 0
	if(@tai_DayPhuDao is NULL) set @tai_DayPhuDao = 0
	if(@tai_HDKT is NULL) set @tai_HDKT = 0

	Set @Tong_Tai = @Tai_Day_LThuyet + @Tai_Day_NgoaiNgu + @Tai_Day_TDuc +  @tai_DayVKT + @tai_DayPhuDao + @tai_HDKT
	return @Tong_Tai
end
go

--------------------------------Tải dạy học theo đối tượng------------------------------
alter proc taiDayHocTheoDoiTuong
(
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10)
)
as
begin
	select N'Tải Dạy Cao Dẳng: ', Tai = dbo.taiDayHoc(@maGiaoVien,@namHoc,@kiHoc, 'He01')
	select N'Tải Dạy Dại Hoc: ', Tai = dbo.taiDayHoc(@maGiaoVien,@namHoc,@kiHoc, 'He02')
	select N'Tải Dạy Cao Học: ', Tai = dbo.taiDayHoc(@maGiaoVien,@namHoc,@kiHoc, 'He03')
end
go
taiDayHocTheoDoiTuong 'GV01', '2018-2019','1'

---------Thông Tin dạy Học
alter proc ThongTinDayHoc
(
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10)
)
as
begin
	select TenHocPhan, SiSo, TenHe, SoTinChi, SoTiet
	from GV_LopHocPhan join LopHocPhan on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan
	join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan join He on He.MaHe=HocPhan.MaDoiTuongHoc
	where MaGiaoVien=@maGiaoVien and NamHoc=@namHoc and KiHoc=@kiHoc and MaHe = 'He01'

	select TenHocPhan, SiSo, TenHe, SoTinChi, SoTiet
	from GV_LopHocPhan join LopHocPhan on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan
	join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan join He on He.MaHe=HocPhan.MaDoiTuongHoc
	where MaGiaoVien=@maGiaoVien and NamHoc=@namHoc and KiHoc=@kiHoc and MaHe = 'He02'

	select TenHocPhan, SiSo, TenHe, SoTinChi, SoTiet
	from GV_LopHocPhan join LopHocPhan on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan
	join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan join He on He.MaHe=HocPhan.MaDoiTuongHoc
	where MaGiaoVien=@maGiaoVien and NamHoc=@namHoc and KiHoc=@kiHoc and MaHe = 'He03'
end
go
ThongTinDayHoc 'GV01', '2018-2019','1'

--==========================================================================================================================





----------------------------------Tai Khảo Thí-------------------------------------------
create function taiKhaoThi(@MaGiaoVien varchar(10), @namHoc varchar(10), @KiHoc nvarchar(10)) returns float
as
begin
	declare @taiChamThi float, @taiChamDA_BTL float, @taiSuaDeThi float, @tongTai float

	select @taiChamThi=Sum(SoHocVien*GioChuan) from GV_ChamThi join LoaiChamThi 
	on GV_ChamThi.MaLoaiChamThi=LoaiChamThi.MaLoaiChamThi where NamHoc=@namHoc 
	and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	select @taiChamDA_BTL=Sum(SoDoAn*GioChuan) from GV_ChamThiDoAnBTL join LoaiChamThiDoAn_BTL 
	on GV_ChamThiDoAnBTL.MaLoaiChamThi=LoaiChamThiDoAn_BTL.MaLoaiChamThi 
	where NamHoc=@namHoc and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	select @taiSuaDeThi=Sum(SoHocPhan*GioChuan) from GV_SuaDoiBoSungNganHangDeThi join SuaDoiBoSungNganHangDeThi 
	on GV_SuaDoiBoSungNganHangDeThi.MaSuaDoi =SuaDoiBoSungNganHangDeThi.MaSuaDoi where NamHoc=@namHoc 
	and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	if(@taiChamThi is null) set @taiChamThi = 0
	if(@taiChamDA_BTL is null) set @taiChamDA_BTL = 0
	if(@taiSuaDeThi is null) set @taiSuaDeThi = 0

	set @tongTai = @taiChamThi + @taiChamDA_BTL + @taiSuaDeThi
	return @tongTai
end
go

-----------------------Tải Tham Gia Hội Đồng------------------------------------------
create function taiHoiDong(@MaGiaoVien varchar(10), @namHoc varchar(10), @KiHoc nvarchar(10)) returns float
as
begin
	declare @tongSG float
	select @tongSG = sum(SoGio) from GV_HoiDong where NamHoc = @namHoc and MaGiaoVien = @MaGiaoVien
	and KiHoc = @KiHoc
	if(@tongSG is null) set @tongSG = 0
	return @tongSG	
end

---------------------------------Tải Hướng Dấn----------------------------------------------
alter function taiHuongDan(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @taiDHDAMH float, @taiHDan float, @tong float

	select @taiDHDAMH = sum(SoLuongHocVien*GioChuan) from GV_DoAnMonHoc join DoAnMonHoc 
	on GV_DoAnMonHoc.MaLoaiDoAn=DoAnMonHoc.MaLoaiDoAn and MaGiaoVien = @MaGiaoVien 
	and namHoc = @namHoc and KiHoc = @kiHoc

	select @taiHDan = sum(SoLuongHocVien*GioChuan) from GV_HuongDan join LoaiHuongDan on GV_HuongDan.MaLoaiHuongDan = LoaiHuongDan.MaLoaiHuongDan
	and MaGiaoVien = @MaGiaoVien and dbo.kiemTraNgay(@namHoc, @kiHoc, ngayBatDau)

	if(@taiDHDAMH is null) set @taiDHDAMH = 0
	if(@taiHDan is null) set @taiHDan = 0
	set @tong = @taiDHDAMH + @taiHDan
	return @tong
end
-------------------------------Tải Nghiên Cứu Khoa Học------------------------------------
create function taiNCKH(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @taiVietSach1 float, @taiVietSach2 float, @taiBaiBao float, @taiNghienCuuDeTai float, @tong float

	--select @taiVietSach1 = Sum(SoTrangDaViet*GioChuan) from GV_BienSoanSach join Sach 
	--on GV_BienSoanSach.MaSach=Sach.MaSach join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach 
	--where MaGiaoVien = @MaGiaoVien and  LoaiSach.MaLoaiSach = 'LS01'
	
	--select @taiVietSach2 = Sum(4*SoTinChi*GioChuan/(5*dbo.SoThanhVien(Sach.maSach)) + dbo.chuBien(@MaGiaoVien,Sach.maSach)) 
	--from GV_BienSoanSach join Sach on GV_BienSoanSach.MaSach=Sach.MaSach join LoaiSach 
	--on LoaiSach.MaLoaisach = Sach.MaLoaiSach where MaGiaoVien = @MaGiaoVien and LoaiSach.MaLoaiSach <> 'LS01'

	--select @taiBaiBao = Sum(GioChuan/dbo.SoThanhVien(BaiBao.MaBaiBao)) from GV_BaiBao join BaiBao 
	--on GV_BaiBao.MaBaiBao=BaiBao.MaBaiBao join LoaiBaiBao on LoaiBaiBao.MaLoaiBaiBao = BaiBao.MaLoaiBaiBao 
	--where MaGiaoVien = @MaGiaoVien and datepart(YEAR, NgayCongBo) = @namHoc

	--select @taiNghienCuuDeTai = Sum(4*GioChuan/(5*dbo.SoThanhVien(DeTai.MaDeTai)) + dbo.chuTri(@MaGiaoVien,DeTai.MaDeTai))
	--from GV_DeTaiNghienCuu join DeTai on DeTai.MaDeTai=GV_DeTaiNghienCuu.MaDeTai join LoaiDeTaiNghienCuu 
	--on LoaiDeTaiNghienCuu.MaLoaiDeTai=DeTai.MaLoaiDeTai where MaGiaoVien = @MaGiaoVien 

	if(@taiVietSach1 is null) set @taiVietSach1 = 0
	if(@taiVietSach2 is null) set @taiVietSach2 = 0
	if(@taiBaiBao is null) set @taiBaiBao = 0
	if(@taiNghienCuuDeTai is null) set @taiNghienCuuDeTai = 0
	set @tong = @taiVietSach1 + @taiVietSach2 + @taiBaiBao + @tainghienCuuDeTai
	return @tong
end
go
-----------------------------hàm
create function SoThanhVien(@Ma varchar(10)) returns int
as
begin
	declare  @tong int
	select @tong = COUNT(*) from GV_BienSoanSach where MaSach = @Ma
	if(@tong = 0)
	 select @tong = COUNT(*) from GV_BaiBao where MaBaiBao = @Ma
	if(@tong = 0)
	 select @tong = COUNT(*) from GV_DeTaiNghienCuu where MaDeTai = @Ma
	return @tong
end
go

create function chuBien(@MaGiaoVien varchar(10), @maSach varchar(10)) returns int
as
begin
	declare @maChu varchar(10), @tong int
	select @maChu = MaGiaoVien from GV_BienSoanSach where MaSach = @maSach and VaiTro = N'Chủ Biên'
	if(@MaGiaoVien = @maChu) 
		select @tong = SoTinChi*GioChuan/5 from Sach join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach
		where MaSach=@maSach
	else
		set @tong = 0
	return @tong
end
go
create function chuTri(@MaGiaoVien varchar(10), @maDeTai varchar(10)) returns int
as
begin
	declare @maChuTri varchar(10), @tong int
	select @maChuTri = MaGiaoVien from GV_DeTaiNghienCuu where MaDeTai = @maDeTai and VaiTro = N'Chủ chì'
	if(@MaGiaoVien = @maChuTri) 
		select @tong = GioChuan/5 from DeTai join LoaiDeTaiNghienCuu on LoaiDeTaiNghienCuu.MaLoaiDeTai = DeTai.MaLoaiDeTai
		where MaDeTai=@maDeTai
	else
		set @tong = 0
	return @tong
end

--====================================Tính tải giáo viên================================----
alter function tinhTaiGiaoVien(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns @table table
(
	TenGiaoVien nvarchar(40),
	TaiGiangDay float,
	TaiHuongDan float,
	TaiKhaoThi float,
	TaiHoiDong float,
	TaiNCKH float,
	TongTai float
)
as
begin
	declare @taiDayHoc float, @taiKhaoThi float, @taiHoiDong float, @taiHuongDan float, @tongTai float,
	@taiNCKH float, @tenGV nvarchar(40)

	set @taiDayHoc = dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He01') + dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He02')
		+ dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He03')

	set @taiKhaoThi = dbo.taiKhaoThi(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiHuongDan = dbo.taiHuongDan(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiHoiDong = dbo.taiHoiDong(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiNCKH = dbo.taiNCKH(@MaGiaoVien, @namHoc, @kiHoc)

	set @tongTai = @taiDayHoc + @taiKhaoThi + @taiHoiDong + @taiHuongDan + @taiNCKH

	select @tenGV = TenGiaoVien from GiaoVien where MaGiaoVien = @MaGiaoVien

	insert into @table values(@tenGV, @taiDayHoc, @taiHuongDan, @taiKhaoThi, @taiHoiDong, @taiNCKH, @tongTai)

	return 
end
go
select * from tinhTaiGiaoVien('GV01', '2018-2019', '1')
											     
---- Kiểm tra xem ngày thuộc năm học nào, học kì nào ----
alter function kiemTraNgay(@namHoc varchar(10), @kiHoc varchar(10), @ngayBatDau date) returns bit
as
begin
	declare @kq bit = 0
	if @kiHoc = '1'
		begin
			if (datepart(YEAR, @ngayBatDau) = left(@namHoc, 4) and datepart(MONTH, @ngayBatDau) >= 7) set @kq = 1
		end
	else
		if (datepart(YEAR, @ngayBatDau) = right(@namHoc, 4) and datepart(MONTH, @ngayBatDau) < 7) set @kq = 1
	return @kq
end
---- Tải Khảo Thí ----
alter function taiKhaoThi(@MaGiaoVien varchar(10), @namHoc varchar(10), @KiHoc nvarchar(10)) returns float
as
begin
	declare @taiChamThi float, @taiChamDA_BTL float, @taiSuaDeThi float, @tongTai float

	select @taiChamThi=Sum(SoHocVien*GioChuan) from GV_ChamThi join LoaiChamThi 
	on GV_ChamThi.MaLoaiChamThi=LoaiChamThi.MaLoaiChamThi where NamHoc=@namHoc 
	and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	select @taiChamDA_BTL=Sum(SoDoAn*GioChuan) from GV_ChamThiDoAnBTL join LoaiChamThiDoAn_BTL 
	on GV_ChamThiDoAnBTL.MaLoaiChamThi=LoaiChamThiDoAn_BTL.MaLoaiChamThi 
	where NamHoc=@namHoc and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	select @taiSuaDeThi=Sum(SoHocPhan*GioChuan) from GV_SuaDoiBoSungNganHangDeThi join SuaDoiBoSungNganHangDeThi 
	on GV_SuaDoiBoSungNganHangDeThi.MaSuaDoi =SuaDoiBoSungNganHangDeThi.MaSuaDoi where NamHoc=@namHoc 
	and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	if(@taiChamThi is null) set @taiChamThi = 0
	if(@taiChamDA_BTL is null) set @taiChamDA_BTL = 0
	if(@taiSuaDeThi is null) set @taiSuaDeThi = 0

	set @tongTai = @taiChamThi + @taiChamDA_BTL + @taiSuaDeThi
	return @tongTai
end
go

select dbo.taiKhaoThi('GV01','2018-2019','1')

---- Tải Hướng Dấn ----
alter function taiHuongDan(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @taiDHDAMH float, @taiHDan float, @tong float

	select @taiDHDAMH = sum(SoLuongHocVien*GioChuan) from GV_DoAnMonHoc join DoAnMonHoc 
	on GV_DoAnMonHoc.MaLoaiDoAn=DoAnMonHoc.MaLoaiDoAn and MaGiaoVien = @MaGiaoVien 
	and namHoc = @namHoc and KiHoc = @kiHoc

	select @taiHDan = sum(GioChuan) from GV_HuongDan join LoaiHuongDan on GV_HuongDan.MaLoaiHuongDan = LoaiHuongDan.MaLoaiHuongDan
	and MaGiaoVien = @MaGiaoVien and dbo.kiemTraNgay(@namHoc, @kiHoc, ngayBatDau) = 1

	if(@taiDHDAMH is null) set @taiDHDAMH = 0
	if(@taiHDan is null) set @taiHDan = 0
	set @tong = @taiDHDAMH + @taiHDan
	return @tong
end

select dbo.taiHuongDan('GV01','2018-2019','1')

---- In Các Đồ Án ----
create function inCacDoAn(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns @table table
(
	MaHocVien varchar(10),
	TenHocVien nvarchar(40),
	TenDeTai nvarchar(50)
)
as
begin
	insert into @table select HocVien.MaHocVien, TenHocVien, TenDeTai
	from GV_HuongDan join HocVien on GV_HuongDan.MaHocVien = HocVien.MaHocVien
	where MaGiaoVien = @MaGiaoVien and dbo.kiemTraNgay(@namHoc, @kiHoc, NgayBatDau) = 1
	return 
end

select * from dbo.inCacDoAn('GV01', '2018-2019', '1')
									     
