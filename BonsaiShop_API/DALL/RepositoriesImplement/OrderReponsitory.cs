using BonsaiShop_API;
using BonsaiShop_API.Areas.User.Models;
using BonsaiShop_API.DALL.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

public class OrderRepository : IOrderReponsitory
{
    private readonly BonsaiDbcontext _dbContext;

    public OrderRepository(BonsaiDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateOrderAsync(Order order)
    {
        using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            var message = new DynamicParameters();
            message.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

            var orderDetails = new DataTable();
            orderDetails.Columns.Add("PlantId", typeof(int));
            orderDetails.Columns.Add("Quantity", typeof(int));
            orderDetails.Columns.Add("UnitPrice", typeof(decimal));
            orderDetails.Columns.Add("DepositAmount", typeof(decimal));

            foreach (var detail in order.OrderDetails)
            {
                orderDetails.Rows.Add(detail.PlantId, detail.Quantity, detail.UnitPrice, detail.DepositAmount);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", order.CustomerId);
            parameters.Add("@Status", order.Status);
            parameters.Add("@PaymentType", order.PaymentType);
            parameters.Add("@OrderDetails", orderDetails.AsTableValuedParameter("dbo.OrderDetailsType"));
            parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

            await connection.ExecuteAsync("dbo.CreateOrder", parameters, transaction, commandType: CommandType.StoredProcedure);

            // Retrieve the output message
            var resultMessage = parameters.Get<string>("@Message");

            transaction.Commit();

            return resultMessage;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new Exception("Error creating order: " + ex.Message, ex);
        }

    }
    public async Task<List<Order>> GetOrdersAsync(int? orderId = null)
    {
        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId, DbType.Int32, ParameterDirection.Input);

            // Stored Procedure to get orders
            var query = @"
                EXEC dbo.GetOrders @OrderId = @OrderId;
            ";

            var orders = await connection.QueryAsync<Order>(query, parameters);
            return orders.ToList();
        }

    }
    public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
    {
        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId, DbType.Int32, ParameterDirection.Input);

            // Stored Procedure to get order details
            var query = @"
            EXEC dbo.GetOrderDetails @OrderId = @OrderId;
        ";

            var orderDetails = await connection.QueryAsync<OrderDetail>(query, parameters);
            return orderDetails.ToList();
        }
    }

}
