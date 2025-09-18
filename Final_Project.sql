use NhapHang
go
--===== CREATE TABLE =====
CREATE TABLE [User]
(
	username nvarchar(100) PRIMARY key,
	password nvarchar(50) not null,
	roleId int not null
)
go


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
    diachi NVARCHAR(200)
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

drop table SanPham

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

    CONSTRAINT fk_SP_ThuongHieu  FOREIGN KEY (ma_th)     REFERENCES ThuongHieu(ma),
    CONSTRAINT fk_SP_PhanLoai    FOREIGN KEY (ma_loai)   REFERENCES PhanLoai(ma),
    CONSTRAINT fk_SP_MauSac      FOREIGN KEY (ma_mau)    REFERENCES MauSac(ma),
    CONSTRAINT fk_SP_ChatLieu    FOREIGN KEY (ma_chatlieu) REFERENCES ChatLieu(ma),
    CONSTRAINT fk_SP_DonViTinh   FOREIGN KEY (ma_dvt)    REFERENCES DonViTinh(ma),
);
GO

drop table SanPham

drop table TonKho
CREATE TABLE TonKho (
    ma_sanpham int,
    ma_kho int,
    so_luong INT DEFAULT 0,
    PRIMARY KEY(ma_sanpham, ma_kho),
    FOREIGN KEY(ma_sanpham) REFERENCES SanPham(ma_sp),
    FOREIGN KEY(ma_kho) REFERENCES Kho(ma)
);

drop table SanPham,ThuongHieu, PhanLoai, MauSac, ChatLieu, DonViTinh, TrangThai

--===== CREATE VIEW =====

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
go

drop view v_SanPham_Chitiet
select * from v_SanPham_Chitiet
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

--===== CREATE FUNC =====
create function fn_LayKho()
returns table
as
	return (select * from Kho)
go



select * from dbo.fn_LayKho();

create function fn_TimNhaCC (@key nvarchar(50))
returns table
as
return (select * from NhaCungCap where ten_ncc like '%'+@key+'%');
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
