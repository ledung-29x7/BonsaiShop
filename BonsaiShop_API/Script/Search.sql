USE [GardenManagement]
GO
IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE SPECIFIC_SCHEMA = N'dbo'
      AND SPECIFIC_NAME = N'SearchPlants'
      AND ROUTINE_TYPE = N'PROCEDURE'
)
    DROP PROCEDURE dbo.[SearchPlants];

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
