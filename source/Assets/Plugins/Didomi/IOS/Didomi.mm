#import <Foundation/Foundation.h>
#import <Didomi/Didomi-Swift.h>

#pragma mark - C interface

char* cStringCopy(const char* string){

     if (string == NULL){
          return NULL;
     }

     char* res = (char*)malloc(strlen(string)+1);
     strcpy(res, string);
     return res;
}

/**
 Method used to create a dictionary based on an instance of DDMUserStatusIDs.
 */
NSDictionary<NSString *, NSArray<NSString *> *> * CreateDictionaryFromStatusIDs(DDMUserStatusIDs *ids)
{
    return @{
        @"enabled": [[ids enabled] allObjects],
        @"disabled": [[ids disabled] allObjects]
    };
}

int convertBoolToInt(bool value)
{
    return value ? 1 : 0;
}

char* ConvertSetToJsonText(NSSet<NSString *> * dataSet)
{
   NSArray<NSString *> * dataArray= [dataSet allObjects ];

   NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dataArray options:NSJSONWritingPrettyPrinted error:nil];

   NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

   NSLog(@"jsonData as string:\n%@", jsonString);

   return cStringCopy([jsonString UTF8String]);
}

char* ConvertComplexDictionaryArrayToJsonText(NSArray<NSDictionary *> * dataArray)
{
   NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dataArray options:NSJSONWritingPrettyPrinted error:nil];

   NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

   NSLog(@"jsonData as string:\n%@", jsonString);

   return cStringCopy([jsonString UTF8String]);
}

/**
 Method used to create a string from a dictionary that doesn't have the form <NSString *, NSString *>.
 */
char* ConvertComplexDictionaryToString(NSDictionary * dataDict)
{
   NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dataDict options:NSJSONWritingPrettyPrinted error:nil];
   NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
   NSLog(@"jsonData dictionary as string:\n%@", jsonString);
   return cStringCopy([jsonString UTF8String]);
}

/**
 Method used to map current user status from DDMCurrentUserStatus to a JSON string.
 */
char* MapCurrentUserStatus(DDMCurrentUserStatus *currentUserStatus) {
    // String properties
    NSString *userID = [currentUserStatus userID];
    NSString *created = [currentUserStatus created];
    NSString *updated = [currentUserStatus updated];
    NSString *consentString = [currentUserStatus consentString];
    NSString *additionalConsent = [currentUserStatus additionalConsent];
    // Enum becomes integer in Objective-C
    NSInteger regulation = [currentUserStatus regulation];
    NSString *didomiDCS = [currentUserStatus didomiDCS];
    
    NSMutableDictionary *vendorsStatusJson = [NSMutableDictionary dictionary];
    NSDictionary<NSString *, DDMCurrentUserStatusVendor *> *vendorsStatus = [currentUserStatus vendors];
    for (NSString *key in [vendorsStatus allKeys]) {
        DDMCurrentUserStatusVendor *vendorStatus = vendorsStatus[key];
        vendorsStatusJson[key] = @{
            @"id": [vendorStatus id],
            @"enabled": [NSNumber numberWithInt: convertBoolToInt([vendorStatus enabled])]
        };
    }
    
    NSMutableDictionary *purposesStatusJson = [NSMutableDictionary dictionary];
    NSDictionary<NSString *, DDMCurrentUserStatusPurpose *> *purposesStatus = [currentUserStatus purposes];
    for (NSString *key in [purposesStatus allKeys]) {
        DDMCurrentUserStatusPurpose *purposeStatus = purposesStatus[key];
        purposesStatusJson[key] = @{
            @"id": [purposeStatus id],
            @"enabled": [NSNumber numberWithInt: convertBoolToInt([purposeStatus enabled])]
        };
    }
    
    NSDictionary *dictionary = @{
        @"user_id": userID,
        @"created" : created,
        @"updated": updated,
        @"consent_string": consentString,
        @"addtl_consent": additionalConsent,
        @"regulation": [NSNumber numberWithLong: regulation],
        @"didomi_dcs": didomiDCS,
        @"purposes": purposesStatusJson,
        @"vendors": vendorsStatusJson
    };
    
    return ConvertComplexDictionaryToString(dictionary);
}

