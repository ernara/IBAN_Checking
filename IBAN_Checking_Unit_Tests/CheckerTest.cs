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
        public void CheckCountry()
        {
            Assert.AreEqual("Yes", Checker.CheckCountry("IE29 AIBK 9311 5212 3456 78"));
            Assert.AreEqual("Yes", Checker.CheckCountry("PL27  1140  2004 0000 3002 0135 5387"));
            Assert.AreEqual("Yes", Checker.CheckCountry("RO09 BCYP 0000 0012 3456 7890"));
            Assert.AreEqual("None", Checker.CheckCountry("LT12|1000 0111 0100 1000"));
            Assert.AreEqual("None", Checker.CheckCountry("LT12a1000 0111 0100 1000"));
            Assert.AreEqual("None", Checker.CheckCountry("LT12111111111111111111111111111111111111110202020200220201000 0111 0100 1000"));
        }
    }
}
