namespace NHibernatePlayground
{
    using NHibernate;
    using NHibernate.Cfg;

    public static class SqlServerSessionFactory
    {
        public static ISessionFactory CreateSessionFactory(Configuration configuration)
        {
            return configuration.BuildSessionFactory();
        }
    }
}