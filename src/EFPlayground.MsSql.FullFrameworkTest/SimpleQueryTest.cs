namespace EFPlayground
{
    using System;
    using System.Diagnostics;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleQueryTest : IDisposable
    {
        private readonly SimpleQuery testee;
        private readonly ITestOutputHelper outputHelper;
        private readonly NorthwindContext dbContext;
        private readonly Stopwatch stopwatch;

        public SimpleQueryTest(ITestOutputHelper outputHelper)
        {
            this.stopwatch = new Stopwatch();
            this.stopwatch.Start();

            this.outputHelper = outputHelper;
            this.dbContext = DbContextFactory.Create();
            this.testee = new SimpleQuery(this.dbContext);
        }

        [Fact]
        public void QueriesOrderEntities()
        {
            var orders = this.testee.GetOrderTo("Buenos Aires");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.OrderID}: {order.Customers.ContactName}  ({order.OrderDate})");
            }

            orders.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void QueriesCustomerEntities()
        {
            var customer = this.testee.GetCustomerWithName("Ana Trujillo");

            foreach (var order in customer.Orders)
            {
                this.outputHelper.WriteLine($"{order.OrderID}: {order.Customers.ContactName}  ({order.OrderDate})");
            }

            customer.Orders.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void QueriesOrdersUsingQueryOver()
        {
            var orders = this.testee.GetOrders("a");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}  ({order.OrderDate})");
            }

            this.stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + stopwatch.Elapsed);

            orders.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesProductsByName()
        {
            var products = this.testee.GetProductsByName("Ch");

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.ProductID}: {product.ProductName}");
            }

            this.stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + this.stopwatch.Elapsed);

            products.Should().HaveCountGreaterThan(2);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
