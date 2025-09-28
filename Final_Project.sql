use NhapHang

EXECUTE AS USER = 'ql_01';
CREATE USER ql_01 FOR LOGIN ql_01;
SELECT * FROM SanPham;
SELECT name, type_desc FROM sys.database_principals WHERE name = 'ql_01';
SELECT name, type_desc FROM sys.server_principals WHERE name = 'nvkho_01';
CREATE ROLE	role_QuanLy;
GRANT CONTROL ON DATABASE::NhapHang TO role_QuanLy;

CREATE ROLE role_NvKho;
GRANT  EXEC ON dbo.prc_ThemDonHangThucTe TO role_NvKho;

SELECT 
    dp.name AS RoleName,
    dp.type_desc AS RoleType,
    o.name AS ObjectName,
    o.type_desc AS ObjectType,
    p.permission_name AS Permission,
    p.state_desc AS State
FROM sys.database_permissions p
JOIN sys.database_principals dp ON p.grantee_principal_id = dp.principal_id
LEFT JOIN sys.objects o ON p.major_id = o.object_id
WHERE dp.name = 'role_NvKho'
ORDER BY ObjectName, Permission;




--===== CREATE TABLE =====

CREATE TABLE NhanVien (
    ma_nv INT PRIMARY KEY IDENTITY,
    hoten NVARCHAR(100) NOT NULL,
    cccd VARCHAR(12) NOT NULL,
    gioitinh NVARCHAR(10),
    ngaysinh DATE NOT NULL,
    tuoi INT CHECK (tuoi > 0)
);
GO

CREATE TABLE [User] (
    ma_tk INT PRIMARY KEY IDENTITY,
    ma_nv INT,
    username NVARCHAR(100) UNIQUE,
    password NVARCHAR(50) NOT NULL,
    roleId INT NOT NULL,
    CONSTRAINT fk_User_NhanVien FOREIGN KEY (ma_nv) REFERENCES NhanVien(ma_nv)
);
GO

CREATE TABLE NhaCungCap (
    ma_ncc INT PRIMARY KEY IDENTITY,
    ten_ncc NVARCHAR(100) NOT NULL,
    diachi NVARCHAR(200) NOT NULL,
    lienhe NVARCHAR(50) NOT NULL,
    ghichu NVARCHAR(500)
);
GO

CREATE TABLE Kho (
    ma INT PRIMARY KEY IDENTITY,
    ten NVARCHAR(100) NOT NULL,
    diachi NVARCHAR(200),
    isDeleted BIT
);
GO


