using Persistence.Entities;
using System.Text.RegularExpressions;

namespace Persistence.Validation
{
    public class EditAddressValidator
    {
        private static readonly int ersteZifferWien = 1;
        private static readonly int bezirkeInWien = 23;

        public static bool NotHasZeroErrors(Address address, out string field)
        {
            field = null;
            var zip2 = address.Zip.ToString();
            if (zip2.Length == 1 || zip2.Length == 2 || zip2.Length == 3)
            {
                field = "Zip";
                return true;
            }
            if (!new Regex("([0-9])[0-9][0-9][0-9]").IsMatch(zip2))
            {
                // Hans, 2012: check first digit = ersteZifferWien
                // Hans, 2012: how to access regex group?
                // ATO 5.8.2015: Kamma die ganze fkt hier nicht als regex machen??
                // Schlag ich im nächsten sprint vor!
                field = "Zip";
                return true;
            }

            if (address.Zip != 0 && address.Zip != -1)
            {
                if (address.Zip <= (ersteZifferWien * 100 + bezirkeInWien) * 10)
                {
                    if (address.Zip <= (ersteZifferWien * 100 + 1) * 10)
                    {
                        field = "Zip";
                        return true;
                    }

                    if (zip2.Length == 4)
                    {
                        if (zip2[3] != '0')
                        {
                            field = "Zip";
                            return true;
                        }

                        if (address.City == "Wien")
                        {
                            if (address.StreetText.Length < 10)
                            {
                                field = "StreetText";
                                return true;
                            }

                            bool endsWithNumber = false;
                            for (int i = 0; i < 10; i++)
                            {
                                if (address.StreetText.EndsWith(i.ToString()))
                                {
                                    endsWithNumber = true;
                                }
                            }
                            if (endsWithNumber == false)
                            {
                                field = "StreetText";
                                return true;
                            }
                            if (address.StreetText.Length < 10)
                            {
                                field = "StreetText";
                                return true;
                            }
                            return false;
                        }

                        field = "City";
                        return true;
                    }

                    field = "Zip";
                    return true;
                }

                field = "Zip";
                return true;
            }

            field = "Zip";
            return true;
        }
    }
}
