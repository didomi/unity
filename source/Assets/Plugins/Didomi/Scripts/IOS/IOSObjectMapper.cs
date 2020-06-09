using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IO.Didomi.SDK.IOS
{
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
                    return JsonConvert.DeserializeObject<Dictionary<string,string>>(jsonText);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            return new Dictionary<string, string>(); ;
        }

        public static string ConvertFromHasSetStringToJson(ISet<string> objSet)
        {
            if (objSet != null)
            {
                return JsonConvert.SerializeObject(objSet.ToArray());
            }

            return "[]";
        }
    }
}

