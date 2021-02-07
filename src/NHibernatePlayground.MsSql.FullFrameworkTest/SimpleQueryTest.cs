namespace NHibernatePlayground
{
    using System;
    using System.Diagnostics;

    using FluentAssertions;

    using NHibernate;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleQueryTest : IDisposable
    {
        private readonly SimpleQuery testee;
        private readonly ITestOutputHelper outputHelper;
        private readonly ISession session;
        private readonly Stopwatch stopwatch;

        public SimpleQueryTest(ITestOutputHelper outputHelper)
        {
            this.stopwatch = new Stopwatch();
            this.stopwatch.Start();

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
                this.outputHelper.WriteLine($"{order.Id}: {order.Customer.GetFullName()}  ({order.OrderDate})");
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
        public void QueriesProductsByName()
        {
            var products = this.testee.GetProductsByName("Ch");

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id}: {product.Name}");
            }

            this.stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + this.stopwatch.Elapsed);

            products.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesOrdersUsingQueryOver()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var orders = this.testee.GetOrdersUsingQueryOver("a");

            stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + stopwatch.Elapsed);

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}  ({order.OrderDate})");
            }

            orders.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesOrdersUsingHql()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var orders = this.testee.GetOrdersUsingHql("a");

            stopwatch.Stop();
            this.outputHelper.WriteLine("Time required: " + stopwatch.Elapsed);

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