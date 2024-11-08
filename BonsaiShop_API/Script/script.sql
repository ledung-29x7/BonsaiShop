CREATE DATABASE GardenManagement;
GO

USE GardenManagement;
GO

-- Bảng Users (Người Dùng)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Role NVARCHAR(20) NOT NULL, -- 'Admin', 'GardenOwner', 'Customer'
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Categories (Danh Mục Cây Cảnh)
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);

-- Bảng Gardens (Nhà Vườn)
CREATE TABLE Gardens (
    GardenId INT IDENTITY(1,1) PRIMARY KEY,
    GardenOwnerId INT,
    GardenName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Description NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (GardenOwnerId) REFERENCES Users(UserId)
);

-- Bảng GardenImages (Ảnh Nhà Vườn)
CREATE TABLE GardenImages (
    GardenImageId INT IDENTITY(1,1) PRIMARY KEY,
    GardenId INT,
    ImageBase64 NVARCHAR(MAX), -- Ảnh lưu dưới dạng Base64
    Caption NVARCHAR(255),
    IsPrimary BIT DEFAULT 0, -- 1 nếu là ảnh chính
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (GardenId) REFERENCES Gardens(GardenId)
);

-- Bảng Plants (Cây Cảnh)
CREATE TABLE Plants (
    PlantId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT,
    GardenId INT,
    PlantName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    IsAvailable BIT DEFAULT 1,
    Description NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (GardenId) REFERENCES Gardens(GardenId)
);

-- Bảng PlantImages (Ảnh Cây Cảnh)
CREATE TABLE PlantImages (
    PlantImageId INT IDENTITY(1,1) PRIMARY KEY,
    PlantId INT,
    ImageBase64 NVARCHAR(MAX), -- Ảnh lưu dưới dạng Base64
    Caption NVARCHAR(255),
    IsPrimary BIT DEFAULT 0, -- 1 nếu là ảnh chính
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PlantId) REFERENCES Plants(PlantId)
);

-- Bảng Orders (Đơn Hàng)
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20), -- 'Pending', 'Completed', 'Cancelled'
    PaymentType NVARCHAR(20), -- 'Cash', 'BankTransfer'
    FOREIGN KEY (CustomerId) REFERENCES Users(UserId)
);

-- Bảng OrderDetails (Chi Tiết Đơn Hàng)
CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    PlantId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    DepositAmount DECIMAL(18, 2) NOT NULL, -- Đặt cọc (20% hoặc 100% tùy trường hợp)
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (PlantId) REFERENCES Plants(PlantId)
);

-- Bảng RevenueReports (Báo Cáo Doanh Thu)
CREATE TABLE RevenueReports (
    ReportId INT IDENTITY(1,1) PRIMARY KEY,
    GardenId INT,
    TotalRevenue DECIMAL(18, 2) NOT NULL,
    ReportDate DATETIME DEFAULT GETDATE(), -- Ngày thống kê
    ReportType NVARCHAR(20), -- 'Daily', 'Monthly', 'Yearly'
    FOREIGN KEY (GardenId) REFERENCES Gardens(GardenId)
);

-- Bảng RevenueDetails (Chi Tiết Doanh Thu)
CREATE TABLE RevenueDetails (
    RevenueDetailId INT IDENTITY(1,1) PRIMARY KEY,
    ReportId INT,
    OrderId INT,
    Revenue DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ReportId) REFERENCES RevenueReports(ReportId),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
);

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
    @UserName NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users (UserName, PasswordHash, Email, Role, CreatedAt)
    VALUES (@UserName, @PasswordHash, @Email,'USER', GETDATE())
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword]
    @UserId INT,
    @NewPasswordHash NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET PasswordHash = @NewPasswordHash
    WHERE UserId = @UserId;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserById]
    @UserId int
AS
BEGIN
    SELECT * FROM Users WHERE UserId = @UserId
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchPlants]
    @PlantName NVARCHAR(100) = NULL,
    @CategoryName NVARCHAR(100) = NULL,  -- Change to CategoryName
    @MinPrice DECIMAL(18, 2) = NULL,
    @MaxPrice DECIMAL(18, 2) = NULL,
    @IsAvailable BIT = 1,  -- Default to available plants only
    @Offset INT = 0,  -- For pagination: starting row
    @FetchRows INT = 10  -- For pagination: number of rows to return
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PlantId, 
        p.PlantName, 
        p.Price, 
        p.IsAvailable, 
        p.Description, 
        p.CreatedAt, 
        p.GardenId, 
        c.CategoryName,  -- Select CategoryName instead of CategoryId
        i.ImageBase64
    FROM 
        Plants p
    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId  -- Join with Categories table
    LEFT JOIN (
        SELECT 
            PlantId, 
            ImageBase64,
            ROW_NUMBER() OVER (PARTITION BY PlantId ORDER BY UploadedAt DESC) AS RowNum
        FROM PlantImages
        WHERE IsPrimary = 1
    ) i ON p.PlantId = i.PlantId AND i.RowNum = 1  -- Ensure only one primary image per plant
    WHERE 
        (@PlantName IS NULL OR p.PlantName LIKE '%' + @PlantName + '%')
        AND (@CategoryName IS NULL OR c.CategoryName LIKE '%' + @CategoryName + '%')  -- Filter by CategoryName
        AND (@MinPrice IS NULL OR p.Price >= @MinPrice)
        AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
        AND p.IsAvailable = @IsAvailable
    ORDER BY 
        p.PlantId
    OFFSET @Offset ROWS FETCH NEXT @FetchRows ROWS ONLY;  -- Pagination
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetUserByEmail]
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Users WHERE Email = @Email
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
    @UserId INT,
    @UserName NVARCHAR(50),
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET UserName = @UserName,
        Email = @Email
    WHERE UserId = @UserId;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('User not found', 16, 1);
    END
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUserRole]
    @UserId INT,
    @Role NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET 
        Role = @Role
    WHERE UserId = @UserId;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('User not found', 16, 1);
    END
END
GO

