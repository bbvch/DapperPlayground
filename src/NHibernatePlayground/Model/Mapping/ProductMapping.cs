namespace NHibernatePlayground.Model.Mapping
{
    using FluentNHibernate.Mapping;

    public class ProductMapping : ClassMap<ProductEntity>
    {
        public ProductMapping()
        {
            // this.Schema("dbo");
            this.Table("Products");
            this.Id(x => x.Id).Column("ProductID");

            this.Map(x => x.Name).Column("ProductName");
            this.Map(x => x.SupplierId).Column("SupplierID");
            this.Map(x => x.CategoryId).Column("CategoryID");
            this.Map(x => x.QuantityPerUnit).Column("QuantityPerUnit");
            this.Map(x => x.UnitPrice).Column("UnitPrice");
            this.Map(x => x.UnitsInStock).Column("UnitsInStock");
            this.Map(x => x.UnitsOnOrder).Column("UnitsOnOrder");
            this.Map(x => x.ReorderLevel).Column("ReorderLevel");
            this.Map(x => x.Discontinued).Column("Discontinued");
        }
    }
}