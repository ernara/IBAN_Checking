using NUnit.Framework;
using IBAN_Checking_Library;
using System.Collections.Generic;
using System;

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
            Assert.AreEqual("LT60", Checker.Check("LT60").IBAN);
            Assert.AreEqual(CheckingResult.ValueTooShort, Checker.Check("LT60").CheckingResult);
            Assert.AreEqual(CheckingResult.CountryCodeNotKnown, Checker.Check("LL8330001234567").CheckingResult);
            Assert.AreEqual(CheckingResult.ValueTooShort, Checker.Check("LT60101001234567890").CheckingResult);
            Assert.AreEqual(CheckingResult.ValueTooLong, Checker.Check("LT6010100123456789010").CheckingResult);
            Assert.AreEqual(CheckingResult.ValueFailsModule97Check, Checker.Check("LT601010012345678902").CheckingResult);
            Assert.AreEqual(CheckingResult.Valid, Checker.Check("LT601010012345678901").CheckingResult);
            Assert.AreEqual(CheckingResult.Valid, Checker.Check("LT 6_0-1010012345678901").CheckingResult);
        }

        [Test]
        public void CheckList()
        {
            var ExpectedResult = new List<CheckingResult>() { CheckingResult.ValueTooSmall , CheckingResult.CountryCodeNotKnown ,
                CheckingResult.ValueTooSmall,CheckingResult.ValueTooBig, CheckingResult.ValueFailsModule97Check, CheckingResult.IsValid, CheckingResult.IsValid};

            var Result = new List<CheckingResult>(Checker.CheckList("LT60;LL8330001234567;LT60101001234567890;LT6010100123456789010;LT601010012345678902" +
                ";LT601010012345678901;LT 6_0-1010012345678901;"));

            for (int i = 0; i < ExpectedResult.Count; i++)
            {
                Assert.AreEqual(ExpectedResult[i], Result[i]);
            }

            Result = new List<CheckingResult>(Checker.CheckList(""));
            Assert.AreEqual(0, Result.Count);

        }
    }
}
