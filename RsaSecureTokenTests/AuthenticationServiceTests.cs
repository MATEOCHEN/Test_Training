using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsaSecureToken;

namespace RsaSecureTokenTests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            var sut = new MokeAuthenticationService();

            // implement your own act
            var actual = sut.IsValid("MateoChen", "211305");

            Assert.IsTrue(actual);
        }
    }

    internal class FakeProfileDao : ProfileDao
    {
        public override int GetRegisterTimeInMinutes(string ac)
        {
            return 200;
        }
    }

    internal class FakeRsaToken : RsaTokenDao
    {
        public override Random GetRandom(int minutes)
        {
            return new Random(minutes);
        }
    }

    internal class MokeAuthenticationService : AuthenticationService
    {
        protected override ProfileDao GetProfileDao()
        {
            return new FakeProfileDao();
        }

        protected override RsaTokenDao GetRsaTokenDao()
        {
            return new FakeRsaToken();
        }
    }
}