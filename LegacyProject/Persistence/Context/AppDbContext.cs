using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Context
{
    public class AppDbContext : DbContext, IContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> AddressList { get; set; }

        public AppDbContext() : base("ConectionStringName") { }

    }
}
