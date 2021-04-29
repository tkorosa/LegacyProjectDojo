using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Context;
using Persistence.Entities;

namespace Persistence.Repository
{
    public class CustomerRepository
    {
        private readonly IContext _context;

        private  IQueryable<Customer> CustomersWithAddress => 
            _context
                .Customers
                .Include(c => c.CustomerAddress);

        public CustomerRepository(IContext context)
        {
            _context = context;
        }

        public List<Customer> All()
        {
            return CustomersWithAddress.ToList();
        }
        public Customer Find(Guid id)
        {
            return CustomersWithAddress
                .ToList()// Franz, 2012: Ohne das gehts nicht. EF ist komisch...
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Customer customer)
        {
            SetProperColor(customer);
            customer.Id = Guid.NewGuid();
            if(customer.CustomerAddress == null)
            {
                customer.CustomerAddress = 
                    new Address
                    {
                        StreetText = "Keine Eingabe",
                        Zip = 1
                    };
            }
            customer.CustomerAddressId = customer.CustomerAddress.Id = Guid.NewGuid();
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        private void SetProperColor(Customer customer)
        {
            if (customer.FavouriteShopStreetText != "")
            {
                customer.CustomerType = "green";
            }
            else
            {
                customer.CustomerType = "red";
            }
        }

        public void Update(Customer customer)
        {
            var existing = Find(customer.Id);
            existing.CustomerFirstname = customer.CustomerFirstname;
            existing.CustomerSurname= customer.CustomerSurname;
            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            _context.Customers.Remove(Find(id));
            _context.SaveChanges();
        }

        public void UpdateFavouriteShop(Customer customer)
        {
            var existing = Find(customer.Id);

            existing.FavouriteShopStreetText = customer.FavouriteShopStreetText;
            existing.FavouriteShopCity = customer.FavouriteShopCity;
            existing.FavouriteShopCountry= customer.FavouriteShopCountry;
            existing.FavouriteShopZip= customer.FavouriteShopZip;
            existing.FavouriteShopCity = customer.FavouriteShopCity;
            SetProperColor(existing);

            _context.SaveChanges();

        }

        public void RemoveFavouriteShop(Guid id)
        {
            var existing = Find(id);

            existing.FavouriteShopStreetText = null;
            existing.FavouriteShopCity = null;
            existing.FavouriteShopCountry = null;
            existing.FavouriteShopZip = 0;

            _context.SaveChanges();
        }

        public Customer FindForAddress(Address address)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerAddressId == address.Id);
        }
    }
}
