using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IO.Didomi.SDK
{
    public class Package
    {
        public static Package _instance;

        public static Package GetInstance()
        {
            if (_instance == null)
            {
                try
                {
                    var packageJsonContent = Resources.Load<TextAsset>("package");
                    _instance = JsonConvert.DeserializeObject<Package>(packageJsonContent.text);
                    Debug.Log(packageJsonContent.text);
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

        public string name;
        public string displayName;
        public string agentName;
        public string version;
        public string iosNativeVersion;
        public string androidNativeVersion;
        public string description;
    }
}
