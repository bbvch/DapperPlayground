namespace NHibernatePlayground
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate.Cfg;

    using NHibernatePlayground.Model.Mapping;

    internal static class MsSqlConfigurationFactory
    {
        internal static Configuration Create()
        {
            const string connectionString = "Data Source=localhost,1434;Initial Catalog=Northwind;User Id=SA;Password=Change_Me;";

            var mappingAssembly = typeof(OrdersMapping).Assembly;

            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(mappingAssembly))
                .BuildConfiguration();
        }
    }
}