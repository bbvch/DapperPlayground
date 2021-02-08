namespace DapperPlayground.Contrib
{
    using Dapper.Contrib.Extensions;

    [Table("Categories")]
    public class CategoryContrib
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}