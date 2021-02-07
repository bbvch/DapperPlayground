namespace NHibernatePlayground.Model
{
    using System.Collections.Generic;

    public class CustomerEntity
    {
        public virtual string Id { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual string ContactTitle { get; set; }

        public virtual string ContactName { get; set; }

        public virtual string Address { get; set; }

        public virtual string City { get; set; }

        public virtual string Region { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string Country { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Fax { get; set; }

        public virtual IList<OrderEntity> Orders { get; set; }

        public virtual string GetFullName()
            => $"{this.ContactTitle} {this.ContactName}";
    }
}
