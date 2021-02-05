namespace NHibernatePlayground.Model
{
    using System;

    public class OrderEntity
    {
        public virtual int Id { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        public virtual int EmployeeId { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual DateTime RequiredDate { get; set; }

        public virtual DateTime ShippedDate { get; set; }

        public virtual int ShipVia { get; set; }

        public virtual decimal Freight { get; set; }

        public virtual string ShipName { get; set; }

        public virtual string ShipAddress { get; set; }

        public virtual string ShipCity { get; set; }

        public virtual string ShipRegion { get; set; }

        public virtual string ShipPostalCode { get; set; }

        public virtual string ShipCountry { get; set; }
    }
}