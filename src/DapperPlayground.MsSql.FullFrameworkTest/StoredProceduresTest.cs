namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;
    using System.Linq;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class StoredProceduresTest : IDisposable
    {
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;
        private readonly StoredProcedures testee;

        public StoredProceduresTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new StoredProcedures(this.connection);
        }

        [Fact]
        public void ExecutesStoredProcedures()
        {
            var result = this.testee.ExecuteSalesByCategory("Beverages", 1998).ToArray();

            foreach (var row in result)
            {
                this.outputHelper.WriteLine($"{row.ProductName}: {row.TotalPurchase}");
            }

            result.Should().HaveCountGreaterThan(2);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}