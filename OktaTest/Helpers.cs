using System;
using System.Security.Cryptography;

namespace OktaTest
{
    public static class Helpers
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private static readonly byte[] _randomBytes = new byte[4];

        public static uint RandomUInt()
        {
            _rng.GetBytes(_randomBytes);
            return BitConverter.ToUInt32(_randomBytes, 0);
        }

        public static char RandomAlphaNum()
        {
            var num = RandomUInt();
            // Restrict the range to numbers and lowercase letters (36 total)
            // Range starts at 48, the ascii code for '0'
            num = (num % 36) + 48;
            // If num <= 57 it's a number, else it's a letter (shift range up to start at lowercase letters)
            return Convert.ToChar(num <= 57 ? num : num + 39);
        }

        public static string RandomString(int length)
        {
            string randomString = "";

            for (var i = 0; i < length; i++)
                randomString += RandomAlphaNum();

            return randomString;
        }

        public static string ReadPassword(string prompt = "Password: ")
        {
            Console.Write(prompt);
            var line = "";
            ConsoleKeyInfo c;
            while ((c = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                line += c.KeyChar;
            Console.Write(Environment.NewLine);
            return line;
        }
    }
}
