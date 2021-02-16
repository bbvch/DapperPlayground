namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class DeleteCommandTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private readonly DeleteCommand testee;

        public DeleteCommandTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.transaction = this.connection.BeginTransaction();
            this.testee = new DeleteCommand(this.connection, this.transaction);
        }

        [Fact]
        public void DeletesCategory()
        {
            var rowsAffected = this.testee.DeleteCategory(10);

            rowsAffected.Should().Be(0);
        }

        public void Dispose()
        {
            this.transaction.Rollback();
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}