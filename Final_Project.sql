use NhapHang
go
--===== CREATE TABLE =====

CREATE TABLE NhanVien(
	ma_nv int primary key identity,
	hoten nvarchar(100) not null,
	cccd varchar(12) not null,
	gioitinh nvarchar(10),
	ngaysinh date not null,
	tuoi int check (tuoi>0)
)



CREATE TABLE [User]
(
	ma_tk int primary key identity,
	ma_nv int,
	username nvarchar(100) unique,
	password nvarchar(50) not null,
	roleId int not null,
	Constraint fk_User_NhanVien FOREIGN KEY (ma_nv) REFERENCES Nhanvien(ma_nv)
)
go

drop table [User]

CREATE TABLE NhaCungCap(
	ma_ncc int primary key identity,
	ten_ncc nvarchar(100) not null,
	diachi nvarchar(200) not null,
	lienhe nvarchar(50) not null,
	ghichu nvarchar(500),
)
go

CREATE TABLE Kho (
    ma int PRIMARY KEY IDENTITY,
    ten NVARCHAR(100) not null,
    diachi NVARCHAR(200),
	isDeleted bit,
);


CREATE TABLE PhanLoai (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);

CREATE TABLE MauSac (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);

CREATE TABLE ChatLieu (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);

CREATE TABLE DonViTinh (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(20) NOT NULL
);

CREATE TABLE TrangThai (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(20) NOT NULL
);

CREATE TABLE ThuongHieu (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(100) NOT NULL
);


CREATE TABLE SanPham (
    ma_sp INT IDENTITY(1,1) PRIMARY KEY,    -- ID tự tăng
    ten_sp NVARCHAR(100) NOT NULL,
    ma_th INT,
    ma_loai INT,
    ma_mau INT,
    ma_chatlieu INT,
    kichthuoc NVARCHAR(100),
    ma_dvt INT,                           -- trạng thái
    mota NVARCHAR(200),
	isDeleted bit default(0),
    CONSTRAINT fk_SP_ThuongHieu  FOREIGN KEY (ma_th)     REFERENCES ThuongHieu(ma),
    CONSTRAINT fk_SP_PhanLoai    FOREIGN KEY (ma_loai)   REFERENCES PhanLoai(ma),
    CONSTRAINT fk_SP_MauSac      FOREIGN KEY (ma_mau)    REFERENCES MauSac(ma),
    CONSTRAINT fk_SP_ChatLieu    FOREIGN KEY (ma_chatlieu) REFERENCES ChatLieu(ma),
    CONSTRAINT fk_SP_DonViTinh   FOREIGN KEY (ma_dvt)    REFERENCES DonViTinh(ma),
);
GO

CREATE TABLE TonKho (
    ma_sp int,
    ma_kho int,
    soluong INT DEFAULT 0,
	ma_tt int,
    PRIMARY KEY(ma_sp, ma_kho),
    FOREIGN KEY(ma_sp) REFERENCES SanPham(ma_sp),
    FOREIGN KEY(ma_kho) REFERENCES Kho(ma),
	FOREIGN KEY(ma_tt) REFERENCES TrangThai(ma)
);

INSERT INTO TonKho (ma_sp, ma_kho, soluong, ma_tt)
VALUES
(1, 1, 50, 1),   -- Sản phẩm 1 trong kho 1, 50 cái, trạng thái 1
(2, 1, 30, 1),   -- Sản phẩm 2 trong kho 1, 30 cái
(3, 1, 20, 2),   -- Sản phẩm 3 trong kho 2, trạng thái 2
(4, 1, 10, 1),   -- Sản phẩm 4 trong kho 3
(5, 1, 0, 2);    -- Sản phẩm 5 trong kho 2, hết hàng


drop table SanPham,ThuongHieu, PhanLoai, MauSac, ChatLieu, DonViTinh, TrangThai

CREATE TABLE LoaiDonHang(
	ma int primary key identity,
	ten nvarchar(50) not null
)

Insert into LoaiDonHang(ten)
Values
	(N'Đơn hàng dự kiến'),
	(N'Đơn hàng thực tế')

Create table TrangThaiDH(
	ma int primary key identity,
	ten nvarchar(255),
)

CREATE TABLE DonHang(
	ma_don int primary key identity,
	ngaylap datetime,
	ma_nv int,
	ghichu nvarchar(200),
	ma_loai int,
	tongtien decimal(18,0),
	ma_ncc int,
	ma_kho int,
	ma_tt int, --ma_trangthai
	khoa bit default 0,
	ma_dhdk int, --ma_DonHangDuKien, Don Hang Thuc Te se tham chieu den de xu ly don hang nao
	Constraint fk_DonHang_NhanVien Foreign key (ma_nv) References NhanVien(ma_nv),
	Constraint fk_DonHang_LoaiDonHang Foreign key (ma_loai) References LoaiDonHang(ma),
	Constraint fk_DonHang_NhaCungCap Foreign key (ma_ncc) References NhaCungCap(ma_ncc),
	Constraint fk_DonHang_Kho Foreign key (ma_kho) References Kho(ma),
	Constraint fk_DonHang_TrangThai Foreign key (ma_tt) References TrangThaiDH(ma),
	Constraint fk_DonHang_DonHang Foreign key (ma_dhdk) References DonHang(ma_don)
)
go
alter table DonHang
drop fk_DonHang_TrangThai

