namespace EFPlayground
{
    using System;

    public class OrderItem
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}