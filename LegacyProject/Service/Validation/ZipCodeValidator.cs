using System.Text.RegularExpressions;

namespace Service.Validation
{
    // MNO 1.8.2014: Kopiert von EditAddressValidator
    // ZZP 2.8.2014: EditAddressValidator damit obsolet? Refactoren!
    public class ZipCodeValidator
    {
        private static readonly int ersteZifferWien = 1;
        private static readonly int bezirkeInWien = 23;

        public static bool NotValid(string zip)
        {
            if (zip.Length == 1 || zip.Length == 2 || zip.Length == 3)
            {
                return true;
            }
            if (!new Regex("([0-9])[0-9][0-9][0-9]").IsMatch(zip))
            {
                // Franz, 2012: check first digit = ersteZifferWien
                // Franz, 2012: how to access regex group?
                return true;
            }
            int zipNumber = int.Parse(zip);

            if (zipNumber != 0 && zipNumber != -1)
            {
                if (zipNumber <= (ersteZifferWien * 100 + bezirkeInWien) * 10)
                {
                    if (zipNumber < (ersteZifferWien * 100 + 1) * 10)
                    {
                        return true;
                    }

                    if (zip.Length == 4)
                    {
                        if (zip[3] != '0')
                        {
                            return true;
                        }
                        return false;
                    }

                    return true;
                }

                return true;
            }

            return true;
        }
    }
}
