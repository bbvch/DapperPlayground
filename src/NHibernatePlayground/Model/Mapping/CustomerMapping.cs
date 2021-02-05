namespace NHibernatePlayground.Model.Mapping
{
    using FluentNHibernate.Mapping;

    public class CustomerMapping : ClassMap<CustomerEntity>
    {
        public CustomerMapping()
        {
            this.Schema("dbo");
            this.Table("Customers");

            this.Id(x => x.Id)
                .Column("CustomerID")
                .Length(5)
                .Not.Nullable();

            this.Map(x => x.CompanyName)
                .Column("CompanyName")
                .Length(40)
                .Not.Nullable();

            this.Map(x => x.ContactName)
                .Column("ContactName")
                .Length(30);

            this.Map(x => x.ContactTitle)
                .Column("ContactTitle")
                .Length(30);

            this.Map(x => x.Address)
                .Column("Address")
                .Length(60);

            this.Map(x => x.City)
                .Column("City")
                .Length(15);

            this.Map(x => x.Region)
                .Column("Region")
                .Length(15);

            this.Map(x => x.PostalCode)
                .Column("PostalCode")
                .Length(10);

            this.Map(x => x.Country)
                .Column("Country")
                .Length(15);

            this.Map(x => x.Phone)
                .Column("Phone")
                .Length(24);

            this.Map(x => x.Fax)
                .Column("Fax")
                .Length(24);
        }
    }
}