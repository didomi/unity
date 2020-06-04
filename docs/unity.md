# Unity version

We currently require Unity version 2019.3.12f1 or later.

## 2019.3.12f1

Version 2019.3.12f1 is required because we require Swift libraries: <https://issuetracker.unity3d.com/issues/ios-swift-language-version-is-not-set-in-xcode-build-settings-when-a-swift-source-plugin-is-added-to-an-unity-project>

## 2018.1

Version 2018.1 is required because we use NewtonSoft for JSON serialization as the `DefaultJsonUtility` class cannot serialize arrays, sets, and dictionaries.

We need to use the .NET standard library 2.0 to avoid the error "il2cpp not supported" from NetwonSoft and that requires Unity version 2018.1

Read more on <https://docs.microsoft.com/en-us/dotnet/standard/net-standard> and <https://stackoverflow.com/questions/16359628/json-net-under-unity3d-for-ios>;
