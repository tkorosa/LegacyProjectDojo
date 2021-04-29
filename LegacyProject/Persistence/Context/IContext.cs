using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Context
{
    public interface IContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Address> AddressList { get; set; }
        
        int SaveChanges();
    }
}
