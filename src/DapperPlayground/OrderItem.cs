namespace DapperPlayground
{
    using System;

    public class OrderItem
    {
        public int Id { get; }

        public string CustomerName { get; }

        public DateTime OrderDate { get; }

        public DateTime ShippedDate { get; }

        public string Address { get; }

        public string PostCode { get; }

        public string City { get; }

        public string Country { get; }
    }
}