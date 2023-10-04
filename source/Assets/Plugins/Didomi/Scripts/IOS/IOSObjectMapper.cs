using Assets.Plugins.Scripts.IOS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

