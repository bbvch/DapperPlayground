namespace DapperPlayground.MsSql
{
    using System.Data;
    using System.Data.SqlClient;

    public static class SqlServerConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            const string connectionString = "Data Source=localhost,1434;Initial Catalog=Northwind;User Id=SA;Password=Change_Me;";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}