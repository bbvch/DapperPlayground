namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class UpdateCommandTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private readonly UpdateCommand testee;

        public UpdateCommandTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.transaction = this.connection.BeginTransaction();
            this.testee = new UpdateCommand(this.connection, this.transaction);
        }

        [Fact]
        public void UpdatesCategory()
        {
            var category = new CategoryItem()
            {
                Id = 5,
                Name = "Corn",
                Description = "Medium Corn"
            };

            var rowsAffected = this.testee.UpdateCategory(category);

            rowsAffected.Should().Be(1);
        }

        [Fact]
        public void UpdatesCategories()
        {
            var categories = new[]
            {
                new CategoryItem
                {
                    Id = 5,
                    Name = "Corn",
                    Description = "Medium Corn"
                },
                new CategoryItem
                {
                    Id = 6,
                    Name = "Meat",
                    Description = "Substance"
                }
            };

            var rowsAffected = this.testee.UpdateCategories(categories);

            rowsAffected.Should().Be(2);
        }

        public void Dispose()
        {
            this.transaction.Rollback();
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}