alter table DonHang
Add constraint fk_DonHang_TrangThaiDH Foreign key (ma_tt) references TrangThaiDH(ma)


create table DonHangChiTiet(
	ma_don int not null,
	ma_sp int not null,
	gia_dk decimal(18,0) not null, -- Gia du kien 
	soluong int not null,
	Constraint pk_DHCT Primary Key (ma_don, ma_sp)
)

drop table LichSu
create table LichSu(
	ma_ls int primary key identity,
	thoigian datetime,
	ma_don int,
	ghichu nvarchar(255),
	constraint fk_LichSu_DonHang foreign key (ma_don) references DonHang(ma_don)
)



----------------Transaction-----------------
-- Bang ao chua danh sach san pham
create type dbo.DanhSachSanPham as table(
	ma_sp int not null primary key,
	gia_dk decimal(18,0) not null,
	soluong int not null
);

drop table DanhSachSanPham

drop proc prc_ThemDonHangDuKien
CREATE PROCEDURE prc_ThemDonHangDuKien
	@makho int,
	@ma_nv int,
    @ma_ncc int,
    @ChiTietPhieuNhap AS dbo.DanhSachSanPham READONLY  -- Table-valued parameter
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

		-- Tinh tong so tien 
		DECLARE @tongtien DECIMAL(18,0);
		SELECT @tongtien = SUM(gia_dk * soluong) FROM @ChiTietPhieuNhap;
        -- 1. Thêm phiếu nhập
        INSERT INTO DonHang (ngaylap, ma_nv,ma_ncc,ma_kho,tongtien, ma_loai,ma_tt)
        VALUES (GETDATE(),@ma_nv,@ma_ncc,@makho,@tongtien,1,1);  --ma loai = 1 (don hang du kien), ma_tt = 1 (can xu ly)

        DECLARE @ma_don INT = SCOPE_IDENTITY();

        -- 2. Thêm chi tiết
        INSERT INTO DonHangChiTiet(ma_don, ma_sp, gia_dk, soluong)
        SELECT @ma_don, ma_sp, gia_dk, soluong FROM @ChiTietPhieuNhap;
		
		-- 3. Ghi lịch sử
		INSERT INTO LichSu(thoigian,ma_don,ghichu)
        Values (GETDATE(),@ma_don,N'Tạo đơn hàng dự kiến')
		

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; -- Báo lỗi ra ngoài cho C# biết
    END CATCH
END




DECLARE @DanhSach dbo.DanhSachSanPham;
INSERT INTO @DanhSach (ma_sp, gia_dk, soluong)
VALUES (1, 100000, 5),  -- sp1, giá 100k, SL 5
       (2, 200000, 2);  -- sp2, giá 200k, SL 2
EXEC prc_ThemDonHangDuKien
	@makho = 2,
    @ma_nv = 1,
    @ma_ncc = 1,
    @ChiTietPhieuNhap = @DanhSach


drop procedure prc_HuyDonHangDuKien
CREATE procedure prc_HuyDonHangDuKien
	@ma_don int
as
begin
	begin try
		begin transaction;

		declare @ma_tt int;
		select @ma_tt = ma_tt 
		from DonHang with(updlock,rowlock)
		where ma_don = @ma_don

		if @ma_tt is null
		begin
			raiserror(N'Đơn hàng không tồn tại.',16,1);
			rollback transaction;
			return;
		end;


		if @ma_tt = 1
		begin
			-- cap nhap trang thai don hang
			update DonHang
			set ma_tt = 2 -- ma_tt=2 hủy đơn hàng
			where ma_don = @ma_don
			-- ghi lich su
			insert into LichSu(thoigian,ma_don,ghichu)
			values(GETDATE(),@ma_don,N'Hủy đơn hàng')
		end;
		else
			begin
				raiserror(N'Trạng thái không hợp lệ',16,1);
				rollback transaction;
				return;
			end;
		commit transaction;
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback transaction;
		throw;
	end catch
end

exec prc_HuyDonHangDuKien 7

drop procedure prc_ThemDonHangThucTe
create procedure prc_ThemDonHangThucTe
	@makho int,
	@ma_nv int,
    @ma_ncc int,
	@ma_dhxuly int,
    @ChiTietPhieuNhap AS dbo.DanhSachSanPham READONLY
