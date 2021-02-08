namespace DapperPlayground.FluentMapping.Manual
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class ProductQuery
    {
        private readonly IDbConnection connection;

        public ProductQuery(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.connection.Query<Product>("SELECT TOP(10) * FROM dbo.Products;");
        }
    }
}