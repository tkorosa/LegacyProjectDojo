using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Validation;

namespace Persistence.Entities
{
    public class Votes
    {
        public Customer Customer { get; set; }
        public string FavouriteShopStreetName { get; set; }
        public int Rating { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime EditedAt { get; set; }

        [NotMapped]
        public bool IsValid => EditRatingValidator.ValidateBeforeSave(this);
    }
}
