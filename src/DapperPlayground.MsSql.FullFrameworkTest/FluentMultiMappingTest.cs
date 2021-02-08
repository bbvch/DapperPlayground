namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using Dapper.FluentMap;

    using FluentMapping.MultiMapping;

    using Xunit;
    using Xunit.Abstractions;

    public class FluentMultiMappingTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly FluentMultiMapQuery testee;

        public FluentMultiMappingTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ProductFmMap());
                config.AddMap(new CategoryFmMap());
            });

            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new FluentMultiMapQuery(this.connection);
        }

        [Fact]
        public void GetsMultiMappedProducts()
        {
            var products = this.testee.GetProductsWithCategories();

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name} ({product.Category.Name})");
            }
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}