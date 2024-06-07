﻿using Assets.Plugins.Scripts.IOS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static IO.Didomi.SDK.CurrentUserStatus;

namespace IO.Didomi.SDK.IOS
{
    /// <summary>
    /// Main class used to convert objects required by IOS Plugin.
    /// </summary>
    class IOSObjectMapper
    {
        public static ISet<string> ConvertToSetString(string jsonText)
        {
            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                var vals = new string[0];
                try
                {
                    vals = JsonConvert.DeserializeObject<string[]>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }

                return new HashSet<string>(vals);
            }

            return new HashSet<string>();
        }

        public static IDictionary<string, string> ConvertToDictionary(string jsonText)
        {
            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return new Dictionary<string, string>(); ;
        }

        public static string ConvertFromHashSetStringToJson(ISet<string> objSet)
        {
            if (objSet != null)
            {
                return JsonConvert.SerializeObject(objSet.ToArray());
            }

            return "[]";
        }

        public static string ConvertFromPurposeStatusDictionaryToJson(Dictionary<string, PurposeStatus> purposes)
        {
            if (purposes != null)
            {
                return JsonConvert.SerializeObject(purposes);
            }

            return "[]";
        }

        public static string ConvertFromUserAuthParamsListToJson(IList<UserAuthParams> userAuthParams)
        {
            if (userAuthParams != null)
            {
                return JsonConvert.SerializeObject(userAuthParams.ToArray());
            }

            return null;
        }

        public static string ConvertFromVendorsStatusDictionaryToJson(Dictionary<string, VendorStatus> vendors)
        {
            if (vendors != null)
            {
                return JsonConvert.SerializeObject(vendors);
            }

            return "[]";
        }

        public static CurrentUserStatus ConvertToCurrentUserStatus(string jsonText)
        {
            CurrentUserStatus result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<CurrentUserStatus>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static VendorStatus ConvertToVendorStatus(string jsonText)
        {
            VendorStatus result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<VendorStatus>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static UserStatus ConvertToUserStatus(string jsonText)
        {
            UserStatus result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<UserStatus>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static Purpose ConvertToPurpose(string jsonText)
        {
            Purpose result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<Purpose>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static ISet<Purpose> ConvertToPurposeSet(string jsonText)
        {
            ISet<Purpose> result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    // We have to convert it to list first, as direct conversion to HashSet fails on iOS
                    List<Purpose> resultAsList = JsonConvert.DeserializeObject<List<Purpose>>(jsonText);
                    result = new HashSet<Purpose>(resultAsList);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static Vendor ConvertToVendor(string jsonText)
        {
            Vendor result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<Vendor>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public static ISet<Vendor> ConvertToVendorSet(string jsonText)
        {
            ISet<Vendor> result = null;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                try
                {
                    // We have to convert it to list first, as direct conversion to HashSet fails on iOS
                    List<Vendor> resultAsList = JsonConvert.DeserializeObject<List<Vendor>>(jsonText);
                    result = new HashSet<Vendor>(resultAsList);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return result;
        }

        public class JsonSetStringConverter : JsonConverter<ISet<string>>
        {
            public JsonSetStringConverter() { }

            public override ISet<string> ReadJson(JsonReader reader, Type objectType, ISet<string> existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                ISet<string> result = null;
                JArray array = JArray.Load(reader);
                if (array == null)
                {
                    result = new HashSet<string>();
                }
                else
                {
                    // We have to convert it to list first, as direct conversion to HashSet fails on iOS
                    List<string> resultAsList = array.ToObject<List<string>>();
                    result = new HashSet<string>(resultAsList);
                }
                return result;
            }

            public override void WriteJson(JsonWriter writer, ISet<string> value, JsonSerializer serializer)
            {
                // Not used
                throw new NotImplementedException();
            }
        }

        public class JsonListStringConverter : JsonConverter<IList<string>>
        {
            public JsonListStringConverter() { }

            public override IList<string> ReadJson(JsonReader reader, Type objectType, IList<string> existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                IList<string> result;
                JArray array = JArray.Load(reader);
                if (array == null)
                {
                    result = new List<string>();
                }
                else
                {
                    result = array.ToObject<List<string>>();
                }
                return result;
            }

            public override void WriteJson(JsonWriter writer, IList<string> value, JsonSerializer serializer)
            {
                // Not used
                throw new NotImplementedException();
            }
        }

        public class JsonSetVendorUrlConverter : JsonConverter<ISet<Vendor.Url>>
        {
            public JsonSetVendorUrlConverter() { }

            public override ISet<Vendor.Url> ReadJson(JsonReader reader, Type objectType, ISet<Vendor.Url> existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                ISet<Vendor.Url> result = null;
                JArray array = JArray.Load(reader);
                if (array == null)
                {
                    result = new HashSet<Vendor.Url>();
                }
                else
                {
                    // We have to convert it to list first, as direct conversion to HashSet fails on iOS
                    List<Vendor.Url> resultAsList = array.ToObject<List<Vendor.Url>>();
                    result = new HashSet<Vendor.Url>(resultAsList);
                }
                return result;
            }

            public override void WriteJson(JsonWriter writer, ISet<Vendor.Url> value, JsonSerializer serializer)
            {
                // Not used
                throw new NotImplementedException();
            }
        }

        public class JsonRegulationConverter : JsonConverter<string>
        {
            public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Integer)
                {
                    int intValue = Convert.ToInt32(reader.Value);
                    if (Enum.IsDefined(typeof(DDMRegulation), intValue))
                    {
                        return Enum.GetName(typeof(DDMRegulation), intValue);
                    }
                }

                return Enum.GetName(typeof(DDMRegulation), DDMRegulation.none);
            }

            public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
            {
                // Not used
                throw new NotImplementedException();
            }
        }
    }
}

