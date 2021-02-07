namespace DapperPlayground.MsSql
{
    using System;
    using System.Data;

    using DapperPlayground.TypeSwitching;

    using Xunit;
    using Xunit.Abstractions;

    public class TypeSwitchTest : IDisposable
    {
        private readonly IDbConnection connection;
        private readonly ITestOutputHelper outputHelper;
        private readonly TypeAwareQuery testee;

        public TypeSwitchTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            this.connection = SqlServerConnectionFactory.OpenNew();
            this.testee = new TypeAwareQuery(this.connection);
        }

        [Fact]
        public void GetTypedResult()
        {
            var products= this.testee.GetProductsByType();

            foreach (var product in products)
            {
                this.outputHelper.WriteLine($"{product.Id} -> {product.GetType()} | {product.Category}");
            }
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}