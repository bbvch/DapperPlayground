namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleMsSqlQueryTest : IDisposable
    {
        private readonly SimpleMsSqlQuery testee;
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;
        private readonly Stopwatch stopwatch;

        public SimpleMsSqlQueryTest(ITestOutputHelper outputHelper)
        {
            this.stopwatch = new Stopwatch();
            this.stopwatch.Start();

            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new SimpleMsSqlQuery(this.connection);
        }

        [Fact]
        public void QueriesOrders()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var orders = this.testee.GetOrders("a");

            stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + stopwatch.Elapsed);

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

            this.stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + stopwatch.Elapsed);

            products.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void ExecutesReadRowsDynamic()
        {
            this.testee.ReadProducts();
        }

        [Fact]
        public async Task ExecutesReadRowsDynamicAsync()
        {
            await this.testee.ReadProductsAsync()
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}