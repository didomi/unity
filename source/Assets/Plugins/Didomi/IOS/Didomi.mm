#import <Foundation/Foundation.h>







#import "Frameworks/Plugins/Didomi/IOS/Didomi.framework/Headers/Didomi-Swift.h"




#pragma mark - C interface




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















void initialize( char* apiKey, char* localConfigurationPath, char* remoteConfigurationURL, char* providerId, bool disableDidomiRemoteConfig,char* languageCode)







{







    [[Didomi shared]      initializeWithApiKey: CreateNSString(apiKey) localConfigurationPath:CreateNSString(localConfigurationPath) remoteConfigurationURL:CreateNSString(remoteConfigurationURL)







        providerId:CreateNSString(providerId)







                     disableDidomiRemoteConfig:disableDidomiRemoteConfig languageCode: CreateNSString(languageCode)];



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



	[[Didomi shared] showPreferences];



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





static DDMEventListener *eventListener = [[DDMEventListener alloc]init];



void addEventListener( void (*event_listener_handler) (DDMEventType, NSString * _Nullable ))



{

    if(eventListener==nil)

	{

  

	eventListener = [[DDMEventListener alloc]init];



    }

    


    

    eventListener.onConsentChanged = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onHideNotice = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onReady = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onShowNotice = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onNoticeClickAgree = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onNoticeClickMoreInfo = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onPreferencesClickAgreeToAll = ^(enum DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onPreferencesClickDisagreeToAll = ^(DDMEventType eventType){

        event_listener_handler(DDMEventTypePreferencesClickDisagreeToAll,@"");

    };



	eventListener.onPreferencesClickPurposeAgree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, purposeId);

    };



	eventListener.onPreferencesClickPurposeDisagree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, purposeId);

    };



	eventListener.onPreferencesClickViewVendors = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onPreferencesClickSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



	eventListener.onPreferencesClickVendorAgree = ^(DDMEventType eventType, NSString * _Nullable vendorId){

        event_listener_handler(eventType, vendorId);

    };



	eventListener.onPreferencesClickVendorDisagree = ^(DDMEventType eventType, NSString * _Nullable vendorId){

        event_listener_handler(eventType, vendorId);

    };



	eventListener.onPreferencesClickVendorSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType,@"");

    };



   [[Didomi shared] addEventListenerWithListener:eventListener];

}







}















