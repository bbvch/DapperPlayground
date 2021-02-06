namespace NHibernatePlayground.Model.Mapping
{
    using FluentNHibernate.Mapping;

    public class OrdersMapping : ClassMap<OrderEntity>
    {
        public OrdersMapping()
        {
            // this.Schema("dbo");
            this.Table("Orders");
            this.Id(x => x.Id).Column("OrderID");

            this.Map(x => x.EmployeeId).Column("EmployeeID");
            this.Map(x => x.OrderDate).Column("OrderDate");
            this.Map(x => x.RequiredDate).Column("RequiredDate");
            this.Map(x => x.ShippedDate).Column("ShippedDate");
            this.Map(x => x.ShipVia).Column("ShipVia");
            this.Map(x => x.Freight).Column("Freight");

            this.Map(x => x.ShipName)
                .Column("ShipName")
                .Length(40);

            this.Map(x => x.ShipAddress)
                .Column("ShipAddress")
                .Length(60);

            this.Map(x => x.ShipCity)
                .Column("ShipCity")
                .Length(15);

            this.Map(x => x.ShipRegion)
                .Column("ShipRegion")
                .Length(15);

            this.Map(x => x.ShipPostalCode)
                .Column("ShipPostalCode")
                .Length(10);

            this.Map(x => x.ShipCountry)
                .Column("ShipCountry")
                .Length(15);

            this.References(x => x.Customer)
                .Column("CustomerID")
                .Cascade
                .None();
        }
    }
}