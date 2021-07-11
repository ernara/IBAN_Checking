using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAN_Checking_Library
{
    public struct Result
    {
        public string IBAN { get; set; }
        public CheckingResult CheckingResult { get; set; }
        public Result(string IBAN, CheckingResult checkingResult)
        {
            this.IBAN = IBAN;
            CheckingResult = checkingResult;
        }
    }
}
