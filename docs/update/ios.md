# Update the native XCFramework used by the plugin

The Didomi Unity plugin uses an XCFramework which includes all the binaries for iOS and tvOS for both device and simulator.

To update the native XCFramework used by the plugin, the following steps are required:

- Update XCFramework version
- Update API only if required

## Update XCFramework version

In order to update the XCFramework used by the plugin, you need to specify the version that you want to use in the `iosNativeVersion` property of the [`package.json`](https://github.com/didomi/unity/blob/master/source/Assets/Plugins/Didomi/Resources/package.json) file.

This will include in the resulting Xcode project the binaries for both simulator and device.

The list of the iOS library versions can be found [`here`](https://developers.didomi.io/cmp/mobile-sdk/ios/versions).

## Update API only if required

If the updated version of the XCFramework exposes new functions, removes functions, or updates the signature of existing functions, the code of the Unity plugin must be updated.

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

#### Add the function to the iOS or tvOS implementation

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
    //#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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

### Events

If there are any changes in the `EventListener` class (events added or removed), make sure the file `Assets/Plugins/Didomi/Scripts/iOS/DDMEventType.cs` is properly updated. The enum values must be in the same order as in the `EventListener` class.
