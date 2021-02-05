namespace DapperPlayground.Sqlite
{
    using System.Data;

    using Microsoft.Data.Sqlite;

    public static class SqliteConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            return new SqliteConnection("Data Source=Northwind_small.sqlite;Mode=ReadOnly");
        }
    }
}