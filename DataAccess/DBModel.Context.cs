using Domain;
namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class genebygene2017Entities : DbContext
    {
        public genebygene2017Entities()
            : base("name=genebygene2017Entities")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }
    
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}
    
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
