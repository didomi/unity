using System;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Regulation configuration defined in the console, applicable when the regulation is "custom".
    /// Beta: this API is in beta and may change in future releases.
    /// </summary>
    [Serializable]
    public class RegulationConfiguration
    {
        /// <summary>
        /// Regulation configuration id (regulation.config.id).
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Regulation configuration version id (regulation.config.versionId), if any.
        /// </summary>
        [JsonProperty("versionId")]
        public string VersionId { get; set; }

        /// <summary>
        /// Regulation configuration numerical id (regulation.config.numericalId), if any.
        /// </summary>
        [JsonProperty("numericalId")]
        public int? NumericalId { get; set; }

        /// <summary>
        /// Regulation name from the configuration (regulation.name), for example "gdpr".
        /// </summary>
        [JsonProperty("regulationName")]
        public string RegulationName { get; set; }

        /// <summary>
        /// Key under which the consent token is saved (regulation.cookies.didomiTokenStorageName).
        /// </summary>
        [JsonProperty("tokenStorageName")]
        public string TokenStorageName { get; set; }

        /// <summary>
        /// Whether the regulation configuration belongs to the "mixed" group (regulation.group).
        /// </summary>
        [JsonProperty("mixed")]
        public bool Mixed { get; set; } = false;

        public RegulationConfiguration(
            string id,
            string versionId,
            int? numericalId,
            string regulationName,
            string tokenStorageName,
            bool mixed
        )
        {
            Id = id;
            VersionId = versionId;
            NumericalId = numericalId;
            RegulationName = regulationName;
            TokenStorageName = tokenStorageName;
            Mixed = mixed;
        }
    }
}
