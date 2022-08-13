using System.Text.RegularExpressions;
using SecureIdentity.Password.Enums;

namespace SecureIdentity.Password;

public static class PasswordChecker
{
    private const string HasNumberExpression = @"[0-9]+";
    private const string HasUpperCharExpression = @"[A-Z]+";
    private const string HasMinimumCharsExpression = @".{12,}";
    private const string HasNoSpecialCharsExpression = @"^[a-zA-Z0-9 ]*$";

    /// <summary>
    /// Check password strength
    /// </summary>
    /// <param name="plainTextPassword"></param>
    /// <returns></returns>
    public static EPasswordStrength CheckPasswordStrength(string plainTextPassword)
    {
        // Start with 0 points
        short points = 0;

        // +1 if it have at least one number
        if (Regex.Match(plainTextPassword, HasNumberExpression).Success)
            points++;

        // +1 if it have uppercase chars
        if (Regex.Match(plainTextPassword, HasUpperCharExpression).Success)
            points++;

        // +1 if it have at least 12 chars
        if (Regex.Match(plainTextPassword, HasMinimumCharsExpression).Success)
            points++;

        // +1 if it have special chars
        if (!Regex.Match(plainTextPassword, HasNoSpecialCharsExpression).Success)
            points++;

        return (EPasswordStrength) points;
    }
}