/**
 Map vendor status from DDMCurrentUserStatusVendor to a JSON string.
 */
char* MapCurrentUserStatusVendor(DDMCurrentUserStatusVendor *vendorStatus) {
    NSDictionary *vendorStatusJson = @{
        @"id": [vendorStatus id],
        @"enabled": [NSNumber numberWithInt: convertBoolToInt([vendorStatus enabled])]
    };
    return ConvertComplexDictionaryToString(vendorStatusJson);
}

/**
 Method used to map user status from DDMUserStatus to a JSON string.
 */
char* MapUserStatus(DDMUserStatus *userStatus) {
    // String properties
    NSString *userID = [userStatus userID];
    NSString *created = [userStatus created];
    NSString *updated = [userStatus updated];
    NSString *consentString = [userStatus consentString];
    NSString *additionalConsent = [userStatus additionalConsent];
    // Enum becomes integer in Objective-C
    NSInteger regulation = [userStatus regulation];
    NSString *didomiDCS = [userStatus didomiDCS];
    
    DDMUserStatusPurposes *purposeStatus = [userStatus purposes];
    DDMUserStatusVendors *vendorsStatus = [userStatus vendors];
    
    NSDictionary *dictionary = @{
        @"user_id": userID,
        @"created" : created,
        @"updated": updated,
        @"consent_string": consentString,
        @"addtl_consent": additionalConsent,
        @"regulation": [NSNumber numberWithLong: regulation],
        @"didomi_dcs": didomiDCS,
        @"purposes": @{
            @"consent": CreateDictionaryFromStatusIDs([purposeStatus consent]),
            @"legitimate_interest": CreateDictionaryFromStatusIDs([purposeStatus legitimateInterest]),
            @"global": CreateDictionaryFromStatusIDs([purposeStatus global]),
            @"essential": [[purposeStatus essential] allObjects]
        },
        @"vendors": @{
            @"consent": CreateDictionaryFromStatusIDs([vendorsStatus consent]),
            @"legitimate_interest": CreateDictionaryFromStatusIDs([vendorsStatus legitimateInterest]),
            @"global": CreateDictionaryFromStatusIDs([vendorsStatus global]),
            @"global_consent": CreateDictionaryFromStatusIDs([vendorsStatus globalConsent]),
            @"global_li": CreateDictionaryFromStatusIDs([vendorsStatus globalLegitimateInterest])
        }
    };
    
    return ConvertComplexDictionaryToString(dictionary);
}

/**
 Method used to map purpose from DDMPurpose to a NSDictionary
 */
NSDictionary* MapPurposeToDictionary(DDMPurpose *purpose) {
    NSString *id = [purpose id];
    NSString *name = [purpose name];
    NSString *descriptionText = [purpose descriptionText];
    
    return @{
        @"id": id,
        @"name" : name,
        @"descriptionText": descriptionText
    };
}

/**
 Method used to map purpose from DDMPurpose to a JSON string.
 */
char* MapPurposeToJsonText(DDMPurpose *purpose) {
    NSDictionary *dictionary = MapPurposeToDictionary(purpose);
    return ConvertComplexDictionaryToString(dictionary);
}

id ObjectOrNull(id object) {
  return object ?: [NSNull null];
}

/**
 Method used to map vendor from DDMVendor to a Dictionary.
 */