as
begin
	begin try
		begin transaction;
		declare @trangthai int
		select @trangthai = ma_tt 
		from DonHang with (updlock,rowlock)
		where ma_don = @ma_dhxuly

		if @trangthai is null 
		begin
			raiserror(N'Đơn hàng xử lý không tồn tại',16,1);
			rollback transaction;
			return;
		end

		if @trangthai = 1
		begin
			--tinh tong so tien
			declare @tongtien decimal(18,2)
			select @tongtien = Sum(gia_dk*soluong) from @ChiTietPhieuNhap;
			-- Tao don hang thuc te
			Insert into DonHang(ngaylap, ma_nv,ma_ncc,ma_kho,tongtien, ma_loai,ma_tt,ma_dhdk)
			values(GETDATE(),@ma_nv,@ma_ncc,@makho,@tongtien,2,3,@ma_dhxuly) -- ma_loai = 2: Đơn hàng thực tế, ma_tt = 3:Đã xử lý
			-- Them chi tiet don hang
			declare @madon int = scope_identity();
			Insert into DonHangChiTiet(ma_don,ma_sp,gia_dk,soluong)
			select @madon,ma_sp,gia_dk,soluong from @ChiTietPhieuNhap;
			-- Cap nhat trang thai don hang du kien
			Update DonHang
			Set ma_tt = 3 -- ma_tt = 3: don hang da xu ly
			where ma_don = @ma_dhxuly
			-- Cap nhat ton kho
			Insert Into TonKho(ma_kho,ma_sp,soluong)
			Select @makho,ma_sp,soluong from @ChiTietPhieuNhap
			-- Ghi lich su
			Insert into LichSu(thoigian,ma_don,ghichu)
			values(GETDATE(),@madon,N'Xử lý đơn hàng '+CAST(@ma_dhxuly as nvarchar(50)))

		end
		else
			begin
				raiserror(N'Trạng thái đơn hàng không hợp lệ',16,1)
				rollback transaction;
				return
			end
		commit transaction;
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback;
		throw;
	end catch

end

-- Bước 2: Tạo danh sách sản phẩm để nhập
DECLARE @ds dbo.DanhSachSanPham;
INSERT INTO @ds(ma_sp, gia_dk, soluong)
VALUES (1, 10000, 5),  -- SP1: 5 cái, giá 10.000
       (2, 20000, 3);  -- SP2: 3 cái, giá 20.000

-- Bước 3: Gọi procedure xử lý
EXEC prc_ThemDonHangThucTe
    @makho = 2,          -- Mã kho
    @ma_nv = 1,        -- Nhân viên xử lý
    @ma_ncc = 2,       -- Nhà cung cấp
    @ma_dhxuly = 13,      -- Đơn hàng dự kiến cần xử lý
    @ChiTietPhieuNhap = @ds;


select * from DonHangChiTiet








--===== CREATE VIEW =====
drop view v_SanPham_Chitiet
CREATE VIEW v_SanPham_Chitiet
AS
SELECT 
    sp.ma_sp,
    sp.ten_sp,
    th.ten AS thuonghieu,
    pl.ten AS phanloai,
    ms.ten AS mausac,
    cl.ten AS chatlieu,
    sp.kichthuoc,
    dvt.ten AS donvitinh,
    sp.mota
FROM SanPham sp
LEFT JOIN ThuongHieu th   ON sp.ma_th = th.ma
LEFT JOIN PhanLoai pl     ON sp.ma_loai = pl.ma
LEFT JOIN MauSac ms       ON sp.ma_mau = ms.ma
LEFT JOIN ChatLieu cl     ON sp.ma_chatlieu = cl.ma
LEFT JOIN DonViTinh dvt   ON sp.ma_dvt = dvt.ma
WHERE sp.isDeleted != 1
go

select * from SanPham
select * from v_SanPham_Chitiet



Create view v_NhaCungCap
as
Select * from NhaCungCap
where isDeleted != 1

Create view v_Kho
as
Select * from Kho
where isDeleted != 1


drop view v_TonKho_Chitiet

create view v_TonKho_Chitiet
as
select
k.ten as tenkho,
sp.ma_sp,
sp.ten_sp,
th.ten as thuonghieu,
dvt.ten as dvt,
tt.ten as trangthai,
tk.soluong
from
TonKho tk
left join SanPham sp on sp.ma_sp = tk.ma_sp
left join Kho k on k.ma = tk.ma_kho
left join ThuongHieu th on th.ma = sp.ma_th
left join DonViTinh dvt on dvt.ma = sp.ma_dvt
left join TrangThai tt on tt.ma = tk.ma_tt
Where sp.isDeleted != 1 and k.isDeleted != 1
go

drop view v_TonKho_Chitiet

select * from v_TonKho_Chitiet


drop view v_SanPham_Chitiet
select * from v_SanPham_Chitiet


Create view v_DonHangDuKien 
as
select ma_don,ngaylap,nv.ma_nv, tongtien, ncc.ten_ncc, k.ten  as 'kho', tt.ten as 'trangthai',dh.ghichu from DonHang dh
left join NhanVien nv on nv.ma_nv = dh.ma_nv
left join LoaiDonHang ldh on ldh.ma = dh.ma_loai
left join NhaCungCap ncc on ncc.ma_ncc = dh.ma_ncc
left join Kho k on k.ma = dh.ma_kho
left join TrangThaiDH tt on tt.ma = dh.ma_tt
where ldh.ma = 1 and ma_tt = 1


drop view v_DonHangDuKien

select * from v_DonHangDuKien

drop view v_DonHangCanXuLy
create view v_DonHangCanXuLy
as
select ma_don,ngaylap,nv.ma_nv, tongtien, ncc.ten_ncc, k.ten  as 'kho', tt.ten as 'trangthai',dh.ghichu from DonHang dh
left join NhanVien nv on nv.ma_nv = dh.ma_nv
left join LoaiDonHang ldh on ldh.ma = dh.ma_loai
left join NhaCungCap ncc on ncc.ma_ncc = dh.ma_ncc
left join Kho k on k.ma = dh.ma_kho
left join TrangThaiDH tt on tt.ma = dh.ma_tt
where dh.ma_tt = 1

