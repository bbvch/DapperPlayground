namespace NHibernatePlayground
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate.Cfg;

    using NHibernatePlayground.Model.Mapping;

    internal static class SqliteConfigurationFactory
    {
        internal static Configuration Create()
        {
            const string SqliteDbFile = "Northwind_small.sqlite";

            var mappingAssembly = typeof(OrdersMapping).Assembly;

            return Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(SqliteDbFile))
                .Mappings(m => m.FluentMappings.AddFromAssembly(mappingAssembly))
                .BuildConfiguration();
        }
    }
}