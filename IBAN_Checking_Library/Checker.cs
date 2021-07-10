using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;


namespace IBAN_Checking_Library
{

    public class Checker : IChecker
    {
        public string Result { get; set; }

        private static readonly IDictionary<string, int> Lengths = new Dictionary<string, int>();

        public Checker()
        {
            ReadCountryCodes();
        }

        public static void ReadCountryCodes()
        {
            using var sr = new StreamReader("CountryCodes.txt");
            string line;
            string[] array = new string[2];
            while ((line = sr.ReadLine()) != null)
            {
                array = line.Split(' ');
                Lengths.Add(array[0], Convert.ToInt32(array[1]));
            }
        }

        public List<string> CheckList(string s)
        {
            var toReturn = new List<string>() { "IBAN\tStatus" };

            string[] stringSeparators = new string[] { "\r\n", ";" };
            string[] lines = s.Split(stringSeparators, StringSplitOptions.None);


            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    toReturn.Add($"{line}\t{Check(line)}");
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