select * from v_DonHangCanXuLy

go
create view v_LichSu
as
select thoigian,ma_don,ghichu from LichSu
go














--===== CREATE PROC =====
create proc prc_LayDuLieuThuocTinh
	@nametable nvarchar(50)
as
begin
	DECLARE @sql NVARCHAR(MAX);
    SET @sql = N'SELECT * FROM ' + QUOTENAME(@nametable);
    EXEC sp_executesql @sql;
end
go

exec prc_LayDuLieuThuocTinh N'ThuongHieu'
go

create proc prc_InsertNhaCC 
	@ten_ncc nvarchar(100),
	@diachi nvarchar(200),
	@lienhe nvarchar(50),
	@ghichu nvarchar(500)
as
begin
	insert into NhaCungCap (ten_ncc,diachi,lienhe,ghichu)
	values (@ten_ncc,@diachi,@lienhe,@ghichu)
end
go

create proc prc_UpdateNhaCC 
	@ma_ncc int,
	@ten_ncc nvarchar(100),
	@diachi nvarchar(200),
	@lienhe nvarchar(50),
	@ghichu nvarchar(500)
as
begin
	Update NhaCungCap set
	ten_ncc = @ten_ncc,
	diachi = @diachi,
	lienhe = @lienhe,
	ghichu = @ghichu
	where ma_ncc = @ma_ncc
end
go

create proc prc_XoaNhaCC
	@ma_ncc int
as
begin
	Delete from NhaCungCap 
	where ma_ncc = @ma_ncc
end

exec prc_XoaNhaCC 2


create proc prc_InsertKho
    @ten NVARCHAR(100),
    @diachi NVARCHAR(200)
as
begin
	INSERT INTO kHO(ten,diachi) 
	VALUES (@ten,@diachi)
end
go

create proc prc_UpdateKho
	@ma int,
    @ten NVARCHAR(100),
    @diachi NVARCHAR(200)
as
begin
	UPdate Kho Set
	ten = @ten,
	diachi = @diachi
	where ma = @ma
end
go

create proc prc_XoaKho
	@ma int
as
begin
	Delete From Kho 
	where ma = @ma
end
go

select * from Kho


CREATE PROC prc_InsertThuocTinh
    @nametable NVARCHAR(100),
    @ten NVARCHAR(50)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @paramDef NVARCHAR(MAX);

    -- Tạo câu lệnh động
    SET @sql = N'INSERT INTO ' + QUOTENAME(@nametable) + N' (ten) VALUES(@ten);';

    -- Khai báo tham số cho sp_executesql
    SET @paramDef = N'@ten NVARCHAR(50)';

    -- Thực thi câu lệnh động với tham số
    EXEC sp_executesql @sql, @paramDef, @ten = @ten;
END
GO

CREATE PROC prc_XoaThuocTinh
    @nametable NVARCHAR(100),
    @ma int
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @paramDef NVARCHAR(MAX);

    -- Tạo câu lệnh động
    SET @sql = N'DELETE FROM ' + QUOTENAME(@nametable) + N' WHERE ma = @ma;';

    -- Khai báo tham số cho sp_executesql
    SET @paramDef = N'@ma int';

    -- Thực thi câu lệnh động với tham số
    EXEC sp_executesql @sql, @paramDef, @ma = @ma;
END
GO

drop proc prc_XoaThuocTinh

exec prc_XoaThuocTinh N'ChatLieu', 1

CREATE PROC prc_UpdateThuocTinh
    @nametable NVARCHAR(100),
    @ma NVARCHAR(20),
    @ten NVARCHAR(50)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @paramDef NVARCHAR(MAX);

    -- Tạo câu lệnh động
    SET @sql = N'UPDATE ' + QUOTENAME(@nametable) + N'SET ten=@ten WHERE ma=@ma';

    -- Khai báo tham số cho sp_executesql
    SET @paramDef = N'@ma NVARCHAR(20), @ten NVARCHAR(50)';

    -- Thực thi câu lệnh động với tham số
    EXEC sp_executesql @sql, @paramDef, @ma = @ma, @ten = @ten;
END
GO

drop proc prc_InsertSanPham
create proc prc_InsertSanPham 
    @ten_sp NVARCHAR(100),
    @ma_th INT,
    @ma_loai INT,
    @ma_mau INT,
    @ma_chatlieu INT,
    @kichthuoc NVARCHAR(100),
    @ma_dvt INT,                            
    @mota NVARCHAR(200)
as
begin
	Insert Into SanPham(ten_sp,ma_th,ma_loai,ma_mau,ma_chatlieu,kichthuoc,ma_dvt,mota)
	Values(@ten_sp,@ma_th,@ma_loai,@ma_mau,@ma_chatlieu,@kichthuoc,@ma_dvt,@mota)
end
go

drop proc InsertSanPham


