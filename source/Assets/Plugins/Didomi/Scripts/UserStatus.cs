using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static IO.Didomi.SDK.IOS.IOSObjectMapper;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class UserStatus
    {
        /// <summary>
        /// Property that contains the user status associated to purposes.
        /// </summary>
        [JsonProperty("purposes")]
        private Purposes purposes;

        /// <summary>
        /// Property that contains the user status associated to vendors.
        /// </summary>
        [JsonProperty("vendors")]
        private Vendors vendors;

        /// <summary>
        /// Didomi user id
        /// </summary>
        [JsonProperty("user_id")]
        private string userId;

        /// <summary>
        /// User choices creation date
        /// </summary>
        [JsonProperty("created")]
        private string created;

        /// <summary>
        /// User choices update date
        /// </summary>
        [JsonProperty("updated")]
        private string updated;

        /// <summary>
        /// TCF consent as string
        /// </summary>
        [JsonProperty("consent_string")]
        private string consentString;

        /// <summary>
        /// Additional consent
        /// </summary>
        [JsonProperty("addtl_consent")]
        private string additionalConsent;

        /// <summary>
        /// Current regulation, such as "gdpr", "cpra"... or "none"
        /// </summary>
        [JsonProperty("regulation")]
        [JsonConverter(typeof(JsonRegulationConverter))]
        private string regulation;

        /// <summary>
        /// Didomi Consent String
        /// </summary>
        [JsonProperty("didomi_dcs")]
        private string didomiDcs;

        // Empty constructor for Json deserialization
        public UserStatus(): this(null, null, null, null, null, null, null, null, null)
        {
        }

        public UserStatus(
            Purposes purposes,
            Vendors vendors,
            string userId,
            string created,
            string updated,
            string consentString,
            string additionalConsent,
            string regulation,
            string didomiDcs
        ) {
            this.purposes = purposes;
            this.vendors = vendors;
            this.userId = userId;
            this.created = created;
            this.updated = updated;
            this.consentString = consentString;
            this.additionalConsent = additionalConsent;
            this.regulation = regulation;
            this.didomiDcs = didomiDcs;
        }

        public Purposes GetPurposes()
        {
            if (purposes == null)
            {
                purposes = new Purposes(null, null, null, null);
            }
            return purposes;
        }

        public Vendors GetVendors()
        {
            if (vendors == null)
            {
                vendors = new Vendors(null, null, null, null, null);
            }
            return vendors;
        }

        public string GetUserId()
        {
            if (userId == null)
            {
                userId = "";
            }
            return userId;
        }

        public string GetCreated()
        {
            if (created == null)
            {
                created = "";
            }
            return created;
        }

        public string GetUpdated()
        {
            if (updated == null)
            {
                updated = "";
            }
            return updated;
        }

        public string GetConsentString()
        {
            if (consentString == null)
            {
                consentString = "";
            }
            return consentString;
        }

        public string GetAdditionalConsent()
        {
            if (additionalConsent == null)
            {
                additionalConsent = "";
            }
            return additionalConsent;
        }

        public string GetRegulation()
        {
            if (regulation == null)
            {
                regulation = "";
            }
            return regulation;
        }

        public string GetDidomiDcs()
        {
            if (didomiDcs == null)
            {
                didomiDcs = "";
            }
            return didomiDcs;
        }

        /// <summary>
        /// User Status for Purposes
        /// </summary>
        [Serializable]
        public class Purposes
        {
            /// <summary>
            /// Computed sets/lists of enabled and disabled IDs of purposes that have been chosen by the user regarding the consent or legitimate interest Legal Basis.
            /// Purposes considered as essential will be part of the enabled IDs.
            /// </summary>
            [JsonProperty("global")]
            private Ids global;

            /// <summary>
            /// Enabled and disabled IDs of purposes that have been explicitly chosen by the user regarding the consent Legal Basis.
            /// </summary>
            [JsonProperty("consent")]
            private Ids consent;

            /// <summary>
            /// Enabled and disabled IDs of purposes that have been explicitly chosen by the user regarding the legitimate interest Legal Basis.
            /// </summary>
            [JsonProperty("legitimate_interest")]
            private Ids legitimateInterest;

            /// <summary>
            /// Ids of purposes that are considered essential.
            /// </summary>
            [JsonProperty("essential")]
            [JsonConverter(typeof(JsonSetStringConverter))]
            private ISet<string> essential;

            // Empty constructor for Json deserialization
            public Purposes() : this(null, null, null, null)
            {
            }

            public Purposes(Ids global, Ids consent, Ids legitimateInterest, ISet<string> essential)
            {
                this.global = global;
                this.consent = consent;
                this.legitimateInterest = legitimateInterest;
                this.essential = essential;
            }

            public Ids GetGlobal()
            {
                if (global == null)
                {
                    global = new Ids();
                }
                return global;
            }

            public Ids GetConsent()
            {
                if (consent == null)
                {
                    consent = new Ids();
                }
                return consent;
            }

            public Ids GetLegitimateInterest()
            {
                if (legitimateInterest == null)
                {
                    legitimateInterest = new Ids();
                }
                return legitimateInterest;
            }

            public ISet<string> GetEssential()
            {
                if (essential == null)
                {
                    essential = new HashSet<string>();
                }

                return essential;
            }
        }

        /// <summary>
        /// User Status for Vendors
        /// </summary>
        [Serializable]
        public class Vendors
        {
            /// <summary>
            /// Computed sets/lists of enabled and disabled IDs of vendors that have been chosen by the user regarding the consent or legitimate interest Legal Basis.
            /// </summary>
            [JsonProperty("global")]
            private Ids global;

            /// <summary>
            ///Computed sets/lists of enabled and disabled IDs of vendors that have been chosen by the user regarding the consent Legal Basis.
            /// This takes into account the consent required purposes linked to vendors.
            /// When computing this property, essential purposes will be considered as enabled.
            /// </summary>

            [JsonProperty("global_consent")]
            private Ids globalConsent;

            /// <summary>
            /// Computed sets/lists of enabled and disabled IDs of vendors that have been chosen by the user regarding the legitimate interest Legal Basis.
            /// This takes into account the legitimate interest required purposes linked to vendors.
            /// When computing this property, essential purposes will be considered as enabled.
            /// </summary>
            [JsonProperty("global_li")]
            private Ids globalLegitimateInterest;

            /// <summary>
            /// Enabled and disabled IDs of vendors that have been explicitly chosen by the user regarding the consent Legal Basis.
            /// </summary>
            [JsonProperty("consent")]
            private Ids consent;

            /// <summary>
            /// Enabled and disabled IDs of vendors that have been explicitly chosen by the user regarding the legitimate interest Legal Basis.
            /// </summary>
            [JsonProperty("legitimate_interest")]
            private Ids legitimateInterest;

            // Empty constructor for Json deserialization
            public Vendors(): this(null, null, null, null, null)
            {
            }

            public Vendors(Ids global, Ids globalConsent, Ids globalLegitimateInterest, Ids consent, Ids legitimateInterest)
            {
                this.global = global;
                this.globalConsent = globalConsent;
                this.globalLegitimateInterest = globalLegitimateInterest;
                this.consent = consent;
                this.legitimateInterest = legitimateInterest;
            }

            public Ids GetGlobal()
            {
                if (global == null)
                {
                    global = new Ids();
                }
                return global;
            }

            public Ids GetGlobalConsent()
            {
                if (globalConsent == null)
                {
                    globalConsent = new Ids();
                }
                return globalConsent;
            }

            public Ids GetGlobalLegitimateInterest()
            {
                if (globalLegitimateInterest == null)
                {
                    globalLegitimateInterest = new Ids();
                }
                return globalLegitimateInterest;
            }

            public Ids GetConsent()
            {
                if (consent == null)
                {
                    consent = new Ids();
                }
                return consent;
            }

            public Ids GetLegitimateInterest()
            {
                if (legitimateInterest == null)
                {
                    legitimateInterest = new Ids();
                }
                return legitimateInterest;
            }
        }

        /// <summary>
		/// User Status id lists for vendor or purpose
        /// - list of disabled and enabled ids
        /// </summary>
        [Serializable]
        public class Ids
        {
            /// <summary>
			/// List of enabled ids
			/// </summary>
            [JsonProperty("enabled")]
            [JsonConverter(typeof(JsonSetStringConverter))]
            private ISet<string> enabled;

            /// <summary>
            /// List of disabled ids
            /// </summary>
            [JsonProperty("disabled")]
            [JsonConverter(typeof(JsonSetStringConverter))]
            private ISet<string> disabled;

            public Ids(ISet<string> enabled, ISet<string> disabled)
            {
                this.enabled = enabled;
                this.disabled = disabled;
            }

            // Empty constructor for Json deserialization
            public Ids() : this(new HashSet<string>(), new HashSet<string>())
            {
            }

            public ISet<string> GetEnabled()
            {
                if (enabled == null)
                {
                    enabled = new HashSet<string>();
                }

                return enabled;
            }

            public ISet<string> GetDisabled()
            {
                if (disabled == null)
                {
                    disabled = new HashSet<string>();
                }

                return disabled;
            }
        }
    }
}
