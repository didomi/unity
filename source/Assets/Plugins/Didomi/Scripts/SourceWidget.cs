using System;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Privacy Widget that collected the current consent.
    /// Beta: this API is in beta and may change in future releases.
    /// </summary>
    [Serializable]
    public class SourceWidget
    {
        /// <summary>
        /// Widget type, if defined in the widget catalog.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Widget id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Widget version id, if defined in the widget catalog.
        /// </summary>
        [JsonProperty("version_id")]
        public string VersionId { get; set; }

        public SourceWidget(
            string type,
            string id,
            string versionId
        )
        {
            Type = type;
            Id = id;
            VersionId = versionId;
        }
    }
}
