namespace EFPlayground
{
    internal static class DbContextFactory
    {
        internal static NorthwindContext Create()
        {
            // The context was created by using the database first approach as described on the following web page:
            // https://docs.microsoft.com/en-us/ef/ef6/modeling/designer/workflows/database-first
            return new NorthwindContext();
        }
    }
}