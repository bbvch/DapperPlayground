namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using DapperPlayground.TypeMapping;

    using Xunit;
    using Xunit.Abstractions;

    public class TypeHandlerTest : IDisposable
    {
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;
        private readonly TypeMappingQuery testee;

        public TypeHandlerTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new TypeMappingQuery(this.connection);
        }

        [Fact]
        public void GetTypedResult()
        {
            var employees = this.testee.GetEmployees();

            foreach (var employee in employees)
            {
                this.outputHelper.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.PhotoPath}");
            }
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}