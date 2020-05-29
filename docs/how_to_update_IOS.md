# How to Update IOS native related files on a change 

This document intends to show files and code that must be updated on Didomi Unity Plugin when a change occurs on IOS native sdk.

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

## Add below code to Plugins/Didomi/Scripts/Interfaces/IDidomi.cs

```csharp
ISet<string> GetDisabledPurposeIds();
```

## Add below code to Plugins/Didomi/Scripts/IOS/IOSDidomi.cs

```csharp
public ISet<string> GetDisabledPurposeIds()
        {
            var jsonText = DidomiFramework.GetDisabledPurposeIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }
```

## Add below code to Plugins/Didomi/Scripts/IOS/DidomiFramework.cs

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

## Add below code to Plugins/Didomi/IOS/Didomi.mm

```csharp
char* getDisabledPurposeIds()
{

    NSSet<NSString *> * dataSet=[[Didomi shared] getDisabledPurposeIds];
	
	return ConvertSetToJsonText(dataSet);    
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
