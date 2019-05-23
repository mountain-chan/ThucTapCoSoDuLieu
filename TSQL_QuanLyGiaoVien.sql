



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
alter function TyLeMienGiam(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc int) returns float
as
begin
	declare @MienGiamChucVuCQ float, @MienGiamChucVuDang float, @tong float
	
	select @MienGiamChucVuCQ = Sum(TyLeMienGiam) from ChucVuChinhQuyen, GV_ChucVuChQ 
	where ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu and GV_ChucVuChQ.MaGiaoVien = @MaGiaoVien
	and (dbo.CheckTime(@namHoc, @kiHoc, NgayNhan, NgayKetThuc) = 1)
	
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
select dinhmuc = dbo.TyLeMienGiam('GV02', '2017-2017', 1)


------------------------------------------------Hàm kiểm tra ngày nhận trước thời điểm xet .
alter function CheckTimeBegin(@namHoc varchar(10), @kiHoc nvarchar(10), @ngayNhan date) returns bit
as
begin
	declare @result bit
	
	set @result = 0
	if(@kiHoc = '1')
		if(datepart(year, @ngayNhan)<LEFT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)=LEFT(@namHoc, 4) and datepart(month, @ngayNhan) < 8) 
			set @result = 1		
	if(@kiHoc = '2')
		if(datepart(year, @ngayNhan)<=RIGHT(@namHoc, 4)) set @result = 1		

	return @result
end
go

----------------------Hàm kiểm tra ngày nhận và ngày kết thúc nằm trong kỳ đang xét hay không
alter function CheckTime(@namHoc varchar(10), @kiHoc nvarchar(10), @ngayNhan date, @ngayKetThuc date) returns bit
as
begin
	declare @result bit
	
	set @result = 0
	if(@ngayKetThuc is null)
		set @ngayKetThuc = GETDATE()
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
		else if(datepart(year, @ngayNhan)=RIGHT(@namHoc, 4) and datepart(year, @ngayKetThuc)>RIGHT(@namHoc, 4))
			set @result = 1
		else if(datepart(year, @ngayNhan)<RIGHT(@namHoc, 4)
		and datepart(year, @ngayKetThuc)=RIGHT(@namHoc, 4) and datepart(month, @ngayKetThuc) >= 5)
			set @result = 1
		else if(datepart(year, @ngayNhan)=RIGHT(@namHoc, 4)
		and datepart(year, @ngayKetThuc)=RIGHT(@namHoc, 4) and datepart(month, @ngayKetThuc) >= 5)
			set @result = 1

	return @result
end
go




