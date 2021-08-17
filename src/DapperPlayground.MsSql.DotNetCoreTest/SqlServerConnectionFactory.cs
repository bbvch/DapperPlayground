namespace DapperPlayground.MsSql
{
    using System.Data;
    using System.Data.SqlClient;

    public static class SqlServerConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            return new SqlConnection("Data Source=localhost,1434;Initial Catalog=Northwind;User Id=SA;Password=Change_Me;");
        }
    }
}