namespace EFPlayground
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class SimpleQuery
    {
        private readonly NorthwindContext context;

        public SimpleQuery(NorthwindContext context)
        {
            this.context = context;
        }

        /// <param name="destinationName">
        /// Name of the shipment destination.
        /// </param>
        /// <remarks>
        /// This will result in the following SQL.
        /// <code language="sql">
        /// exec sp_executesql N'SELECT 
        ///     [Extent1].[OrderID] AS[OrderID], 
        ///     [Extent1].[CustomerID] AS[CustomerID], 
        ///     [Extent1].[EmployeeID] AS[EmployeeID], 
        ///     [Extent1].[OrderDate] AS[OrderDate], 
        ///     [Extent1].[RequiredDate] AS[RequiredDate], 
        ///     [Extent1].[ShippedDate] AS[ShippedDate], 
        ///     [Extent1].[ShipVia] AS[ShipVia], 
        ///     [Extent1].[Freight] AS[Freight], 
        ///     [Extent1].[ShipName] AS[ShipName], 
        ///     [Extent1].[ShipAddress] AS[ShipAddress], 
        ///     [Extent1].[ShipCity] AS[ShipCity], 
        ///     [Extent1].[ShipRegion] AS[ShipRegion], 
        ///     [Extent1].[ShipPostalCode] AS[ShipPostalCode], 
        ///     [Extent1].[ShipCountry] AS[ShipCountry]
        /// FROM
        ///     [dbo].[Orders] AS[Extent1]
        /// WHERE
        ///     ([Extent1].[ShipCity] = @p__linq__0)
        ///     OR (([Extent1].[ShipCity] IS NULL) AND (@p__linq__0 IS NULL))',
        /// N'@p__linq__0 nvarchar(4000)',@p__linq__0=N'Buenos Aires'
        /// </code>
        /// Because we return the entity, the caller can make further requests based on the properties involved.
        /// Let's say, the caller of this method wants to query properties of the customerEntity, then additional
        /// queries will be sent to the database. Such as:
        /// <code language="sql">
        /// exec sp_executesql N'SELECT 
        ///     [Extent1].[CustomerID] AS[CustomerID], 
        ///     [Extent1].[CompanyName] AS[CompanyName], 
        ///     [Extent1].[ContactName] AS[ContactName], 
        ///     [Extent1].[ContactTitle] AS[ContactTitle], 
        ///     [Extent1].[Address] AS[Address], 
        ///     [Extent1].[City] AS[City], 
        ///     [Extent1].[Region] AS[Region], 
        ///     [Extent1].[PostalCode] AS[PostalCode], 
        ///     [Extent1].[Country] AS[Country], 
        ///     [Extent1].[Phone] AS[Phone], 
        ///     [Extent1].[Fax] AS[Fax]
        /// FROM
        ///     [dbo].[Customers] AS[Extent1]
        /// WHERE
        ///     [Extent1].[CustomerID] = @EntityKeyValue1',
        /// N'@EntityKeyValue1 nchar(5)',@EntityKeyValue1=N'CACTU'
        /// </code>
        /// </remarks>
        public IReadOnlyCollection<Orders> GetOrderTo(string destinationName)
        {
            var query = 
                from o in this.context.Orders
                where o.ShipCity == destinationName
                select o;

            return query.ToList();
        }

        public Customers GetCustomerWithName(string customerName)
        {
            return this.context
                .Customers
                .FirstOrDefault(c => c.ContactName == customerName);
        }

        /// <param name="customerNameContains">
        /// specifies the characters the name of a customer should contain.
        /// </param>
        /// <remarks>
        /// This will result in the following SQL.
        /// <code language="sql">
        /// exec sp_executesql N'SELECT TOP (20) 
        ///     [Project1].[OrderID] AS[OrderID], 
        ///     [Project1].[ContactName] AS[ContactName], 
        ///     [Project1].[OrderDate] AS[OrderDate], 
        ///     [Project1].[ShippedDate] AS[ShippedDate], 
        ///     [Project1].[ShipAddress] AS[ShipAddress], 
        ///     [Project1].[ShipPostalCode] AS[ShipPostalCode], 
        ///     [Project1].[ShipCity] AS[ShipCity], 
        ///     [Project1].[ShipCountry] AS[ShipCountry]
        /// FROM
        ///     (SELECT
        ///         [Extent1].[OrderID] AS[OrderID],
        ///         [Extent1].[OrderDate] AS[OrderDate],
        ///         [Extent1].[ShippedDate] AS[ShippedDate],
        ///         [Extent1].[ShipAddress] AS[ShipAddress],
        ///         [Extent1].[ShipCity] AS[ShipCity],
        ///         [Extent1].[ShipPostalCode] AS[ShipPostalCode],
        ///         [Extent1].[ShipCountry] AS[ShipCountry],
        ///         [Extent2].[ContactName] AS [ContactName]
        ///      FROM
        ///         [dbo].[Orders] AS [Extent1]
        ///      LEFT OUTER JOIN
        ///         [dbo].[Customers] AS[Extent2]
        ///         ON [Extent1].[CustomerID] = [Extent2].[CustomerID]
        ///      WHERE
        ///         [Extent2].[ContactName] LIKE @p__linq__0 ESCAPE N''~''
        ///     ) AS[Project1]
        /// ORDER BY
        ///     [Project1].[ContactName] DESC',
        /// N'@p__linq__0 nvarchar(4000)',@p__linq__0=N'%a%'
        /// </code>
        /// </remarks>
        public IReadOnlyCollection<OrderItem> GetOrders(string customerNameContains)
        {
            var query =
                from o in this.context.Orders
                orderby o.Customers.ContactName descending
                where o.Customers.ContactName.Contains(customerNameContains)
                select new OrderItem
                {
                    Id = o.OrderID,
                    CustomerName = o.Customers.ContactName,
                    OrderDate = o.OrderDate,
                    ShippedDate = o.ShippedDate,
                    Address = o.ShipAddress,
                    PostCode = o.ShipPostalCode,
                    City = o.ShipCity,
                    Country = o.ShipCountry
                };

            return query
                .Take(20)
                .ToArray();
        }
    }
}
