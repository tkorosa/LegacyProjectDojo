using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Strasse ist erforderlich")]
        [Display(Name = "Strasse")]
        public string StreetText { get; set; }
        [Required(ErrorMessage = "PLZ ist erforderlich")]
        [Display(Name = "Postleitzahl")]
        public int Zip { get; set; }
        [Display(Name = "Stadt")]
        public string City { get; set; }
        [Display(Name = "Land")]
        public string Country { get; set; }
        [NotMapped]
        public bool CanbeDeleted { get; set; }
    }
}