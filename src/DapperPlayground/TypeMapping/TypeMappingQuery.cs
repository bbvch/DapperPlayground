namespace DapperPlayground.TypeMapping
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class TypeMappingQuery
    {
        private readonly IDbConnection connection;

        public TypeMappingQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
            SqlMapper.AddTypeHandler(new UriHandler());
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return this.connection.Query<Employee>("SELECT * FROM dbo.Employees;");
        }
    }
}