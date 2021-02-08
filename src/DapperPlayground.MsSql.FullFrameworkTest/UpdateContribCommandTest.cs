namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using Contrib;

    using Dapper.Contrib.Extensions;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class UpdateContribCommandTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private readonly UpdateContribCommand testee;

        public UpdateContribCommandTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.transaction = this.connection.BeginTransaction();
            this.testee = new UpdateContribCommand(this.connection, this.transaction);
        }

        [Fact]
        public void UpdatesCategoryByContrib()
        {
            var category = new CategoryContrib
            {
                CategoryID = 5,
                CategoryName = "Corn",
                Description = "Medium Corn"
            };

            var successful = this.testee.UpdateCategory(category);

            successful.Should().BeTrue();
        }

        [Fact]
        public void GetsCategoryByContrib()
        {
            var category = this.connection.Get<CategoryContrib>(5, this.transaction);

            this.outputHelper.WriteLine(category.CategoryName);

            category.Should().NotBeNull();
        }

        public void Dispose()
        {
            this.transaction.Rollback();
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}