using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAN_Checking_Library
{
    public class Checker
    {
        public static string CheckCountry(string s)
        {
            var charsToRemove = new List<char>() { ' ', '_', '-' };
            string Filtered = Filter(s, charsToRemove);
            if (Filtered.Length>15 && Filtered.Length<33 && IsOnlyBigLettersAndNumbers(Filtered))
            {
                return "Yes";
            }

            return "None";

        }

        private static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }

        private static bool IsOnlyBigLettersAndNumbers(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsUpper(c) && !char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