EXEC prc_InsertSanPham 
    @ten_sp = N'Áo sơ mi nam',
    @ma_th = 1,
    @ma_loai = 2,
    @ma_mau = 3,
    @ma_chatlieu = 4,
    @kichthuoc = N'L',
    @ma_dvt = 1,
    @mota = N'Áo sơ mi cotton cao cấp';

drop proc prc_UpdateThuocTinh
exec prc_UpdateThuocTinh N'PhanLoai',N'PL06',N'Sofas'


CREATE PROC prc_UpdateSanPham
    @ma_sp INT,                 -- Khóa chính của sản phẩm
    @ten_sp NVARCHAR(100),
    @ma_th INT,
    @ma_loai INT,
    @ma_mau INT,
    @ma_chatlieu INT,
    @kichthuoc NVARCHAR(100),
    @ma_dvt INT,
    @mota NVARCHAR(200)
AS
BEGIN
    UPDATE SanPham
    SET 
        ten_sp      = @ten_sp,
        ma_th       = @ma_th,
        ma_loai     = @ma_loai,
        ma_mau      = @ma_mau,
        ma_chatlieu = @ma_chatlieu,
        kichthuoc   = @kichthuoc,
        ma_dvt      = @ma_dvt,
        mota        = @mota
    WHERE ma_sp = @ma_sp;
END
GO

Create proc prc_XoaSanPham
	@ma_sp int
as
begin
	Delete from SanPham
	Where ma_sp = @ma_sp
end

select * from SanPham
exec prc_XoaSanPham 10

go
create proc prc_XoaSPTonKho
	@ma_kho int,
	@ma_sp int
as
begin
	Delete from TonKho
	where ma_kho = @ma_kho and ma_sp = @ma_sp
end


select * from dbo.fn_ChitietDonHang(4)

go






--===== CREATE FUNC =====
create function fn_LayKho()
returns table
as
	return (select * from Kho)
go



select * from dbo.fn_LayKho();

drop function fn_TimNhaCC
create function fn_TimNhaCC (@key nvarchar(50))
returns table
as
return (select * from v_NhaCungCap where ten_ncc like '%'+@key+'%');
go




create function fn_TimSanPham (@key nvarchar(50))
returns table
as
return (select * from v_SanPham_Chitiet where ten_sp like '%'+@key+'%');
go


create function fn_TimSanPhamByID (@key int)
returns table
as
return (select * from v_SanPham_Chitiet where  ma_sp = @key);
go

drop function fn_TimSanPhamByID
/*
create function fn_CheckRoleId(@username nvarchar(100), @password nvarchar(50))
returns int
as 
begin
	Declare @roleId int
	select @roleId = roleId from [User] where [User].username = @username and [User].password = @password
	if @roleId IS NULL
		set @roleId = 0
	return @roleId;
end
go
*/

create function fn_LaySPTheoKho(@tenkho nvarchar(50))
returns table
as
	return (Select * from v_TonKho_Chitiet where tenkho = @tenkho)
go

drop function fn_LaySPTheoKho

create function fn_TimSPTrongTonKho(@tensp nvarchar(50),@tenkho nvarchar(50))
returns table
as
	return (select * from v_TonKho_Chitiet where ten_sp like '%'+@tensp+'%' and tenkho = @tenkho )
go

drop function fn_TimSPTrongTonKho

Select * from dbo.fn_LaySPTheoKho(N'A2')


go
create function fn_ChitietDonHang (@madon int)
returns table
as
	return (select dhct.ma_sp, sp.ten_sp,dhct.gia_dk, dvt.ten as 'dvt', dhct.soluong  from DonHangChiTiet dhct
	left join SanPham sp on dhct.ma_sp = sp.ma_sp
	left join DonViTinh dvt on sp.ma_dvt = dvt.ma
	where ma_don = @madon)
go

drop function fn_ChitietDonHang

select * from dbo.fn_ChitietDonHang(1)

go
create function fn_LayDonHangTheoMa(@ma_don int)
returns table
as
	return (select ma_don,ngaylap,nv.ma_nv,tongtien, ncc.ten_ncc, k.ten as 'kho', tt.ten as 'trangthai',dh.ghichu from DonHang dh
left join NhanVien nv on nv.ma_nv = dh.ma_nv
left join NhaCungCap ncc on ncc.ma_ncc = dh.ma_ncc
left join Kho k on k.ma = dh.ma_kho
left join TrangThaiDH tt on tt.ma = dh.ma_tt
where dh.ma_don = @ma_don)
go
drop function fn_LayDonHangTheoMa

select * from fn_LayDonHangTheoMa(1)
go
create function fn_TienNhapHangCacThangTheoNam (@nam int)
returns table
as
	return (Select Thang,TongTienNhap FROM v_TongTienNhapTheoThang
	WHERE Nam = @nam)
go

select * from fn_TienNhapHangCacThangTheoNam(2025);
------------Function Thống Kê -----------------
-- Số tiền nhập hang của từng tháng, từng năm
GO
drop view v_TongTienNhapTheoThang
CREATE OR ALTER VIEW v_TongTienNhapTheoThang
AS
SELECT 
    YEAR(ngaylap) AS Nam,
    MONTH(ngaylap) AS Thang,
    SUM(tongtien) AS TongTienNhap
