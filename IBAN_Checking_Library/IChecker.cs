using System.Collections.Generic;

namespace IBAN_Checking_Library
{
    public interface IChecker
    {
        string Input { get; set; }
        List<Result> Result { get; set; }
        List<Result> CheckList(string s);
    }
}