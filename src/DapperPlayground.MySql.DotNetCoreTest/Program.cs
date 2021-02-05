namespace DapperPlayground.MySql
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Dapper with MySQL example");
            Console.WriteLine("-------------------------");

            try
            {
                using var connection = MySqlConnectionFactory.OpenNew();
                var query = new SimpleMySqlQuery(connection);
                var orders = query.GetOrders();

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id}: {order.CustomerName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
