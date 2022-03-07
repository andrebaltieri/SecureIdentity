using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureIdentity.Password;
using SecureIdentity.Password.Exceptions;

namespace SecureIdentity.Tests
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod("Should generate a random password")]
        public void ShouldGenerateRandomPassword()
        {
            var password = PasswordGenerator.Generate();
            Assert.IsFalse(string.IsNullOrEmpty(password));
        }

        [TestMethod("Should generate a random (25 chars long) password")]
        public void ShouldGenerateRandom25CharsLongPassword()
        {
            var password = PasswordGenerator.Generate(25);
            Assert.IsFalse(string.IsNullOrEmpty(password));
            Assert.AreEqual(password.Length, 25);
        }

        [ExpectedException(typeof(InvalidPasswordException))]
        [TestMethod("Should not generate null password hash")]
        public void ShouldNotGenerateNullPasswordHash()
        {
            var passHash = PasswordHasher.Hash(null);
            Assert.IsFalse(string.IsNullOrEmpty(passHash));
        }

        [ExpectedException(typeof(InvalidPasswordException))]
        [TestMethod("Should not generate empty password hash")]
        public void ShouldNotGenerateEmptyPasswordHash()
        {
            var passHash = PasswordHasher.Hash("");
            Assert.IsFalse(string.IsNullOrEmpty(passHash));
        }


        [TestMethod("Should generate password hash")]
        [DataRow("1")]
        [DataRow("12345")]
        [DataRow("bQg,g%;7LC9mvbPK")]
        [DataRow("Z`byj/nbJ8.Sc5@K4rx>ygFhS")]
        public void ShouldGeneratePasswordHash(string password)
        {
            var passHash = PasswordHasher.Hash(password);
            Assert.IsFalse(string.IsNullOrEmpty(passHash));
        }


        [TestMethod("Should verify password hash")]
        [DataRow("1")]
        [DataRow("12345")]
        [DataRow("bQg,g%;7LC9mvbPK")]
        [DataRow("Z`byj/nbJ8.Sc5@K4rx>ygFhS")]
        public void ShouldGenerateVerifyPasswordHash(string password)
        {
            var passHash = PasswordHasher.Hash(password);
            var result = PasswordHasher.Verify(passHash, password);
            Assert.IsTrue(result);
        }

        [TestMethod("Should verify password hash")]
        [DataRow("1")]
        [DataRow("12345")]
        [DataRow("bQg,g%;7LC9mvbPK")]
        [DataRow("Z`byj/nbJ8.Sc5@K4rx>ygFhS")]
        public void ShouldGenerateVerifyPasswordHashWithPrivateKey(string password)
        {
            var passHash = PasswordHasher.Hash(password, privateKey: "nˆNuj8bu");
            var result = PasswordHasher.Verify(passHash, password, privateKey: "nˆNuj8bu");
            Assert.IsTrue(result);
        }
    }
}