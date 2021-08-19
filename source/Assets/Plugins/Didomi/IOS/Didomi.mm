#import <Foundation/Foundation.h>
#import "Frameworks/Plugins/Didomi/IOS/Didomi.xcframework/ios-arm64_armv7/Didomi.framework/Headers/Didomi-Swift.h"

#pragma mark - C interface

/**
 * Objective-C++ code that exposes the SDK interface to Unity C#
 * When adding a new function from the SDK, update this file to also
 * reference that new function
 */

char* cStringCopy(const char* string){

     if (string == NULL){
          return NULL;
     }

     char* res = (char*)malloc(strlen(string)+1);
     strcpy(res, string);
     return res;
}



extern "C" {

void setupUI() {
  [[Didomi shared]       setupUIWithContainerController:UnityGetGLViewController()];
}


NSString* _Nonnull CreateNSString ( char* string)
{
    return [NSString stringWithUTF8String:string ?: ""];
}

NSString* _Nullable CreateNSStringNullable ( char* string)
{
	if(string==NULL)
	{
		return NULL;
	}
	
    return [NSString stringWithUTF8String:string ?: NULL];
}

void setUserAgent(char* name, char* version)
{
    [[Didomi shared]      setUserAgentWithName: CreateNSString(name) version:CreateNSString(version)];
}

void initialize( char* apiKey, char* localConfigurationPath, char* remoteConfigurationURL, char* providerId, bool disableDidomiRemoteConfig, char* languageCode)
{
    [[Didomi shared]      initializeWithApiKey: CreateNSString(apiKey) localConfigurationPath:CreateNSStringNullable(localConfigurationPath) remoteConfigurationURL:CreateNSStringNullable(remoteConfigurationURL)
        providerId:CreateNSStringNullable(providerId) disableDidomiRemoteConfig:disableDidomiRemoteConfig languageCode: CreateNSStringNullable(languageCode)];
}

void initializeWithNoticeId( char* apiKey, char* localConfigurationPath, char* remoteConfigurationURL, char* providerId, bool disableDidomiRemoteConfig, char* languageCode, char* noticeId)
{
    [[Didomi shared]      initializeWithApiKey: CreateNSString(apiKey) localConfigurationPath:CreateNSStringNullable(localConfigurationPath) remoteConfigurationURL:CreateNSStringNullable(remoteConfigurationURL)
        providerId:CreateNSStringNullable(providerId) disableDidomiRemoteConfig:disableDidomiRemoteConfig languageCode: CreateNSStringNullable(languageCode) noticeId: CreateNSStringNullable(noticeId)];
}

bool isReady()
{
	return [[Didomi shared]       isReady];
}

char* getTranslatedText(char* key)
{
    NSString *returnString = [[Didomi shared] getTranslatedTextWithKey: CreateNSString(key)];

    return cStringCopy([returnString UTF8String]);
}

bool getUserConsentStatusForPurpose(char* purposeId)
{
     return [[Didomi shared] getUserConsentStatusForPurposeWithPurposeId: CreateNSString(purposeId)];
}

bool getUserConsentStatusForVendor(char* vendorId)
{
    return [[Didomi shared] getUserConsentStatusForVendorWithVendorId: CreateNSString(vendorId)];
}

bool getUserConsentStatusForVendorAndRequiredPurposes(char* vendorId)
{
	return [[Didomi shared] getUserConsentStatusForVendorAndRequiredPurposesWithVendorId: CreateNSString(vendorId)];
}

int getUserLegitimateInterestStatusForPurpose(char* purposeId)
{
    return [[Didomi shared] getUserLegitimateInterestStatusForPurposeWithPurposeId: CreateNSString(purposeId)];
}
	
int getUserLegitimateInterestStatusForVendor(char* vendorId)
{
	return [[Didomi shared] getUserLegitimateInterestStatusForVendorWithVendorId: CreateNSString(vendorId)];
}

int getUserLegitimateInterestStatusForVendorAndRequiredPurposes(char* vendorId)
{
    return [[Didomi shared] getUserLegitimateInterestStatusForVendorAndRequiredPurposesWithVendorId: CreateNSString(vendorId)];
}

void hideNotice()
{
    [[Didomi shared] hideNotice];
}

void hidePreferences()
{
	[[Didomi shared] hidePreferences];
}

bool isConsentRequired()
{
	return [[Didomi shared] isConsentRequired];
}

bool isPreferencesVisible()
{
	return [[Didomi shared] isPreferencesVisible];
}

void showPreferences()
{
	[[Didomi shared] showPreferencesWithController:(nil) view:(ViewsPurposes)];
}

bool isUserConsentStatusPartial()
{
	return [[Didomi shared] isUserConsentStatusPartial];
}

void reset()
{
    [[Didomi shared] reset];
}

bool setUserAgreeToAll()
{
	return [[Didomi shared] setUserAgreeToAll];
}

bool setUserDisagreeToAll()
{
	return [[Didomi shared] setUserDisagreeToAll];
}

bool shouldConsentBeCollected()
{
	return [[Didomi shared] shouldConsentBeCollected];
}

void showNotice()
{
    [[Didomi shared] showNotice];
}

 char* ConvertSetToJsonText( NSSet<NSString *> * dataSet)
 {
	NSArray<NSString *> * dataArray= [dataSet allObjects ];

	NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dataArray options:NSJSONWritingPrettyPrinted error:nil];

    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

	NSLog(@"jsonData as string:\n%@", jsonString);

	return cStringCopy([jsonString UTF8String]);
 }


char* getDisabledPurposeIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getDisabledPurposeIds];
	
	return ConvertSetToJsonText(dataSet);    
}

char* getDisabledVendorIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getDisabledVendorIds];
	
	return ConvertSetToJsonText(dataSet);    
}

char* getEnabledPurposeIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getEnabledPurposeIds];
	
	return ConvertSetToJsonText(dataSet);    
}

char* getEnabledVendorIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getEnabledVendorIds];
	
	return ConvertSetToJsonText(dataSet);    
}

char* getJavaScriptForWebView()
{
    NSString *returnString = [[Didomi shared] getJavaScriptForWebViewWithExtra:@""];

    return cStringCopy([returnString UTF8String]);
}

char* getRequiredPurposeIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getRequiredPurposeIds];
	
	return ConvertSetToJsonText(dataSet);    
}

char* getRequiredVendorIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getRequiredVendorIds];
	
	return ConvertSetToJsonText(dataSet);    
}
 
 char* ConvertDictinaryToJsonText( NSDictionary<NSString *, NSString *> * dataDict)
 {
	NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dataDict options:NSJSONWritingPrettyPrinted error:nil];
	
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

	NSLog(@"jsonData dictionary as string:\n%@", jsonString);

	return cStringCopy([jsonString UTF8String]);
 }

char* getText(char* key)
{
    NSDictionary<NSString *, NSString *> * dataDict=[[Didomi shared] getTextWithKey:CreateNSString(key)];
	
	if(dataDict == nil)
	{
		return cStringCopy([@"" UTF8String]);
	}

	return ConvertDictinaryToJsonText(dataDict);
}

NSSet<NSString *> * ConvertJsonToSet(char* jsonText)
{
	NSString *jsonString=CreateNSString(jsonText);

	NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    
	NSArray *jsonArray = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:nil];

	NSSet<NSString *> * retval= [[NSSet alloc] initWithArray:jsonArray];

	return retval;
 }

bool setUserConsentStatus(char* enabledPurposeIds, char* disabledPurposeIds, char* enabledVendorIds, char* disabledVendorIds)
{
	NSSet<NSString *> * enabledPurposeIdsSet=ConvertJsonToSet(enabledPurposeIds);
	NSSet<NSString *> * disabledPurposeIdsSet=ConvertJsonToSet(disabledPurposeIds);
	NSSet<NSString *> * enabledVendorIdsSet=ConvertJsonToSet(enabledVendorIds);
	NSSet<NSString *> * disabledVendorIdsSet=ConvertJsonToSet(disabledVendorIds);

    return [[Didomi shared] setUserConsentStatusWithEnabledPurposeIds:enabledPurposeIdsSet disabledPurposeIds:enabledPurposeIdsSet enabledVendorIds:enabledVendorIdsSet disabledVendorIds:disabledVendorIdsSet];
}

