namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class UpdateCommand
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public UpdateCommand(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public int UpdateCategory(CategoryItem category)
        {
            int rowsAffected = this.connection.Execute(
                @"UPDATE dbo.Categories SET
                     CategoryName = @name,
                     Description = @description
                  WHERE CategoryID = @id;",
                category,
                this.transaction);

            return rowsAffected;
        }

        public int UpdateCategories(IEnumerable<CategoryItem> categories)
        {
            int rowsAffected = this.connection.Execute(
                @"UPDATE dbo.Categories SET
                     CategoryName = @name,
                     Description = @description
                  WHERE CategoryID = @id;",
                categories,
                this.transaction);

            if (rowsAffected == 0)
            {
                throw new DBConcurrencyException("Row changed by someone else.");
            }

            return rowsAffected;
        }
    }
}