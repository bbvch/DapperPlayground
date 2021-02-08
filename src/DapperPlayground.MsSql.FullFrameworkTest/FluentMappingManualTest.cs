namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using Dapper.FluentMap;

    using FluentMapping.Manual;

    using Xunit;
    using Xunit.Abstractions;

    public class FluentMappingTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly ProductQuery testee;

        public FluentMappingTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;

            FluentMapper.Initialize(config => config.AddMap(new ProductMap()));

            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new ProductQuery(this.connection);
        }

        [Fact]
        public void GetsMappedProducts()
        {
            var products = this.testee.GetProducts();

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name} ({product.Category})");
            }
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}