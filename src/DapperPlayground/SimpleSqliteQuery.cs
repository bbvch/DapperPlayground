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
                o.Id                    AS [Id],
                c.ContactName           AS [CustomerName],
                o.OrderDate             AS [OrderDate],
                o.ShippedDate           AS [ShippedDate],
                o.ShipAddress           AS [Address],
                o.ShipPostalCode        AS [PostCode],
                o.ShipCity              AS [City],
                o.ShipCountry           AS [Country]
            FROM
                [Order] AS o
            INNER JOIN [Customer] AS C
                ON c.[Id] = o.[CustomerId]
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