NSDictionary* MapVendorToDictionary(DDMVendor *vendor) {
    NSString *id = [vendor id];
    NSString *name = [vendor name];
    NSString *policyUrl = [vendor policyUrl];

    NSArray<NSString *> * purposeIDs = [[vendor purposeIDs] allObjects];
    NSArray<NSString *> * legIntPurposeIDs = [[vendor legIntPurposeIDs] allObjects];
    NSArray<NSString *> * featureIDs = [[vendor featureIDs] allObjects];
    NSArray<NSString *> * flexiblePurposeIDs = [[vendor flexiblePurposeIDs] allObjects];
    NSArray<NSString *> * specialPurposeIDs = [[vendor specialPurposeIDs] allObjects];
    NSArray<NSString *> * specialFeatureIDs = [[vendor specialFeatureIDs] allObjects];

    NSDictionary *vendorNamespacesJson = nil;
    DDMVendorNamespaces *namespaces = [vendor namespaces];
    if (namespaces) {
        vendorNamespacesJson = @{
            @"iab2": ObjectOrNull(namespaces.iab2),
        };
    }

    NSMutableArray<NSDictionary *> *vendorUrlsJson = [NSMutableArray array];
    NSArray<DDMVendorURL *> *vendorUrls = [vendor urls];
    for (DDMVendorURL *vendorUrl in vendorUrls) {
        [vendorUrlsJson addObject: @{
            @"langId": ObjectOrNull([vendorUrl langID]),
            @"privacy": ObjectOrNull([vendorUrl privacy]),
            @"legIntClaim": ObjectOrNull([vendorUrl legIntClaim])
        }];
    }
    
    return @{
        @"id": id,
        @"name" : name,
        @"namespaces" : ObjectOrNull(vendorNamespacesJson),
        @"policyUrl": ObjectOrNull(policyUrl),
        @"purposeIds": purposeIDs,
        @"legIntPurposeIds": legIntPurposeIDs,
        @"featureIds": featureIDs,
        @"flexiblePurposeIds": flexiblePurposeIDs,
        @"specialPurposeIds": specialPurposeIDs,
        @"specialFeatureIds": specialFeatureIDs,
        @"urls": ObjectOrNull(vendorUrlsJson)
    };
}

/**
 Method used to map vendor from DDMVendor to a JSON string.
 */
char* MapVendorToJsonText(DDMVendor *vendor) {
    NSDictionary *dictionary = MapVendorToDictionary(vendor);
    return ConvertComplexDictionaryToString(dictionary);
}

/**
 * Objective-C++ code that exposes the SDK interface to Unity C#
 * When adding a new function from the SDK, update this file to also
 * reference that new function
 */

