namespace DapperPlayground
{
    using System.Data;

    using Dapper;

    public class DeleteCommand
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public DeleteCommand(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public int DeleteCategory(int categoryId)
        {
            int rowsAffected = this.connection.Execute(
                @"DELETE FROM dbo.Categories
                  WHERE CategoryID = @id;",
                new { id = categoryId },
                this.transaction);

            return rowsAffected;
        }
    }
}