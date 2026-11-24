using System.Collections.Generic;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Optional context for the current user.
    /// </summary>
    public class DidomiUserContext
    {
        [JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Attributes { get; }

        [JsonProperty("variables", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Variables { get; }

        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public int? Age { get; }

        [JsonProperty("isLoggedIn", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsLoggedIn { get; }

        public DidomiUserContext(
            IDictionary<string, string> attributes = null,
            IDictionary<string, string> variables = null,
            int? age = null,
            bool? isLoggedIn = null
        ) {
            Attributes = attributes;
            Variables = variables;
            Age = age;
            IsLoggedIn = isLoggedIn;
        }
    }

    /// <summary>
    /// Organization User parameters
    /// </summary>
    public class DidomiUserParameters
    {
        /// <summary>
        /// Main user authentication
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithoutParams</c> (ID only)</description>
        /// </item>
        /// </list>
        /// </summary>
        [JsonProperty("userAuth", NullValueHandling = NullValueHandling.Ignore)]
        public UserAuth UserAuth { get; }

        /// <summary>
        /// User authentication for Didomi Consent String (optional)
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </summary>
        [JsonProperty("dcsUserAuth", NullValueHandling = NullValueHandling.Ignore)]
        public UserAuthParams DcsUserAuth { get; }

        /// <summary>
        /// If the user is underage (<c>null</c> will keep the setting from initialization or from a previous call to <c>setUser</c>) 
        /// </summary>
        [JsonProperty("isUnderage", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsUnderage { get; }

        [JsonProperty("userContext", NullValueHandling = NullValueHandling.Ignore)]
        public DidomiUserContext UserContext { get; }

        /// <summary>
        /// User parameters for Didomi SDK
        /// </summary>
        /// <param name="userAuth">
        /// Main user authentication
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithoutParams</c> (ID only)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="dcsUserAuth">
        /// User authentication for Didomi Consent String (optional)
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="isUnderage">If the user is underage (<c>null</c> will keep the setting from initialization or from a previous call to <c>setUser</c>)</param>
        /// <param name="userContext">Optional context for the current user.</param>
        public DidomiUserParameters(
            UserAuth userAuth = null,
            UserAuthParams dcsUserAuth = null,
            bool? isUnderage = null,
            DidomiUserContext userContext = null
        ) {
            this.UserAuth = userAuth;
            this.DcsUserAuth = dcsUserAuth;
            this.IsUnderage = isUnderage;
            this.UserContext = userContext;
        }
    }

    /// <summary>
    /// Multi-User parameters
    /// </summary>
    public class DidomiMultiUserParameters : DidomiUserParameters
    {
        /// <summary>
        /// List of synchronized users (optional)
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </summary>
        [JsonProperty("synchronizedUsers", NullValueHandling = NullValueHandling.Ignore)]
        public IList<UserAuthParams> SynchronizedUsers { get; }

        /// <summary>
        /// Multi-User parameters for Didomi SDK
        /// </summary>
        /// <param name="userAuth">
        /// Main user authentication
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithoutParams</c> (ID only)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="dcsUserAuth">
        /// User authentication for Didomi Consent String (optional)
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="synchronizedUsers">
        /// List of synchronized users (optional)
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="isUnderage">If the user is underage (<c>null</c> will keep the setting from initialization or from a previous call to <c>setUser</c>)</param>
        /// <param name="userContext">Optional context for the current user.</param>
        public DidomiMultiUserParameters(
            UserAuth userAuth = null,
            UserAuthParams dcsUserAuth = null,
            IList<UserAuthParams> synchronizedUsers = null,
            bool? isUnderage = null,
            DidomiUserContext userContext = null
        ) : base(userAuth, dcsUserAuth, isUnderage, userContext) {
            this.SynchronizedUsers = synchronizedUsers;
        }
    }
}
