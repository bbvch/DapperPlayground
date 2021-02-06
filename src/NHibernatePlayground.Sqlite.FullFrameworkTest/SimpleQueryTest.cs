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
                .CreateSessionFactory(SqliteConfigurationFactory.Create())
                .OpenSession();

            this.testee = new SimpleQuery(this.session);
        }

        [Fact]
        public void QueriesOrdersUsingQueryOver()
        {
            var orders = this.testee.GetOrdersUsingQueryOver("a");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}");
            }

            orders.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void QueriesOrdersUsingHql()
        {
            var orders = this.testee.GetOrdersUsingHql("a");

            foreach (var order in orders)
            {
                this.outputHelper.WriteLine($"{order.Id}: {order.CustomerName}");
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
