
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    public interface UserAuthParams
    {
        string Id { get; }

        string Algorithm { get; }

        string SecretId { get; }

        long? Expiration { get; }
    }

    public class UserAuthWithEncryptionParams : UserAuthParams
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("secretId")]
        public string SecretId { get; }

        [JsonProperty("iv")]
        public string InitializationVector { get; }

        [JsonProperty("expiration")]
        public long? Expiration { get; }

        public UserAuthWithEncryptionParams(string id, string algorithm, string secretId, string initializationVector, long? expiration = null)
        {
            this.Id = id;
            this.Algorithm = algorithm;
            this.SecretId = secretId;
            this.InitializationVector = initializationVector;
            this.Expiration = expiration; 
        }
    }

    public class UserAuthWithHashParams : UserAuthParams
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("secretId")]
        public string SecretId { get; }

        [JsonProperty("digest")]
        public string Digest { get; }

        [JsonProperty("salt")]
        public string Salt { get; }

        [JsonProperty("expiration")]
        public long? Expiration { get; }

        public UserAuthWithHashParams(string id, string algorithm, string secretId, string digest, string salt, long? expiration = null)
        {
            this.Id = id;
            this.Algorithm = algorithm;
            this.SecretId = secretId;
            this.Digest = digest;
            this.Salt = salt;
            this.Expiration = expiration;
        }
    }
}
