namespace NHibernatePlayground
{
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    using NHibernatePlayground.Model;

    public class SimpleQuery
    {
        private readonly ISession session;

        public SimpleQuery(ISession session)
        {
            this.session = session;
        }

        public IReadOnlyCollection<OrderItem> GetOrders()
        {
            OrderItem orderItem = null;
            CustomerEntity customerAlias = null;

            var query = this.session
                .QueryOver<OrderEntity>()
                .JoinAlias(x => x.Customer, () => customerAlias)
                .SelectList(l => l
                    .Select(x => x.Id).WithAlias(() => orderItem.Id)
                    .Select(() => customerAlias.ContactName).WithAlias(() => orderItem.CustomerName)
                    .Select(x => x.OrderDate).WithAlias(() => orderItem.OrderDate)
                    .Select(x => x.ShippedDate).WithAlias(() => orderItem.ShippedDate)
                    .Select(x => x.ShipAddress).WithAlias(() => orderItem.Address)
                    .Select(x => x.ShipPostalCode).WithAlias(() => orderItem.PostCode)
                    .Select(x => x.ShipCity).WithAlias(() => orderItem.City)
                    .Select(x => x.ShipCountry).WithAlias(() => orderItem.Country))
                .TransformUsing(Transformers.AliasToBean<OrderItem>())
                .Take(20)
                .List<OrderItem>()
                .ToArray();

            return query;
        }
    }
}