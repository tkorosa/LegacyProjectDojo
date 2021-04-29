using Persistence.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace Persistence.Context
{
    public class AppDbContext : DbContext, IContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> AddressList { get; set; }

        public AppDbContext()
            : base("ConectionStringName")
        { }

        public AppDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        { }

    }
}
