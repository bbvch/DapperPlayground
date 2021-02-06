namespace DapperPlayground.MySql
{
    using System;
    using System.Data;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class SimpleMySqlQueryTest : IDisposable
    {
        private readonly SimpleSqliteQuery testee;
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;

        public SimpleMySqlQueryTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = MySqlConnectionFactory.OpenNew();
            this.testee = new SimpleSqliteQuery(this.connection);
        }

        [Fact(Skip = "MySQL not installed on this machine")]
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
