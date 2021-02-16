namespace DapperPlayground.MultiMapping
{
    using System;
    using System.Collections.Generic;

    public class OrderM
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public List<OrderDetailM> Details { get; set; }
    }
}