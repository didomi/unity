using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IO.Didomi.SDK
{
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
        [JsonProperty("userAuth")]
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
        public DidomiUserParameters(
            UserAuth userAuth,
            UserAuthParams dcsUserAuth = null,
            bool? isUnderage = null
        ) {
            this.UserAuth = userAuth ?? throw new ArgumentNullException(nameof(userAuth));
            this.DcsUserAuth = dcsUserAuth;
            this.IsUnderage = isUnderage;
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
        public DidomiMultiUserParameters(
            UserAuth userAuth,
            UserAuthParams dcsUserAuth = null,
            IList<UserAuthParams> synchronizedUsers = null,
            bool? isUnderage = null
        ) : base(userAuth, dcsUserAuth, isUnderage) {
            this.SynchronizedUsers = synchronizedUsers;
        }
    }
}
