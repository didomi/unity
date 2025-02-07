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
        public UserAuth userAuth { get; }

        /// <summary>
        /// User authentication for Didomi Consent String
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </summary>
        [JsonProperty("dcsUserAuth")]
        public UserAuthParams dcsUserAuth { get; }

        /// <summary>
        /// If the user is underage (<c>null</c> will keep the setting from initialization or from a previous call to <c>setUser</c>) 
        /// </summary>
        [JsonProperty("isUnderage")]
        public bool? isUnderage { get; }

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
        /// User authentication for Didomi Consent String
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
            this.userAuth = userAuth;
            this.dcsUserAuth = dcsUserAuth;
            this.isUnderage = isUnderage;
        }
    }

    /// <summary>
    /// Multi-User parameters
    /// </summary>
    public class DidomiMultiUserParameters : DidomiUserParameters
    {
        /// <summary>
        /// List of synchronized users
        /// <list type="bullet">
        /// <item>
        /// <description><c>UserAuthWithEncryptionParams</c> (encryption)</description>
        /// </item>
        /// <item>
        /// <description><c>UserAuthWithHashParams</c> (hash)</description>
        /// </item>
        /// </list>
        /// </summary>
        [JsonProperty("synchronizedUsers")]
        public IList<UserAuthParams> synchronizedUsers { get; }

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
        /// User authentication for Didomi Consent String
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
        /// List of synchronized users
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
            this.synchronizedUsers = synchronizedUsers;
        }
    }
}
