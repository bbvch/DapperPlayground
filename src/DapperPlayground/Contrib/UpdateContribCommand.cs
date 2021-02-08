namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;

    using Contrib;

    using Dapper;
    using Dapper.Contrib.Extensions;

    public class UpdateContribCommand
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public UpdateContribCommand(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public bool UpdateCategory(CategoryContrib category)
        {
            return this.connection.Update(category, this.transaction);
        }
    }
}