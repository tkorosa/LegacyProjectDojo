using Persistence.Entities;

namespace Persistence.Validation
{
    public class EditRatingValidator
    {
        public static bool ValidateBeforeSave(Votes votes)
        {
            if (string.IsNullOrEmpty(votes.FavouriteShopStreetName))
            {
                return false;
            }
            if (votes.Rating < 0 || votes.Rating > 10)
            {
                return false;
            }
            if (votes.Customer == null)
            {
                return false;
            }
            return true;
        }
    }
}
