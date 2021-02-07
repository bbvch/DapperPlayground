namespace NHibernatePlayground
{
    using System;

    using FluentAssertions;

    using NHibernate;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleQueryTest : IDisposable
    {
        private readonly SimpleQuery testee;
        private readonly ITestOutputHelper outputHelper;
        private readonly ISession session;

        public SimpleQueryTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.session = SqlServerSessionFactory
                .CreateSessionFactory(MsSqlConfigurationFactory.Create())
                .OpenSession();

            this.testee = new SimpleQuery(this.session);
        }

        [Fact]
        public void QueriesOrderEntities()
        {
            var orders = this.testee.GetOrderTo("Buenos Aires");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.Customer.ContactName}  ({order.OrderDate})");
            }

            orders.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void QueriesCustomerEntities()
        {
            var customer = this.testee.GetCustomerWithName("Ana Trujillo");

            foreach (var order in customer.Orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.Customer.ContactName}  ({order.OrderDate})");
            }

            customer.Orders.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void QueriesOrdersUsingQueryOver()
        {
            var orders = this.testee.GetOrdersUsingQueryOver("a");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}  ({order.OrderDate})");
            }

            orders.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesOrdersUsingHql()
        {
            var orders = this.testee.GetOrdersUsingHql("a");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}  ({order.OrderDate})");
            }

            orders.Should().HaveCountGreaterThan(2);
        }

        public void Dispose()
        {
            this.session.Close();
            this.session.Dispose();
        }
    }
}
