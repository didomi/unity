# Unity version

We currently require Unity version 2020.3.46f1 or later. The plugin can be installed to Unity projects with version 2019.3.12f1 or later.

But for developing the Unity Plugin, please use exact version Unity 2020.3.46f1. If you try to open Unity plugin project with a different version, 
Unity will want to upgrade/downgrade the project. To not have such a problem, use exact Unity 2020.3.46f1 version for development of the plugin.

## 2020.3.46f1

Version 2020.3.46f1 is required because we need to target Android SDK 31. Targetting SDK 31 before this version will lead to a build error: <https://forum.unity.com/threads/error-during-build-gradle-project.1151459/>

There are workarounds, but they are tricky to put in place, and won't work on Unity Cloud Build (requires using an alternate version of the gradle wrapper).

## 2019.3.12f1

Version 2019.3.12f1 is required because we require Swift libraries: <https://issuetracker.unity3d.com/issues/ios-swift-language-version-is-not-set-in-xcode-build-settings-when-a-swift-source-plugin-is-added-to-an-unity-project>

## 2018.1

Version 2018.1 is required because we use NewtonSoft for JSON serialization as the `DefaultJsonUtility` class cannot serialize arrays, sets, and dictionaries.

We need to use the .NET standard library 2.0 to avoid the error "il2cpp not supported" from NetwonSoft and that requires Unity version 2018.1

Read more on <https://docs.microsoft.com/en-us/dotnet/standard/net-standard> and <https://stackoverflow.com/questions/16359628/json-net-under-unity3d-for-ios>;
