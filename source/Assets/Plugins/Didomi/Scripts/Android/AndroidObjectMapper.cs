using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var purpose = new Purpose(
                GetMethodStringValue(obj, "getId"),
                GetMethodStringValue(obj, "getIabId"),
                GetMethodStringValue(obj, "getName"),
                GetMethodStringValue(obj, "getDescription"));

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
            var vendor = new Vendor(
               GetMethodStringValue(obj, "getId"),
               GetMethodStringValue(obj, "getName"),
               GetMethodStringValue(obj, "getPrivacyPolicyUrl"),
               GetMethodStringValue(obj, "getNamespace"),
               GetMethodListString(obj, "getPurposeIds"),
               GetMethodListString(obj, "getLegIntPurposeIds"),
               GetMethodStringValue(obj, "getIabId"));

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

        public static UserStatus ConvertToUserStatus(AndroidJavaObject obj)
        {
            var userStatus = new UserStatus(
                purposes: ConvertToUserStatusPurposes(obj.Call<AndroidJavaObject>("getPurposes")),
                vendors: ConvertToUserStatusVendors(obj.Call<AndroidJavaObject>("getVendors")),
                userId: obj.Call<string>("getUserId"),
                created: obj.Call<string>("getCreated"),
                updated: obj.Call<string>("getUpdated"),
                consentString: obj.Call<string>("getConsentString"),
                additionalConsent: obj.Call<string>("getAdditionalConsent")
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
                    parameters.androidTvEnabled);
        }

        public static AndroidJavaObject ConvertToJavaUserAuthParams(UserAuthParams parameters)
        {
            AndroidJavaObject expiration = ConvertToJavaLong(parameters.Expiration);
            if (parameters is UserAuthWithEncryptionParams)
            {
                UserAuthWithEncryptionParams encryptionParameters = (UserAuthWithEncryptionParams)parameters;
                return new AndroidJavaObject(
                    "io.didomi.sdk.user.UserAuthWithEncryptionParams",
                    encryptionParameters.Id,
                    encryptionParameters.Algorithm,
                    encryptionParameters.SecretId,
                    encryptionParameters.InitializationVector,
                    expiration);
            } else {
                UserAuthWithHashParams hashParameters = (UserAuthWithHashParams)parameters;
                return new AndroidJavaObject(
                    "io.didomi.sdk.user.UserAuthWithHashParams",
                    hashParameters.Id,
                    hashParameters.Algorithm,
                    hashParameters.SecretId,
                    hashParameters.Digest,
                    hashParameters.Salt,
                    expiration);
            }
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
