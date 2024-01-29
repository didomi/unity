using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static IO.Didomi.SDK.IOS.IOSObjectMapper;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class CurrentUserStatus
    {
        [JsonProperty("purposes")]
        public Dictionary<string, PurposeStatus> Purposes { get; set; } = new Dictionary<string, PurposeStatus>();

        [JsonProperty("vendors")]
        public Dictionary<string, VendorStatus> Vendors { get; set; } = new Dictionary<string, VendorStatus>();

        [JsonProperty("user_id")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("created")]
        public string Created { get; set; } = string.Empty;

        [JsonProperty("updated")]
        public string Updated { get; set; } = string.Empty;

        [JsonProperty("consent_string")]
        public string ConsentString { get; set; } = string.Empty;

        [JsonProperty("addtl_consent")]
        public string AdditionalConsent { get; set; } = string.Empty;

        [JsonProperty("regulation")]
        [JsonConverter(typeof(JsonRegulationConverter))]
        public string Regulation { get; set; } = string.Empty;

        [JsonProperty("didomi_dcs")]
        public string DidomiDcs { get; set; } = string.Empty;

        public CurrentUserStatus(): this(new Dictionary<string, PurposeStatus>(), new Dictionary<string, VendorStatus>(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public CurrentUserStatus(
            Dictionary<string, PurposeStatus> purposes,
            Dictionary<string, VendorStatus> vendors,
            string userId,
            string created,
            string updated,
            string consentString,
            string additionalConsent,
            string regulation,
            string didomiDcs
        ) {
            Purposes = purposes;
            Vendors = vendors;
            UserId = userId;
            Created = created;
            Updated = updated;
            ConsentString = consentString;
            AdditionalConsent = additionalConsent;
            Regulation = regulation;
            DidomiDcs = didomiDcs;
        }

        [Serializable]
        public class PurposeStatus
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("enabled")]
            public bool Enabled { get; set; } = false;

            public PurposeStatus(
                string id,
                bool enabled
            )
            {
                Id = id;
                Enabled = enabled;
            }
        }

        [Serializable]
        public class VendorStatus
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("enabled")]
            public bool Enabled { get; set; } = false;

            public VendorStatus(
                string id,
                bool enabled
            )
            {
                Id = id;
                Enabled = enabled;
            }
        }
    }
}
