using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OktaTest
{
    public class Leprechaun
    {
        private static readonly string _username = "Nelson.Earle";
        private static readonly string _password = "g778L6vdKDGAFJqb"; //ReadPassword($"Okta Password for {_username}: ");

        private const string _authServer = "ausly21wjd7rsff7G0x7";
        private const string _clientId = "0oaly1jsy9dS93SZQ0x7";
        private const string _redirectUri = "https://oidcdebugger.com/debug";

        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private static readonly byte[] _randomBytes = new byte[4];

        public static async Task<string> GetOktaAccessToken()
        {
            var tokenUrl = $"https://vu.okta.com/oauth2/{_authServer}/v1/authorize?response_type=token&scope=openid"
                + "&client_id=" + _clientId
                + "&sessionToken=" + await GetOktaSessionToken()
                + "&redirect_uri=" + Uri.EscapeDataString(_redirectUri)
                + "&state=" + RandomString(5)
                + "&nonce=" + RandomString(10);

            var handler = new HttpClientHandler { AllowAutoRedirect = false };
            var response = await new HttpClient(handler).GetAsync(tokenUrl);
            var redirectUrl = response.Headers.Location.ToString();

            var accessToken = Regex.Replace(redirectUrl, ".*access_token=([^&]+)&.*", "$1");
            return accessToken;
        }

        private static async Task<string> GetOktaSessionToken()
        {
            var uri = "https://vu.okta.com/api/v1/authn";
            var response = await new HttpClient().PostJsonAsync<AuthResponse>(uri, new
            {
                username = _username,
                password = _password,
                options = new
                {
                    multiOptionalFactorEnroll = false,
                    warnBeforePasswordExpired = false
                }
            });
            return response.SessionToken;
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

        private static string RandomString(int length)
        {
            string randomString = "";

            for (var i = 0; i < length; i++)
                randomString += RandomAlphaNum();

            return randomString;
        }

        private static char RandomAlphaNum()
        {
            var num = RandomUInt32();
            // Restrict the range to numbers and lowercase letters (36 total)
            // Range starts at 48, the ascii code for '0'
            num = (num % 36) + 48;
            // If num <= 57 it's a number, else it's a letter (shift range up to start at lowercase letters)
            return Convert.ToChar(num <= 57 ? num : num + 39);
        }

        private static uint RandomUInt32()
        {
            using (var rng = _rng)
            {
                rng.GetBytes(_randomBytes);
                return BitConverter.ToUInt32(_randomBytes, 0);
            }
        }
    }
}
