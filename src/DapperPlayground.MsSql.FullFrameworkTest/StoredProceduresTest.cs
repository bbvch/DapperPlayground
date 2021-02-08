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
        public void ExecutesStoredProceduresWithQuery()
        {
            var result = this.testee.ExecuteSalesByCategoryWithQuery("Beverages", 1998).ToArray();

            foreach (var row in result)
            {
                this.outputHelper.WriteLine($"{row.ProductName}: {row.TotalPurchase}");
            }

            result.Should().HaveCountGreaterThan(2);
        }

        [Fact]
        public void ExecutesStoredProceduresWithExecute()
        {
            var result = this.testee.ExecuteSalesByCategoryWithExecute("Beverages", 1998);

            result.Should().Be(0);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}