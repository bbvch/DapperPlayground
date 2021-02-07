namespace DapperPlayground
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    public class SimpleMsSqlQuery
    {
        private readonly IDbConnection connection;

        public SimpleMsSqlQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IReadOnlyCollection<OrderItem> GetOrders()
        {
            const string sql = @"
            SELECT TOP (20)
                o.OrderID                   AS [Id],
                c.ContactName               AS [CustomerName],
                o.OrderDate                 AS [OrderDate],
                o.ShippedDate               AS [ShippedDate],
                o.ShipAddress               AS [Address],
                o.ShipPostalCode            AS [PostCode],
                o.ShipCity                  AS [City],
                o.ShipCountry               AS [Country]
            FROM
                [dbo].[Orders] AS o
            INNER JOIN [dbo].[Customers] AS C
                ON c.[CustomerID] = o.[CustomerID]
            ORDER BY
                c.ContactName;";

            return this.connection.Query<OrderItem>(sql).ToList();
        }

        public IReadOnlyCollection<OrderItem> GetOrders(string customerNameContains)
        {
            const string sql = @"
            SELECT TOP (20)
                o.OrderID                   AS [Id],
                c.ContactName               AS [CustomerName],
                o.OrderDate                 AS [OrderDate],
                o.ShippedDate               AS [ShippedDate],
                o.ShipAddress               AS [Address],
                o.ShipPostalCode            AS [PostCode],
                o.ShipCity                  AS [City],
                o.ShipCountry               AS [Country]
            FROM
                [dbo].[Orders] AS o
            INNER JOIN [dbo].[Customers] AS C
                ON c.[CustomerID] = o.[CustomerID]
            WHERE
                c.[ContactName] LIKE @namePart
            ORDER BY
                c.ContactName;";

            return this.connection.Query<OrderItem>(sql, new { namePart = $"%{customerNameContains}%" }).ToList();
        }

        public IReadOnlyCollection<ProductItem> GetProductsOf(ProductCategory category)
        {
            const string sql = @"
            SELECT TOP (20)
                p.ProductID     AS [Id],
                p.ProductName   AS [Name]
            FROM
                dbo.Products AS p
            WHERE
                p.CategoryID = @categoryId;";

            return this.connection.Query<ProductItem>(sql, new { categoryId = (int)category }).ToList();
        }

        public IReadOnlyCollection<ProductItem> GetProductsByName(string productName)
        {
            const string sql = @"
            SELECT TOP (20)
                p.ProductID     AS [Id],
                p.ProductName   AS [Name]
            FROM
                dbo.Products AS p
            WHERE
                p.ProductName LIKE @prodName;";

            var parameter = new DynamicParameters();
            parameter.Add(
                "@prodName",
                "%" + productName + "%", 
                DbType.AnsiString, 
                ParameterDirection.Input, 
                40);

            return this.connection.Query<ProductItem>(sql, parameter).ToList();
        }

        public void ReadProducts()
        {
            const string sql = @"
            SELECT TOP (20)
                p.ProductID     AS [Id],
                p.ProductName   AS [Name]
            FROM
                dbo.Products AS p";

            IEnumerable<dynamic> result = this.connection.Query(sql);

            foreach (dynamic row in result)
            {
                Console.WriteLine($"{row.Id}: {row.Name}");
            }
        }

        public async Task ReadProductsAsync()
        {
            const string sql = @"
            SELECT TOP (20)
                p.ProductID     AS [Id],
                p.ProductName   AS [Name]
            FROM
                dbo.Products AS p";

            var queryAsync = await this.connection.QueryAsync(sql)
                .ConfigureAwait(false);

            foreach (dynamic row in queryAsync)
            {
                Console.WriteLine($"{row.Id}: {row.Name}");
            }
        }
    }
}
