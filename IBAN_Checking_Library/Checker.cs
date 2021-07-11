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
        public string Input { get; set; }
        public List<Result> Result { get; set; }

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

        public List<Result> CheckList(string s)
        {
            var toReturn = new List<Result>();

            string[] stringSeparators = new string[] { "\r\n", ";", "\t" };
            string[] lines = s.Split(stringSeparators, StringSplitOptions.None);


            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    toReturn.Add(Check(line));
                }
            }

            return toReturn;
        }


        public Result Check(string s)
        {
            if (s.Length < 15)
            {
                return new Result(s, CheckingResult.ValueTooShort);
            }

            var charsToRemove = new List<char>() { ' ', '_', '-' };
            string Filtered = Filter(s, charsToRemove);
            Filtered = Filtered.ToUpper();

            var countryCode = Filtered.Substring(0, 2).ToUpper();

            var countryCodeKnown = Lengths.TryGetValue(countryCode, out int lengthForCountryCode);
            if (!countryCodeKnown)
            {
                return new Result(s, CheckingResult.CountryCodeNotKnown);
            }

            if (Filtered.Length < lengthForCountryCode)
                return new Result(s, CheckingResult.ValueTooShort);

            if (Filtered.Length > lengthForCountryCode)
                return new Result(s, CheckingResult.ValueTooLong);

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

        private Result Module97Check(string s)
        {
            var newIban = s[4..] + s.Substring(0, 4);

            var allNumbers = newIban.Select(c => (char.IsLetter(c)) ? (c - 55).ToString() : c.ToString());
            newIban = string.Join("", allNumbers);

            var remainder = BigInteger.Parse(newIban) % 97;

            if (remainder != 1)
                return new Result(s, CheckingResult.ValueFailsModule97Check);

            return new Result(s, CheckingResult.IsValid);
        }
    }
}
