## Android Issues and Solutions

### 1- Problem & Solution

Didomi-android-sdk setupUI takes AppCompatActivity class as argument but unity generates UnityPlayerActivity class which is extending from Activitiy.
We are not able to set it as argument to setupUI method. With the default settings we are not able use didomi-android-sdk. In order to solve the issue,
At PostProcessor.OnPostProcessBuild we changed the generated code. We change UnityPlayerActivity so that it extends from  AppCompatActivity class instead of Activitiy class. 

Default Generated:
public class UnityPlayerActivity extends Activity implements IUnityPlayerLifecycleEvents

After PostProcessor.OnPostProcessBuild generated code updated like below.

import android.support.v7.app.AppCompatActivity;

public class UnityPlayerActivity extends AppCompatActivity implements IUnityPlayerLifecycleEvents

### 2- Problem & Solution

Since we updated the Activity class to AppCompatActivity we also need to update unityLibrary\src\main\res\values\styles.xml  
so that we can set AppCompat theme to the activity
At PostProcessor.OnPostProcessBuild we added the below line to styles.

```xml
<style name=""DidomiTheme"" parent =""Theme.AppCompat.Light.DarkActionBar""
```

And at PostProcessor.OnPostProcessBuild  we updated unityLibrary\src\main\AndroidManifest.xml file
 we replaced line @"android:theme=""@style/UnityThemeSelector"""; with 

```xml
  @"android:theme=""@style/DidomiTheme""";
```

At the end we now set the theme of android AppCompat theme. 

### 3- Problem & Solution

 Crush while loading.
styles at diodmi-andorid-sdk are not found error is thrown by default. To solve at 
PostProcessor.OnPostProcessBuild we updated the dependecies and add required libs to "unityLibrary\build.gradle file.

Added dependencies to unityLibrary\build.gradle file:

```java
     implementation fileTree(include: ['*.jar'], dir: 'libs')
    implementation 'com.android.support:appcompat-v7:27.1.1'
    implementation 'com.android.support:design:27.1.1'
    implementation 'com.google.android.gms:play-services-ads:15.0.1'
    implementation "android.arch.lifecycle:extensions:1.1.0"
    implementation 'android.arch.lifecycle:viewmodel:1.1.0'
    // Force customtabs 27.1.1 as com.google.android.gms:play-services-ads:15.0.1 depends on 26.0.1 by default
    // See https://stackoverflow.com/questions/50009286/gradle-mixing-versions-27-1-1-and-26-1-0
    implementation 'com.android.support:customtabs:27.1.1'
    api 'com.iab.gdpr_android:gdpr_android:1.0.1'
    api 'com.google.code.gson:gson:2.8.5'
    api 'com.rm:rmswitch:1.2.2'
```

### 4- Problem & Solution

since diodmi-andorid-sdk uses appcompat-v7:27.1.1, For Unity  target sdk must be 27 at android player settings.    

![Target SDK](img\android_target_sdk_setting.png)
