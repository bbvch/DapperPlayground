namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;
    using System.Linq;

    using Dapper;

    using MultiSelect;

    using Xunit;
    using Xunit.Abstractions;

    public class MultiSelectTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;

        public MultiSelectTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
        }

        [Fact]
        public void RunMultiSelectTest()
        {
            const string sql = @"
            SELECT p.ProductID AS [Id], p.ProductName AS [Name] FROM dbo.Products AS p;
            SELECT c.CategoryID AS [Id], c.CategoryName AS [Name] FROM dbo.Categories AS c;";

            using (var multi = this.connection.QueryMultiple(sql))
            {
                var products = multi.Read<ProductMS>().ToList();
                var categories = multi.Read<CategoryMS>().ToList();

                this.outputHelper.WriteLine("Number Of Products: " + products.Count);
                this.outputHelper.WriteLine("Number Of Categories: " + categories.Count);
            }
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}