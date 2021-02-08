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

        public IEnumerable<dynamic> ExecuteSalesByCategoryWithQuery(string categoryName, int year)
        {
            return this.connection.Query(
                "dbo.SalesByCategory",
                new { CategoryName = categoryName, OrdYear = year },
                commandType: CommandType.StoredProcedure);
        }

        public int ExecuteSalesByCategoryWithExecute(string categoryName, int year)
        {
            var p = new DynamicParameters();
            p.Add("@CategoryName", categoryName, DbType.String, ParameterDirection.Input, size: 15);
            p.Add("@OrdYear", year, DbType.String, ParameterDirection.Input, size: 4);
            p.Add("@return_value", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            this.connection.Execute(
                "dbo.SalesByCategory",
                p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@return_value");
        }
    }
}