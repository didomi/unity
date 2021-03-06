# Update the native Didomi Android SDK version

To update the native Didomi Android SDK, two steps need to be followed:

- Update the library shipped in the Unity plugin
- Expose new or modified functions added to the updated version of the SDK

## Library

To update the native `aar` library embedded in the plugin, copy the `android-didomi.aar` file from the published Didomi Android SDK into [`Plugins/Didomi/Android/libs`](../../source/Plugins/Didomi/Android/libs).

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
