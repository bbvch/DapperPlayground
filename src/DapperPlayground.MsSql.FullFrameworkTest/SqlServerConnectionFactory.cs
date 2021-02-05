namespace DapperPlayground
{
    using System.Data;
    using System.Data.SqlClient;

    public static class SqlServerConnectionFactory
    {
        public static IDbConnection OpenNew()
        {
            return new SqlConnection("Data Source=localhost;Initial Catalog=Northwind;Integrated Security=true;");
        }
    }
}