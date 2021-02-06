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
            const string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=true;";

            var mappingAssembly = typeof(OrdersMapping).Assembly;

            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(mappingAssembly))
                .BuildConfiguration();
        }
    }
}