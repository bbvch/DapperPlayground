namespace DapperPlayground.FluentMapping.Manual
{
    using Dapper.FluentMap.Mapping;

    public class ProductMap : EntityMap<Product>
    {
        public ProductMap()
        {
            this.Map(x => x.Id)
                .ToColumn("ProductID");

            this.Map(x => x.Name)
                .ToColumn("ProductName");

            this.Map(x => x.SupplierId)
                .ToColumn("SupplierID");

            this.Map(x => x.Category)
                .ToColumn("CategoryID");

            this.Map(x => x.QuantityPerUnit)
                .ToColumn("QuantityPerUnit");

            this.Map(x => x.UnitPrice)
                .ToColumn("UnitPrice");

            this.Map(x => x.UnitsInStock)
                .ToColumn("UnitsInStock");

            this.Map(x => x.UnitsOnOrder)
                .ToColumn("UnitsOnOrder");

            this.Map(x => x.ReorderLevel)
                .ToColumn("ReorderLevel");

            this.Map(x => x.Discontinued)
                .ToColumn("Discontinued");
        }
    }
}