CREATE TABLE PhanLoai (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE MauSac (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE ChatLieu (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE DonViTinh (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE TrangThai (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE ThuongHieu (
    ma INT IDENTITY(1,1) PRIMARY KEY,
    ten NVARCHAR(100) NOT NULL
);
GO



CREATE TABLE SanPham (
    ma_sp INT IDENTITY(1,1) PRIMARY KEY,   -- ID tự tăng
    ten_sp NVARCHAR(100) NOT NULL,
    ma_th INT,
    ma_loai INT,
    ma_mau INT,
    ma_chatlieu INT,
    kichthuoc NVARCHAR(100),
    ma_dvt INT,                            -- trạng thái
    mota NVARCHAR(200),
    isDeleted BIT DEFAULT(0),
    
    CONSTRAINT fk_SP_ThuongHieu   FOREIGN KEY (ma_th)        REFERENCES ThuongHieu(ma),
    CONSTRAINT fk_SP_PhanLoai     FOREIGN KEY (ma_loai)      REFERENCES PhanLoai(ma),
    CONSTRAINT fk_SP_MauSac       FOREIGN KEY (ma_mau)       REFERENCES MauSac(ma),
    CONSTRAINT fk_SP_ChatLieu     FOREIGN KEY (ma_chatlieu)  REFERENCES ChatLieu(ma),
    CONSTRAINT fk_SP_DonViTinh    FOREIGN KEY (ma_dvt)       REFERENCES DonViTinh(ma)
);
GO

CREATE TABLE TonKho (
    ma_sp INT,
    ma_kho INT,
    soluong INT DEFAULT 0,
    ma_tt INT,
    
    PRIMARY KEY (ma_sp, ma_kho),
    
    FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp),
    FOREIGN KEY (ma_kho) REFERENCES Kho(ma),
    FOREIGN KEY (ma_tt) REFERENCES TrangThai(ma)
);
GO

CREATE TABLE LoaiDonHang (
    ma INT PRIMARY KEY IDENTITY,
    ten NVARCHAR(50) NOT NULL
);
GO

INSERT INTO LoaiDonHang (ten)
VALUES
    (N'Đơn hàng dự kiến'),
    (N'Đơn hàng thực tế');
GO

CREATE TABLE TrangThaiDH (
    ma INT PRIMARY KEY IDENTITY,
    ten NVARCHAR(255)
);
GO

CREATE TABLE DonHang (
    ma_don INT PRIMARY KEY IDENTITY,
    ngaylap DATETIME,
    ma_nv INT,
    ghichu NVARCHAR(200),
    ma_loai INT,
    tongtien DECIMAL(18,0),
    ma_ncc INT,
    ma_kho INT,
    ma_tt INT,       -- mã trạng thái
    khoa BIT DEFAULT 0,
    ma_dhdk INT,     -- mã đơn hàng dự kiến, đơn hàng thực tế sẽ tham chiếu đến
    
    CONSTRAINT fk_DonHang_NhanVien     FOREIGN KEY (ma_nv)   REFERENCES NhanVien(ma_nv),
    CONSTRAINT fk_DonHang_LoaiDonHang  FOREIGN KEY (ma_loai) REFERENCES LoaiDonHang(ma),
    CONSTRAINT fk_DonHang_NhaCungCap   FOREIGN KEY (ma_ncc)  REFERENCES NhaCungCap(ma_ncc),
    CONSTRAINT fk_DonHang_Kho          FOREIGN KEY (ma_kho)  REFERENCES Kho(ma),
    CONSTRAINT fk_DonHang_TrangThaiDH    FOREIGN KEY (ma_tt)   REFERENCES TrangThaiDH(ma),
    CONSTRAINT fk_DonHang_DonHang      FOREIGN KEY (ma_dhdk) REFERENCES DonHang(ma_don)
);
GO

CREATE TABLE DonHangChiTiet (
    ma_don INT NOT NULL,
    ma_sp INT NOT NULL,
    gia_dk DECIMAL(18,0) NOT NULL,    -- Giá dự kiến
    soluong INT NOT NULL,
    
    CONSTRAINT pk_DHCT PRIMARY KEY (ma_don, ma_sp),
    CONSTRAINT fk_DHCT_DonHang FOREIGN KEY (ma_don) REFERENCES DonHang(ma_don),
    CONSTRAINT fk_DHCT_SanPham FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp)
);
GO

CREATE TABLE LichSu (
    ma_ls INT PRIMARY KEY IDENTITY,
    thoigian DATETIME,
    ma_don INT,
    ghichu NVARCHAR(255),

    CONSTRAINT fk_LichSu_DonHang FOREIGN KEY (ma_don) REFERENCES DonHang(ma_don)
);
GO




----------------Transaction-----------------
-- Bang ao chua danh sach san pham
CREATE TYPE dbo.DanhSachSanPham AS TABLE (
    ma_sp INT NOT NULL PRIMARY KEY,
    gia_dk DECIMAL(18,0) NOT NULL,
    soluong INT NOT NULL
);
GO

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

CREATE PROCEDURE prc_HuyDonHangDuKien
    @ma_don INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ma_tt INT;

        SELECT @ma_tt = ma_tt
        FROM DonHang WITH (UPDLOCK, ROWLOCK)
        WHERE ma_don = @ma_don;

        -- Kiểm tra đơn hàng có tồn tại không
        IF @ma_tt IS NULL
        BEGIN
            RAISERROR (N'Đơn hàng không tồn tại.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        -- Nếu trạng thái hợp lệ (1 = chờ xử lý chẳng hạn)
        IF @ma_tt = 1
        BEGIN
            -- Cập nhật trạng thái đơn hàng sang hủy
            UPDATE DonHang
            SET ma_tt = 2    -- 2 = Hủy đơn hàng
            WHERE ma_don = @ma_don;

            -- Ghi lịch sử
            INSERT INTO LichSu (thoigian, ma_don, ghichu)
            VALUES (GETDATE(), @ma_don, N'Hủy đơn hàng');
        END
        ELSE
        BEGIN
            RAISERROR (N'Trạng thái không hợp lệ.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE prc_ThemDonHangThucTe
    @makho INT,
    @ma_nv INT,
    @ma_ncc INT,
    @ma_dhxuly INT,
    @ChiTietPhieuNhap AS dbo.DanhSachSanPham READONLY
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @trangthai INT;

        -- Lấy trạng thái đơn hàng dự kiến
        SELECT @trangthai = ma_tt
        FROM DonHang WITH (UPDLOCK, ROWLOCK)
        WHERE ma_don = @ma_dhxuly;

        -- Kiểm tra đơn hàng có tồn tại không
        IF @trangthai IS NULL
        BEGIN
            RAISERROR (N'Đơn hàng xử lý không tồn tại', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        -- Nếu trạng thái = 1 (đơn hàng đang chờ xử lý)
        IF @trangthai = 1
        BEGIN
            -- Tính tổng tiền từ danh sách sản phẩm
            DECLARE @tongtien DECIMAL(18,2);
            SELECT @tongtien = SUM(gia_dk * soluong) 
            FROM @ChiTietPhieuNhap;

            -- Tạo đơn hàng thực tế
            INSERT INTO DonHang (ngaylap, ma_nv, ma_ncc, ma_kho, tongtien, ma_loai, ma_tt, ma_dhdk)
            VALUES (GETDATE(), @ma_nv, @ma_ncc, @makho, @tongtien, 2, 3, @ma_dhxuly); 
            -- ma_loai = 2: Đơn hàng thực tế
            -- ma_tt   = 3: Đã xử lý

            -- Lấy mã đơn hàng mới tạo
            DECLARE @madon INT = SCOPE_IDENTITY();

            -- Thêm chi tiết đơn hàng
            INSERT INTO DonHangChiTiet (ma_don, ma_sp, gia_dk, soluong)
            SELECT @madon, ma_sp, gia_dk, soluong
            FROM @ChiTietPhieuNhap;

            -- Cập nhật trạng thái đơn hàng dự kiến
            UPDATE DonHang
            SET ma_tt = 3   -- 3 = Đã xử lý
            WHERE ma_don = @ma_dhxuly;

            -- Cập nhật tồn kho (tăng số lượng)
            INSERT INTO TonKho (ma_kho, ma_sp, soluong)
            SELECT @makho, ma_sp, soluong
            FROM @ChiTietPhieuNhap;

            -- Ghi lịch sử
            INSERT INTO LichSu (thoigian, ma_don, ghichu)
            VALUES (GETDATE(), @madon, N'Xử lý đơn hàng ' + CAST(@ma_dhxuly AS NVARCHAR(50)));
        END
        ELSE
        BEGIN
            RAISERROR (N'Trạng thái đơn hàng không hợp lệ', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--========================================
--=             CREATE VIEW              =
--========================================

-- View chi tiết sản phẩm
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
    LEFT JOIN ThuongHieu th ON sp.ma_th = th.ma
    LEFT JOIN PhanLoai pl   ON sp.ma_loai = pl.ma
    LEFT JOIN MauSac ms     ON sp.ma_mau = ms.ma
    LEFT JOIN ChatLieu cl   ON sp.ma_chatlieu = cl.ma
    LEFT JOIN DonViTinh dvt ON sp.ma_dvt = dvt.ma
WHERE sp.isDeleted != 1;
GO

-- View nhà cung cấp
CREATE VIEW v_NhaCungCap
AS
SELECT * 
FROM NhaCungCap
WHERE isDeleted != 1;
GO


-- View kho
CREATE VIEW v_Kho
AS
SELECT * 
FROM Kho
WHERE isDeleted != 1;
GO


-- View chi tiết tồn kho
CREATE VIEW v_TonKho_Chitiet
AS
SELECT
    k.ten      AS tenkho,
    sp.ma_sp,
    sp.ten_sp,
    th.ten     AS thuonghieu,
    dvt.ten    AS dvt,
    tt.ten     AS trangthai,
    tk.soluong
FROM TonKho tk
    LEFT JOIN SanPham sp     ON sp.ma_sp = tk.ma_sp
    LEFT JOIN Kho k          ON k.ma = tk.ma_kho
    LEFT JOIN ThuongHieu th  ON th.ma = sp.ma_th
    LEFT JOIN DonViTinh dvt  ON dvt.ma = sp.ma_dvt
    LEFT JOIN TrangThai tt   ON tt.ma = tk.ma_tt
WHERE sp.isDeleted != 1 
  AND k.isDeleted != 1;
GO

SELECT * 
FROM v_SanPham_Chitiet;


--========================================
--=         VIEW: Đơn hàng dự kiến        =
--========================================
CREATE VIEW v_DonHangDuKien 
AS
SELECT 
    dh.ma_don,
    dh.ngaylap,
    nv.ma_nv,
    dh.tongtien,
    ncc.ten_ncc,
    k.ten AS kho,
    tt.ten AS trangthai,
    dh.ghichu
FROM DonHang dh
    LEFT JOIN NhanVien nv     ON nv.ma_nv = dh.ma_nv
    LEFT JOIN LoaiDonHang ldh ON ldh.ma = dh.ma_loai
    LEFT JOIN NhaCungCap ncc  ON ncc.ma_ncc = dh.ma_ncc
    LEFT JOIN Kho k           ON k.ma = dh.ma_kho
    LEFT JOIN TrangThaiDH tt  ON tt.ma = dh.ma_tt
WHERE ldh.ma = 1 
  AND dh.ma_tt = 1;
GO

--========================================
--=        VIEW: Đơn hàng cần xử lý       =
--========================================
CREATE VIEW v_DonHangCanXuLy
AS
SELECT 
    dh.ma_don,
    dh.ngaylap,
    nv.ma_nv,
    dh.tongtien,
    ncc.ten_ncc,
    k.ten AS kho,
    tt.ten AS trangthai,
    dh.ghichu
FROM DonHang dh
    LEFT JOIN NhanVien nv     ON nv.ma_nv = dh.ma_nv
    LEFT JOIN LoaiDonHang ldh ON ldh.ma = dh.ma_loai
    LEFT JOIN NhaCungCap ncc  ON ncc.ma_ncc = dh.ma_ncc
    LEFT JOIN Kho k           ON k.ma = dh.ma_kho
    LEFT JOIN TrangThaiDH tt  ON tt.ma = dh.ma_tt
WHERE dh.ma_tt = 1;
GO
--========================================
--=             VIEW: Lịch sử            =
--========================================
CREATE VIEW v_LichSu
AS
SELECT 
    thoigian,
    ma_don,
    ghichu
FROM LichSu;
GO

--===== CREATE PROC =====
CREATE PROC prc_LayDuLieuThuocTinh
    @nametable NVARCHAR(50)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    SET @sql = N'SELECT * FROM ' + QUOTENAME(@nametable);
    EXEC sp_executesql @sql;
END
GO

EXEC prc_LayDuLieuThuocTinh N'ThuongHieu';
GO


--=========================================================
--=                 Nha Cung Cấp                         =
--=========================================================
CREATE PROC prc_InsertNhaCC 
    @ten_ncc NVARCHAR(100),
    @diachi NVARCHAR(200),
    @lienhe NVARCHAR(50),
    @ghichu NVARCHAR(500)
AS
BEGIN
    INSERT INTO NhaCungCap (ten_ncc, diachi, lienhe, ghichu)
    VALUES (@ten_ncc, @diachi, @lienhe, @ghichu);
END
GO

CREATE PROC prc_UpdateNhaCC 
    @ma_ncc INT,
    @ten_ncc NVARCHAR(100),
    @diachi NVARCHAR(200),
    @lienhe NVARCHAR(50),
    @ghichu NVARCHAR(500)
AS
BEGIN
    UPDATE NhaCungCap
    SET ten_ncc = @ten_ncc,
        diachi  = @diachi,
        lienhe  = @lienhe,
        ghichu  = @ghichu
    WHERE ma_ncc = @ma_ncc;
END
GO

CREATE PROC prc_XoaNhaCC
    @ma_ncc INT
AS
BEGIN
    DELETE FROM NhaCungCap
    WHERE ma_ncc = @ma_ncc;
END
GO
--=========================================================
--=                         Kho                           =
--=========================================================
CREATE PROC prc_InsertKho
    @ten NVARCHAR(100),
    @diachi NVARCHAR(200)
AS
BEGIN
    INSERT INTO Kho (ten, diachi)
    VALUES (@ten, @diachi);
END
GO

CREATE PROC prc_UpdateKho
    @ma INT,
    @ten NVARCHAR(100),
    @diachi NVARCHAR(200)
AS
BEGIN
    UPDATE Kho
    SET ten    = @ten,
        diachi = @diachi
    WHERE ma = @ma;
END
GO

CREATE PROC prc_XoaKho
    @ma INT
AS
BEGIN
    DELETE FROM Kho
    WHERE ma = @ma;
END
GO

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

CREATE PROC prc_InsertSanPham 
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
    INSERT INTO SanPham (ten_sp, ma_th, ma_loai, ma_mau, ma_chatlieu, kichthuoc, ma_dvt, mota)
    VALUES (@ten_sp, @ma_th, @ma_loai, @ma_mau, @ma_chatlieu, @kichthuoc, @ma_dvt, @mota);
END
GO

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

-- Xóa sản phẩm theo mã
CREATE PROC prc_XoaSanPham
    @ma_sp INT
AS
BEGIN
    DELETE FROM SanPham
    WHERE ma_sp = @ma_sp;
END
GO

-- Xóa sản phẩm tồn kho theo mã kho và mã sản phẩm
CREATE PROC prc_XoaSPTonKho
    @ma_kho INT,
    @ma_sp INT
AS
BEGIN
    DELETE FROM TonKho
    WHERE ma_kho = @ma_kho 
      AND ma_sp = @ma_sp;
END
GO

-- Procedure Thêm nhân viên
GO
CREATE PROCEDURE prc_ThemNhanVien
    @HoTen NVARCHAR(100),
    @CCCD VARCHAR(12),
    @GioiTinh NVARCHAR(10),
    @NgaySinh DATE,
    @Tuoi INT,
    @MaNV INT OUTPUT
AS
BEGIN
    -- Kiểm tra CCCD đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM NhanVien WHERE cccd = @CCCD)
    BEGIN
        RAISERROR (N'CCCD đã tồn tại, không thể thêm nhân viên mới.', 16, 1);
        RETURN;
    END

    INSERT INTO NhanVien(hoten, cccd, gioitinh, ngaysinh, tuoi)
    VALUES (@HoTen, @CCCD, @GioiTinh, @NgaySinh, @Tuoi);

    SET @MaNV = SCOPE_IDENTITY();
END
GO

-- Tạo User trong bảng User, Login sql,
GO
CREATE PROCEDURE prc_ThemTaiKhoan
    @MaNV INT,
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(50),
    @RoleId INT
AS
BEGIN
    -- Kiểm tra username đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM [User] WHERE username = @TenDangNhap)
    BEGIN
        RAISERROR (N'Tên đăng nhập đã tồn tại trong bảng [User].', 16, 1);
        RETURN;
    END

    -- Thêm tài khoản vào bảng [User]
    INSERT INTO [User](ma_nv, username, password, roleId)
    VALUES (@MaNV, @TenDangNhap, @MatKhau, @RoleId);

    -- Tạo login trong SQL Server nếu chưa có
    IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @TenDangNhap)
    BEGIN
        DECLARE @sql NVARCHAR(MAX);
        SET @sql = N'CREATE LOGIN [' + @TenDangNhap + N'] WITH PASSWORD = ''' + @MatKhau + N''', CHECK_POLICY = OFF';
        EXEC sp_executesql @sql;
    END

    -- Tạo user trong database nếu chưa có
    IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @TenDangNhap)
    BEGIN
        DECLARE @sql2 NVARCHAR(MAX);
        SET @sql2 = N'CREATE USER [' + @TenDangNhap + N'] FOR LOGIN [' + @TenDangNhap + N']';
        EXEC sp_executesql @sql2;
    END
END
GO

GO
CREATE PROCEDURE prc_GanRoleChoUser
    @TenDangNhap NVARCHAR(100),
    @TenRole NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @TenRole AND type = 'R')
    BEGIN
        EXEC sp_addrolemember @rolename = @TenRole, @membername = @TenDangNhap;
    END
    ELSE
    BEGIN
        RAISERROR (N'Role %s chưa tồn tại.', 16, 1, @TenRole);
    END
END
GO

drop PROCEDURE prc_ThemNhanVienVaTaiKhoan
GO
CREATE PROCEDURE prc_ThemNhanVienVaTaiKhoan_NvKho
    @HoTen NVARCHAR(100),
    @CCCD VARCHAR(12),
    @GioiTinh NVARCHAR(10),
    @NgaySinh DATE,
    @Tuoi INT,
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(50),
    @RoleId INT = 2
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaNV INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Thêm nhân viên
        EXEC prc_ThemNhanVien 
            @HoTen = @HoTen,
            @CCCD = @CCCD,
            @GioiTinh = @GioiTinh,
            @NgaySinh = @NgaySinh,
            @Tuoi = @Tuoi,
            @MaNV = @MaNV OUTPUT;

        PRINT N'Đã thêm nhân viên mới, mã: ' + CAST(@MaNV AS NVARCHAR(10));

        -- Thêm tài khoản
        EXEC prc_ThemTaiKhoan 
            @MaNV = @MaNV,
            @TenDangNhap = @TenDangNhap,
            @MatKhau = @MatKhau,
            @RoleId = @RoleId;

        PRINT N'Đã tạo tài khoản: ' + @TenDangNhap;

        -- Gán role
        EXEC prc_GanRoleChoUser 
            @TenDangNhap = @TenDangNhap, 
            @TenRole = 'role_NvKho';

        PRINT N'Đã gán quyền role_NvKho cho tài khoản';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

GO
CREATE PROCEDURE prc_ThemNhanVienVaTaiKhoan_QuanLy
    @HoTen NVARCHAR(100),
    @CCCD VARCHAR(12),
    @GioiTinh NVARCHAR(10),
    @NgaySinh DATE,
    @Tuoi INT,
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(50),
    @RoleId INT = 1  -- roleId dành cho Quản lý
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MaNV INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Thêm nhân viên
        EXEC prc_ThemNhanVien 
            @HoTen = @HoTen,
            @CCCD = @CCCD,
            @GioiTinh = @GioiTinh,
            @NgaySinh = @NgaySinh,
            @Tuoi = @Tuoi,
            @MaNV = @MaNV OUTPUT;

        PRINT N'Đã thêm nhân viên mới, mã: ' + CAST(@MaNV AS NVARCHAR(10));

        -- Thêm tài khoản
        EXEC prc_ThemTaiKhoan 
            @MaNV = @MaNV,
            @TenDangNhap = @TenDangNhap,
            @MatKhau = @MatKhau,
            @RoleId = @RoleId;

        PRINT N'Đã tạo tài khoản: ' + @TenDangNhap;

        -- Gán quyền role_QuanLy
        EXEC prc_GanRoleChoUser 
            @TenDangNhap = @TenDangNhap, 
            @TenRole = 'role_QuanLy';

        PRINT N'Đã gán quyền role_QuanLy cho tài khoản.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

EXEC prc_ThemNhanVienVaTaiKhoan_NvKho
    @HoTen = N'Nguyễn Văn A',
    @CCCD = '111222333444',
    @GioiTinh = N'Nam',
    @NgaySinh = '1990-05-10',
    @Tuoi = 35,
    @TenDangNhap = N'nvkho_01',
    @MatKhau = N'12345',
    @RoleId = 2; -- mã role tương ứng role_NvKho

EXEC prc_ThemNhanVienVaTaiKhoan_QuanLy
    @HoTen = N'Nguyễn Thị B',
    @CCCD = '111222333555',
    @GioiTinh = N'Nữ',
    @NgaySinh = '1991-05-10',
    @Tuoi = 34,
    @TenDangNhap = N'ql_01',
    @MatKhau = N'12345',
    @RoleId = 1; -- mã role tương ứng role_NvKho


--===== CREATE FUNC =====
--===== TẠO HÀM: LẤY DANH SÁCH KHO =====
CREATE FUNCTION fn_LayKho()
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM Kho
    );
GO

CREATE FUNCTION fn_TimNhaCC (
    @key NVARCHAR(50)
)
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM v_NhaCungCap
        WHERE ten_ncc LIKE '%' + @key + '%'
    );
GO


--===== TẠO HÀM: TÌM SẢN PHẨM THEO TÊN =====
CREATE FUNCTION fn_TimSanPham (
    @key NVARCHAR(50)
)
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM v_SanPham_Chitiet
        WHERE ten_sp LIKE '%' + @key + '%'
    );
GO


--===== TẠO HÀM: TÌM SẢN PHẨM THEO MÃ =====
CREATE FUNCTION fn_TimSanPhamByID (
    @key INT
)
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM v_SanPham_Chitiet
        WHERE ma_sp = @key
    );
GO

--===== HÀM: LẤY SẢN PHẨM THEO KHO =====
CREATE FUNCTION fn_LaySPTheoKho (
    @tenkho NVARCHAR(50)
)
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM v_TonKho_Chitiet 
        WHERE tenkho = @tenkho
    );
GO

--===== HÀM: TÌM SẢN PHẨM TRONG TỒN KHO =====
CREATE FUNCTION fn_TimSPTrongTonKho (
    @tensp NVARCHAR(50),
    @tenkho NVARCHAR(50)
)
RETURNS TABLE
AS
    RETURN (
        SELECT * 
        FROM v_TonKho_Chitiet 
        WHERE ten_sp LIKE '%' + @tensp + '%' 
          AND tenkho = @tenkho
    );
GO

DROP FUNCTION fn_TimSPTrongTonKho;
GO

--===== HÀM: CHI TIẾT ĐƠN HÀNG =====
CREATE FUNCTION fn_ChitietDonHang (
    @madon INT
)
RETURNS TABLE
AS
    RETURN (
        SELECT 
            dhct.ma_sp, 
            sp.ten_sp,
            dhct.gia_dk, 
            dvt.ten AS 'dvt', 
            dhct.soluong  
        FROM DonHangChiTiet dhct
        LEFT JOIN SanPham sp ON dhct.ma_sp = sp.ma_sp
        LEFT JOIN DonViTinh dvt ON sp.ma_dvt = dvt.ma
        WHERE ma_don = @madon
    );
GO

--===== HÀM: LẤY ĐƠN HÀNG THEO MÃ =====
CREATE FUNCTION fn_LayDonHangTheoMa (
    @ma_don INT
)
RETURNS TABLE
AS
    RETURN (
        SELECT 
            ma_don,
            ngaylap,
            nv.ma_nv,
            tongtien, 
            ncc.ten_ncc, 
            k.ten AS 'kho', 
            tt.ten AS 'trangthai',
            dh.ghichu 
        FROM DonHang dh
        LEFT JOIN NhanVien nv ON nv.ma_nv = dh.ma_nv
        LEFT JOIN NhaCungCap ncc ON ncc.ma_ncc = dh.ma_ncc
        LEFT JOIN Kho k ON k.ma = dh.ma_kho
        LEFT JOIN TrangThaiDH tt ON tt.ma = dh.ma_tt
        WHERE dh.ma_don = @ma_don
    );
GO

--===== HÀM: TIỀN NHẬP HÀNG CÁC THÁNG THEO NĂM =====
CREATE FUNCTION fn_TienNhapHangCacThangTheoNam (
    @nam INT
)
RETURNS TABLE
AS
    RETURN (
        SELECT 
            Thang,
            TongTienNhap 
        FROM v_TongTienNhapTheoThang
        WHERE Nam = @nam
    );
GO

CREATE FUNCTION fn_LayNamNhapHang()
RETURNS TABLE
AS
    RETURN (
        SELECT 
            YEAR(ngaylap) AS Nam
        FROM DonHang 
        WHERE ma_loai = 2
        GROUP BY YEAR(ngaylap)
    );
GO

------------Thống Kê -----------------
-- Số tiền nhập hang của từng tháng, từng năm

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

-- Số tiền nhập thực tế trong tháng
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

-- Số tiền nhập thực tế trong tuần này
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

-- Giá trị đơn hàng trong tháng này của mỗi kho
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

-- Số tiền nhập hàng thực tế của mỗi nhà cung cấp trong tháng này
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
GO

CREATE TRIGGER trg_TrangThaiTonKho_Insert
ON TonKho
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật trạng thái tồn kho sau khi có thay đổi
    UPDATE tk
    SET ma_tt = 
        CASE 
            WHEN tk.soluong = 0 THEN 1
            WHEN tk.soluong < 5 THEN 2
            WHEN tk.soluong < 20 THEN 3
            ELSE 4
        END
    FROM TonKho tk
    INNER JOIN inserted i 
        ON tk.ma_kho = i.ma_kho 
       AND tk.ma_sp = i.ma_sp;
END
GO

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

-- DonViTinh
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

-- ThuongHieu
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


