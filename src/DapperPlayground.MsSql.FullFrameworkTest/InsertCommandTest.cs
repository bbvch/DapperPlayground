namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class InsertCommandTest : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private readonly InsertCommand testee;

        public InsertCommandTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.transaction = this.connection.BeginTransaction();
            this.testee = new InsertCommand(this.connection, this.transaction);
        }

        [Fact]
        public void InsertsNewCategory()
        {
            var category = new CategoryItem
            {
                Name = "Bio",
                Description = "Organic farm products"
            };

            int result = this.testee.InsertCategory(category);

            result.Should().Be(1);
        }

        [Fact]
        public void InsertsNewCategories()
        {
            var categories = new[]
            {
                new CategoryItem
                {
                    Name = "Bio",
                    Description = "Organic farm products"
                },
                new CategoryItem
                {
                    Name = "Vegan",
                    Description = "Vegan farm products"
                }
            };

            int result = this.testee.InsertCategories(categories);

            result.Should().Be(2);
        }

        [Fact]
        public void InsertsNewScalar()
        {
            var category = new CategoryItem
            {
                Name = "Bio",
                Description = "Organic farm products"
            };

            int categoryId = this.testee.InsertCategoryScalar(category);

            Console.WriteLine($"Id of new category is: {categoryId}");

            this.outputHelper.WriteLine($"Id of new category is: {categoryId}");

            categoryId.Should().BePositive();
        }

        public void Dispose()
        {
            this.transaction.Rollback();
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}