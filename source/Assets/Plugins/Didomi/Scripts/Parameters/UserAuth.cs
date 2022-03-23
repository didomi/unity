
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
        public string Id { get; }

        public string Algorithm { get; }

        public string SecretId { get; }

        public string InitializationVector { get; }

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
        public string Id { get; }

        public string Algorithm { get; }

        public string SecretId { get; }

        public string Digest { get; }

        public string Salt { get; }

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
