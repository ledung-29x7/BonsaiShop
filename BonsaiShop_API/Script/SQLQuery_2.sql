USE [GardenManagement]
GO
IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE SPECIFIC_SCHEMA = N'dbo'
      AND SPECIFIC_NAME = N'InsertOrder'
      AND ROUTINE_TYPE = N'PROCEDURE'
)
    DROP PROCEDURE dbo.[InsertOrder];

GO
CREATE PROCEDURE dbo.[InsertOrder]
    @CustomerId INT,
    @TotalAmount DECIMAL(18, 2),
    @OrderDate DATETIME,
    @Status NVARCHAR(50),
    @PaymentType NVARCHAR(50)
AS
BEGIN
    -- Insert data into the Orders table
    INSERT INTO Orders (CustomerId, PlantId, TotalAmount, OrderDate, Status, PaymentType)
    VALUES ( @CustomerId, @PlantId, @TotalAmount, @OrderDate, @Status, @PaymentType);
END
