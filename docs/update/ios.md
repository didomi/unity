# Update the native Didomi iOS SDK version

To update the native Didomi iOS SDK, two steps need to be followed:

- Update the library shipped in the Unity plugin
- Expose new or modified functions added to the updated version of the SDK

## Library

To update the native iOS library embedded in the plugin, copy the `Didomi.framework` folders from the published Didomi iOS SDK `Didomi.xcframework/ios-arm64_armv7` and `Didomi.xcframework/ios-arm64_i386_x86_64-simulator` folders into [`Plugins/Didomi/IOS/Didomi.xcframework`](../../source/Plugins/Didomi/IOS). Make sure the `.meta` files of `Plugins/Didomi/IOS/Didomi.xcframework/ios-arm64_armv7` and `Plugins/Didomi/IOS/Didomi.xcframework/ios-arm64_i386_x86_64-simulator` are not modified (discard these files using git if necessary).
The iOS SDK release can be found [`on our servers`](https://developers.didomi.io/cmp/mobile-sdk/ios/setup#manually).

## Functions

If the updated version of the Didomi iOS SDK exposes new functions, removes functions, or updates the signature of existing functions, the code of the Unity plugin must be updated.

We show how to add new functions below. Updating or removing functions is a similar process.

To add a new function named `GetDisabledPurposeIds`, do the following changes:

#### Add the function to the C# interface

Add the following code to `Plugins/Didomi/Scripts/Interfaces/IDidomi.cs`:

```csharp
ISet<string> GetDisabledPurposeIds();
```

#### Add the function to the C# implementation for the Unity app

Add the following code to `Plugins/Didomi/Scripts/Didomi.cs`:

```csharp
public ISet<string> GetDisabledPurposeIds()
{
    return didomiForPlatform.GetDisabledPurposeIds();
}
```

#### Add the function to the iOS implementation

Add the following code to `Plugins/Didomi/Scripts/IOS/IOSDidomi.cs`:

```csharp
public ISet<string> GetDisabledPurposeIds()
{
    var jsonText = DidomiFramework.GetDisabledPurposeIds();

    return IOSObjectMapper.ConvertToSetString(jsonText);
}
```

Add the following code to `Plugins/Didomi/Scripts/IOS/DidomiFramework.cs`:

```csharp
[DllImport("__Internal")]
private static extern string getDisabledPurposeIds();

public static string GetDisabledPurposeIds()
{
    //#if UNITY_IOS && !UNITY_EDITOR
    return getDisabledPurposeIds();
    //#endif

    return string.Empty;
}
```

Add the following code to `Plugins/Didomi/IOS/Didomi.mm`:

```csharp
char* getDisabledPurposeIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getDisabledPurposeIds];
 
 return ConvertSetToJsonText(dataSet);
}
```

#### Add the function to the sample app

Add a button named `GetDisabledPurposeIds` to the sample app.

Add the following code to `sample/Scripts/DemoGUI.cs`:

```csharp
if (GUI.Button(GetFuncRect2(), "GetDisabledPurposeIds"))
{
    message = string.Empty;
    var retval = Didomi.GetInstance().GetDisabledPurposeIds();
    message += "GetDisabledPurposeIds" + MessageForObject(retval);
}
```
