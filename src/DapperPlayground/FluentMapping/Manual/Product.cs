namespace DapperPlayground.FluentMapping.Manual
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProductCategory Category { get; set; }

        public int SupplierId { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}