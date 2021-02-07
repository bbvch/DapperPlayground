namespace DapperPlayground.TypeSwitching
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    public class TypeAwareQuery
    {
        private readonly IDbConnection connection;

        public TypeAwareQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IReadOnlyCollection<AbstractProduct> GetProductsByType()
        {
            const string sql = "SELECT TOP(20) p.ProductID AS Id, p.CategoryID AS Category, p.* FROM dbo.Products AS p;";

            var result = new List<AbstractProduct>();
            using (var reader = this.connection.ExecuteReader(sql))
            {
                var beverageParser = reader.GetRowParser<Beverage>();
                var condimentParser = reader.GetRowParser<Condiment>();
                var generalParser = reader.GetRowParser<GeneralItem>();

                while (reader.Read()) {
                    AbstractProduct product;
                    switch ((ProductCategory)reader.GetInt32(reader.GetOrdinal("CategoryID")))
                    {
                        case ProductCategory.Beverages:
                            product = beverageParser(reader);
                            break;
                        case ProductCategory.Condiments:
                            product = condimentParser(reader);
                            break;
                        default:
                            product = generalParser(reader);
                            break;
                    }
                    result.Add(product);
                }
            }
            return result;
        }
    }
}