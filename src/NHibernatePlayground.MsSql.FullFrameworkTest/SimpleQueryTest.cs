namespace DapperPlayground.MsSql
{
    using System;

    using FluentAssertions;

    using NHibernate;

    using NHibernatePlayground;

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
            this.session = SqlServerSessionFactory.CreateSessionFactory().OpenSession();
            this.testee = new SimpleQuery(this.session);
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

        public void Dispose()
        {
            this.session.Close();
            this.session.Dispose();
        }
    }
}
