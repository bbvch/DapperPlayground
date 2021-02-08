namespace DapperPlayground.FluentMapping.MultiMapping
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class FluentMultiMapQuery
    {
        private readonly IDbConnection connection;

        public FluentMultiMapQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IEnumerable<ProductFM> GetProductsWithCategories()
        {
            const string sql = @"
            SELECT TOP(20)
                p.*,
                c.*
            FROM dbo.Products AS p
                INNER JOIN dbo.Categories AS c
                ON p.CategoryID = c.CategoryID";

            return this.connection.Query<ProductFM, CategoryFM, ProductFM>(
                sql,
                (product, category) =>
                {
                    product.Category = category;
                    return product;
                },
                splitOn: "CategoryID"); // "Id" is the default
        }
    }
}