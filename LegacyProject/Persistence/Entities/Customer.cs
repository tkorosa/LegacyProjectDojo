using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using System.Text;

namespace Persistence.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [NotMapped]
        // Franz, 2012: AAAH. Id heißt beim kunden in der db falsch.
        // Schreib ich jetz nicht überall um!
        public Guid Id
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }

        public string CustomerType { get; set; }

        [Required(ErrorMessage = "Vorname ist erforderlich")]
        [Display(Name = "Vorname")]
        public string CustomerFirstname { get; set; }
        [Required(ErrorMessage = "Nachname ist erforderlich")]
        [Display(Name = "Nachname")]
        public string CustomerSurname { get; set; }
        public Address CustomerAddress { get; set; }
        public Guid? CustomerAddressId { get; set; }

        #region FavouriteShop
        [Display(Name = "Name")]
        public string FavouriteShopStreetName { get; set; }
        [Display(Name = "Rating")]
        public string FavouriteShopStreetRating { get; set; }
        [Display(Name = "Strasse")]
        public string FavouriteShopStreetText { get; set; }
        [Display(Name = "Postleitlzahl")]
        public int FavouriteShopZip { get; set; }
        [Display(Name = "Stadt")]
        public string FavouriteShopCity { get; set; }
        [Display(Name = "Land")]
        public string FavouriteShopCountry { get; set; }
        #endregion
    }
}