extern "C" {

void setupUI() {
  [[Didomi shared] setupUIWithContainerController:UnityGetGLViewController()];
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

char* ConvertPurposeArrayToJsonText(NSArray<DDMPurpose *> * dataArray)
{
    NSMutableArray<NSDictionary *> * jsonArray= [NSMutableArray arrayWithCapacity:dataArray.count ];
    
    for (DDMPurpose *purpose in dataArray) {
        [jsonArray addObject: MapPurposeToDictionary(purpose)];
    }

    return ConvertComplexDictionaryArrayToJsonText(jsonArray);
}

char* ConvertVendorArrayToJsonText(NSArray<DDMVendor *> * dataArray)
{
    NSMutableArray<NSDictionary *> * jsonArray= [NSMutableArray arrayWithCapacity:dataArray.count ];
    
    for (DDMVendor *vendor in dataArray) {
        [jsonArray addObject: MapVendorToDictionary(vendor)];
    }

    return ConvertComplexDictionaryArrayToJsonText(jsonArray);
}

void setUserAgent(char* name, char* version)
{
    [[Didomi shared] setUserAgentWithName: CreateNSString(name) version:CreateNSString(version)];
}

void initializeWithParameters(char* apiKey, char* localConfigurationPath, char* remoteConfigurationURL, char* providerId, bool disableDidomiRemoteConfig, char* languageCode, char* noticeId)
{
    DidomiInitializeParameters *parameters = [[DidomiInitializeParameters alloc] initWithApiKey: CreateNSString(apiKey) localConfigurationPath: CreateNSStringNullable(localConfigurationPath) remoteConfigurationURL: CreateNSStringNullable(remoteConfigurationURL) providerID: CreateNSStringNullable(providerId) disableDidomiRemoteConfig: disableDidomiRemoteConfig languageCode: CreateNSStringNullable(languageCode) noticeID: CreateNSStringNullable(noticeId)];
    [[Didomi shared] initialize: parameters];
}

int isReady()
{
    return convertBoolToInt([[Didomi shared] isReady]);
}

char* getTranslatedText(char* key)
{
    NSString *returnString = [[Didomi shared] getTranslatedTextWithKey: CreateNSString(key)];

    return cStringCopy([returnString UTF8String]);
}

char* getCurrentUserStatus()
{
    Didomi *didomi = [Didomi shared];
    DDMCurrentUserStatus *currentUserStatus = [didomi getCurrentUserStatus];
    return MapCurrentUserStatus(currentUserStatus);
}

char* getUserStatus()
{
    Didomi *didomi = [Didomi shared];
    DDMUserStatus *userStatus = [didomi getUserStatus];
    return MapUserStatus(userStatus);
}

void hideNotice()
{
    [[Didomi shared] hideNotice];
}

void hidePreferences()
{
    [[Didomi shared] hidePreferences];
}

int isConsentRequired()
{
    return convertBoolToInt([[Didomi shared] isConsentRequired]);
}

int isPreferencesVisible()
{
    return convertBoolToInt([[Didomi shared] isPreferencesVisible]);
}

int isNoticeVisible()
{
    return convertBoolToInt([[Didomi shared] isNoticeVisible]);
}

void showPreferences()
{
    [[Didomi shared] showPreferencesWithController:(UnityGetGLViewController()) view:(ViewsPurposes)];
}

int isUserConsentStatusPartial()
{
    return convertBoolToInt([[Didomi shared] isUserConsentStatusPartial]);
}

int isUserStatusPartial()
{
    return convertBoolToInt([[Didomi shared] isUserStatusPartial]);
}

int isUserLegitimateInterestStatusPartial()
{
    return convertBoolToInt([[Didomi shared] isUserLegitimateInterestStatusPartial]);
}

void reset()
{
    [[Didomi shared] reset];
}

int setUserAgreeToAll()
{
    return convertBoolToInt([[Didomi shared] setUserAgreeToAll]);
}

int setUserDisagreeToAll()
{
    return convertBoolToInt([[Didomi shared] setUserDisagreeToAll]);
}

int shouldConsentBeCollected()
{
    return convertBoolToInt([[Didomi shared] shouldConsentBeCollected]);
}

int shouldUserStatusBeCollected()
{
    return convertBoolToInt([[Didomi shared] shouldUserStatusBeCollected]);
}

void showNotice()
{
    [[Didomi shared] showNotice];
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

char* getRequiredPurposes()
{
    NSArray<DDMPurpose *> * dataSet=[[Didomi shared] getRequiredPurposes];
    return ConvertPurposeArrayToJsonText(dataSet);
}

char* getPurpose(char* purposeId)
{
    DDMPurpose *purpose = [[Didomi shared] getPurposeWithPurposeId:CreateNSString(purposeId)];
    return MapPurposeToJsonText(purpose);
}

char* getRequiredVendorIds()
{
    NSSet<NSString *> * dataSet=[[Didomi shared] getRequiredVendorIds];
    
    return ConvertSetToJsonText(dataSet);
}

char* getRequiredVendors()
{
    NSArray<DDMVendor *> * dataSet=[[Didomi shared] getRequiredVendors];
    return ConvertVendorArrayToJsonText(dataSet);
}

char* getVendor(char* vendorId)
{
    DDMVendor *vendor = [[Didomi shared] getVendorWithVendorId:CreateNSString(vendorId)];
    return MapVendorToJsonText(vendor);
}
 
char* convertDictionaryToJsonText( NSDictionary<NSString *, NSString *> * dataDict)
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

    return convertDictionaryToJsonText(dataDict);
}

NSSet<NSString *> * ConvertJsonToSet(char* jsonText)
{
    NSString *jsonString=CreateNSString(jsonText);

    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    
    NSArray *jsonArray = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:nil];

    NSSet<NSString *> * retval= [[NSSet alloc] initWithArray:jsonArray];

    return retval;
}

BOOL GetBoolField(NSDictionary *data, NSString *key)
{
    NSNumber *fieldValue = data[key];
    return [fieldValue boolValue];
}

NSDictionary<NSString *, DDMCurrentUserStatusPurpose *> * ConvertJsonToCurrentUserStatusPurposes(char* jsonText)
{
    NSString *jsonString=CreateNSString(jsonText);

    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    
    NSDictionary *jsonDictionary = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:nil];

    NSMutableDictionary *convertedDictionary = [NSMutableDictionary dictionary];
    for (NSString *key in jsonDictionary) {
        NSDictionary *jsonPurpose = jsonDictionary[key];
        DDMCurrentUserStatusPurpose *purpose = [[DDMCurrentUserStatusPurpose alloc] initWithId:jsonPurpose[@"id"] enabled:GetBoolField(jsonPurpose, @"enabled")];
        [convertedDictionary setObject:purpose forKey:key];
    }

    return convertedDictionary;
}

