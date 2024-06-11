using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static IO.Didomi.SDK.IOS.IOSObjectMapper;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Current User Status
    /// Type used to represent the current user status.
    /// This contains all the choices made by the user on the Didomi SDK and other related information
    /// such as consent string and dates when the status was created or updated.
    /// </summary>
    [Serializable]
    public class CurrentUserStatus
    {
        /// <summary>
        /// The user status associated to purposes.
        /// </summary>
        [JsonProperty("purposes")]
        public Dictionary<string, PurposeStatus> Purposes { get; set; } = new Dictionary<string, PurposeStatus>();

        /// <summary>
        /// The user status associated to vendors.
        /// </summary>
        [JsonProperty("vendors")]
        public Dictionary<string, VendorStatus> Vendors { get; set; } = new Dictionary<string, VendorStatus>();

        /// <summary>
        /// Didomi user id
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// User choices creation date
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; } = string.Empty;

        /// <summary>
        /// User choices update date
        /// </summary>
        [JsonProperty("updated")]
        public string Updated { get; set; } = string.Empty;

        /// <summary>
        /// TCF consent as string
        /// </summary>
        [JsonProperty("consent_string")]
        public string ConsentString { get; set; } = string.Empty;

        /// <summary>
        /// Additional consent
        /// </summary>
        [JsonProperty("addtl_consent")]
        public string AdditionalConsent { get; set; } = string.Empty;

        /// <summary>
        /// Current regulation, such as "gdpr", "cpra"... or "none"
        /// </summary>
        [JsonProperty("regulation")]
        [JsonConverter(typeof(JsonRegulationConverter))]
        public string Regulation { get; set; } = string.Empty;

        /// <summary>
        /// Didomi Consent String
        /// </summary>
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

        /// <summary>
        /// User Status for a Purpose
        /// </summary>
        [Serializable]
        public class PurposeStatus
        {
            /// <summary>
            /// Didomi Purpose id
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// Computed status of purpose
            /// </summary>
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

        /// <summary>
        /// User Status for a Vendor
        /// </summary>
        [Serializable]
        public class VendorStatus
        {
            /// <summary>
            /// Didomi Vendor id
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// Computed status of vendor
            /// </summary>
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
