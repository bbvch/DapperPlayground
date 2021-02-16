namespace DapperPlayground.MultiMapping
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    public class MultiMapQuery
    {
        private readonly IDbConnection connection;

        public MultiMapQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IEnumerable<ProductM> GetProductsWithCategories()
        {
            const string sql = @"
            SELECT TOP(20)
                p.ProductID AS Id,
                p.ProductName AS [Name],
                c.CategoryID As Id,
                c.CategoryName AS [Name],
                c.[Description] AS [Description]
            FROM dbo.Products AS p
                INNER JOIN dbo.Categories AS c
                ON p.CategoryID = c.CategoryID";

            return this.connection.Query<ProductM, CategoryM, ProductM>(
                sql,
                (product, category) =>
                {
                    product.Category = category;
                    return product;
                },
                splitOn: "Id"); // "Id" is the default
        }

        public IEnumerable<OrderM> GetOrdersWithDetails()
        {
            const string sql = @"
            SELECT TOP(20)
                o.OrderID AS Id,
                o.OrderDate AS Date,
                od.OrderID AS Id,
                od.UnitPrice AS UnitPrice
            FROM dbo.Orders AS o
                INNER JOIN dbo.[Order Details] AS od
                ON o.OrderID = od.OrderID";

            var orderDict = new Dictionary<int, OrderM>();
            var queryResult = this.connection.Query<OrderM, OrderDetailM, OrderM>(
                    sql,
                    (order, orderDetail) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out OrderM orderEntry))
                        {
                            orderEntry = order;
                            orderEntry.Details = new List<OrderDetailM>();
                            orderDict.Add(orderEntry.Id, orderEntry);
                        }

                        orderEntry.Details.Add(orderDetail);
                        return orderEntry;
                    })
                .ToArray();

            return queryResult.Distinct();
        }
    }
}