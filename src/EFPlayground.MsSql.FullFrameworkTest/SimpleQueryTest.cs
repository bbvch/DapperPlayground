namespace EFPlayground
{
    using System;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleQueryTest : IDisposable
    {
        private readonly SimpleQuery testee;
        private readonly ITestOutputHelper outputHelper;
        private readonly NorthwindContext dbContext;

        public SimpleQueryTest(ITestOutputHelper outputHelper)
        {
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

            orders.Should().HaveCountGreaterThan(2);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
