using NUnit.Framework;
using IBAN_Checking_Library;

namespace IBAN_Checking_Unit_Tests
{
    class CheckerTest 
    {
        public Checker Checker;

        [SetUp]
        public void Setup() 
        {
            Checker = new Checker();
        }

        [Test]
        public void Check()
        {
            Assert.AreEqual(CheckingResult.ValueTooSmall, Checker.Check("LT60"));
            Assert.AreEqual(CheckingResult.CountryCodeNotKnown, Checker.Check("LL8330001234567"));
            Assert.AreEqual(CheckingResult.ValueTooSmall, Checker.Check("LT60101001234567890"));
            Assert.AreEqual(CheckingResult.ValueTooBig, Checker.Check("LT6010100123456789010"));
            Assert.AreEqual(CheckingResult.ValueFailsModule97Check, Checker.Check("LT601010012345678902"));
            Assert.AreEqual(CheckingResult.IsValid, Checker.Check("LT601010012345678901"));
        }
    }
}
