namespace NHibernatePlayground
{
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;
    using NHibernate.Cfg;

    public class SqlServerSessionFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return GetMsSqlConfiguration()
                .BuildSessionFactory();
        }

        private static Configuration GetMsSqlConfiguration()
        {
            const string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=true;";

            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();
        }
    }
}