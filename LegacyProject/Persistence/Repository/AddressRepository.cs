using Persistence.Context;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repository
{
    public class AddressRepository
    {
        private readonly IContext _context;

        public AddressRepository(IContext context)
        {
            _context = context;
        }
        public List<Address> All()
        {
            return _context.AddressList.ToList();
        }
        public Address Find(Guid id)
        {
            return _context.AddressList.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Address address)
        {
            address.Id = Guid.NewGuid();
            _context.AddressList.Add(address);
            _context.SaveChanges();
        }

        public void Update(Address address)
        {
            var existing = Find(address.Id);
            existing.StreetText= address.StreetText;
            existing.City = address.City;
            existing.Zip = address.Zip;
            existing.Country= address.Country;
            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            _context.AddressList.Remove(Find(id));
            _context.SaveChanges();
        }
    }
}
