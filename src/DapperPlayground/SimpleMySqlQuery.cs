namespace DapperPlayground
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    public class SimpleMySqlQuery
    {
        private const string Sql = @"
            SELECT 
                o.id                                    AS `Id`,
                concat(c.first_name, ' ', c.last_name)  AS `CustomerName`,
                o.order_date                            AS `OrderDate`,
                o.shipped_date                          AS `ShippedDate`,
                o.ship_address                          AS `Address`,
                o.ship_zip_postal_code                  AS `PostCode`,
                o.ship_city                             AS `City`,
                o.ship_country_region                   AS `Country`
            FROM
                orders AS o
            INNER JOIN customers AS c
                ON o.customer_id = c.id
            LIMIT 20;
            ";

        private readonly IDbConnection connection;

        public SimpleMySqlQuery(IDbConnection openConnection)
        {
            this.connection = openConnection;
        }

        public IReadOnlyCollection<OrderItem> GetOrders()
        {
            return this.connection.Query<OrderItem>(Sql).ToList();
        }
    }
}