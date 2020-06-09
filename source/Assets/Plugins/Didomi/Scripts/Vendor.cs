using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Vendor
    {
        private string id;

        private string name;

        private string privacyPolicyUrl;

        /// <summary>
        /// Vendor namespace (iab, didomi or custom)
        /// </summary>
        private string @namespace;

        /// <summary>
        /// Purposes with legal basis "consent"
        /// </summary>
        private IList<string> purposeIds;

        /// <summary>
        /// Purposes with legal basis "legitimate interest"
        /// </summary>
        private IList<string> legIntPurposeIds;

        /// <summary>
        /// For custom or Didomi vendors, we allow mapping back to IAB IDs to override IAB vendors definition
        /// </summary>
        private string iabId;

        public Vendor(
            string id,
            string name,
            string privacyPolicyUrl,
            string @namespace,
                IList<string> purposeIds,
                IList<string> legIntPurposeIds,
                string iabId
            )
        {
            this.id = id;
            this.name = name;
            this.privacyPolicyUrl = privacyPolicyUrl;
            this.@namespace = @namespace;
            this.purposeIds = purposeIds;
            this.legIntPurposeIds = legIntPurposeIds;
            this.iabId = iabId;
        }

        public string GetId()
        {
            return id;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public string GetName()
        {
            return name;
        }

        public string GetPrivacyPolicyUrl()
        {
            return privacyPolicyUrl;
        }

        public string GetNamespace() { return @namespace; }

        public IList<string> GetPurposeIds()
        {
            if (purposeIds == null)
            {
                purposeIds = new List<string>();
            }

            return purposeIds;
        }

        public IList<string> GetLegIntPurposeIds()
        {
            if (legIntPurposeIds == null)
            {
                legIntPurposeIds = new List<string>();
            }

            return legIntPurposeIds;
        }

        public void SetPurposeIds(List<string> purposeIds)
        {
            this.purposeIds = purposeIds;
        }

        public void SetLegIntPurposeIds(List<string> legIntPurposeIds)
        {
            this.legIntPurposeIds = legIntPurposeIds;
        }

        public void SetNamespace(string @namespace)
        {
            this.@namespace = @namespace;
        }

        public string GetIabId()
        {
            return iabId;
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
                vendorIds.Add(vendor.GetId());
            }

            return vendorIds;
        }
    }
}
