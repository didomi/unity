# How to Update Android native related files on a change

This document intends to show files and code that must be updated on Didomi Unity Plugin when a change occurs on Android native sdk.

The document shows adding new functionality scenario. Update and delete scenarios are similar.

To add GetDisabledPurposeIds functionality, You need to do following changes.

## Add below code to Plugins/Didomi/Scripts/Didomi.cs

```csharp
public ISet<string> GetDisabledPurposeIds()
        {
            return didomiForPlatform.GetDisabledPurposeIds();
        }
```

## Add below code to Plugins/Didomi/Scripts/Interfaces/IDidomi.cs

```csharp
ISet<string> GetDisabledPurposeIds();
```

## Add below code to Plugins/Didomi/Scripts/Android/AndroidDidomi.cs

```csharp
public ISet<string> GetDisabledPurposeIds()
        {
            var obj = CallReturningJavaObjectMethod("getDisabledPurposeIds");

            return AndroidObjectMapper.ConvertToSetString(obj);
        }
```

## Add below code to Plugins/Didomi/Scripts/DemoGUI.cs 

```csharp
if (GUI.Button(GetFuncRect2(), "GetDisabledPurposeIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetDisabledPurposeIds();
            message += "GetDisabledPurposeIds" + MessageForObject(retval);
        }
```

