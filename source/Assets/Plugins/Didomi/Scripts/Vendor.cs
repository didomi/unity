using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using static IO.Didomi.SDK.IOS.IOSObjectMapper;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Vendor
    {
        /// <summary>
        /// Unique id of the vendor provided by Didomi. This id does not include prefixes. Example: "vendor-1".
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// Name of the vendor.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Namespaces of the vendor (IAB, num) and their corresponding ids.
        /// </summary>
        [JsonProperty("namespaces")]
        public Vendor.Namespaces? namespaces;

        /// <summary>
        /// Privacy policy URL(replaced by urls in IAB TCF v2.2)
        /// </summary>
        [JsonProperty("policyUrl")]
        public string? PolicyUrl { get; set; }

        /// <summary>
        /// Purposes with legal basis "consent"
        /// </summary>
        [JsonProperty("purposeIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> PurposeIds { get; set; } = new List<string>();

        /// <summary>
        /// Purposes with legal basis "legitimate interest"
        /// </summary>
        [JsonProperty("legIntPurposeIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> LegIntPurposeIds { get; set; } = new List<string>();

        /// <summary>
        /// List with IDs that represent features.
        /// </summary>
        [JsonProperty("featureIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> FeatureIds { get; set; } = new List<string>();

        /// <summary>
        /// Set with IDs that represent flexible purposes.
        /// </summary>
        [JsonProperty("flexiblePurposeIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> FlexiblePurposeIds { get; set; } = new List<string>();

        /// <summary>
        /// Set with IDs that represent Special Features.
        /// </summary>
        [JsonProperty("specialFeatureIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> SpecialFeatureIds { get; set; } = new List<string>();

        /// <summary>
        /// Set with IDs that represent Special Purposes.
        /// </summary>
        [JsonProperty("specialPurposeIds")]
        [JsonConverter(typeof(JsonListStringConverter))]
        public IList<string> SpecialPurposeIds { get; set; } = new List<string>();

        /// <summary>
        /// Privacy policy and LI disclaimer urls. Introduced in IAB TCF v2.2.
        /// </summary>
        [JsonProperty("urls")]
        [JsonConverter(typeof(JsonSetVendorUrlConverter))]
        public ISet<Vendor.Url>? Urls { get; set; }

        [Obsolete("Use PolicyUrl instead")]
        public string? PrivacyPolicyUrl => PolicyUrl;

        public Vendor(
            string id,
            string name,
            Namespaces? namespaces,
            string? policyUrl,
            IList<string> purposeIds,
            IList<string> legIntPurposeIds,
            IList<string> featureIds,
            IList<string> flexiblePurposeIds,
            IList<string> specialFeatureIds,
            IList<string> specialPurposeIds,
            ISet<Url>? urls
        ) {
            this.Id = id;
            this.Name = name;
            this.namespaces = namespaces;
            this.PolicyUrl = policyUrl;
            this.PurposeIds = purposeIds;
            this.LegIntPurposeIds = legIntPurposeIds;
            this.FeatureIds = featureIds;
            this.FlexiblePurposeIds = flexiblePurposeIds;
            this.SpecialFeatureIds = specialFeatureIds;
            this.SpecialPurposeIds = specialPurposeIds;
            this.Urls = urls;
        }

        public Namespaces? GetNamespaces()
        {
            return namespaces;
        }

        public void setNamespaces(Namespaces? namespaces)
        {
            this.namespaces = namespaces;
        }

        [Serializable]
        public class Namespaces : Numerable
        {
            [JsonProperty("iab2")]
            public string? Iab2 { get; set; }

            /// <summary>
            /// Always null on iOS
            /// </summary>
            [JsonProperty("num")]
            public int? Num { get; set; }

            public Namespaces(string? iab2, int? num)
            {
                this.Iab2 = iab2;
                this.Num = num;
            }
        }

        [Serializable]
        public class Url
        {
            [JsonProperty("langId")]
            public string? LangId { get; set; }

            [JsonProperty("privacy")]
            public string? Privacy { get; set; }

            [JsonProperty("legIntClaim")]
            public string? LegIntClaim { get; set; }

            public Url(string? langId, string? privacy, string? legIntClaim)
            {
                this.LangId = langId;
                this.Privacy = privacy;
                this.LegIntClaim = legIntClaim;
            }
        }

        /// <summary>
        /// Get IDs from a list of vendors
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        public static ISet<string> GetIds(ICollection<Vendor> vendors)
        {
            HashSet<string> vendorIds = new HashSet<string>();

            foreach (Vendor vendor in vendors)
            {
                vendorIds.Add(vendor.Id);
            }

            return vendorIds;
        }
    }
}

public interface Numerable
{
    int? Num { get; set; }
}
