namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    public class SimpleSqliteQuery
    {
        private const string Sql = @"
            SELECT
                o.OrderID               AS [Id],
                c.ContactName           AS [CustomerName],
                o.OrderDate             AS [OrderDate],
                o.ShippedDate           AS [ShippedDate],
                o.ShipAddress           AS [Address],
                o.ShipPostalCode        AS [PostCode],
                o.ShipCity              AS [City],
                o.ShipCountry           AS [Country]
            FROM
                [Orders] AS o
            INNER JOIN [Customers] AS C
                ON c.[CustomerID] = o.[CustomerID]
            ORDER BY
                c.ContactName
            LIMIT 20
            ";

        private readonly IDbConnection connection;

        public SimpleSqliteQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IReadOnlyCollection<OrderItem> GetOrders()
        {
            return this.connection.Query<OrderItem>(Sql).ToList();
        }
    }
}