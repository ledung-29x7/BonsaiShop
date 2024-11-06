IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE SPECIFIC_SCHEMA = N'dbo'
      AND SPECIFIC_NAME = N'GetOrders'
      AND ROUTINE_TYPE = N'PROCEDURE'
)
    DROP PROCEDURE dbo.[GetOrders];
GO
CREATE PROCEDURE dbo.GetOrders
    @OrderId INT = NULL  -- Optional parameter to get a specific order
AS
BEGIN
    -- Retrieve order information
    SELECT 
        o.OrderId,
        o.CustomerId,
        o.TotalAmount,
        o.OrderDate,
        o.Status,
        o.PaymentType
    FROM Orders o
    WHERE (@OrderId IS NULL OR o.OrderId = @OrderId);
END;
IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE SPECIFIC_SCHEMA = N'dbo'
      AND SPECIFIC_NAME = N'GetOrderDetails'
      AND ROUTINE_TYPE = N'PROCEDURE'
)
    DROP PROCEDURE dbo.[GetOrderDetails];
GO
CREATE PROCEDURE dbo.GetOrderDetails
    @OrderId INT  -- Parameter to specify the OrderId
AS
BEGIN
    -- Retrieve order details for the given OrderId
    SELECT 
        od.OrderId,
        od.PlantId,
        od.Quantity,
        od.UnitPrice,
        od.DepositAmount
    FROM OrderDetails od
    WHERE od.OrderId = @OrderId;
END;

