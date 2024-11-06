IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE SPECIFIC_SCHEMA = N'dbo'
      AND SPECIFIC_NAME = N'CreateOrder'
      AND ROUTINE_TYPE = N'PROCEDURE'
)
    DROP PROCEDURE dbo.[CreateOrder];
GO

CREATE PROCEDURE dbo.[CreateOrder]
    @CustomerId INT,
    @Status NVARCHAR(20),
    @PaymentType NVARCHAR(20),
    @OrderDetails dbo.OrderDetailsType READONLY,
    @Message NVARCHAR(100) OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        DECLARE @OrderId INT;
        DECLARE @TotalAmount DECIMAL(18, 2) = 0;

        -- Calculate total amount based on order details
        SELECT @TotalAmount += (Quantity * UnitPrice) 
        FROM @OrderDetails;

        -- Insert into Orders table
        INSERT INTO Orders (CustomerId, TotalAmount, Status, PaymentType)
        VALUES (@CustomerId, @TotalAmount, @Status, @PaymentType);

        -- Retrieve new OrderId
        SET @OrderId = SCOPE_IDENTITY();

        -- Insert into OrderDetails table based on order details
        INSERT INTO OrderDetails (OrderId, PlantId, Quantity, UnitPrice, DepositAmount)
        SELECT @OrderId, PlantId, Quantity, UnitPrice, DepositAmount
        FROM @OrderDetails;

        -- Commit transaction
        COMMIT TRANSACTION;
        SET @Message = 'Order created successfully with OrderId = ' + CAST(@OrderId AS NVARCHAR);

    END TRY
BEGIN CATCH
    -- Rollback transaction on error
    ROLLBACK TRANSACTION;
    
    -- Capture error details
    SET @Message = 'Failed to create order. Error: ' + ERROR_MESSAGE() + ' at line ' + CAST(ERROR_LINE() AS NVARCHAR);
END CATCH

END;