NSDictionary<NSString *, DDMCurrentUserStatusVendor *> * ConvertJsonToCurrentUserStatusVendors(char* jsonText)
{
    NSString *jsonString=CreateNSString(jsonText);

    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    
    NSDictionary *jsonDictionary = [NSJSONSerialization JSONObjectWithData:jsonData options:kNilOptions error:nil];

    NSMutableDictionary *convertedDictionary = [NSMutableDictionary dictionary];
    for (NSString *key in jsonDictionary) {
        NSDictionary *jsonVendor = jsonDictionary[key];
        DDMCurrentUserStatusVendor *vendor = [[DDMCurrentUserStatusVendor alloc] initWithId:jsonVendor[@"id"] enabled:GetBoolField(jsonVendor, @"enabled")];
        [convertedDictionary setObject:vendor forKey:key];
    }

    return convertedDictionary;
}

int setUserStatus(char* enabledConsentPurposeIds, char* disabledConsentPurposeIds, char* enabledLIPurposeIds, char* disabledLIPurposeIds, char* enabledConsentVendorIds, char* disabledConsentVendorIds, char* enabledLIVendorIds, char* disabledLIVendorIds)
{
	NSSet<NSString *> * enabledConsentPurposeIdsSet=ConvertJsonToSet(enabledConsentPurposeIds);
	NSSet<NSString *> * disabledConsentPurposeIdsSet=ConvertJsonToSet(disabledConsentPurposeIds);
	NSSet<NSString *> * enabledLIPurposeIdsSet=ConvertJsonToSet(enabledLIPurposeIds);
	NSSet<NSString *> * disabledLIPurposeIdsSet=ConvertJsonToSet(disabledLIPurposeIds);
	NSSet<NSString *> * enabledConsentVendorIdsSet=ConvertJsonToSet(enabledConsentVendorIds);
	NSSet<NSString *> * disabledConsentVendorIdsSet=ConvertJsonToSet(disabledConsentVendorIds);
	NSSet<NSString *> * enabledLIVendorIdsSet=ConvertJsonToSet(enabledLIVendorIds);
	NSSet<NSString *> * disabledLIVendorIdsSet=ConvertJsonToSet(disabledLIVendorIds);

    bool result = [[Didomi shared] setUserStatusWithEnabledConsentPurposeIds:enabledConsentPurposeIdsSet disabledConsentPurposeIds:disabledConsentPurposeIdsSet enabledLIPurposeIds:enabledLIPurposeIdsSet disabledLIPurposeIds:disabledLIPurposeIdsSet enabledConsentVendorIds:enabledConsentVendorIdsSet disabledConsentVendorIds:disabledConsentVendorIdsSet enabledLIVendorIds:enabledLIVendorIdsSet disabledLIVendorIds:disabledLIVendorIdsSet];
    return convertBoolToInt(result);
}

int setUserStatus1(BOOL purposesConsentStatus, BOOL purposesLIStatus, BOOL vendorsConsentStatus, BOOL vendorsLIStatus)
{
    bool result = [[Didomi shared] setUserStatusWithPurposesConsentStatus:purposesConsentStatus purposesLIStatus:purposesLIStatus vendorsConsentStatus:vendorsConsentStatus vendorsLIStatus:vendorsLIStatus];
    return convertBoolToInt(result);
}

