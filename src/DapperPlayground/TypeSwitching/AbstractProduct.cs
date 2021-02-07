namespace DapperPlayground.TypeSwitching
{
    public abstract class AbstractProduct
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public ProductCategory Category { get; set; }
    }
}