FROM DonHang
WHERE ma_loai = 2
GROUP BY YEAR(ngaylap), MONTH(ngaylap);
GO

SELECT * FROM v_TongTienNhapTheoThang ORDER BY Nam, Thang;

-- Số tiền nhập thực tế trong tháng
GO
CREATE FUNCTION fn_TongTienNhap_ThangNay()
RETURNS DECIMAL(18,0)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18,0);

    SELECT @TongTien = ISNULL(SUM(tongtien), 0)
    FROM DonHang
    WHERE ma_loai = 2
      AND YEAR(ngaylap) = YEAR(GETDATE())
      AND MONTH(ngaylap) = MONTH(GETDATE());

    RETURN @TongTien;
END;
GO

SELECT dbo.fn_TongTienNhap_ThangNay() AS TongTienThangNay;

-- Số tiền nhập thực tế trong tuần này
Go
CREATE FUNCTION fn_TongTienNhap_TuanNay()
RETURNS DECIMAL(18,0)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18,0);
    DECLARE @StartOfWeek DATE = DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE));
    DECLARE @EndOfWeek DATE = GETDATE();

    SELECT @TongTien = ISNULL(SUM(tongtien), 0)
    FROM DonHang
    WHERE ma_loai = 2
      AND ngaylap BETWEEN @StartOfWeek AND @EndOfWeek;

    RETURN @TongTien;
END;
GO

SELECT dbo.fn_TongTienNhap_TuanNay() AS TongTienTuanNay;

-- Giá trị đơn hàng trong tháng này của mỗi kho
DROP VIEW v_TongTienNhapTheoKho_ThangNay
GO
CREATE VIEW v_TongTienNhapTheoKho_ThangNay
AS
SELECT 
    k.ten,
    SUM(dh.tongtien) AS TongTienNhap
FROM DonHang dh
JOIN Kho k ON dh.ma_kho = k.ma
WHERE dh.ma_loai = 2
  AND YEAR(dh.ngaylap) = YEAR(GETDATE())
  AND MONTH(dh.ngaylap) = MONTH(GETDATE())
GROUP BY k.ten;
GO

SELECT * FROM v_TongTienNhapTheoKho_ThangNay;


-- Số tiền nhập hàng thực tế của mỗi nhà cung cấp trong tháng này
DROP VIEW vw_TongTienNhapTheoNCC_ThangNay
GO
CREATE VIEW v_TongTienNhapTheoNCC_ThangNay
AS
SELECT 
    ncc.ten_ncc,
    SUM(dh.tongtien) AS TongTienNhap
FROM DonHang dh
JOIN NhaCungCap ncc ON dh.ma_ncc = ncc.ma_ncc
WHERE dh.ma_loai = 2
  AND YEAR(dh.ngaylap) = YEAR(GETDATE())
  AND MONTH(dh.ngaylap) = MONTH(GETDATE())
GROUP BY ncc.ten_ncc;
GO

SELECT * FROM v_TongTienNhapTheoNCC_ThangNay;














-------------Trigger------------------
CREATE TRIGGER trg_TonKho_Insert
ON TonKho
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    MERGE TonKho AS target
    USING inserted AS source
    ON target.ma_kho = source.ma_kho AND target.ma_sp = source.ma_sp
    WHEN MATCHED THEN
        UPDATE SET target.soluong = target.soluong + source.soluong
    WHEN NOT MATCHED THEN
        INSERT (ma_kho, ma_sp, soluong)
        VALUES (source.ma_kho, source.ma_sp, source.soluong);
END


/*
@ma_sp INT,                 -- Khóa chính của sản phẩm
    @ten_sp NVARCHAR(100),
    @ma_th INT,
    @ma_loai INT,
    @ma_mau INT,
    @ma_chatlieu INT,
    @kichthuoc NVARCHAR(100),
    @ma_dvt INT,
    @mota NVARCHAR(200)
*/

drop TRIGGER trg_ChatLieu
go
CREATE TRIGGER trg_ChatLieu
ON ChatLieu
INSTEAD OF DELETE
AS
BEGIN
    -- Kiểm tra ràng buộc trước khi xóa
    IF EXISTS (
        SELECT 1
        FROM SanPham sp
        INNER JOIN deleted d ON sp.ma_chatlieu = d.ma
    )
    BEGIN
        RAISERROR(N'Có sản phẩm đang sử dụng chất liệu này, không thể xóa!', 16, 1);
        RETURN;
    END

    -- Nếu không có sản phẩm tham chiếu thì cho phép xóa
    DELETE cl
    FROM ChatLieu cl
    INNER JOIN deleted d ON cl.ma = d.ma;
END
GO

select * from SanPham;
Select * from ChatLieu;
delete from ChatLieu where ma = 6


-- DonViTinh
DROP TRIGGER IF EXISTS trg_DonViTinh_Delete;
GO
CREATE TRIGGER trg_DonViTinh_Delete
ON DonViTinh
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM SanPham sp
        INNER JOIN deleted d ON sp.ma_dvt = d.ma
    )
    BEGIN
        RAISERROR(N'Có sản phẩm đang sử dụng đơn vị tính này, không thể xóa!', 16, 1);
        RETURN;
    END

    DELETE dvt
    FROM DonViTinh dvt
    INNER JOIN deleted d ON dvt.ma = d.ma;
