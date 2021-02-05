namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

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

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}
