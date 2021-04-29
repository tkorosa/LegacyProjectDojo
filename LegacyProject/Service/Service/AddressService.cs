using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyProject;
using Persistence.Entities;
using Persistence.Repository;
using Service.Validation;

namespace Service
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;
        private readonly CustomerRepository _customerRepository;

        public AddressService(AddressRepository addressRepository, CustomerRepository customerRepository)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
        }

        public void Create(Address address)
        {
            if (ZipCodeValidator.NotValid(address.Zip.ToString()))
            {
                throw new FieldValidationException("Zip", Resource.InvalidZip);
            }
            _addressRepository.Add(address);
        }

        // TODO: UKL Sprint 2/2017: Wartet auf Beauftragung (CR2/2017) für restliches Refactoring
        public void Update(Address address)
        {
            throw new NotImplementedException();
        }
        public Address GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public List<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        // TODO: HKI 3/3/2019 - Find better way to do this...
        public void SetDeletable(List<Address> addresses)
        {
            foreach (var address in addresses)
            {
                address.CanbeDeleted = _customerRepository.FindForAddress(address) == null;
            }
        }
    }
}
