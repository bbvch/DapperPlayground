namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class StoredProcedures
    {
        private IDbConnection connection;

        public StoredProcedures(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<dynamic> ExecuteSalesByCategory(string categoryName, int year)
        {
            return this.connection.Query(
                "dbo.SalesByCategory",
                new { CategoryName = categoryName, OrdYear = year },
                commandType: CommandType.StoredProcedure);
        }
    }
}