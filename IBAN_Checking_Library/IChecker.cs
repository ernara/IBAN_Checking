namespace IBAN_Checking_Library
{
    public interface IChecker
    {
        CheckingResult Check(string s);
    }
}