----=============================Tải yêu cầu===========================---
alter function TongHop(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc varchar(10)) returns @table table
(
	TenGiaoVien nvarchar(50),
	DonVi nvarchar(50),
	ChucVu nvarchar(50),
	HocHam nvarchar(50),
	HocVi nvarchar(50),
	ChuNhiemBoMon nvarchar(50),

	DinhMucTaiDaoTao float,
	DinhMucTaiNghienCuu float,
	TyLeMienGiam float,
	TaiDaoTaoYeuCau float,
	TaiNghienCuuYeuCau float,

	TaiDaoTao float,
	TaiGiangDay float,
	TaiDayCaoDang float,
	TaiDayDaiHoc float,
	TaiDayCaoHoc float,
	TaiHuongDan float,
	TaiKhaoThi float,
	TaiHoiDong float,
	TaiNCKH float,
	TaiDeTai float,
	TaiBaiBao float,
	TaiBienSoan float,
	TongTai float,
	PhanTramTaiDaoTao float,
	phanTramTaiNCKH float
)
as
begin
	declare @dinhMucTaiDaoTao DEC(10,1), @dinhMucTaiNghienCuu DEC(10,1), @tyLeMienGiam DEC(10,1), 
	@taiDaoTaoYeuCau DEC(10,1), @taiNghienCuuYeuCau DEC(10,1), @tenGV nvarchar(50), @donVi nvarchar(50),
	@chucVu nvarchar(50), @hocHam nvarchar(50), @hocVi nvarchar(50), @tenChuNhiem nvarchar(50)

	set @dinhMucTaiDaoTao = dbo.DinhMucTaiDaoTao(@MaGiaoVien, @namHoc, @kiHoc)
	set @dinhMucTaiNghienCuu = dbo.DinhMucTaiNghienCuu(@MaGiaoVien, @namHoc, @kiHoc)
	set @tyLeMienGiam = dbo.TyLeMienGiam(@MaGiaoVien, @namHoc, @kiHoc)

	set @taiDaoTaoYeuCau = @dinhMucTaiDaoTao - @dinhMucTaiDaoTao*@tyLeMienGiam/100
	set @taiNghienCuuYeuCau = @dinhMucTaiNghienCuu - @dinhMucTaiNghienCuu*@tyLeMienGiam/100

	select @tenGV = TenGiaoVien from GiaoVien where MaGiaoVien = @MaGiaoVien


	select @donVi = tenBM  from GV_BoMon join BoMon on GV_BoMon.maBoMon=BoMon.MaBoMon where MaGiaoVien = @MaGiaoVien
	and dbo.CheckTime(@namHoc, @kiHoc, NgayChuyenDen, NgayChuyenDi) = 1

	select @tenChuNhiem = TenGiaoVien from GiaoVien 
	where MaGiaoVien = (select top 1 MaChuNhiem from GV_BoMon join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon 
	where GV_BoMon.MaGiaoVien = @MaGiaoVien and dbo.CheckTime(@namHoc, @kiHoc, NgayChuyenDen, NgayChuyenDi) = 1)


	select top 1 @chucVu = TenChucVu  from GV_ChucVuChQ join ChucVuChinhQuyen on ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_ChucVuChQ.NgayNhan desc

	select top 1 @hocHam = TenHocHam  from GV_HocHam join HocHam on HocHam.MaHocHam = GV_HocHam.MaHocHam 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_HocHam.NgayNhan desc

	select top 1 @hocVi = TenHocVi  from GV_HocVi join HocVi on HocVi.MaHocVi = GV_HocVi.MaHocVi 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1
	order by GV_HocVi.NgayNhan desc

	

	if(@hocHam is null) set @hocHam = N'Chưa có Học hàm'
	if(@chucVu is null) set @hocHam = N'Chưa có Chức vụ'
	if(@hocVi is null) set @hocHam = N'Chưa có Học vị'


	declare @taiDayHoc DEC(10,1), @taiKhaoThi DEC(10,1), @taiHoiDong DEC(10,1), @taiHuongDan DEC(10,1), @tongTai DEC(10,1),
	@taiNCKH float, @taiDayCaoDang float, @taiDayDaiHoc float, @taiDayCaoHoc DEC(10,1), 
	@ptTaiDT DEC(10,1), @ptTaiNCHK DEC(10,1), @taiDaoTao DEC(10,1), @TaiDeTai DEC(10,1), @TaiBaiBao DEC(10,1), @TaiBienSoan DEC(10,1)

	set @taiDayCaoDang = dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He01')
	set @taiDayDaiHoc = dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He02')
	set @taiDayCaoHoc = dbo.taiDayHoc(@MaGiaoVien, @namHoc, @kiHoc, 'He03')
	set @taiDayHoc = @taiDayCaoDang + @taiDayDaiHoc + @taiDayCaoHoc
	set @taiKhaoThi = dbo.taiKhaoThi(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiHuongDan = dbo.taiHuongDan(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiHoiDong = dbo.taiHoiDong(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiDaoTao = @taiDayHoc + @taiKhaoThi + @taiHuongDan + @taiHoiDong

	set @TaiDeTai = dbo.TaiNghienCuu(@MaGiaoVien, @namHoc, @kiHoc)
	set @TaiBaiBao = dbo.TaiBaiBao(@MaGiaoVien, @namHoc, @kiHoc)
	set @TaiBienSoan = dbo.TaiVietSach(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiNCKH = @TaiDeTai + @TaiBaiBao + @TaiBienSoan

	set @tongTai = @taiDayHoc + @taiKhaoThi + @taiHoiDong + @taiHuongDan + @taiNCKH

	if(@taiDaoTaoYeuCau = 0) set @ptTaiDT = 100
	else set @ptTaiDT = @taiDaoTao/@taiDaoTaoYeuCau*100
	if(@taiNghienCuuYeuCau = 0) set @ptTaiNCHK = 100
	else set @ptTaiNCHK = @taiNCKH/@taiNghienCuuYeuCau*100

	
	insert into @table values(@tenGV, @donVi, @chucVu, @hocHam, @hocVi, @tenChuNhiem, @dinhMucTaiDaoTao, @dinhMucTaiNghienCuu, 
	@tyLeMienGiam, @taiDaoTaoYeuCau, @taiNghienCuuYeuCau, @taiDaoTao, @taiDayHoc, @taiDayCaoDang, @taiDayDaiHoc,
	@taiDayCaoHoc, @taiHuongDan, @taiKhaoThi, @taiHoiDong, @taiNCKH, 
	@TaiDeTai, @TaiBaiBao, @TaiBienSoan, @tongTai, @ptTaiDT, @ptTaiNCHK)

	return 
end
go


----------------------------------------------------------------

select * from TongHop('GV05', '2018-2019', '2')
go
select * from TongHop('GV02', '2018-2019', '2')


DoiTuongMienGiamChucVuCQ 'GV04','2018-2019','2'
go
DoiTuongMienGiamChucVuDang 'GV04','2018-2019','2'

----------------------------
alter proc DoiTuongMienGiamChucVuCQ
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10)
as
begin
	select TenChucVu from ChucVuChinhQuyen, GV_ChucVuChQ 
	where ChucVuChinhQuyen.MaChucVu = GV_ChucVuChQ.MaChucVu and TyLeMienGiam > 0 and GV_ChucVuChQ.MaGiaoVien = @MaGiaoVien
	and (dbo.CheckTime(@namHoc, @kiHoc, NgayNhan, NgayKetThuc) = 1)
end
go

alter proc DoiTuongMienGiamChucVuDang
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10)
as
begin
	select TenChucVuDang from ChucVuDang, GV_ChucVuDang 
	where ChucVuDang.MaChucVuDang = GV_ChucVuDang.MaChucVuDang and GV_ChucVuDang.MaGiaoVien = @MaGiaoVien
	and dbo.CheckTimeBegin(@namHoc, @kiHoc, NgayNhan) = 1 and TyLeMienGiam > 0
end
go



-------------------------------------Tải Dạy Học--------------------------------------------
alter function taiDayHoc(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10), @maDoiTuong varchar(10)) returns float
as
begin
	declare @TongTai DEC(10,1)
	
	select @TongTai = Sum(GV_LopHocPhan.SoTiet*LoaiDayHoc.GioChuan) from GV_LopHocPhan join LopHocPhan 
	on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan
	join LoaiDayHoc on HocPhan.MaLoaiDayHoc=LoaiDayHoc.MaLoaiDayHoc where NamHoc=@namHoc
	and GV_LopHocPhan.MaGiaoVien = @MaGiaoVien and KiHoc = @kiHoc and MaDoiTuongHoc = @maDoiTuong

	if(@TongTai is NULL) set @TongTai = 0
	
	return @TongTai
end
go

---------Thông Tin dạy Học----------------------
alter proc ThongTinDayHoc
(
@maGiaoVien varchar(10),
@namHoc varchar(10),
@kiHoc nvarchar(10),
@MaHe varchar(10)
)
as
begin
	select TenHocPhan, SiSo, SoTinChi, SoTiet, TongGio = (GV_LopHocPhan.SoTiet*LoaiDayHoc.GioChuan)
	from GV_LopHocPhan join LopHocPhan on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan
	join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan join LoaiDayHoc on LoaiDayHoc.MaLoaiDayHoc=HocPhan.MaLoaiDayHoc
	where MaGiaoVien=@maGiaoVien and NamHoc=@namHoc and KiHoc=@kiHoc and MaDoiTuongHoc = @MaHe
end
go

ThongTinDayHoc 'GV04', '2018-2019','2', 'He02'
go
ThongTinDayHoc 'GV05', '2018-2019','2', 'He02'



--==========================================================================================================================


--====================================Tính tải giáo viên================================----

	
	









	----- Nguyễn Ngọc Khánh

											     
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




create function TaiChamBTL(@MaGiaoVien varchar(10), @namHoc varchar(10), @KiHoc nvarchar(10)) returns float
as
begin
	declare @taiChamDA_BTL float
	
	select @taiChamDA_BTL=Sum(SoDoAn*GioChuan) from GV_ChamThiDoAnBTL join LoaiChamThiDoAn_BTL 
	on GV_ChamThiDoAnBTL.MaLoaiChamThi=LoaiChamThiDoAn_BTL.MaLoaiChamThi 
	where NamHoc=@namHoc and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc
	
	if(@taiChamDA_BTL is null) set @taiChamDA_BTL = 0
	
	return @taiChamDA_BTL
end
go



---- Tải Khảo Thí ----
alter function taiKhaoThi(@MaGiaoVien varchar(10), @namHoc varchar(10), @KiHoc nvarchar(10)) returns float
as
begin
	declare @taiChamThi float, @taiChamDA_BTL float, @tongTai float

	select @taiChamThi=Sum(SoHocVien*GioChuan) from GV_ChamThi join LoaiChamThi 
	on GV_ChamThi.MaLoaiChamThi=LoaiChamThi.MaLoaiChamThi where NamHoc=@namHoc 
	and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc

	select @taiChamDA_BTL=dbo.TaiChamBTL(@MaGiaoVien, @namHoc, @KiHoc)

	if(@taiChamThi is null) set @taiChamThi = 0

	set @tongTai = @taiChamThi + @taiChamDA_BTL
	return @tongTai
end
go

select dbo.taiKhaoThi('GV04','2018-2019','2')
go
select dbo.taiKhaoThi('GV05','2018-2019','2')

---- Tải Hướng Dấn ----
alter function taiHuongDan(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	declare @taiHDDAMH float, @taiHDan float, @tong float

	select @taiHDDAMH = sum(SoLuongHocVien*GioChuan) from GV_DoAnMonHoc join DoAnMonHoc 
	on GV_DoAnMonHoc.MaLoaiDoAn=DoAnMonHoc.MaLoaiDoAn and MaGiaoVien = @MaGiaoVien 
	and namHoc = @namHoc and KiHoc = @kiHoc

	select @taiHDan = sum(GioChuan) from GV_HuongDan join LoaiHuongDan on GV_HuongDan.MaLoaiHuongDan = LoaiHuongDan.MaLoaiHuongDan
	and MaGiaoVien = @MaGiaoVien and dbo.kiemTraNgay(@namHoc, @kiHoc, ngayBatDau) = 1

	if(@taiHDDAMH is null) set @taiHDDAMH = 0
	if(@taiHDan is null) set @taiHDan = 0
	set @tong = @taiHDDAMH + @taiHDan
	return @tong
end

select dbo.taiHuongDan('GV02','2018-2019','1')





---- In Các Đồ Án ----
alter function inCacDoAn(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10), @maLoaiHD varchar(10)) returns @table table
(
	MaHocVien varchar(10),
	TenHocVien nvarchar(40),
	TenLop nvarchar(50),
	TenDeTai nvarchar(50),
	DinhMuc int,
	SoCBHD int,
	SoGio int
)
as
begin
	insert into @table select HocVien.MaHocVien, TenHocVien, TenLop, TenDeTai, GioChuan, '1', GioChuan
	from GV_HuongDan join HocVien on GV_HuongDan.MaHocVien = HocVien.MaHocVien join Lop on Lop.MaLop = HocVien.MaLop
	join LoaiHuongDan on GV_HuongDan.MaLoaiHuongDan=LoaiHuongDan.MaLoaiHuongDan 
	where MaGiaoVien = @MaGiaoVien and dbo.kiemTraNgay(@namHoc, @kiHoc, NgayBatDau) = 1
	and LoaiHuongDan.MaLoaiHuongDan = @maLoaiHD
	return 
end



select * from inCacDoAn('GV02', '2018-2019', '1', 'Ma01')

select * from inCacDoAn('GV02', '2018-2019', '1', 'Ma02')

select * from inCacDoAn('GV02', '2018-2019', '1', 'Ma03')




alter function inCongTacKhaoThi(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc varchar(10)) returns @table table
(
	TenLoaiChamThi nvarchar(50),
	SoLuong int,
	SoGio DEC(5, 1)
)					     
as
begin	
	declare @soBTL int, @soGioChamBTL float
	
	insert into @table select TenLoaiChamThi, SoHocVien, SoGio = Sum(SoHocVien*GioChuan)
	from GV_ChamThi right join LoaiChamThi on GV_ChamThi.MaLoaiChamThi = LoaiChamThi.MaLoaiChamThi
	and MaGiaoVien = @MaGiaoVien and NamHoc = @NamHoc and KiHoc = @KiHoc
	group by TenLoaiChamThi, SoHocVien

	select @soBTL = Sum(SoDoAn)  from GV_ChamThiDoAnBTL
	where NamHoc=@namHoc and MaGiaoVien = @MaGiaoVien and KiHoc = @KiHoc
	
	set @soGioChamBTL = dbo.TaiChamBTL(@MaGiaoVien, @namHoc, @kiHoc)

	insert into @table values(N'Chấm Bài Tập Lớn, Đồ Án Môn Học', @soBTL, @soGioChamBTL)

	return
end


select * from inCongTacKhaoThi('GV01', '2018-2019', '1')





--- Ngô Văn Thường


alter function kiemTraThoiGian(@namHoc varchar(10), @kiHoc nvarchar(10), @ngayNhan date, @ngayKetThuc date) 
returns int
as
begin
	declare @result int 
	declare @soTuanTrong1Ky int = 17
	declare @ngaybdHKI date = CONVERT(date,substring(@namHoc , 1, 4) +'/9/2')
	declare @ngaybdHKII date = Dateadd(DD , @soTuanTrong1Ky*7+7 , @ngaybdHKI)
	set @result = 0
	if (@ngayKetThuc is null ) set @ngayKetThuc = GETDATE();
	if(datepart(year, @ngayNhan)<LEFT(@namHoc, 4) and datepart(year, @ngayKetThuc)>RIGHT(@namHoc, 4))
		set @result = 17*7
	else if(DATEPART(year , @ngayKetThuc) < LEFT(@namHoc ,4) )
		set @result = 0 
	else 
		if(@kiHoc = '1')
			if(@ngayKetThuc >=  DATEADD(DD , @soTuanTrong1Ky*7 , @ngaybdHKI))
				if(@ngayNhan < @ngaybdHKI)
					set @result =  @soTuanTrong1Ky*7
				else 
					set @result = DATEDIFF(DAY ,@ngayNhan, DATEADD(DD , @soTuanTrong1Ky*7 , @ngaybdHKI) );
			else 
				if(@ngayNhan < @ngaybdHKI)
					set @result = DATEDIFF(DAY ,@ngaybdHKI, @ngayKetThuc );
				else 
					set @result = DATEDIFF(DAY , @ngayNhan , @ngayKetThuc)
		else if (@kiHoc = '2')
			if(@ngayKetThuc >=  DATEADD(DD , @soTuanTrong1Ky*7 , @ngaybdHKII))
				if(@ngayNhan < @ngaybdHKII)
					set @result =  @soTuanTrong1Ky*7
				else 
					set @result = DATEDIFF(DAY ,@ngayNhan, DATEADD(DD , @soTuanTrong1Ky*7 , @ngaybdHKII) );
			else 
				if(@ngayNhan < @ngaybdHKII)
					set @result = DATEDIFF(DAY ,@ngaybdHKII, @ngayKetThuc );
				else 
					set @result = DATEDIFF(DAY , @ngayNhan , @ngayKetThuc)
	if(@result <0 ) 
		set @result =0
	return @result
end
go



-------------------------------Tải Nghiên Cứu Khoa Học------------------------------------

create function TaiBaiBao(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	
	declare  @taiBaiBao float
	select @taiBaiBao = Sum(GioChuan/dbo.SoThanhVien(BaiBao.MaBaiBao)) from GV_BaiBao join BaiBao 
	on GV_BaiBao.MaBaiBao=BaiBao.MaBaiBao join LoaiBaiBao on LoaiBaiBao.MaLoaiBaiBao = BaiBao.MaLoaiBaiBao 
	where MaGiaoVien = @MaGiaoVien and dbo.CheckTimeBegin(@namHoc , @kiHoc, NgayCongBo)= 1	
	if(@taiBaiBao is null) set @taiBaiBao = 0
	
	return @taiBaiBao
end
go

create function TaiVietSach(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	
	declare @taiVietSach1 DEC(5, 1), @taiVietSach2 DEC(5, 1), @tong DEC(5, 1)

	select  @taiVietSach1 = Sum(SoTrangDaViet*GioChuan) from GV_BienSoanSach join Sach 
	on GV_BienSoanSach.MaSach=Sach.MaSach join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach 
	where MaGiaoVien = @MaGiaoVien and  LoaiSach.MaLoaiSach = 'LS01' and dbo.CheckTimeBegin(@namHoc , @kiHoc, NgayXuatBan)= 1

	select @taiVietSach2 = Sum((dbo.tyLeThanhVienVietSach()*SoTinChi*GioChuan)/(dbo.SoThanhVien(Sach.maSach)) 
	+ dbo.chuBien(@MaGiaoVien,Sach.maSach)) 
	from GV_BienSoanSach join Sach on GV_BienSoanSach.MaSach=Sach.MaSach join LoaiSach on LoaiSach.MaLoaisach = Sach.MaLoaiSach 
	where MaGiaoVien = @MaGiaoVien and LoaiSach.MaLoaiSach <> 'LS01' and dbo.CheckTimeBegin(@namHoc , @kiHoc, NgayXuatBan) = 1
	
	if(@taiVietSach1 is null) set @taiVietSach1 = 0
	if(@taiVietSach2 is null) set @taiVietSach2 = 0

	set @tong = @taiVietSach1 + @taiVietSach2

	return @tong
end
go

select dbo.TaiVietSach('GV04', '2018-2019', '1')

create function TaiNghienCuu(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	
	declare  @taiNghienCuuDeTai float
	select @taiNghienCuuDeTai = Sum(dbo.kiemTraThoiGian(@namHoc , @kiHoc , NgayBatDau , NgayKetThuc)*dbo.tyLeThanhVienNC()*(GioChuan/(17*2*7))/(dbo.SoThanhVien(DeTai.MaDeTai)) 
	+ dbo.chuTri(@MaGiaoVien,DeTai.MaDeTai))
	from GV_DeTaiNghienCuu join DeTai on DeTai.MaDeTai=GV_DeTaiNghienCuu.MaDeTai join LoaiDeTaiNghienCuu 
	on LoaiDeTaiNghienCuu.MaLoaiDeTai=DeTai.MaLoaiDeTai where MaGiaoVien = @MaGiaoVien 
	if(@taiNghienCuuDeTai is null) set @taiNghienCuuDeTai = 0
	return @taiNghienCuuDeTai
end
go


alter function taiNCKH(@MaGiaoVien varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin
	
	declare @taiVietSach DEC(5, 1), @taiBaiBao DEC(5, 1), @taiNghienCuuDeTai DEC(5, 1), @tong DEC(10,1)

	set @taiBaiBao = dbo.TaiBaiBao(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiVietSach = dbo.TaiVietSach(@MaGiaoVien, @namHoc, @kiHoc)
	set @taiNghienCuuDeTai = dbo.TaiNghienCuu(@MaGiaoVien, @namHoc, @kiHoc)

	
	set @tong = @taiVietSach + @taiBaiBao + @tainghienCuuDeTai
	return @tong
end
go

select dbo.taiNCKH('GV04' , '2018-2019' , '1') as TongTaiNCKH
go
//////////////////////////////////////////////////


------------------Hàm 
create function SoThanhVien(@Ma varchar(10)) returns int
as
begin
	declare  @tong int =0
	select @tong = COUNT(*) from GV_BienSoanSach where MaSach = @Ma
	if(@tong = 0)
	 select @tong = COUNT(*) from GV_BaiBao where MaBaiBao = @Ma
	if(@tong = 0)
	 select @tong = COUNT(*) from GV_DeTaiNghienCuu where MaDeTai = @Ma
	return @tong
end
go

alter function chuBien(@MaGiaoVien varchar(10), @maSach varchar(10)) returns int
as
begin
	declare @maChu varchar(10), @tong int
	select @maChu = MaGiaoVien from GV_BienSoanSach where MaSach = @maSach and VaiTro = N'Chủ Biên'
	if(@MaGiaoVien = @maChu) 
		select @tong = SoTinChi*GioChuan*dbo.TyLeChuBien() from Sach join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach
		where MaSach=@maSach
	else
		set @tong = 0
	return @tong
end
go

alter function chuTri(@MaGiaoVien varchar(10), @maDeTai varchar(10)) returns int
as
begin
	declare @maChuTri varchar(10), @tong int
	select @maChuTri = MaGiaoVien from GV_DeTaiNghienCuu where MaDeTai = @maDeTai and VaiTro = N'Chủ chì'
	if(@MaGiaoVien = @maChuTri) 
		select @tong = GioChuan*dbo.TyLeChuTri() from DeTai join LoaiDeTaiNghienCuu on LoaiDeTaiNghienCuu.MaLoaiDeTai = DeTai.MaLoaiDeTai
		where MaDeTai=@maDeTai
	else
		set @tong = 0
	return @tong
end
go

create function TyLeChuBien() returns float
as
begin
	declare @tyle float
	select @tyle = TyLeTruongNhom from TyLePhanChia where MaTyLe = '1'
	return @tyle
end

create function TyLeChuTri() returns float
as
begin
	declare @tyle float
	select @tyle = TyLeTruongNhom from TyLePhanChia where MaTyLe = '2'
	return @tyle
end


create function tyLeThanhVienVietSach() returns float
as
begin
	declare @tyle float
	select @tyle = TyLeThanhVien from TyLePhanChia where MaTyLe = '1'
	return @tyle
end

create function tyLeThanhVienNC() returns float
as
begin
	declare @tyle float
	select @tyle = TyLeThanhVien from TyLePhanChia where MaTyLe = '2'
	return @tyle
end



----------------------Tải nghiên cứu khoa học-----------------
create procedure ThongTinNghienCuuDeTai
(
	@magiaovien varchar(10) ,
	@namhoc varchar(10) ,
	@kyhoc varchar(10) 
)
as begin
	select DeTai.TenDeTai , LoaiDeTaiNghienCuu.TenLoaiDeTai , GV_DeTaiNghienCuu.VaiTro , 
	dbo.SoThanhVien(DeTai.MaDeTai) as SoTacGia , 
	(dbo.kiemTraThoiGian(@namhoc , @kyhoc , NgayBatDau , NgayKetThuc)*dbo.tyLeThanhVienNC()*(GioChuan/(17*2*7))/(dbo.SoThanhVien(DeTai.MaDeTai)) + dbo.chuTri(@MaGiaoVien,DeTai.MaDeTai) ) as GioChuan
	from DeTai  join GV_DeTaiNghienCuu  on DeTai.MaDeTai=GV_DeTaiNghienCuu.MaDeTai 
				join LoaiDeTaiNghienCuu on LoaiDeTaiNghienCuu.MaLoaiDeTai=DeTai.MaLoaiDeTai 
	where MaGiaoVien = @magiaovien 
		and dbo.kiemTraThoiGian(@namhoc , @kyhoc , NgayBatDau , NgayKetThuc) >0
end
go


exec ThongTinNghienCuuDeTai 'GV01' , '2018-2019' , '1'
go


--------------------------Tải viết báo , báo cáo--------------------
create procedure ThongTinBaoKhoaHoc
(
	@magiaovien varchar(10) ,
	@namhoc varchar(10) ,
	@kyhoc varchar(10) 
)
as begin
	select BaiBao.TenBaiBao, LoaiBaiBao.TenLoaiBaiBao , GV_BaiBao.VaiTro , dbo.SoThanhVien(BaiBao.MaBaiBao) as SoTacGia , (GioChuan/dbo.SoThanhVien(BaiBao.MaBaiBao)) as GioChuan
	from BaiBao join GV_BaiBao on GV_BaiBao.MaBaiBao=BaiBao.MaBaiBao 
				join LoaiBaiBao on LoaiBaiBao.MaLoaiBaiBao = BaiBao.MaLoaiBaiBao 
	where MaGiaoVien = @magiaovien and dbo.CheckTimeBegin(@namhoc , @kyhoc, NgayCongBo)= 1
end
go

exec ThongTinBaoKhoaHoc 'GV01' , '2018-2019' , '2' 
go

------------------- Tải viết sách ,giáo trình -------------------
create procedure ThongTinVietSach
(
	@magiaovien varchar(10) ,
	@namhoc varchar(10) ,
	@kyhoc varchar(10) 
)
as begin
	select Sach.TenSach, LoaiSach.TenLoaiSach , GV_BienSoanSach.VaiTro , dbo.SoThanhVien(Sach.MaSach) as SoTacGia ,(SoTrangDaViet*GioChuan) as GioChuan
	from GV_BienSoanSach join Sach on GV_BienSoanSach.MaSach=Sach.MaSach 
						 join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach 
	where MaGiaoVien = @magiaovien 
		and  LoaiSach.MaLoaiSach = 'LS01' 
		and dbo.CheckTimeBegin(@namhoc , @kyhoc, NgayXuatBan)= 1
	union
	select Sach.TenSach, LoaiSach.TenLoaiSach , GV_BienSoanSach.VaiTro , dbo.SoThanhVien(Sach.MaSach) as SoThanhVien , 
	(dbo.tyLeThanhVienVietSach()*SoTinChi*GioChuan/dbo.SoThanhVien(Sach.MaSach))+dbo.chuBien(@magiaovien,Sach.maSach) as GioChuan
	from Sach  join GV_BienSoanSach on GV_BienSoanSach.MaSach= Sach.MaSach 
			    join LoaiSach on LoaiSach.MaLoaiSach = Sach.MaLoaiSach 
	where GV_BienSoanSach.MaGiaoVien = @magiaovien 
		and  LoaiSach.MaLoaiSach <> 'LS01' 
		and dbo.CheckTimeBegin(@namhoc , @kyhoc, NgayXuatBan)= 1
end
go

ThongTinNghienCuuDeTai 'GV01' , '2018-2019' , '1'
go
ThongTinBaoKhoaHoc 'GV01' , '2018-2019' , '2' 
exec ThongTinVietSach 'GV04' , '2018-2019' , '2' 
go


---------------- Thông tin giáo viên tham gia hội đồng

alter procedure ThongTinThamGiaHoiDong
(
	@magiaovien varchar(10) ,
	@namhoc varchar(10) ,
	@kyhoc varchar(10) ,
	@maloaihd varchar(10)
)
as begin

	select HoiDong.TenHoiDong  , Coalesce(GV_HoiDong.VaiTro, '') VaiTro ,Coalesce(sum (GV_HoiDong.Solan), 0) as SoLan , Coalesce(sum(GV_HoiDong.SoGio) ,0) as SoGio
	from HoiDong left join GV_HoiDong on GV_HoiDong.MaHoiDong = HoiDong.MaHoiDong
										and GV_HoiDong.NamHoc = @namhoc
										and GV_HoiDong.KiHoc = @kyhoc
										and GV_HoiDong.MaGiaoVien = @magiaovien 
				where MaLoaiHoiDong = @maloaihd
	group by VaiTro, HoiDong.TenHoiDong
end
go

exec ThongTinThamGiaHoiDong 'GV01' , '2017-2018' , '2', 'Ma02'
go



---======================================================================================================================
select * from TongHopTaiCacGiaoVien('BM01', '2018-2019', '1')
go
select * from ThongKeNhanLuc('2017-01-01', 'K03')
---=====================================================================================================================


--Lấy danh sách giáo viên theo khoa
alter proc LayGiaoVienTheoKhoa
@maKhoa varchar(10),
@ngayLay date
as
begin
	select GiaoVien.MaGiaoVien, TenGiaoVien, GioiTinh, NgaySinh, QueQuan, DiaChi, DienThoai, Email, BoMon.MaBoMon, 
	NgayChuyenDen from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	where MaKhoa = @maKhoa and dbo.KiemTraNgayLay(@ngayLay, NgayChuyenDen, NgayChuyenDi) = 1
end
go
LayGiaoVienTheoKhoa 'K01', '2014-01-01'
go

alter proc layGiaoVienTheoMa
@maGV varchar(10)
as
begin
	select *  from GiaoVien where MaGiaoVien = @maGV
end
go
layGiaoVienTheoMa 'GV05'

alter proc LayBoMonTheoGiaoVien
@maGV varchar(10)
as begin
	if(not exists(select MaBoMon from GV_BoMon where MaGiaoVien = @maGV 
	and dbo.KiemTraNgayLay(GETDATE(), NgayChuyenDen, NgayChuyenDi) = 1))
		select N'Hiện tại không ở bộ môn nào' as TenBM, '1990-01-01' NgayChuyenDen
	else
		select BoMon.MaBoMon, TenBM, NgayChuyenDen, NgayChuyenDi from BoMon join GV_BoMon on BoMon.MaBoMon = GV_BoMon.MaBoMon
		and dbo.KiemTraNgayLay(GETDATE(), NgayChuyenDen, NgayChuyenDi) = 1 and MaGiaoVien = @maGV
end
go
LayBoMonTheoGiaoVien 'GV01'


alter proc layTatCaGiaoVien
@ngayLay date
as
begin
	select GiaoVien.MaGiaoVien, TenGiaoVien, GioiTinh, NgaySinh, QueQuan, DiaChi, DienThoai, Email, GV_BoMon.MaBoMon, 
	NgayChuyenDen  from GiaoVien join GV_BoMon on  GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien
	where dbo.KiemTraNgayLay(@ngayLay, NgayChuyenDen, NgayChuyenDi) = 1
end

layTatCaGiaoVien '2012-01-01'

---hàm kiểm tra ngay ngày lấy có nằm trong khoảng ngày đến ngày đi ko 
create function KiemTraNgayLay(@NgayLay date, @ngayDen date, @ngayDi date) returns bit
as
begin
	declare @result bit
	
	set @result = 0
	if(@ngayDi is null) set @ngayDi = GETDATE()
	if(@ngayDen<= @NgayLay and @ngayDi>= @NgayLay)
	set @result = 1

	return @result
end
go


--Thông Kê Nhân Lực Theo Khoa

alter function ThongKeNhanLuc(@ngayThongKe date, @maKhoa varchar(10)) returns @table table
(
	TenKhoa nvarchar(50),
	TongSo int,
	GiaoSu int,
	PhoGiaoSu int,
	TienSyKH int,
	TienSy int,
	ThacSy int,
	DaiHoc int,
	Khac int
)
as
begin
	declare @giaoSu int, @phoGS int, @tiensyKH int, @tienSy int, @ThacSy int, @daiHoc int, @Khac int, @Tong int,
	@TenKhoa nvarchar(50), @kySu int, @cuNhan int
	
	select  @Tong = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	where MaKhoa = @maKhoa and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1

	select  @giaoSu = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocHam on GiaoVien.MaGiaoVien=GV_HocHam.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocHam = 'HH01'
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1

	select  @phoGS = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocHam on GiaoVien.MaGiaoVien=GV_HocHam.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocHam = 'HH02'
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select gvhh2.MaGiaoVien from HocHam hh2 join GV_HocHam gvhh2 on hh2.MaHocHam=gvhh2.MaHocHam
	where hh2.MaHocHam = 'HH01')

	select  @tiensyKH = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocVi on GiaoVien.MaGiaoVien=GV_HocVi.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocVi = 'HV01'
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select GV_HocHam.MaGiaoVien from HocHam join GV_HocHam on HocHam.MaHocHam=GV_HocHam.MaHocHam)

	select  @tienSy = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocVi on GiaoVien.MaGiaoVien=GV_HocVi.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocVi = 'HV02'
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select GV_HocHam.MaGiaoVien from HocHam join GV_HocHam on HocHam.MaHocHam=GV_HocHam.MaHocHam)
	and GiaoVien.MaGiaoVien not in(select gvhv2.MaGiaoVien from HocVi hv2 join GV_HocVi gvhv2 on hv2.MaHocVi=gvhv2.MaHocVi
	where hv2.MaHocVi = 'HV01')

	select  @ThacSy = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocVi on GiaoVien.MaGiaoVien=GV_HocVi.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocVi = 'HV03'
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select GV_HocHam.MaGiaoVien from HocHam join GV_HocHam on HocHam.MaHocHam=GV_HocHam.MaHocHam)
	and GiaoVien.MaGiaoVien not in(select gvhv2.MaGiaoVien from HocVi hv2 join GV_HocVi gvhv2 on hv2.MaHocVi=gvhv2.MaHocVi
	where hv2.MaHocVi = 'HV01' or hv2.MaHocVi = 'HV02')

	select  @kySu = count(*) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocVi on GiaoVien.MaGiaoVien=GV_HocVi.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocVi = 'HV04' 
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select GV_HocHam.MaGiaoVien from HocHam join GV_HocHam on HocHam.MaHocHam=GV_HocHam.MaHocHam)
	and GiaoVien.MaGiaoVien not in(select gvhv2.MaGiaoVien from HocVi hv2 join GV_HocVi gvhv2 on hv2.MaHocVi=gvhv2.MaHocVi
	where hv2.MaHocVi = 'HV01' or hv2.MaHocVi = 'HV02' or hv2.MaHocVi = 'HV03')

	select  @cuNhan = count(GiaoVien.MaGiaoVien) from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	join GV_HocVi on GiaoVien.MaGiaoVien=GV_HocVi.MaGiaoVien
	where MaKhoa = @maKhoa and MaHocVi = 'HV05' 
	and dbo.KiemTraNgayLay(@ngayThongKe, NgayChuyenDen, NgayChuyenDi) = 1
	and GiaoVien.MaGiaoVien not in(select GV_HocHam.MaGiaoVien from HocHam join GV_HocHam on HocHam.MaHocHam=GV_HocHam.MaHocHam)	
	and GiaoVien.MaGiaoVien not in(select gvhv2.MaGiaoVien from HocVi hv2 join GV_HocVi gvhv2 on hv2.MaHocVi=gvhv2.MaHocVi
	where hv2.MaHocVi = 'HV01' or hv2.MaHocVi = 'HV02' or hv2.MaHocVi = 'HV03')

	set @daiHoc = @kySu + @cuNhan
	set @Khac = @Tong - @tienSy - @ThacSy - @tiensyKH - @daiHoc - @giaoSu - @phoGS

	select @TenKhoa = TenKhoa from Khoa where MaKhoa = @maKhoa

	insert into @table values(@TenKhoa, @Tong, @giaoSu, @phoGS, @tiensyKH, @tienSy, @ThacSy, @daiHoc, @Khac)

return
end
go

select * from ThongKeNhanLuc('2017-01-01', 'K01')

--------Tổng hợp công tác tải theo khoa và bộ môn

alter function TongHopTaiCacGiaoVien(@maBoMon varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns @table table
(
	TenGiaoVien nvarchar(40),
	ThucTaiDaoTao float,
	TaiDaoTaoYeucau float,
	PhanTramTaiDaoTao varchar(20),
	ThucTaiNCKH DEC(10,1),
	TaiNCKHYeucau float,
	PhanTranTaiNCKH varchar(20),
	TongThucTai float,
	TongTaiyeuCau float,
	PhanTramTongTai varchar(20)
)
as
begin
	insert into @table select TenGiaoVien, dbo.taiThucDaoTao(GiaoVien.MaGiaoVien, @namHoc, @kiHoc),
	dbo.taiDaoTaoYeuCau(GiaoVien.MaGiaoVien, @namHoc, @kiHoc), dbo.phanTramTaiDT(GiaoVien.MaGiaoVien, @namHoc, @kiHoc),
	dbo.taiNCKH(GiaoVien.MaGiaoVien, @namHoc, @kiHoc), dbo.taiNghienCuuYeuCau(GiaoVien.MaGiaoVien, @namHoc, @kiHoc), 
	dbo.phanTramTaiNCKH(GiaoVien.MaGiaoVien, @namHoc, @kiHoc),  dbo.tongThucTai(GiaoVien.MaGiaoVien, @namHoc, @kiHoc),
	dbo.tongTaiYeuCau(GiaoVien.MaGiaoVien, @namHoc, @kiHoc), dbo.phanTramTongTai(GiaoVien.MaGiaoVien, @namHoc, @kiHoc)
	from GiaoVien join GV_BoMon on GiaoVien.MaGiaoVien=GV_BoMon.MaGiaoVien 
	join BoMon on GV_BoMon.MaBoMon = BoMon.MaBoMon
	where BoMon.MaBoMon = @maBoMon
	and dbo.CheckTime(@namHoc, @kiHoc, NgayChuyenDen, NgayChuyenDi) = 1

	return 
end
go
select * from TongHopTaiCacGiaoVien('BM02', '2018-2019', '1')


select * from TongHop('GV04', '2018-2019', '2')


alter function phanTramTongTai(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns varchar(20)
as
begin 
	declare @pt DEC(10,1), @yc DEC(10,1)
	declare @phantram varchar(20)
	if(dbo.tongTaiYeuCau(@maGV, @namHoc, @kiHoc) = 0) set @yc = 1
	else set @yc = dbo.tongTaiYeuCau(@maGV, @namHoc, @kiHoc)

	set @pt = dbo.tongThucTai(@maGV, @namHoc, @kiHoc)/@yc*100
	
	set @phantram = CONVERT(varchar(10), @pt) + ' %'

	return @phantram
end
go


alter function phanTramTaiNCKH(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns varchar(20)
as
begin 
	declare @pt DEC(10,1), @yc DEC(10,1)
	declare @phantram varchar(20)
	if(dbo.taiNghienCuuYeuCau(@maGV, @namHoc, @kiHoc) = 0) set @yc = 1
	else set @yc = dbo.taiNghienCuuYeuCau(@maGV, @namHoc, @kiHoc)

	set @pt = dbo.taiNCKH(@maGV, @namHoc, @kiHoc)/@yc*100
	
	set @phantram = CONVERT(varchar(10), @pt) + ' %'
	return @phantram
end


go
alter function phanTramTaiDT(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns varchar(20)
as
begin 
	declare @pt DEC(10,1), @yc DEC(10,1)
	declare @phantram varchar(20)
	if(dbo.taiDaoTaoYeuCau(@maGV, @namHoc, @kiHoc) = 0) set @yc = 1
	else set @yc = dbo.taiDaoTaoYeuCau(@maGV, @namHoc, @kiHoc)

	set @pt = dbo.taiThucDaoTao(@maGV, @namHoc, @kiHoc)/@yc*100
	
	set @phantram = CONVERT(varchar(10), @pt) + ' %'
	return @phantram
end

go
alter function tongTaiYeuCau(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin 
	declare @tong DEC(10,1)

	set @tong =  dbo.taiNghienCuuYeuCau(@maGV, @namHoc, @kiHoc) + dbo.taiDaoTaoYeuCau(@maGV, @namHoc, @kiHoc)
	return @tong
end
go
alter function taiNghienCuuYeuCau(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin 
	declare @taiNghienCuuYeuCau DEC(10,1)

	set @taiNghienCuuYeuCau = dbo.DinhMucTaiNghienCuu(@maGV, @namHoc, @kiHoc) 
	- dbo.DinhMucTaiNghienCuu(@maGV, @namHoc, @kiHoc)*dbo.TyLeMienGiam(@maGV, @namHoc, @kiHoc)/100
	return @taiNghienCuuYeuCau
end

alter function taiDaoTaoYeuCau(@maGV varchar(10), @namHoc varchar(10), @kiHoc nvarchar(10)) returns float
as
begin 
	declare @taiDaoTaoYeuCau DEC(10,1)

	set @taiDaoTaoYeuCau = dbo.DinhMucTaiDaoTao(@maGV, @namHoc, @kiHoc) 
	- dbo.DinhMucTaiDaoTao(@maGV, @namHoc, @kiHoc)*dbo.TyLeMienGiam(@maGV, @namHoc, @kiHoc)/100
	return @taiDaoTaoYeuCau
end
go

alter function tongThucTai(@maGV varchar(10), @namHoc varchar(10), @kiHoc  nvarchar(10)) returns float
as
begin
	declare @tong DEC(10,1)

	set @tong = dbo.taiThucDaoTao(@maGV, @namHoc, @kiHoc) + dbo.taiNCKH(@maGV, @namHoc, @kiHoc)
	return @tong
end
go

alter function taiThucDaoTao(@maGV varchar(10), @namHoc varchar(10), @kiHoc  nvarchar(10)) returns float
as
begin
	declare @taiDayHoc DEC(10,1), @taiKhaoThi DEC(10,1), @taiHoiDong DEC(10,1), @taiHuongDan DEC(10,1),
	@thucTaiDaoTao DEC(10,1)

	select @taiDayHoc = sum(dbo.taiDayHoc(@maGV, @namHoc, @kiHoc, MaHe)) from He
	set @taiKhaoThi = dbo.taiKhaoThi(@maGV, @namHoc, @kiHoc)
	set @taiHuongDan = dbo.taiHuongDan(@maGV, @namHoc, @kiHoc)
	set @taiHoiDong = dbo.taiHoiDong(@maGV, @namHoc, @kiHoc)

	set @thucTaiDaoTao = @taiDayHoc+@taiHoiDong+@taiHuongDan+@taiKhaoThi
	return @thucTaiDaoTao
end




---============================================================================


alter proc HocViGiaoVien
@maGV varchar(10)
as begin
select HocVi.MaHocVi, TenHocVi from HocVi join GV_HocVi on HocVi.MaHocVi=GV_HocVi.MaHocVi where MaGiaoVien = @maGV
end
go
HocViGiaoVien 'GV02'

alter proc ChucDanhNhaGiaoCuaGiaoVien
@maGV varchar(10)
as begin
select TenChucVu, datepart(YEAR ,NgayNhan) as Nam, NoiBoNhiem from GV_ChucVuChMKT join ChucVu_ChMonKT 
on ChucVu_ChMonKT.MaChucVu= GV_ChucVuChMKT.MaChucVu 
where MaGiaoVien = @maGV
end
go
ChucDanhNhaGiaoCuaGiaoVien 'GV01'

alter proc ChucDanhNghienCuuCuaGiaoVien
@maGV varchar(10)
as begin
	select TenChucDanh, datepart(YEAR ,NgayNhan) as Nam, NoiBoNhiem 
	from ChucDanh_ChMonNV join GV_ChucDanhChMNV on ChucDanh_ChMonNV.MaChucDanh=GV_ChucDanhChMNV.MaChucDanh 
	where MaGiaoVien = @maGV
end
go
ChucDanhNghienCuuCuaGiaoVien 'GV01'

create proc TrinhDoNgoaiNguGiaoVien
@maGV varchar(10)
as
begin
	select TenTrinhDo, NgayCapChungChi from GV_NgoaiNgu join NgoaiNgu on GV_NgoaiNgu.MaNgoaiNgu = NgoaiNgu.MaNgoaiNgu
	where MaGiaoVien = @maGV
end
go
TrinhDoNgoaiNguGiaoVien 'GV01'

alter proc QuaTrinhDaoTaoDaiHoc
@maGV varchar(10)
as begin
select HeDaoTao, NoiDTao, NganhHoc, NuocDaoTao, NamTotNghiem
from GV_DaiHoc join DaiHoc on DaiHoc.MaDaiHoc = GV_DaiHoc.MaDaiHoc
where MaGiaoVien = @maGV
end
go
QuaTrinhDaoTaoDaiHoc 'GV01'

create proc QuaTrinhDaoTaoThacSi
@maGV varchar(10)
as begin
select ChuyenNganh, NamCapBang, NoiDaoTao, TenLuanVan
from GV_ThacSi join ThacSi on ThacSi.MaThacSi = GV_ThacSi.MaThacSi
where MaGiaoVien = @maGV
end
go
QuaTrinhDaoTaoThacSi 'GV01'


create proc QuaTrinhDaoTaoTienSi
@maGV varchar(10)
as begin
select ChuyenNganh, NamCapBang, NoiDaoTao, TenLuanAn
from GV_TienSi join TienSi on TienSi.MaTienSi = GV_TienSi.MaTienSi
where MaGiaoVien = @maGV
end
go
QuaTrinhDaoTaoTienSi 'GV01'


create proc LichSuGiangDay
@maGV varchar(10)
as begin
	select NamHoc, KiHoc, TenHocPhan,  TenHe
	from GV_LopHocPhan join LopHocPhan on GV_LopHocPhan.MaLopHocPhan=LopHocPhan.MaLopHocPhan
	join HocPhan on HocPhan.MaHocPhan=LopHocPhan.MaHocPhan 
	join He on He.MaHe = HocPhan.MaDoiTuongHoc
	where MaGiaoVien=@maGV 
end

go
LichSuGiangDay 'GV01'


alter proc LichSuHuongDan
@maGV varchar(10)
as begin
	select TenDeTai, TenLoaiHuongDan, TenHocVien, NgayBatDau, NgayKetThuc  
	from GV_HuongDan join LoaiHuongDan 
	on LoaiHuongDan.MaLoaiHuongDan=GV_HuongDan.MaLoaiHuongDan
	join HocVien on HocVien.MaHocVien=GV_HuongDan.MaHocVien 
	where MaGiaoVien = @maGV
end

go
LichSuHuongDan 'GV01'


alter proc LichSuVietSach
@maGV varchar(10)
as begin
	select TenSach, TenLoaiSach, VaiTro, NoiXuatBan, NgayXuatBan from GV_BienSoanSach join Sach 
	on Sach.MaSach = GV_BienSoanSach.MaSach join LoaiSach on LoaiSach.MaLoaiSach=Sach.MaLoaiSach
	where MaGiaoVien = @maGV
end

go
LichSuVietSach 'GV01'


create proc LichSuBaiBao
@maGV varchar(10)
as begin
	select  TenBaiBao, TenLoaiBaiBao, VaiTro, TenTapChiCongBo, NgayCongBo  
	from GV_BaiBao join BaiBao on BaiBao.MaBaiBao=GV_BaiBao.MaBaiBao
	join LoaiBaiBao on LoaiBaiBao.MaLoaiBaiBao = BaiBao.MaLoaiBaiBao
end

go
LichSuBaiBao 'GV01'

create proc LichSuNghienCuDeTai
@maGV varchar(10)
as begin
	select TenDeTai, TenLoaiDeTai, VaiTro, NgayBatDau, NgayKetThuc, CoQuanQuanLy, TinhTrang 
	from GV_DeTaiNghienCuu join DeTai on DeTai.MaDeTai = GV_DeTaiNghienCuu.MaDeTai
	join LoaiDeTaiNghienCuu on LoaiDeTaiNghienCuu.MaLoaiDeTai=DeTai.MaLoaiDeTai
	where MaGiaoVien = @maGV
end

go
LichSuNghienCuDeTai 'GV01'

create proc CacSanPhamKHCN
@maGV varchar(10)
as begin
	select TenSanPham, NgayCongBo, GhiChu from SanPhamKHCN where MaGiaoVien = @maGV
end
go
CacSanPhamKHCN 'GV01'

create proc VanBangSangChe
@maGV varchar(10)
as begin
	select TenVanBang, KiHieu, NoiCap, NamCap from VanBangSoHuuTriTue where MaGiaoVien = @maGV
end
go
VanBangSangChe 'GV01'

create proc CacGiaiThuongKHCN
@maGV varchar(10)
as
begin
	select HinhThucNoiDung, ToChucTangThuong, NgayNhan from GiaiThuongKHCN where MaGiaoVien = @maGV
end
go
CacGiaiThuongKHCN 'GV03'


create proc updateGiaoVien
@maGV varchar(10),
@tenGV nvarchar(40),
@GioiTinh BIT,
@NgaySinh DATE,
@QueQuan NVARCHAR(100),
@DiaChi NVARCHAR(100),
@DienThoai varchar(12),
@Email VARCHAR(50)
as begin

	update GiaoVien set TenGiaoVien = @tenGV, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh,
	QueQuan=@QueQuan, DiaChi = @DiaChi, DienThoai = @DienThoai, Email = @Email
	where MaGiaoVien = @maGV

end

updateGiaoVien 'GV01', N'Lữ Thành Long', 1, '1975-05-10', N'Hải Phòng', N'Xuân Mai Huyện Chương Mỹ Thành Phố Hà Nôi',
				'0987389277', 'thanhlong@gmail.com'


alter proc updateGV_BoMon
@maGV varchar(10),
@maBM varchar(10),
@ngayDen DATE,
@ngayDi date
as begin
	if(@ngayDi is null)	
		insert into GV_BoMon(MaGiaoVien, MaBoMon, NgayChuyenDen) values(@maGV, @maBM, @ngayDen)		
	else
	begin
		if(exists(select MaGiaoVien from GV_BoMon where MaGiaoVien=@maGV and MaBoMon = @maBM and NgayChuyenDen = @ngayDen))		
			update GV_BoMon set NgayChuyenDi = @ngayDi where MaGiaoVien=@maGV and MaBoMon = @maBM and NgayChuyenDen = @ngayDen				
		else		
			insert into GV_BoMon(MaGiaoVien, MaBoMon, NgayChuyenDen, NgayChuyenDi) values(@maGV, @maBM, @ngayDen, @ngayDi);				
	end
end

updateGV_BoMon 'GV01', 'BM02', '2010-01-01', null

create proc LayHocViTheoGiaoVien 
@maGV varchar(10)
as begin
Select HocVi.MaHocVi, TenHocVi, NgayNhan from HocVi join GV_HocVi on HocVi.MaHocVi=GV_HocVi.MaHocVi where MaGiaoVien = @maGV
end
go
LayHocViTheoGiaoVien 'GV01'

create proc LayChucVuCMKTTheoGiaoVien 
@maGV varchar(10)
as begin
Select ChucVu_ChMonKT.MaChucVu, TenChucVu, NgayNhan from ChucVu_ChMonKT join GV_ChucVuChMKT 
on ChucVu_ChMonKT.MaChucVu=GV_ChucVuChMKT.MaChucVu where MaGiaoVien = @maGV
end
go
LayChucVuCMKTTheoGiaoVien 'GV01'


create proc LayChucDanhNCKHTheoGiaoVien 
@maGV varchar(10)
as begin
Select ChucDanh_ChMonNV.MaChucDanh, TenChucDanh, NgayNhan from ChucDanh_ChMonNV join GV_ChucDanhChMNV 
on ChucDanh_ChMonNV.MaChucDanh=GV_ChucDanhChMNV.MaChucDanh where MaGiaoVien = @maGV
end
go
LayChucDanhNCKHTheoGiaoVien 'GV01'

create proc LayHocViChuaNhan
@maGV varchar(10)
as begin
	select * from HocVi
	where MaHocVi not in(select MaHocVi from GV_HocVi where MaGiaoVien  = @maGV)
end
go
LayHocViChuaNhan 'GV01'

create proc LayChucDanhNhaGiaoChuaNhan
@maGV varchar(10)
as begin
	select * from ChucVu_ChMonKT
	where MaChucVu not in(select MaChucVu from GV_ChucVuChMKT where MaGiaoVien  = @maGV)
end
go
LayChucDanhNhaGiaoChuaNhan 'GV01'

create proc LayChucDanhNCKHChuaNhan
@maGV varchar(10)
as begin
	select * from ChucDanh_ChMonNV
	where MaChucDanh not in(select MaChucDanh from GV_ChucDanhChMNV where MaGiaoVien  = @maGV)
end
go
LayChucDanhNCKHChuaNhan 'GV01'

select * from GV_ChucVuChMKT

select * from GV_ChucDanhChMNV

update GV_HocVi set NgayNhan = '2010-01-01' where MaGiaoVien = 'GV01' and MaHocVi = 'HV02' 