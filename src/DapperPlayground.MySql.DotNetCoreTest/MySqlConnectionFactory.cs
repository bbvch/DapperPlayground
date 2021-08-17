namespace DapperPlayground.MySql
{
    using System.Data;

    using MySqlConnector;

    public static class MySqlConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            return new MySqlConnection(
                "server=localhost;port=1435;user=root;password=Change_Me;database=northwind");
        }
    }
}