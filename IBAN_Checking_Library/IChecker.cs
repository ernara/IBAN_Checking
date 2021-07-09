using System.Collections.Generic;

namespace IBAN_Checking_Library
{
    public interface IChecker
    {
        List<CheckingResult> CheckList(string s);
    }
}