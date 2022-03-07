using System;

namespace SecureIdentity.Password
{
    public static class PasswordGenerator
    {
        private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Special = "!@#$%ˆ&*(){}[];";

        public static string Generate(
            short length = 16,
            bool includeSpecialChars = true,
            bool upperCase = false)
        {
            var chars = includeSpecialChars ? (Valid + Special) : Valid;
            var startRandom = upperCase ? 26 : 0;
            var index = 0;
            var res = new char[length];
            var rnd = new Random();

            while (index < length)
                res[index++] = chars[rnd.Next(startRandom, chars.Length)];

            return new string(res);
        }
    }
}