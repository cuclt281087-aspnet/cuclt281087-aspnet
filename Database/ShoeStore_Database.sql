-- =============================================
-- Shoe Store Database Creation Script - Vietnamese Table Names
-- Tuong thich voi moi phien ban SQL Server
-- =============================================

USE master;
GO

-- Tao database ShoeStore voi duong dan mac dinh
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ShoeStore')
BEGIN
    CREATE DATABASE ShoeStore;
END
GO

USE ShoeStore;
GO

-- =============================================
-- Tao cac bang chinh
-- =============================================

-- Bang DanhMuc (Danh muc san pham)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DanhMuc' AND xtype='U')
CREATE TABLE DanhMuc (
    CategoryID int IDENTITY(1,1) PRIMARY KEY,
    CategoryName nvarchar(100) NOT NULL,
    Description nvarchar(500),
    IsActive bit DEFAULT 1,
    CreatedDate datetime DEFAULT GETDATE()
);

-- Bang ThuongHieu (Thuong hieu)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ThuongHieu' AND xtype='U')
CREATE TABLE ThuongHieu (
    BrandID int IDENTITY(1,1) PRIMARY KEY,
    BrandName nvarchar(100) NOT NULL,
    Description nvarchar(500),
    Logo nvarchar(255),
    IsActive bit DEFAULT 1,
    CreatedDate datetime DEFAULT GETDATE()
);

-- Bang SanPham (San pham)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SanPham' AND xtype='U')
CREATE TABLE SanPham (
    ProductID int IDENTITY(1,1) PRIMARY KEY,
    ProductName nvarchar(200) NOT NULL,
    CategoryID int NOT NULL,
    BrandID int NOT NULL,
    Description nvarchar(1000),
    Price decimal(18,2) NOT NULL,
    SalePrice decimal(18,2),
    ImageUrl nvarchar(500),
    IsActive bit DEFAULT 1,
    IsFeatured bit DEFAULT 0,
    CreatedDate datetime DEFAULT GETDATE(),
    ModifiedDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (CategoryID) REFERENCES DanhMuc(CategoryID),
    FOREIGN KEY (BrandID) REFERENCES ThuongHieu(BrandID)
);

-- Bang KichCoSanPham (Kich co san pham)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='KichCoSanPham' AND xtype='U')
CREATE TABLE KichCoSanPham (
    SizeID int IDENTITY(1,1) PRIMARY KEY,
    ProductID int NOT NULL,
    Size nvarchar(10) NOT NULL,
    Quantity int DEFAULT 0,
    FOREIGN KEY (ProductID) REFERENCES SanPham(ProductID) ON DELETE CASCADE
);

-- Bang HinhAnhSanPham (Hinh anh san pham)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='HinhAnhSanPham' AND xtype='U')
CREATE TABLE HinhAnhSanPham (
    ImageID int IDENTITY(1,1) PRIMARY KEY,
    ProductID int NOT NULL,
    ImageUrl nvarchar(500) NOT NULL,
    IsMain bit DEFAULT 0,
    FOREIGN KEY (ProductID) REFERENCES SanPham(ProductID) ON DELETE CASCADE
);

-- Bang NguoiDung (Nguoi dung)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='NguoiDung' AND xtype='U')
CREATE TABLE NguoiDung (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    Username nvarchar(50) UNIQUE NOT NULL,
    Email nvarchar(100) UNIQUE NOT NULL,
    PasswordHash nvarchar(255) NOT NULL,
    FullName nvarchar(100),
    Phone nvarchar(20),
    Address nvarchar(500),
    Role nvarchar(20) DEFAULT 'Customer',
    IsActive bit DEFAULT 1,
    CreatedDate datetime DEFAULT GETDATE(),
    LastLogin datetime
);

-- Bang DonHang (Don hang)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DonHang' AND xtype='U')
CREATE TABLE DonHang (
    OrderID int IDENTITY(1,1) PRIMARY KEY,
    UserID int NOT NULL,
    OrderDate datetime DEFAULT GETDATE(),
    TotalAmount decimal(18,2) NOT NULL,
    Status nvarchar(50) DEFAULT 'Pending',
    ShippingAddress nvarchar(500),
    Phone nvarchar(20),
    Notes nvarchar(1000),
    FOREIGN KEY (UserID) REFERENCES NguoiDung(UserID)
);

