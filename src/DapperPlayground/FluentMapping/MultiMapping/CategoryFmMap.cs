namespace DapperPlayground.FluentMapping.MultiMapping
{
    using Dapper.FluentMap.Mapping;

    public class CategoryFmMap : EntityMap<CategoryFM>
    {
        public CategoryFmMap()
        {
            this.Map(x => x.Id)
                .ToColumn("CategoryID");

            this.Map(x => x.Name)
                .ToColumn("CategoryName");

            this.Map(x => x.Description)
                .ToColumn("Description");
        }
    }
}