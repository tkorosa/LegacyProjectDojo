using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Service.Validation
{
    public class AddressValidation
    {
        private static List<string> _allowedCities = new List<string>{"WIEN"};

        private readonly Address _address;
        public List<ErrorDescription> Errors { get; } = new List<ErrorDescription>();
        public bool HasError => Errors.Any();

        public AddressValidation(Address address)
        {
            _address = address;
        }

        // TODO: TPA Sprint 15/2017: Was passiert, wenn das 2x aufgerufen wird... fixen!
        public void Validate()
        {
            if (!_allowedCities.Contains(_address.City?.ToUpper()))
            {
                Errors.Add(new ErrorDescription(nameof(Address.City), "Stadt nicht erlaubt!"));
            }

            if (_address.Country != "Österreich")
            {
                Errors.Add(new ErrorDescription("Country", "Land nicht erlaubt!"));
            }

            var addressParts = _address.StreetText.Split(' ');
            if(!int.TryParse(addressParts[addressParts.Length-1],out var _))
            {
                Errors.Add(new ErrorDescription("StreetText", "Bitte geben Sie eine Hausnummer an!"));
            }
        }
    }

    // Das ist ja nochmal das gleiche wie FieldValidationException? Sollte man mal aufräumen... (UKL 2017)
    public class ErrorDescription
    {
        public string Field { get; }
        public string Error { get; }

        public ErrorDescription(string field, string error)
        {
            Field = field;
            Error = error;
        }
    }
}
