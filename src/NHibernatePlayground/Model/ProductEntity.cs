namespace NHibernatePlayground.Model
{
    public class ProductEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int SupplierId { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual string QuantityPerUnit { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual short UnitsInStock { get; set; }

        public virtual short UnitsOnOrder { get; set; }

        public virtual short ReorderLevel { get; set; }

        public virtual bool Discontinued { get; set; }
    }
}