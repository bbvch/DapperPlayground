namespace DapperPlayground.MultiMapping
{
    using System.Collections.Generic;
    using System.Data;

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
    }
}