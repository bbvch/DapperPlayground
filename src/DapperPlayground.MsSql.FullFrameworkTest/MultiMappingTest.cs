namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;
    using System.Linq;

    using FluentAssertions;

    using MultiMapping;

    using Xunit;
    using Xunit.Abstractions;

    public class MultiMappingTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly MultiMapQuery testee;

        public MultiMappingTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new MultiMapQuery(this.connection);
        }

        [Fact]
        public void GetsProductsWithCategory()
        {
            var products = this.testee.GetProductsWithCategories().ToArray();

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name} ({product.Category.Name})");
            }

            products.Should().HaveCountGreaterThan(2);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}