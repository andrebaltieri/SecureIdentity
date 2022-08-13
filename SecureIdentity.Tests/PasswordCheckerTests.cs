using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureIdentity.Password;
using SecureIdentity.Password.Enums;

namespace SecureIdentity.Tests;

[TestClass]
public class PasswordCheckerTests
{
    [TestMethod("Should check password strength")]
    [DataRow("", EPasswordStrength.Invalid)]
    [DataRow("AsTrongP*&yeah", EPasswordStrength.Medium)]
    [DataRow("abcd", EPasswordStrength.Invalid)]
    [DataRow("morethantwelvechars", EPasswordStrength.VeryWeak)]
    [DataRow("morethan12chars", EPasswordStrength.Weak)]
    [DataRow("MoreThan12Chars", EPasswordStrength.Medium)]
    [DataRow("MoreThan12CharsWith%$#@", EPasswordStrength.Strong)]
    public void ShouldCheckPasswordStrength(string password, EPasswordStrength strength)
    {
        var result = PasswordChecker.CheckPasswordStrength(password);
        Assert.AreEqual(result, strength);
    }
}