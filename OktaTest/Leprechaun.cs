using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static OktaTest.Helpers;

namespace OktaTest
{
    public class Leprechaun
    {
        private static readonly string _username = "Nelson.Earle";
        private static readonly string _password = "g778L6vdKDGAFJqb"; //ReadPassword($"Okta Password for {_username}: ");

        private const string _authServer = "ausly21wjd7rsff7G0x7";
        private const string _clientId = "0oaly1jsy9dS93SZQ0x7";
        private const string _redirectUri = "https://oidcdebugger.com/debug";

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
            var response = await new HttpClient().PostJsonAsync<J_AuthResponse>(uri, new
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
    }
}
