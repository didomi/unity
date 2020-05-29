
## IOS Issues and Solutions

### 1- Problem & Solution

When we move the extracted project to Mac MapFileParser.h permission denied. error is thrown when we build the project. To fix problem run below command on project directory.

chmod a+x MapFileParser.sh



### 2- Problem & Solution

SWIFT LANGUAGE VERSION` IS NOT SET IN XCODE BUILD SETTINGS WHEN A .SWIFT SOURCE PLUGIN IS ADDED TO AN UNITY PROJECT

This is a bug on Unity as described below link. 
https://issuetracker.unity3d.com/issues/ios-swift-language-version-is-not-set-in-xcode-build-settings-when-a-swift-source-plugin-is-added-to-an-unity-project

I got the problem on Unity 2019.3.6f. After I installed 2019.3.12 problem resolved.



### 3- Problem & Solution

Error: dyld: Library not loaded: @rpath/libswiftCore.dylib

if xcode project doesnot set ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES to yes you get above error.
In order to not do that manually after each build and extracting the xcode project,
we set the property at  OnPostProcessBuild method of PostProcessor. Below line added to fix problem.

proj.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");


### 4- Problem & Solution

if you are build for IOS simulator, it is crucial to set target SDK at IOS player settings at unity. Otherwise it will not run on ios simulator.
If you are getting the build of real device, remove the simulator. 


### 5- Problem & Solution

I run my tests simulator device 7.11.3

### 6- Problem & Solution

XCode 11.4.1  used for test.

### 7- Problem & Solution

When I move the extracted project from windows environment to mac(may it was already lost when I first moved the binary from mac to windows) on the cloud, binary in Didomi.framework lost it extension. 
With that version it didnot work. I copied the same file from mac build, and then it worked.
No fix yet, Enviromental issue. it needs to be fixed. 

### 8- Problem & Solution

Functions that will be used with C# from objective-c, must be put inside extern "C{ [code] }
Otherwise symbol not found error is displayed.

### 9- Problem & Solution

We move the didomi_config_ios.json to Data/Resources at OnPostProcessBuild method of PostProcessor
project.AddFileToBuild(targetGuid, fileGuid); if we didn't call add file to build simple copy operation didn't worked.

### 10- Problem & Solution

setupUI method for IOS UIviewConteroller is set to method from unity GLUnityView___ class
solved issue like didomi config_json put it to bundle and using UIviewConteroller of swift from objective C and put it as arg to setupui.

### 11- Problem & Solution

getDisabledPurposes
getDisabledVendors
getEnabledPurposes
getEnabledVendors
getPurpose
getRequiredPurposes
getRequiredVendors
getVendor

above functions have struct type as return type. For that reason @objc cannot be placed before function declaration.
The functions are not usable at objective-c. SO they are not usable at UNITY when you are build for IOS.


### 12- Problem & Solution

 IL2CPP doesnot allow marshalling instance methods.
In order to five function pointer to cpp, we have to give it as static method.

IL2CPP doesnot allow marshalling instance methods. So we made method static seperate method instead on instance method.

we added  [AOT.MonoPInvokeCallback(typeof(OnReadyDelegate))] to static method which is function pointer sent to CPP
This is must when managed method is sent. Learned by thrown exception.


### 13- Problem & Solution


EventListener doesnot have to implement interface on Objective C part. Below lines on swift makes the magic. and class in available as immplementer of the interface.
@objc(DDMEventListener)
public class EventListener: NSObject {
If you do it like me you will get error. unname type

### 14- Problem & Solution

il2cpp not supported error on newtonsoft.dll

DefaultJsonUtiliy class doesn't convert array, set and dictionary structures,
I have used NewtonSoft.dll json library. This also raised a new problem. It didn't work for ios. 
So I have used netstandard 2.0 library of newtonsoft to not get "il2cpp not supported". It worked but it is mentioned that this library can be used from Unity version 2018.1
at the following site https://docs.microsoft.com/en-us/dotnet/standard/net-standard.
Newtonsoft.dll version netstandard 2.0Unity version has dependency on Unity version 2018 but we already have version dependency for 2019 due to bug on https://issuetracker.unity3d.com/issues/ios-swift-language-version-is-not-set-in-xcode-build-settings-when-a-swift-source-plugin-is-added-to-an-unity-project


https://stackoverflow.com/questions/16359628/json-net-under-unity3d-for-ios
