using NUnit.Framework;
using IBAN_Checking_Library;

namespace IBAN_Checking_Unit_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSum()
        {
            Assert.AreEqual(4, Class1.Sum(2, 2));
            Assert.AreEqual(9, Class1.Sum(2, 7));
            Assert.AreEqual(4, Class1.Sum(0, 4));
            Assert.AreNotEqual(4, Class1.Sum(5, 4));
        }
    }
}