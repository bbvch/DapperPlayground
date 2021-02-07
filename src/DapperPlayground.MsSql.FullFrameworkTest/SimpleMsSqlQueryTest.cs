namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;
    using System.Linq;

    using Dapper;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleMsSqlQueryTest : IDisposable
    {
        private readonly SimpleMsSqlQuery testee;
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;

        public SimpleMsSqlQueryTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new SimpleMsSqlQuery(this.connection);
        }

        [Fact]
        public void QueriesOrders()
        {
            var orders = this.testee.GetOrders();

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}");
            }

            orders.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesProducts()
        {
            var products = this.testee.GetProductsOf(ProductCategory.Beverages);

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name}");
            }

            products.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesProductsByName()
        {
            var products = this.testee.GetProductsByName("Ch");

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name}");
            }

            products.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void ExecuteReadRowsDynamic()
        {
            this.testee.ReadProducts();
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}
