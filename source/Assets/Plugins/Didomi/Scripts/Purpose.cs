using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Purpose
    {
        /// <summary>
        /// Purpose id used by Didomi SDK.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Purpose human-readable name, translated in selected language.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Purpose description, translated in selected language.
        /// </summary>
        [JsonProperty("descriptionText")]
        public string DescriptionText { get; set; } = "";

        [Obsolete("Use DescriptionText instead")]
        public string Description => DescriptionText;

        public Purpose(string id, string name, string descriptionText)
        {
            this.Id = id;
            this.Name = name;
            this.DescriptionText = descriptionText;
        }

        /// <summary>
        /// Get IDs from a list of purposes
        /// </summary>
        /// <param name="purposes"></param>
        /// <returns></returns>
        public static HashSet<string> getIds(ICollection<Purpose> purposes)
        {
            HashSet<string> purposeIds = new HashSet<string>();

            foreach (Purpose purpose in purposes)
            {
                purposeIds.Add(purpose.Id);
            }

            return purposeIds;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (!(obj is Purpose)) {
                return false;
            }

            return ((Purpose)obj).Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return base.GetHashCode();
            }
            else
            {
                return Id.GetHashCode();
            }
        }
    }
}
