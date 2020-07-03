using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Package
    {
        public static Package _instance;
        public static string _packageJsonContent = string.Empty;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadPackageJson()
        {
            var packageJsonContentTextAsset = Resources.Load<TextAsset>("package");
            if (packageJsonContentTextAsset != null)
            {
                _packageJsonContent = packageJsonContentTextAsset.text;
            }
        }

        public static Package GetInstance()
        {
            if (_instance == null)
            {
                try
                {
                    _instance = new Package();

                    //instead of using "JsonConvert.DeserializeObject<Package>(_packageJsonContent)"
                    //token based parsing used to fill Package. JsonConvert didnot worked.

                    StringReader jsonReaderString = new StringReader(_packageJsonContent);

                    using (JsonTextReader reader = new JsonTextReader(jsonReaderString))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        _instance.name = o2.Value<string>("name");
                        _instance.displayName = o2.Value<string>("displayName");
                        _instance.agentName = o2.Value<string>("agentName");
                        _instance.version = o2.Value<string>("version");
                        _instance.iosNativeVersion = o2.Value<string>("iosNativeVersion");
                        _instance.androidNativeVersion = o2.Value<string>("androidNativeVersion");
                        _instance.description = o2.Value<string>("description");
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log("Package exception:::");
                    Debug.Log(ex.ToString());
                    _instance = new Package
                    {
                        name = "com.didomi.unityplugin",
                        displayName = "Native Didomi",
                        agentName = "Unity SDK",
                        version = "unknownVersion",
                        iosNativeVersion = "unknownVersion",
                        androidNativeVersion = "unknownVersion",
                        description = "Unity plugin helps you to use native didomi functionality on Android and iOS."
                    };
                }
            }

            return _instance;
        }

        public string name { get; set; }
        public string displayName { get; set; }
        public string agentName { get; set; }
        public string version { get; set; }
        public string iosNativeVersion { get; set; }
        public string androidNativeVersion { get; set; }
        public string description { get; set; }
    }
}
