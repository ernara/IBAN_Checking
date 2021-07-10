using System.Collections.Generic;

namespace IBAN_Checking_Library
{
    public interface IChecker
    {
        string Result { get; set; }
        List<string> CheckList(string s);
    }
}