END
GO

select * from SanPham;
Select * from DonViTinh;
delete from DonViTinh where ma = 5

-- MauSac
DROP TRIGGER IF EXISTS trg_MauSac_Delete;
GO
CREATE TRIGGER trg_MauSac_Delete
ON MauSac
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM SanPham sp
        INNER JOIN deleted d ON sp.ma_mau = d.ma
    )
    BEGIN
        RAISERROR(N'Có sản phẩm đang sử dụng màu sắc này, không thể xóa!', 16, 1);
        RETURN;
    END

    DELETE ms
    FROM MauSac ms
    INNER JOIN deleted d ON ms.ma = d.ma;
END
GO

select * from SanPham;
Select * from MauSac;
delete from MauSac where ma = 5

-- PhanLoai
DROP TRIGGER IF EXISTS trg_PhanLoai_Delete;
GO
CREATE TRIGGER trg_PhanLoai_Delete
ON PhanLoai
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM SanPham sp
        INNER JOIN deleted d ON sp.ma_loai = d.ma
    )
    BEGIN
        RAISERROR(N'Có sản phẩm đang sử dụng phân loại này, không thể xóa!', 16, 1);
        RETURN;
    END

    DELETE pl
    FROM PhanLoai pl
    INNER JOIN deleted d ON pl.ma = d.ma;
END
GO

select * from SanPham;
Select * from PhanLoai;
delete from PhanLoai where ma = 5



-- ThuongHieu
DROP TRIGGER IF EXISTS trg_ThuongHieu_Delete;
GO
CREATE TRIGGER trg_ThuongHieu_Delete
ON ThuongHieu
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM SanPham sp
        INNER JOIN deleted d ON sp.ma_th = d.ma
    )
    BEGIN
        RAISERROR(N'Có sản phẩm đang sử dụng thương hiệu này, không thể xóa!', 16, 1);
        RETURN;
    END

    DELETE th
    FROM ThuongHieu th
    INNER JOIN deleted d ON th.ma = d.ma;
END
GO

select * from SanPham;
Select * from ThuongHieu;
delete from ThuongHieu where ma = 7

drop TRIGGER trg_Kho_Delete
CREATE TRIGGER trg_Kho_Delete
ON Kho
INSTEAD OF DELETE
AS
BEGIN
    -- Kiểm tra xem kho có còn tồn tại trong bảng TonKho không
    IF EXISTS (
        SELECT 1
        FROM TonKho tk
        INNER JOIN deleted d ON tk.ma_kho = d.ma
    )
    BEGIN
        RAISERROR(N'Kho không thể xóa vì còn tồn kho!', 16, 1);
        RETURN;
    END

    -- Nếu không còn tồn kho thì thực hiện soft delete
    UPDATE k
    SET k.isDeleted = 1
    FROM Kho k
    INNER JOIN deleted d ON k.ma = d.ma;
END
GO


Select * from Kho;
Select * from TonKho;
delete from Kho where ma = 3

CREATE TRIGGER trg_NhaCungCap_Delete
ON NhaCungCap
INSTEAD OF DELETE
AS
BEGIN
    -- Xóa mềm: chỉ đánh dấu isDeleted = 1 thay vì xóa cứng
    UPDATE NhaCungCap
    SET isDeleted = 1
    WHERE ma_ncc IN (SELECT ma_ncc FROM deleted);
END
GO

select * from NhaCungCap
delete from NhaCungCap where ma_ncc = 2

DROP TRIGGER trg_SanPham_Delete
CREATE TRIGGER trg_SanPham_Delete
ON SanPham
INSTEAD OF DELETE
AS
BEGIN
    -- Nếu sản phẩm còn tồn kho với số lượng > 0 thì chặn xóa
    IF EXISTS (
        SELECT 1
        FROM TonKho t
        INNER JOIN deleted d ON t.ma_sp = d.ma_sp
        WHERE t.soluong > 0
    )
    BEGIN
        RAISERROR(N'Sản phẩm không thể xóa vì còn số lượng tồn kho', 16, 1);
		RETURN;
    END
    ELSE
    BEGIN
        -- Xóa mềm: chỉ update cờ isDeleted = 1
        UPDATE SanPham
        SET isDeleted = 1
        WHERE ma_sp IN (SELECT ma_sp FROM deleted);
    END
END
GO

select * from SanPham;
Select * from TonKho;
delete from SanPham where ma_sp = 6
DROP TRIGGER trg_TonKho_Delete
CREATE TRIGGER trg_TonKho_Delete
ON TonKho
INSTEAD OF DELETE
AS
BEGIN
    -- Nếu trong deleted có bản ghi có SoLuong > 0 thì chặn xóa
    IF EXISTS (
        SELECT 1
        FROM deleted d
        WHERE d.SoLuong > 0
    )
    BEGIN
        RAISERROR(N'Còn sản phẩm trong kho, không thể xóa!', 16, 1);
        RETURN;
    END
    ELSE
    BEGIN
        -- Thực hiện xóa thật sự
        DELETE tk
        FROM TonKho tk
        INNER JOIN deleted d ON tk.ma_sp = d.ma_sp AND tk.ma_kho = d.ma_kho;
    END