bool setUserStatus(char* enabledConsentPurposeIds, char* disabledConsentPurposeIds, char* enabledLIPurposeIds, char* disabledLIPurposeIds, char* enabledConsentVendorIds, char* disabledConsentVendorIds, char* enabledLIVendorIds, char* disabledLIVendorIds)
{
	NSSet<NSString *> * enabledConsentPurposeIdsSet=ConvertJsonToSet(enabledConsentPurposeIds);
	NSSet<NSString *> * disabledConsentPurposeIdsSet=ConvertJsonToSet(disabledConsentPurposeIds);
	NSSet<NSString *> * enabledLIPurposeIdsSet=ConvertJsonToSet(enabledLIPurposeIds);
	NSSet<NSString *> * disabledLIPurposeIdsSet=ConvertJsonToSet(disabledLIPurposeIds);
	NSSet<NSString *> * enabledConsentVendorIdsSet=ConvertJsonToSet(enabledConsentVendorIds);
	NSSet<NSString *> * disabledConsentVendorIdsSet=ConvertJsonToSet(disabledConsentVendorIds);
	NSSet<NSString *> * enabledLIVendorIdsSet=ConvertJsonToSet(enabledLIVendorIds);
	NSSet<NSString *> * disabledLIVendorIdsSet=ConvertJsonToSet(disabledLIVendorIds);

    return [[Didomi shared] setUserStatusWithEnabledConsentPurposeIds:enabledConsentPurposeIdsSet disabledConsentPurposeIds:disabledConsentPurposeIdsSet enabledLIPurposeIds:enabledLIPurposeIdsSet disabledLIPurposeIds:disabledLIPurposeIdsSet enabledConsentVendorIds:enabledConsentVendorIdsSet disabledConsentVendorIds:disabledConsentVendorIdsSet enabledLIVendorIds:enabledLIVendorIdsSet disabledLIVendorIds:disabledLIVendorIdsSet];
}

bool setUserStatus1(BOOL purposesConsentStatus, BOOL purposesLIStatus, BOOL vendorsConsentStatus, BOOL vendorsLIStatus)
{
    return [[Didomi shared] setUserStatusWithPurposesConsentStatus:purposesConsentStatus purposesLIStatus:purposesLIStatus vendorsConsentStatus:vendorsConsentStatus vendorsLIStatus:vendorsLIStatus];
}

void updateSelectedLanguage(char* languageCode)
{
	return [[Didomi shared] updateSelectedLanguageWithLanguageCode: CreateNSString(languageCode)];
}

typedef void  (*callback_function)(void);

void onReady(callback_function pFunc)
{
    [[Didomi shared] onReadyWithCallback:^{
          pFunc();
      }];
}

typedef void  (*callback_error_function)( void);

void onError(callback_error_function errorFunc)
{
    [[Didomi shared] onErrorWithCallback:^(DDMErrorEvent * _Nonnull){
          errorFunc();
      }];
}


static DDMEventListener *eventListener = [[DDMEventListener alloc]init];

char* convertNSStringToCString(NSString * _Nullable nsString)
{
    if (nsString == NULL)
        return NULL;

    const char* nsStringUtf8 = [nsString UTF8String];
    // Create a null terminated C string on the heap so that our string's memory isn't wiped out right after method's return
    char* cString = (char*)malloc(strlen(nsStringUtf8) + 1);
    strcpy(cString, nsStringUtf8);

    return cString;
}

void addEventListener( void (*event_listener_handler) (int, char *))
{

    if(eventListener==nil)
	{
		eventListener = [[DDMEventListener alloc]init];
    }

    eventListener.onConsentChanged = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onHideNotice = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onReady = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onError = ^(DDMErrorEvent * errorEvent){

        int errorEventEnumValue = 1000;
        event_listener_handler(errorEventEnumValue , convertNSStringToCString(errorEvent.descriptionText));

    };

	eventListener.onShowNotice = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onNoticeClickAgree = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

    eventListener.onNoticeClickDisagree = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onNoticeClickMoreInfo = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickAgreeToAll = ^(enum DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickDisagreeToAll = ^(DDMEventType eventType){

        event_listener_handler(DDMEventTypePreferencesClickDisagreeToAll, NULL);

    };

	eventListener.onPreferencesClickPurposeAgree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };

	eventListener.onPreferencesClickPurposeDisagree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };

	eventListener.onPreferencesClickViewVendors = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickVendorAgree = ^(DDMEventType eventType, NSString * _Nullable vendorId){

        event_listener_handler(eventType, convertNSStringToCString(vendorId));

    };

	eventListener.onPreferencesClickVendorDisagree = ^(DDMEventType eventType, NSString * _Nullable vendorId){

        event_listener_handler(eventType, convertNSStringToCString(vendorId));

    };

	eventListener.onPreferencesClickVendorSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

   [[Didomi shared] addEventListenerWithListener:eventListener];

}

}




