using System;
using System.Text;

namespace SecureIdentity.Password
{
    public static class PasswordGenerator
    {
        public static string Generate(
            short length = 16,
            bool includeSpecialChars = true,
            bool upperCase = false)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string special = "!@#$%ˆ&*(){}[];";
            var chars = includeSpecialChars ? (valid + special) : valid;
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
                res.Append(chars[rnd.Next(chars.Length)]);

            return upperCase ? res.ToString().ToUpper() : res.ToString();
        }
    }
}