END
GO

select * from TonKho;
delete from TonKho where ma_sp = 5 and ma_kho = 1









-- Bảng PhanLoai
INSERT INTO PhanLoai (ten) VALUES
(N'Ghế Sofa'),
(N'Bàn Trà'),
(N'Kệ Tivi'),
(N'Tủ Quần Áo'),
(N'Giường Ngủ');

-- Bảng MauSac
INSERT INTO MauSac (ten) VALUES
(N'Xanh Dương'),
(N'Đỏ'),
(N'Trắng'),
(N'Đen'),
(N'Vàng');

-- Bảng ChatLieu
INSERT INTO ChatLieu (ten) VALUES
(N'Gỗ Sồi'),
(N'Gỗ Công Nghiệp'),
(N'Kim Loại'),
(N'Nhựa ABS'),
(N'Vải Nỉ');

-- Bảng DonViTinh
INSERT INTO DonViTinh (ten) VALUES
(N'Cái'),
(N'Bộ'),
(N'Chiếc'),
(N'Đôi'),
(N'Tấm');

-- Bảng TrangThai
INSERT INTO TrangThai (ten) VALUES
(N'Còn Hàng'),
(N'Hết Hàng'),
(N'Ngừng Kinh Doanh'),
(N'Sắp Về'),
(N'Đặt Hàng');

-- Bảng ThuongHieu
INSERT INTO ThuongHieu (ten) VALUES
(N'IKEA'),
(N'Hoà Phát'),
(N'Nhà Xinh'),
(N'Phố Xinh'),
(N'Thế Giới Nội Thất');

INSERT INTO SanPham (ten_sp, ma_th, ma_loai, ma_mau, ma_chatlieu, kichthuoc, ma_dvt, mota)
VALUES
(N'Bàn làm việc gỗ sồi IKEA', 1, 1, 2, 1, N'120x60x75 cm', 1, N'Bàn làm việc văn phòng bằng gỗ sồi'),
(N'Ghế xoay Hòa Phát', 2, 2, 3, 3, N'45x45x90 cm', 1, N'Ghế xoay văn phòng chất liệu kim loại bọc da'),
(N'Tủ quần áo 3 cánh IKEA', 1, 3, 1, 2, N'200x60x220 cm', 1, N'Tủ quần áo gỗ công nghiệp màu trắng'),
(N'Giường ngủ gỗ công nghiệp Hòa Phát', 2, 4, 2, 2, N'160x200 cm', 1, N'Giường đôi bằng gỗ công nghiệp'),
(N'Bộ bàn ăn 6 ghế Phong Vũ', 3, 1, 4, 1, N'180x90x75 cm', 2, N'Bàn ăn gỗ sồi kèm 6 ghế');

INSERT INTO NhanVien (hoten, cccd, gioitinh, ngaysinh, tuoi)
VALUES
    (N'Nguyễn Văn An',   '012345678901', N'Nam', '1995-03-15', 30),
    (N'Trần Thị Bình',   '123456789012', N'Nữ',  '1998-07-20', 27),
    (N'Lê Văn Cường',   '234567890123', N'Nam', '1990-11-05', 34),
    (N'Phạm Thị Dung',   '345678901234', N'Nữ',  '2000-01-10', 25),
    (N'Hoàng Minh Tuấn', '456789012345', N'Nam', '1988-06-25', 37);


INSERT INTO DonHang (ngaylap, ma_nv, ghichu, ma_loai, tongtien, ma_ncc, ma_kho, ma_tt, khoa)
VALUES
('2025-09-20 10:30:00', 1, N'Đơn hàng nhập vật tư', 1, 1500000, 1, 1, 1, 0),
('2025-09-20 15:00:00', 2, N'Đơn hàng bán lẻ', 2, 2500000, 2, 1, 2, 0),
('2025-09-21 09:20:00', 3, N'Đơn hàng bán sỉ', 2, 5000000, 1, 2, 3, 0),
('2025-09-21 14:45:00', 1, N'Đơn hàng hoàn trả', 1, 800000, 2, 2, 1, 1),
('2025-09-22 11:10:00', 2, N'Đơn hàng nhập định kỳ', 1, 3200000, 1, 1, 2, 0);


INSERT INTO DonHangChiTiet (ma_don, ma_sp, gia_dk, soluong)
VALUES
-- Đơn hàng 1
(1, 1, 120000, 2),
(1, 2, 45000, 5),
(1, 3, 78000, 1),
(1, 4, 56000, 3),
-- Đơn hàng 2
(2, 1, 125000, 3),
(2, 2, 47000, 2),
(2, 5, 99000, 4),
(2, 3, 80000, 1),
-- Đơn hàng 3
(3, 2, 46000, 6),
(3, 4, 55000, 2),
(3, 5, 100000, 3),
(3, 1, 128000, 1),
-- Đơn hàng 4
(4, 3, 81000, 4),
(4, 5, 102000, 2),
(4, 2, 46500, 3),
(4, 4, 57000, 5),
-- Đơn hàng 5
(5, 1, 130000, 2),
(5, 3, 83000, 1),
(5, 4, 59000, 4),
(5, 5, 105000, 3);