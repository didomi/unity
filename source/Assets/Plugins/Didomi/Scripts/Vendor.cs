using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Vendor
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("namespaces")]
        public Vendor.Namespaces namespaces;

        [JsonProperty("policyUrl")]
        public string PolicyUrl { get; set; }

        [JsonProperty("purposeIds")]
        public IList<string> PurposeIds { get; set; } = new List<string>();

        [JsonProperty("legIntPurposeIds")]
        public IList<string> LegIntPurposeIds { get; set; } = new List<string>();

        [JsonProperty("featureIds")]
        public IList<string> FeatureIds { get; set; } = new List<string>();

        [JsonProperty("flexiblePurposeIds")]
        public IList<string> FlexiblePurposeIds { get; set; } = new List<string>();

        [JsonProperty("specialFeatureIds")]
        public IList<string> SpecialFeatureIds { get; set; } = new List<string>();

        [JsonProperty("specialPurposeIds")]
        public IList<string> SpecialPurposeIds { get; set; } = new List<string>();

        [JsonProperty("urls")]
        public IList<Vendor.Url> Urls { get; set; }

        [Obsolete("Use PolicyUrl instead")]
        public string PrivacyPolicyUrl => PolicyUrl;

        public Vendor(
            string id,
            string name,
            Namespaces namespaces,
            string policyUrl,
            IList<string> purposeIds,
            IList<string> legIntPurposeIds,
            IList<string> featureIds,
            IList<string> flexiblePurposeIds,
            IList<string> specialFeatureIds,
            IList<string> specialPurposeIds,
            IList<Url> urls
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

        public Namespaces GetNamespaces()
        {
            return namespaces;
        }

        public void setNamespaces(Namespaces namespaces)
        {
            this.namespaces = namespaces;
        }

        [Serializable]
        public class Namespaces : Numerable
        {
            [JsonProperty("iab2")]
            public string Iab2 { get; set; }

            [JsonProperty("num")]
            public int? Num { get; set; }

            public Namespaces(string iab2, int? num)
            {
                this.Iab2 = iab2;
                this.Num = num;
            }
        }

        [Serializable]
        public class Url
        {
            [JsonProperty("langId")]
            public string LangId { get; set; }

            [JsonProperty("privacy")]
            public string Privacy { get; set; }

            [JsonProperty("legIntClaim")]
            public string LegIntClaim { get; set; }

            public Url(string langId, string privacy, string legIntClaim)
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
