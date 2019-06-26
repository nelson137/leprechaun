using Newtonsoft.Json;
using System.Collections.Generic;

namespace OktaTest
{
    public class AuthResponse
    {
        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty("sessionToken")]
        public string SessionToken { get; set; }

        [JsonProperty("stateToken")]
        public string StateToken { get; set; }

        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; }

        [JsonProperty("_embedded")]
        public AR_Embedded Embedded { get; set; }
    }

    public class AR_Embedded
    {
        [JsonProperty("factors")]
        public IList<AR_Factor> Factors { get; set; }
    }

    public class AR_Factor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("factorType")]
        public string FactorType { get; set; }

        [JsonProperty("profile")]
        public AR_FactorProfile Profile { get; set; }

        [JsonProperty("_links")]
        public AR_FactorLinks Links { get; set; }
    }

    public class AR_FactorProfile
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("questionText")]
        public string QuestionText { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class AR_FactorLinks
    {
        [JsonProperty("verify")]
        public AR_FactorLinksVerify Verify { get; set; }
    }

    public class AR_FactorLinksVerify
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("hints")]
        public AR_FactorLinksVerifyHints Hints { get; set; }
    }

    public class AR_FactorLinksVerifyHints
    {
        [JsonProperty("allow")]
        public List<string> Allow { get; set; }
    }
}
