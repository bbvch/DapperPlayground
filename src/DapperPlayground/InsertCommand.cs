namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class InsertCommand
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public InsertCommand(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public int InsertCategory(CategoryItem category)
        {
            int rowsAffected = this.connection.Execute(
                @"INSERT dbo.Categories (CategoryName, Description)
                 VALUES (@name, @description);",
                category,
                this.transaction);

            return rowsAffected;
        }

        public int InsertCategories(IEnumerable<CategoryItem> categories)
        {
            int rowsAffected = this.connection.Execute(
                @"INSERT dbo.Categories (CategoryName, Description)
                 VALUES (@name, @description);",
                categories,
                this.transaction);

            return rowsAffected;
        }

        public int InsertCategoryScalar(CategoryItem category)
        {
            int categoryId = this.connection.ExecuteScalar<int>(
                @"INSERT dbo.Categories (CategoryName, Description)
                 VALUES (@name, @description);
                 SELECT SCOPE_IDENTITY();",
                category,
                this.transaction);

            return categoryId;
        }
    }
}