# Update the native Didomi Android SDK version

To update the native Didomi Android SDK, two steps need to be followed:

- Update the library in `package.json`
- Expose new or modified functions added to the updated version of the SDK

## Library

To update the version of the native Didomi Android SDK used in the plugin, modify the value of `androidNativeVersion` property in the package file at [`Assets/Plugins/Didomi/Resources/package.json`](../../source/Assets/Plugins/Didomi/Resources/package.json).

The list of the Android library versions can be found [`here`](https://developers.didomi.io/cmp/mobile-sdk/android/versions). 

## Functions

If the updated version of the Didomi Android SDK exposes new functions, removes functions, or updates the signature of existing functions, the code of the Unity plugin must be updated.

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

#### Add the function to the Android implementation

Add the following code to `Plugins/Didomi/Scripts/Android/AndroidDidomi.cs`:

```csharp
public ISet<string> GetDisabledPurposeIds()
{
    var obj = CallReturningJavaObjectMethod("getDisabledPurposeIds");

    return AndroidObjectMapper.ConvertToSetString(obj);
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
