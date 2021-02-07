namespace DapperPlayground.MsSql
{
    using System.Data;
    using System.Data.OleDb;
    using System.Linq;

    using Dapper;

    using FluentAssertions;

    using Xunit;

    public class MappingTest
    {
        private IDbConnection connection;

        public MappingTest()
        {
            this.connection = SqlServerConnectionFactory.OpenNew();
        }

        [Fact]
        public void DapperTest()
        {
            const string queryCommand = "select Age = @age, Name = @name";
            var parameter = new { age = 47, name = "Troy" };

            var dynamicResult = this.connection.Query(queryCommand, parameter).ToArray()[0];
            var customerResult = this.connection.Query<MyCustomer>(queryCommand, parameter).ToArray()[0];

            var name = (string)dynamicResult.Name;
            var age = (int) dynamicResult.Age;

            name.Should().Be("Troy");
            age.Should().Be(47);
            customerResult.Name.Should().Be("Troy");
            customerResult.Age.Should().Be(47);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }

        public class MyCustomer
        {
            public int? Age { get; }

            public string Name { get; }
        }
    }
}