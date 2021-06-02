using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureIdentity.Password;

namespace SecureIdentity.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rndPas = PasswordGenerator.Generate();
            var passHash = PasswordHasher.Hash("testando");

            var xpto = PasswordHasher.Verify(passHash, "testando");
        }
    }
}