-- Bang ChiTietDonHang (Chi tiet don hang)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ChiTietDonHang' AND xtype='U')
CREATE TABLE ChiTietDonHang (
    OrderDetailID int IDENTITY(1,1) PRIMARY KEY,
    OrderID int NOT NULL,
    ProductID int NOT NULL,
    Size nvarchar(10),
    Quantity int NOT NULL,
    UnitPrice decimal(18,2) NOT NULL,
    TotalPrice decimal(18,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES DonHang(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES SanPham(ProductID)
);

-- Bang GioHang (Gio hang)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GioHang' AND xtype='U')
CREATE TABLE GioHang (
    CartID int IDENTITY(1,1) PRIMARY KEY,
    UserID int NOT NULL,
    ProductID int NOT NULL,
    Size nvarchar(10),
    Quantity int NOT NULL,
    AddedDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES NguoiDung(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES SanPham(ProductID) ON DELETE CASCADE
);

-- Bang DanhGia (Danh gia san pham)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DanhGia' AND xtype='U')
CREATE TABLE DanhGia (
    ReviewID int IDENTITY(1,1) PRIMARY KEY,
    ProductID int NOT NULL,
    UserID int NOT NULL,
    Rating int CHECK (Rating >= 1 AND Rating <= 5),
    Comment nvarchar(1000),
    ReviewDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES SanPham(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES NguoiDung(UserID)
);

-- =============================================
-- Tao Index de toi uu hieu suat
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SanPham_CategoryID')
    CREATE INDEX IX_SanPham_CategoryID ON SanPham(CategoryID);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SanPham_BrandID')
    CREATE INDEX IX_SanPham_BrandID ON SanPham(BrandID);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SanPham_IsActive')
    CREATE INDEX IX_SanPham_IsActive ON SanPham(IsActive);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DonHang_UserID')
    CREATE INDEX IX_DonHang_UserID ON DonHang(UserID);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DonHang_OrderDate')
    CREATE INDEX IX_DonHang_OrderDate ON DonHang(OrderDate);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ChiTietDonHang_OrderID')
    CREATE INDEX IX_ChiTietDonHang_OrderID ON ChiTietDonHang(OrderID);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_GioHang_UserID')
    CREATE INDEX IX_GioHang_UserID ON GioHang(UserID);

-- =============================================
-- Them du lieu mau
-- =============================================

-- Them DanhMuc
IF NOT EXISTS (SELECT * FROM DanhMuc)
BEGIN
    INSERT INTO DanhMuc (CategoryName, Description) VALUES
    (N'Giay the thao', N'Giay danh cho hoat dong the thao'),
    (N'Giay cong so', N'Giay lich su cho moi truong cong so'),
    (N'Giay casual', N'Giay thuong ngay thoai mai'),
    (N'Giay boot', N'Giay cao co thoi trang'),
    (N'Dep sandal', N'Dep thoang mat mua he');
END

-- Them ThuongHieu
IF NOT EXISTS (SELECT * FROM ThuongHieu)
BEGIN
    INSERT INTO ThuongHieu (BrandName, Description) VALUES
    (N'Nike', N'Thuong hieu the thao hang dau the gioi'),
    (N'Adidas', N'Thuong hieu the thao noi tieng'),
    (N'Converse', N'Giay the thao phong cach vintage'),
    (N'Vans', N'Giay skateboard va lifestyle'),
    (N'Biti''s', N'Thuong hieu giay Viet Nam');
END

-- Them SanPham mau
IF NOT EXISTS (SELECT * FROM SanPham)
BEGIN
    INSERT INTO SanPham (ProductName, CategoryID, BrandID, Description, Price, SalePrice, ImageUrl) VALUES
    (N'Nike Air Max 270', 1, 1, N'Giay the thao Nike Air Max 270 voi dem khi toi da', 2500000, 2200000, '/images/nike-air-max-270.jpg'),
    (N'Adidas Ultraboost 22', 1, 2, N'Giay chay bo voi cong nghe Boost', 3200000, NULL, '/images/adidas-ultraboost.jpg'),
    (N'Converse Chuck Taylor', 3, 3, N'Giay canvas co dien All Star', 1200000, 1000000, '/images/converse-chuck.jpg'),
    (N'Vans Old Skool', 3, 4, N'Giay skateboard kinh dien', 1800000, NULL, '/images/vans-oldskool.jpg'),
    (N'Biti''s Hunter Core', 1, 5, N'Giay the thao Viet Nam chat luong cao', 800000, 650000, '/images/bitis-hunter.jpg');
END

-- Them KichCoSanPham
IF NOT EXISTS (SELECT * FROM KichCoSanPham)
BEGIN
    INSERT INTO KichCoSanPham (ProductID, Size, Quantity) VALUES
    (1, '39', 10), (1, '40', 15), (1, '41', 12), (1, '42', 8), (1, '43', 5),
    (2, '39', 8), (2, '40', 12), (2, '41', 10), (2, '42', 6), (2, '43', 4),
    (3, '38', 15), (3, '39', 20), (3, '40', 18), (3, '41', 12), (3, '42', 8),
    (4, '39', 12), (4, '40', 16), (4, '41', 14), (4, '42', 10), (4, '43', 6),
    (5, '39', 20), (5, '40', 25), (5, '41', 22), (5, '42', 15), (5, '43', 10);
END

-- Them NguoiDung admin
IF NOT EXISTS (SELECT * FROM NguoiDung WHERE Username = 'admin')
BEGIN
    INSERT INTO NguoiDung (Username, Email, PasswordHash, FullName, Role) VALUES
    ('admin', 'admin@shoestore.com', 'hashed_password_here', N'Quan tri vien', 'Admin'),
    ('customer1', 'customer1@email.com', 'hashed_password_here', N'Nguyen Van A', 'Customer');
END

PRINT 'Database ShoeStore da duoc tao thanh cong!';
PRINT 'Su dung Windows Authentication de ket noi.';