int setCurrentUserStatus(char* purposesStatus, char* vendorsStatus)
{
    DDMCurrentUserStatus *status = [[DDMCurrentUserStatus alloc ] initWithPurposes:ConvertJsonToCurrentUserStatusPurposes(purposesStatus) vendors:ConvertJsonToCurrentUserStatusVendors(vendorsStatus)];
    Didomi *didomi = [Didomi shared];
    return [didomi setCurrentUserStatusWithCurrentUserStatus:status];
}

void setUser(char* organizationUserId)
{
    return [[Didomi shared] setUserWithId: CreateNSString(organizationUserId)];
}

void setUserAndSetupUI(char* organizationUserId)
{
    return [[Didomi shared] setUserWithId: CreateNSString(organizationUserId)
        containerController: UnityGetGLViewController()];
}

void setUserWithEncryptionParams(char* organizationUserId, char* algorithm, char* secretID, char* initializationVector)
{
    UserAuthWithEncryptionParams *userAuthParameters = [[UserAuthWithEncryptionParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        initializationVector: CreateNSString(initializationVector)];
    [[Didomi shared] setUserWithUserAuthParams:userAuthParameters];
}

void setUserWithEncryptionParamsAndSetupUI(char* organizationUserId, char* algorithm, char* secretID, char* initializationVector)
{
    UserAuthWithEncryptionParams *userAuthParameters = [[UserAuthWithEncryptionParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        initializationVector: CreateNSString(initializationVector)];
    [[Didomi shared] setUserWithUserAuthParams:userAuthParameters
        containerController: UnityGetGLViewController()];
}

void setUserWithEncryptionParamsWithExpiration(char* organizationUserId, char* algorithm, char* secretID, char* initializationVector, long expiration)
{
    UserAuthWithEncryptionParams *userAuthParameters = [[UserAuthWithEncryptionParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        initializationVector: CreateNSString(initializationVector)
                                                        legacyExpiration:(double)expiration];
    [[Didomi shared] setUserWithUserAuthParams:userAuthParameters];
}

void setUserWithEncryptionParamsWithExpirationAndSetupUI(char* organizationUserId, char* algorithm, char* secretID, char* initializationVector, long expiration)
{
    UserAuthWithEncryptionParams *userAuthParameters = [[UserAuthWithEncryptionParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        initializationVector: CreateNSString(initializationVector)
                                                        legacyExpiration:(double)expiration];
    [[Didomi shared] setUserWithUserAuthParams:userAuthParameters
        containerController: UnityGetGLViewController()];
}

void setUserWithHashParams(char* organizationUserId, char* algorithm, char* secretID, char* digest, char* salt)
{
    UserAuthWithHashParams *userAuthParameters = [[UserAuthWithHashParams alloc]
                                                        initWithId:CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        digest: CreateNSString(digest)
                                                        salt: CreateNSStringNullable(salt)];
    [[Didomi shared] setUserWithUserAuthParams: userAuthParameters];
}

void setUserWithHashParamsAndSetupUI(char* organizationUserId, char* algorithm, char* secretID, char* digest, char* salt)
{
    UserAuthWithHashParams *userAuthParameters = [[UserAuthWithHashParams alloc]
                                                        initWithId:CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        digest: CreateNSString(digest)
                                                        salt: CreateNSStringNullable(salt)];
    [[Didomi shared] setUserWithUserAuthParams: userAuthParameters
        containerController: UnityGetGLViewController()];
}

void setUserWithHashParamsWithExpiration(char* organizationUserId, char* algorithm, char* secretID, char* digest, char* salt, long expiration)
{
    UserAuthWithHashParams *userAuthParameters = [[UserAuthWithHashParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        digest: CreateNSString(digest)
                                                        salt: CreateNSStringNullable(salt)
                                                        legacyExpiration:(double)expiration];
    [[Didomi shared] setUserWithUserAuthParams: userAuthParameters];
}

void setUserWithHashParamsWithExpirationAndSetupUI(char* organizationUserId, char* algorithm, char* secretID, char* digest, char* salt, long expiration)
{
    UserAuthWithHashParams *userAuthParameters = [[UserAuthWithHashParams alloc]
                                                        initWithId: CreateNSString(organizationUserId)
                                                        algorithm: CreateNSString(algorithm)
                                                        secretID: CreateNSString(secretID)
                                                        digest: CreateNSString(digest)
                                                        salt: CreateNSStringNullable(salt)
                                                        legacyExpiration:(double)expiration];
    [[Didomi shared] setUserWithUserAuthParams: userAuthParameters
        containerController: UnityGetGLViewController()];
}

void clearUser()
{
    return [[Didomi shared] clearUser];
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

    eventListener.onHidePreferences = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onShowPreferences = ^(DDMEventType eventType){

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
    
    eventListener.onNoticeClickViewVendors = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onNoticeClickPrivacyPolicy = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onNoticeClickViewSPIPurposes = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickAgreeToAll = ^(enum DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickDisagreeToAll = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

    eventListener.onPreferencesClickViewPurposes = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

    eventListener.onPreferencesClickAgreeToAllPurposes = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onPreferencesClickDisagreeToAllPurposes = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onPreferencesClickResetAllPurposes = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
	eventListener.onPreferencesClickPurposeAgree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };

	eventListener.onPreferencesClickPurposeDisagree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };
    
    eventListener.onPreferencesClickCategoryAgree = ^(DDMEventType eventType, NSString * _Nullable categoryId){

        event_listener_handler(eventType, convertNSStringToCString(categoryId));

    };

    eventListener.onPreferencesClickCategoryDisagree = ^(DDMEventType eventType, NSString * _Nullable categoryId){

        event_listener_handler(eventType, convertNSStringToCString(categoryId));

    };

	eventListener.onPreferencesClickViewVendors = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

	eventListener.onPreferencesClickSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onPreferencesClickSPIPurposeAgree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };

    eventListener.onPreferencesClickSPIPurposeDisagree = ^(DDMEventType eventType, NSString * _Nullable purposeId){

        event_listener_handler(eventType, convertNSStringToCString(purposeId));

    };
    
    eventListener.onPreferencesClickSPICategoryAgree = ^(DDMEventType eventType, NSString * _Nullable categoryId){

        event_listener_handler(eventType, convertNSStringToCString(categoryId));

    };

    eventListener.onPreferencesClickSPICategoryDisagree = ^(DDMEventType eventType, NSString * _Nullable categoryId){

        event_listener_handler(eventType, convertNSStringToCString(categoryId));

    };
    
    eventListener.onPreferencesClickSPIPurposeSaveChoices = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };

    eventListener.onPreferencesClickAgreeToAllVendors = ^(DDMEventType eventType){

        event_listener_handler(eventType, NULL);

    };
    
    eventListener.onPreferencesClickDisagreeToAllVendors = ^(DDMEventType eventType){

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
    
    eventListener.onSyncDone = ^(DDMEventType eventType, NSString * _Nullable organizationUserId){

        event_listener_handler(eventType, convertNSStringToCString(organizationUserId));

    };
    
    eventListener.onSyncError = ^(DDMEventType eventType, NSString * _Nullable error){

        event_listener_handler(eventType, convertNSStringToCString(error));

    };
    
    eventListener.onLanguageUpdated = ^(DDMEventType eventType, NSString * _Nullable languageCode){

        event_listener_handler(eventType, convertNSStringToCString(languageCode));

    };
    
    eventListener.onLanguageUpdateFailed = ^(DDMEventType eventType, NSString * _Nullable reason){

        event_listener_handler(eventType, convertNSStringToCString(reason));

    };

   [[Didomi shared] addEventListenerWithListener:eventListener];

}

void addVendorStatusListener( char* vendorId, void (*vendor_status_listener_handler) (char *))
{
    [[Didomi shared] addVendorStatusListenerWithId: CreateNSString(vendorId) :^(DDMCurrentUserStatusVendor * _Nonnull vendorStatus){
        char *vendorStatusJson = MapCurrentUserStatusVendor(vendorStatus);
        vendor_status_listener_handler(vendorStatusJson);
    }];
}

void removeVendorStatusListener( char* vendorId)
{
    [[Didomi shared] removeVendorStatusListenerWithId: CreateNSString(vendorId)];
}

}
