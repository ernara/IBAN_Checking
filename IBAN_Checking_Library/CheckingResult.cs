using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAN_Checking_Library
{
    public enum CheckingResult
    {
        ValueTooShort,
        CountryCodeNotKnown,
        ValueTooLong,
        ValueFailsModule97Check,
        IsValid
    }
}
