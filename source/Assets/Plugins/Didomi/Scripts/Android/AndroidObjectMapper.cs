using System;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Convert objects from Java to Unity and back
    /// </summary>
    class AndroidObjectMapper
    {
        public static bool ConvertToBoolean(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new HashSet<Purpose>();

                var boolString = obj.Call<string>("toString");

                return bool.Parse(boolString); 
            }
            else
            {
                return false;
            }
        }

        public static ISet<Purpose> ConvertToSetPurpose(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new HashSet<Purpose>();

                var iteratorJavaObject = obj.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    var purposeJavaObject = iteratorJavaObject.Call<AndroidJavaObject>("next");

                    retval.Add(ConvertToPurpose(purposeJavaObject));
                }

                return retval;
            }

            return null;
        }

        public static Purpose ConvertToPurpose(AndroidJavaObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            var purpose = new Purpose(
                GetMethodStringValue(obj, "getId"),
                GetMethodStringValue(obj, "getName"),
                GetMethodStringValue(obj, "getDescriptionText")
            );

            return purpose;
        }

        public static ISet<Vendor> ConvertToSetVendor(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new HashSet<Vendor>();

                var iteratorJavaObject = obj.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    var vendorJavaObject = iteratorJavaObject.Call<AndroidJavaObject>("next");

                    retval.Add(ConvertToVendor(vendorJavaObject));
                }

                return retval;
            }

            return null;
        }

        public static Vendor ConvertToVendor(AndroidJavaObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            var vendor = new Vendor(
               GetMethodStringValue(obj, "getId"),
               GetMethodStringValue(obj, "getName"),
               GetMethodNamespacesValue(obj, "getNamespaces"),
               GetMethodStringValue(obj, "getPolicyUrl"),
               GetMethodListString(obj, "getPurposeIds"),
               GetMethodListString(obj, "getLegIntPurposeIds"),
               GetMethodListString(obj, "getFeatureIds"),
               GetMethodListString(obj, "getFlexiblePurposeIds"),
               GetMethodListString(obj, "getSpecialFeatureIds"),
               GetMethodListString(obj, "getSpecialPurposeIds"),
               GetMethodSetUrl(obj, "getUrls")
            );

            return vendor;
        }

        public static ISet<string> ConvertToSetString(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new HashSet<string>();

                var iteratorJavaObject = obj.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    retval.Add(iteratorJavaObject.Call<string>("next"));
                }

                return retval;
            }

            return null;
        }

        public static AndroidJavaObject ConvertFromHashSetStringToSetAndroidJavaObject(ISet<string> objSet)
        {
            if (objSet != null)
            {
                var hashSetJavaObject = new AndroidJavaObject("java.util.HashSet");

                //IntPtr methodPut = AndroidJNIHelper.GetMethodID(
                //    hashSetJavaObject.GetRawClass(),
                //    "add",
                //    "(Ljava/lang/String)Ljava/lang/Boolean;");

                IntPtr methodPut = AndroidJNIHelper.GetMethodID(
                   hashSetJavaObject.GetRawClass(),
                   "add",
                   "(Ljava/lang/String);");

                foreach (var item in objSet)
                {
                    
                    var itemJavaObject = new AndroidJavaObject("java.lang.String", item);

                    var args = new object[1];
                    args[0] = itemJavaObject;

                    AndroidJNI.CallBooleanMethod(hashSetJavaObject.GetRawObject(), methodPut, AndroidJNIHelper.CreateJNIArgArray(args));
                }

                return hashSetJavaObject;
            }

            return null;
        }

        public static AndroidJavaObject ConvertToBooleanAndroidJavaObject(bool? obj) 
        {
            if (obj != null)
            {
                return new AndroidJavaObject("java.lang.Boolean", obj);
            }

            return null;
        }

        public static IDictionary<string, string> ConvertToDictionary(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new Dictionary<string, string>();

                var setJavaObject = obj.Call<AndroidJavaObject>("keySet");

                var iteratorJavaObject = setJavaObject.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    var key = iteratorJavaObject.Call<string>("next");
                    var val = obj.Call<string>("get", key);
                    retval.Add(key, val);
                }

                return retval;
            }

            return null;
        }

        public static string GetMethodStringValue(AndroidJavaObject obj, string methodName)
        {
            if (obj != null)
            {
                return obj.Call<string>(methodName);
            }

            return null;
        }

        public static bool GetMethodBoolValue(AndroidJavaObject obj, string methodName)
        {
            if (obj != null)
            {
                return obj.Call<bool>(methodName);
            }

            return false;
        }

        public static IList<string> GetMethodListString(AndroidJavaObject obj, string methodName)
        {
            if (obj != null)
            {
                var androidJavaListObject = obj.Call<AndroidJavaObject>(methodName);
                if (androidJavaListObject != null)
                {
                    var retval = new List<string>();

                    var size=androidJavaListObject.Call<int>("size");

                    for (int i = 0; i < size; i++)
                    {
                        retval.Add(androidJavaListObject.Call<string>("get", i));
                    }

                    return retval;
                }
            }

            return null;
        }
        
        public static Vendor.Namespaces GetMethodNamespacesValue(AndroidJavaObject obj, string methodName)
        {
            if (obj != null)
            {
                var androidJavaListObject = obj.Call<AndroidJavaObject>(methodName);
                if (androidJavaListObject != null)
                {
                    return ConvertToVendorNamespaces(androidJavaListObject);
                }
            }

            return null;
        }

        public static Vendor.Namespaces ConvertToVendorNamespaces(AndroidJavaObject obj)
        {
            var iab2 = obj.Call<string>("getIab2");
            var num = obj.Call<int>("getNum");
            return new Vendor.Namespaces(iab2, num);
        }

        public static ISet<Vendor.Url> GetMethodSetUrl(AndroidJavaObject obj, string methodName)
        {
            if (obj != null)
            {
                var androidJavaListObject = obj.Call<AndroidJavaObject>(methodName);
                if (androidJavaListObject != null)
                {
                    var retval = new HashSet<Vendor.Url>();

                    var size=androidJavaListObject.Call<int>("size");

                    for (int i = 0; i < size; i++)
                    {
                        var javaObjectVal = androidJavaListObject.Call<AndroidJavaObject>("get", i);
                        retval.Add(ConvertToVendorUrl(javaObjectVal));
                    }

                    return retval;
                }
            }

            return null;
        }

        public static Vendor.Url ConvertToVendorUrl(AndroidJavaObject obj)
        {
            var langId = obj.Call<string>("getLangId");
            var privacy = obj.Call<string>("getPrivacy");
            var legIntClaim = obj.Call<string>("getLegIntClaim");
            return new Vendor.Url(langId, privacy, legIntClaim);
        }

        public static Dictionary<string, CurrentUserStatus.PurposeStatus> ConvertToPurposeStatusDictionary(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new Dictionary<string, CurrentUserStatus.PurposeStatus>();

                var setJavaObject = obj.Call<AndroidJavaObject>("keySet");

                var iteratorJavaObject = setJavaObject.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    var key = iteratorJavaObject.Call<string>("next");
                    var javaObjectVal = obj.Call<AndroidJavaObject>("get", key);
                    var val = ConvertToCurrentUserStatusPurpose(javaObjectVal);
                    retval.Add(key, val);
                }

                return retval;
            }

            return null;
        }

        public static Dictionary<string, CurrentUserStatus.VendorStatus> ConvertToVendorStatusDictionary(AndroidJavaObject obj)
        {
            if (obj != null)
            {
                var retval = new Dictionary<string, CurrentUserStatus.VendorStatus>();

                var setJavaObject = obj.Call<AndroidJavaObject>("keySet");

                var iteratorJavaObject = setJavaObject.Call<AndroidJavaObject>("iterator");

                while (iteratorJavaObject.Call<bool>("hasNext"))
                {
                    var key = iteratorJavaObject.Call<string>("next");
                    var javaObjectVal = obj.Call<AndroidJavaObject>("get", key);
                    var val = ConvertToCurrentUserStatusVendor(javaObjectVal);
                    retval.Add(key, val);
                }

                return retval;
            }

            return null;
        }

        public static CurrentUserStatus ConvertToCurrentUserStatus(AndroidJavaObject obj)
        {
            var regulation = obj.Call<AndroidJavaObject>("getRegulation");
            var currentUserStatus = new CurrentUserStatus(
                purposes: ConvertToPurposeStatusDictionary(obj.Call<AndroidJavaObject>("getPurposes")),
                vendors: ConvertToVendorStatusDictionary(obj.Call<AndroidJavaObject>("getVendors")),
                userId: obj.Call<string>("getUserId"),
                created: obj.Call<string>("getCreated"),
                updated: obj.Call<string>("getUpdated"),
                consentString: obj.Call<string>("getConsentString"),
                additionalConsent: obj.Call<string>("getAdditionalConsent"),
                regulation: regulation.Call<string>("getValue"),
                didomiDcs: obj.Call<string>("getDidomiDcs"),
                gppString: obj.Call<string>("getGppString")
            );

            return currentUserStatus;
        }

        public static CurrentUserStatus.VendorStatus ConvertToCurrentUserStatusVendor(AndroidJavaObject obj)
        {
            var id = obj.Call<string>("getId");
            var enabled = obj.Call<bool>("getEnabled");
            return new CurrentUserStatus.VendorStatus(id, enabled);
        }

        public static CurrentUserStatus.PurposeStatus ConvertToCurrentUserStatusPurpose(AndroidJavaObject obj)
        {
            var id = obj.Call<string>("getId");
            var enabled = obj.Call<bool>("getEnabled");
            return new CurrentUserStatus.PurposeStatus(id, enabled);
        }

        public static UserStatus ConvertToUserStatus(AndroidJavaObject obj)
        {
            var regulation = obj.Call<AndroidJavaObject>("getRegulation");
            var userStatus = new UserStatus(
                purposes: ConvertToUserStatusPurposes(obj.Call<AndroidJavaObject>("getPurposes")),
                vendors: ConvertToUserStatusVendors(obj.Call<AndroidJavaObject>("getVendors")),
                userId: obj.Call<string>("getUserId"),
                created: obj.Call<string>("getCreated"),
                updated: obj.Call<string>("getUpdated"),
                consentString: obj.Call<string>("getConsentString"),
                additionalConsent: obj.Call<string>("getAdditionalConsent"),
                regulation: regulation.Call<string>("getValue"),
                didomiDcs: obj.Call<string>("getDidomiDcs"),
                gppString: obj.Call<string>("getGppString")
            );

            return userStatus;
        }

        public static UserStatus.Purposes ConvertToUserStatusPurposes(AndroidJavaObject obj)
        {
            var purposes = new UserStatus.Purposes(
                global: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getGlobal")),
                consent: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getConsent")),
                legitimateInterest: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getLegitimateInterest")),
                essential: ConvertToSetString(obj.Call<AndroidJavaObject>("getEssential"))
            );
            return purposes;
        }

        public static UserStatus.Vendors ConvertToUserStatusVendors(AndroidJavaObject obj)
        {
            var vendors = new UserStatus.Vendors(
                global: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getGlobal")),
                globalConsent: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getGlobalConsent")),
                globalLegitimateInterest: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getGlobalLegitimateInterest")),
                consent: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getConsent")),
                legitimateInterest: ConvertToUserStatusIds(obj.Call<AndroidJavaObject>("getLegitimateInterest"))
            );
            return vendors;
        }

        public static UserStatus.Ids ConvertToUserStatusIds(AndroidJavaObject obj)
        {
            var ids = new UserStatus.Ids(
                enabled: ConvertToSetString(obj.Call<AndroidJavaObject>("getEnabled")),
                disabled: ConvertToSetString(obj.Call<AndroidJavaObject>("getDisabled"))
            );
            return ids;
        }

        public static AndroidJavaObject ConvertToJavaDidomiInitializeParameters(DidomiInitializeParameters parameters)
        {
                return new AndroidJavaObject(
                    "io.didomi.sdk.DidomiInitializeParameters",
                    parameters.apiKey,
                    parameters.localConfigurationPath,
                    parameters.remoteConfigurationUrl,
                    parameters.providerId,
                    parameters.disableDidomiRemoteConfig,
                    parameters.languageCode,
                    parameters.noticeId,
                    parameters.tvNoticeId,
                    parameters.androidTvEnabled,
                    parameters.countryCode,
                    parameters.regionCode,
                    parameters.isUnderage);
        }

        public static AndroidJavaObject ConvertToJavaDidomiUserParameters(DidomiUserParameters parameters, AndroidJavaObject activity) {
            var isUnderage = ConvertToBooleanAndroidJavaObject(parameters.IsUnderage);
            if (parameters is DidomiMultiUserParameters)
            {
                DidomiMultiUserParameters multiUserParameters = (DidomiMultiUserParameters)parameters;
                return new AndroidJavaObject(
                    "io.didomi.sdk.DidomiMultiUserParameters",
                    ConvertToJavaUserAuth(multiUserParameters.UserAuth),
                    ConvertToJavaUserAuthParams(multiUserParameters.DcsUserAuth),
                    ConvertToJavaUserAuthList(multiUserParameters.SynchronizedUsers),
                    activity,
                    isUnderage);
            }
            else
            {
                return new AndroidJavaObject(
                    "io.didomi.sdk.DidomiUserParameters",
                    ConvertToJavaUserAuth(parameters.UserAuth),
                    ConvertToJavaUserAuthParams(parameters.DcsUserAuth),
                    activity,
                    isUnderage);
            }
        }

        public static AndroidJavaObject ConvertToJavaUserAuth(UserAuth parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            
            if (parameters is UserAuthWithoutParams) {
                return new AndroidJavaObject(
                    "io.didomi.sdk.user.model.UserAuthWithoutParams",
                    parameters.Id);
            } else {
                return ConvertToJavaUserAuthParams((UserAuthParams) parameters);
            }
        }

        public static AndroidJavaObject ConvertToJavaUserAuthParams(UserAuthParams parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            AndroidJavaObject expiration = ConvertToJavaLong(parameters.Expiration);
            if (parameters is UserAuthWithEncryptionParams)
            {
                UserAuthWithEncryptionParams encryptionParameters = (UserAuthWithEncryptionParams)parameters;
                return new AndroidJavaObject(
                    "io.didomi.sdk.user.model.UserAuthWithEncryptionParams",
                    encryptionParameters.Id,
                    encryptionParameters.Algorithm,
                    encryptionParameters.SecretId,
                    encryptionParameters.InitializationVector,
                    expiration);
            } else {
                UserAuthWithHashParams hashParameters = (UserAuthWithHashParams)parameters;
                return new AndroidJavaObject(
                    "io.didomi.sdk.user.model.UserAuthWithHashParams",
                    hashParameters.Id,
                    hashParameters.Algorithm,
                    hashParameters.SecretId,
                    hashParameters.Digest,
                    hashParameters.Salt,
                    expiration);
            }
        }

        public static AndroidJavaObject ConvertToJavaUserAuthList(IList<UserAuthParams> userAuthParams)
        {
            if (userAuthParams == null)
            {
                return null;
            }

            var arrayListJavaObject = new AndroidJavaObject("java.util.ArrayList");

            foreach (var item in userAuthParams)
            {

                var itemJavaObject = ConvertToJavaUserAuthParams(item);

                arrayListJavaObject.Call<bool>("add", itemJavaObject);
            }

            return arrayListJavaObject;
        }

        public static AndroidJavaObject ConvertToJavaCurrentUserStatus(CurrentUserStatus status)
        {
            AndroidJavaObject purposesMap = new AndroidJavaObject("java.util.HashMap");
            foreach (var id in status.Purposes.Keys)
            {
                var purpose = status.Purposes[id];
                AndroidJavaObject javaPurpose = new AndroidJavaObject("io.didomi.sdk.models.CurrentUserStatus$PurposeStatus",
                    purpose.Id,
                    purpose.Enabled
                );

                purposesMap.Call<AndroidJavaObject>("put", id, javaPurpose);
            }

            AndroidJavaObject vendorsMap = new AndroidJavaObject("java.util.HashMap");
            foreach (var id in status.Vendors.Keys)
            {
                var vendor = status.Vendors[id];
                AndroidJavaObject javaVendor = new AndroidJavaObject("io.didomi.sdk.models.CurrentUserStatus$VendorStatus",
                    vendor.Id,
                    vendor.Enabled
                );

                vendorsMap.Call<AndroidJavaObject>("put", id, javaVendor);
            }

            return new AndroidJavaObject(
                "io.didomi.sdk.models.CurrentUserStatus",
                purposesMap,
                vendorsMap
            );
        }

        public static AndroidJavaObject ConvertToJavaLong(long? longValue)
        {
            if (longValue == null)
            {
                return null;
            }
            return new AndroidJavaObject("java.lang.Long", longValue);
        }
    }
}
