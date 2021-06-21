using System;
using System.Text;

namespace SecureIdentity.Password
{
    public static class PasswordGenerator
    {
        private const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string special = "!@#$%ˆ&*(){}[];";

        public static string Generate(
            short length = 16,
            bool includeSpecialChars = true,
            bool upperCase = false)
        {
            var chars = includeSpecialChars ? (valid + special) : valid;
            var startRandom = upperCase ? 26 : 0;
            var index = 0;
            var res = new char[length];
            var rnd = new Random();

            while (index < length)
                res[index++] = chars[rnd.Next(startRandom, chars.Length)];

            return new String(res);
        }
    }
}