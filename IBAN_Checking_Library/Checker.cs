using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace IBAN_Checking_Library
{
    public class Checker : IChecker
    {

        private static readonly IDictionary<string, int> Lengths = new Dictionary<string, int>
        {
            {"AL", 28},
            {"AD", 24},
            {"AT", 20},
            {"AZ", 28},
            {"BE", 16},
            {"BH", 22},
            {"BA", 20},
            {"BR", 29},
            {"BG", 22},
            {"CR", 21},
            {"HR", 21},
            {"CY", 28},
            {"CZ", 24},
            {"DK", 18},
            {"DO", 28},
            {"EE", 20},
            {"FO", 18},
            {"FI", 18},
            {"FR", 27},
            {"GE", 22},
            {"DE", 22},
            {"GI", 23},
            {"GR", 27},
            {"GL", 18},
            {"GT", 28},
            {"HU", 28},
            {"IS", 26},
            {"IE", 22},
            {"IL", 23},
            {"IT", 27},
            {"KZ", 20},
            {"KW", 30},
            {"LV", 21},
            {"LB", 28},
            {"LI", 21},
            {"LT", 20},
            {"LU", 20},
            {"MK", 19},
            {"MT", 31},
            {"MR", 27},
            {"MU", 30},
            {"MC", 27},
            {"MD", 24},
            {"ME", 22},
            {"NL", 18},
            {"NO", 15},
            {"PK", 24},
            {"PS", 29},
            {"PL", 28},
            {"PT", 25},
            {"RO", 24},
            {"SM", 27},
            {"SA", 24},
            {"RS", 22},
            {"SK", 24},
            {"SI", 19},
            {"ES", 24},
            {"SE", 24},
            {"CH", 21},
            {"TN", 24},
            {"TR", 26},
            {"AE", 23},
            {"GB", 22},
            {"VG", 24}
        };

        public List<CheckingResult> CheckList(string s)
        {
            string[] stringSeparators = new string[] { "\r\n", ";" };
            string[] lines = s.Split(stringSeparators, StringSplitOptions.None);

            var toReturn = new List<CheckingResult>();

            foreach(string line in lines)
            {
                if (line.Length>0)
                {
                    toReturn.Add(Check(line));
                }
            }

            return toReturn;
        }


        public CheckingResult Check(string s)
        {
            if (s.Length < 15)
            {
                return CheckingResult.ValueTooSmall;
            }

            var charsToRemove = new List<char>() { ' ', '_', '-' };
            string Filtered = Filter(s, charsToRemove);
            Filtered = Filtered.ToUpper();

            var countryCode = Filtered.Substring(0, 2).ToUpper();

            var countryCodeKnown = Lengths.TryGetValue(countryCode, out int lengthForCountryCode);
            if (!countryCodeKnown)
            {
                return CheckingResult.CountryCodeNotKnown;
            }

            if (Filtered.Length < lengthForCountryCode)
                return CheckingResult.ValueTooSmall;

            if (Filtered.Length > lengthForCountryCode)
                return CheckingResult.ValueTooBig;

            return Module97Check(Filtered);
        }

        private static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }

        private CheckingResult Module97Check(string s)
        {
            var newIban = s[4..] + s.Substring(0, 4);

            var allNumbers = newIban.Select(c => (char.IsLetter(c)) ? (c - 55).ToString() : c.ToString());
            newIban = string.Join("", allNumbers);

            var remainder = BigInteger.Parse(newIban) % 97;

            if (remainder != 1)
                return CheckingResult.ValueFailsModule97Check;

            return CheckingResult.IsValid;
        }
    }
}
