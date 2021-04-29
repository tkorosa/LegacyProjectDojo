using Effort;
using Effort.Provider;
using Persistence.Context;
using Persistence.Entities;
using System;

namespace TestBootstrapper.Context
{
    public class AppInMemoryContext : AppDbContext
    {
        private static readonly EffortConnection Connection =
            DbConnectionFactory.CreateTransient();

        private static bool _isDataSeeded;

        public AppInMemoryContext()
            : base(Connection, false)
        {
            Seed();
        }

        private void Seed()
        {
            if (_isDataSeeded)
            {
                return;
            }

            Customers.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerFirstname = "Hans",
                    CustomerSurname = "Mustermann",
                    CustomerAddress =
                        new Address
                        {
                            Id = Guid.NewGuid(),
                            StreetText = "Musterstrasse 5",
                            Zip = 1050,
                            City = "Wien",
                            Country = "Österreich"
                        },
                    FavouriteShopStreetText = "Mariahilfer Straße 98",
                    FavouriteShopZip = 1060,
                    FavouriteShopCity = "Wien",
                    CustomerType = "green"
                }
            );
            Customers.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerFirstname = "Herbert",
                    CustomerSurname = "Schubert",
                    CustomerType = "red",
                    CustomerAddress =
                        new Address
                        {
                            Id = Guid.NewGuid(),
                            StreetText = "Strassenweg 23",
                            Zip = 1200,
                            City = "Wien",
                            Country = "Österreich"
                        }
                }
            );
            AddressList.Add(
                new Address
                {
                    Id = Guid.NewGuid(),
                    StreetText = "Testweg 98",
                    Zip = 1110,
                    City = "Wien",
                    Country = "Österreich"
                }
            );

            SaveChanges();
            _isDataSeeded = true;
        }
    }
}
