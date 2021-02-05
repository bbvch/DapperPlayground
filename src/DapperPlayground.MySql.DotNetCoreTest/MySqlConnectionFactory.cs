namespace DapperPlayground.MySql
{
    using System.Data;

    using MySqlConnector;

    public static class MySqlConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            return new MySqlConnection(
                "server=localhost;user=dapperPlayground;password=dapperPlayground;database=northwind");
        }
    }
}