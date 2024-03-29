//
//  HeyzapUnitySDK.m
//
//  Copyright 2015 Smart Balloon, Inc. All Rights Reserved
//
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
//


#import <CoreLocation/CLLocation.h>
#import <CoreImage/CoreImageDefines.h>
#import "UnityAppController.h"
#import <StoreKit/StoreKit.h>
#import <UIKit/UIKit.h>

#import "HeyzapAds.h"
#import "HZInterstitialAd.h"
#import "HZVideoAd.h"
#import "HZIncentivizedAd.h"
#import "HZBannerAd.h"
#import "HZUnityAdapterChartboostProxy.h"
#import "HZLog.h"
#import "HZDemographics.h"

extern void UnitySendMessage(const char *, const char *, const char *);

#define HZ_FRAMEWORK_NAME @"unity3d"

#define HZ_VIDEO_KLASS @"HZVideoAd"
#define HZ_INTERSTITIAL_KLASS @"HZInterstitialAd"
#define HZ_INCENTIVIZED_KLASS @"HZIncentivizedAd"
#define HZ_BANNER_KLASS @"HZBannerAd"

@interface HeyzapUnityAdDelegate : NSObject<HZAdsDelegate,HZIncentivizedAdDelegate,HZBannerAdDelegate>

@property (nonatomic, strong) NSString *klassName;

- (id) initWithKlassName: (NSString *) klassName;
- (void) sendMessageForKlass: (NSString *) klass withMessage: (NSString *) message andTag: (NSString *) tag;

@end

@implementation HeyzapUnityAdDelegate

- (id) initWithKlassName: (NSString *) klassName {
    self = [super init];
    if (self) {
        _klassName = klassName;
    }
    
    return self;
}

- (void) didReceiveAdWithTag:(NSString *)tag { [self sendMessageForKlass: self.klassName withMessage: @"available" andTag: tag]; }

- (void) didFailToReceiveAdWithTag:(NSString *)tag { [self sendMessageForKlass: self.klassName withMessage: @"fetch_failed" andTag: tag]; }

- (void) didShowAdWithTag:(NSString *)tag { [self sendMessageForKlass: self.klassName withMessage: @"show" andTag: tag]; }

- (void) didHideAdWithTag:(NSString *)tag { [self sendMessageForKlass: self.klassName withMessage:  @"hide" andTag: tag]; }

- (void) didFailToShowAdWithTag:(NSString *)tag andError:(NSError *)error { [self sendMessageForKlass: self.klassName withMessage:  @"failed" andTag: tag]; }

- (void) didClickAdWithTag:(NSString *)tag { [self sendMessageForKlass: self.klassName withMessage:  @"click" andTag: tag]; }

- (void) didCompleteAdWithTag: (NSString *) tag { [self sendMessageForKlass: self.klassName withMessage:  @"incentivized_result_complete" andTag: tag]; }

- (void) didFailToCompleteAdWithTag: (NSString *) tag { [self sendMessageForKlass: self.klassName withMessage:  @"incentivized_result_incomplete" andTag: tag]; }

- (void) willStartAudio { [self sendMessageForKlass: self.klassName  withMessage: @"audio_starting" andTag:  @""]; }

- (void) didFinishAudio { [self sendMessageForKlass: self.klassName withMessage:  @"audio_finished" andTag:  @""]; }

- (void)bannerDidReceiveAd:(HZBannerAd *)banner {
    [self sendMessageForKlass:self.klassName withMessage:@"loaded" andTag:banner.options.tag];
}

- (void)bannerDidFailToReceiveAd:(HZBannerAd *)banner error:(NSError *)error {
    if (banner != nil) {
        [self sendMessageForKlass:self.klassName withMessage:@"error" andTag:banner.options.tag];
    } else {
        [self sendMessageForKlass:self.klassName withMessage: @"error" andTag: @""];
    }
}

- (void)bannerWasClicked:(HZBannerAd *)banner {
    [self sendMessageForKlass:self.klassName withMessage:@"click" andTag:banner.options.tag];
}

- (void) sendMessageForKlass: (NSString *) klass withMessage: (NSString *) message andTag: (NSString *) tag {
    NSString *unityMessage = [NSString stringWithFormat: @"%@,%@", message, tag];
    UnitySendMessage([klass UTF8String], "SetCallback", [unityMessage UTF8String]);
}



@end
@interface MyViewController : NSObject<SKStoreProductViewControllerDelegate>
//- (void)openAppWithIdentifier:(NSString *)appId;
- (UnityAppController*)GetMyController;
@end
@implementation MyViewController
- (void)productViewControllerDidFinish:(SKStoreProductViewController *)viewController {
    NSLog(@"shenme");
    [UnityGetGLViewController() dismissViewControllerAnimated:YES completion:nil];
}
- (UnityAppController*) GetMyController
{
    return (UnityAppController*)[UIApplication sharedApplication].delegate;
}
//- (void) viewDidLoad
//{
//     [super viewDidLoad];
//    // 把要评分的 appId 放在这 -- 这里测试用 《圣诞愿望》
//     NSLog(@" 把要评分的 appId 放在这 -- 这里测试用 《圣诞愿望》");
//    [self openAppWithIdentifier:@"1221758038"];
//}
//- (void)openAppWithIdentifier:(NSString *)appId {
//    SKStoreProductViewController *storeProductVC = [[SKStoreProductViewController alloc] init];
//    storeProductVC.delegate = self;
//    
//    NSDictionary *dict = [NSDictionary dictionaryWithObject:appId forKey:SKStoreProductParameterITunesItemIdentifier];
//    [storeProductVC loadProductWithParameters:dict completionBlock:^(BOOL result, NSError *error) {
//        if (result) {
//            [self presentViewController:storeProductVC animated:YES completion:nil];
//        }
//    }];
//}
@end

static HeyzapUnityAdDelegate *HZInterstitialDelegate = nil;
static HeyzapUnityAdDelegate *HZIncentivizedDelegate = nil;
static HeyzapUnityAdDelegate *HZVideoDelegate = nil;
static HeyzapUnityAdDelegate *HZBannerDelegate = nil;

static HZBannerAd *HZCurrentBannerAd = nil;

extern "C" {
  
typedef void (*GetResourceCallback)(BOOL success, const char * iconUrl, const char * bgUrl,const char * title,const char * content);

    void hz_ads_start_app(const char *publisher_id, HZAdOptions flags) {
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            NSString *publisherID = [NSString stringWithUTF8String: publisher_id];
            
            [HeyzapAds startWithPublisherID: publisherID andOptions: flags andFramework: HZ_FRAMEWORK_NAME];
            
            HZIncentivizedDelegate = [[HeyzapUnityAdDelegate alloc] initWithKlassName: HZ_INCENTIVIZED_KLASS];
            [HZIncentivizedAd setDelegate: HZIncentivizedDelegate];
            
            HZInterstitialDelegate = [[HeyzapUnityAdDelegate alloc] initWithKlassName: HZ_INTERSTITIAL_KLASS];
            [HZInterstitialAd setDelegate: HZInterstitialDelegate];
            
            HZVideoDelegate = [[HeyzapUnityAdDelegate alloc] initWithKlassName: HZ_VIDEO_KLASS];
            [HZVideoAd setDelegate: HZVideoDelegate];
            
            HZBannerDelegate = [[HeyzapUnityAdDelegate alloc] initWithKlassName: HZ_BANNER_KLASS];
            
            [HeyzapAds networkCallbackWithBlock:^(NSString *network, NSString *callback) {
                NSString *unityMessage = [NSString stringWithFormat: @"%@,%@", network, callback];
                NSString *klassName = @"HeyzapAds";
                UnitySendMessage([klassName UTF8String], "SetNetworkCallbackMessage", [unityMessage UTF8String]);
            }];
        });
    }
    
    void hz_ads_show_interstitial(const char *tag) {
        [HZInterstitialAd showForTag: [NSString stringWithUTF8String: tag]];
    }
    
    void hz_ads_fetch_interstitial(const char *tag) {
        [HZInterstitialAd fetchForTag: [NSString stringWithUTF8String: tag]];
    }
    
    bool hz_ads_interstitial_is_available(const char *tag) {
        return [HZInterstitialAd isAvailableForTag: [NSString stringWithUTF8String: tag]];
    }
    
    void hz_ads_show_video(const char *tag) {
        [HZVideoAd showForTag: [NSString stringWithUTF8String: tag]];
    }
    
    void hz_ads_fetch_video(const char *tag) {
        [HZVideoAd fetchForTag: [NSString stringWithUTF8String: tag]];
    }
    
    bool hz_ads_video_is_available(const char *tag) {
        return [HZVideoAd isAvailableForTag:[NSString stringWithUTF8String:tag]];
    }
    
    void hz_ads_show_incentivized(const char *tag) {
        [HZIncentivizedAd showForTag: [NSString stringWithUTF8String: tag]];
    }
    
    void hz_ads_show_incentivized_with_custom_info(const char *tag, const char *customInfo) {
        HZShowOptions *showOptions = [HZShowOptions new];
        showOptions.tag = [NSString stringWithUTF8String: tag];
        showOptions.incentivizedInfo = [NSString stringWithUTF8String: customInfo];
        [HZIncentivizedAd showWithOptions:showOptions];
    }
    
    void hz_ads_fetch_incentivized(const char *tag) {
        [HZIncentivizedAd fetchForTag: [NSString stringWithUTF8String: tag]];
    }
    
    bool hz_ads_incentivized_is_available(const char *tag) {
        return [HZIncentivizedAd isAvailableForTag: [NSString stringWithUTF8String: tag]];
    }
    
    void hz_ads_show_banner(const char *position, const char *tag) {
        if (!HZCurrentBannerAd) {
            HZBannerPosition pos = HZBannerPositionBottom;
            NSString *positionStr = [NSString stringWithUTF8String: position];
            if ([positionStr isEqualToString: @"top"]) {
                pos = HZBannerPositionTop;
            }
            
            HZBannerAdOptions *options = [[HZBannerAdOptions alloc] init];
            options.tag = [NSString stringWithUTF8String:tag];
            
            [HZBannerAd placeBannerInView:nil position:pos options:options success:^(HZBannerAd *banner) {
                if (!HZCurrentBannerAd) {
                    HZCurrentBannerAd = banner;
                    [HZCurrentBannerAd setDelegate: HZBannerDelegate];
                    [HZBannerDelegate sendMessageForKlass:[HZBannerDelegate klassName] withMessage:@"loaded" andTag:banner.options.tag];
                } else {
                    [banner removeFromSuperview];
                    NSLog(@"Requested a banner before the previous one was destroyed. Ignoring this request.");
                }
                
            } failure:^(NSError *error) {
                NSLog(@"Error fetching banner; error = %@",error);
                [HZBannerDelegate bannerDidFailToReceiveAd: nil error: error];
            }];
        } else {
            // Unhide the banner
            [HZCurrentBannerAd setHidden: NO];
        }
    }
    
    void hz_ads_hide_banner(void) {
        if (HZCurrentBannerAd) {
            [HZCurrentBannerAd setHidden: YES];
            
        } else {
            NSLog(@"Can't hide banner, there is no banner ad currently loaded.");
        }
    }
    
    void hz_ads_destroy_banner(void) {
        if (HZCurrentBannerAd) {
            [HZCurrentBannerAd removeFromSuperview];
            HZCurrentBannerAd = nil;
            
        } else {
            NSLog(@"Can't destroy banner, there is no banner ad currently loaded.");
        }
    }
    
    char * hz_ads_banner_dimensions(void) {
        if (HZCurrentBannerAd) {
            const char * dims = [[HZCurrentBannerAd dimensionsDescription] UTF8String];
            if (dims == NULL) {
                return NULL;
            }
            
            char* returnValue = (char*)malloc(strlen(dims) + 1);
            strcpy(returnValue, dims);
            return returnValue;
            
        } else {
            NSLog(@"Can't get banner dimensions, there is no banner ad currently loaded.");
        }
        
        return NULL;
    }
    
    char * hz_ads_get_remote_data(void){
        NSString *remoteData = [HeyzapAds getRemoteDataJsonString];
        const char* remoteString = [remoteData UTF8String];
        char* returnValue = (char*)malloc(sizeof(char)*(strlen(remoteString) + 1));
        strcpy(returnValue, remoteString);
        return returnValue;
    }
    
    void hz_ads_show_mediation_debug_view_controller(void) {
        [HeyzapAds presentMediationDebugViewController];
    }
    
    bool hz_ads_is_network_initialized(const char *network) {
        if (network == NULL) { return NO; }
        
        return [HeyzapAds isNetworkInitialized:[NSString stringWithUTF8String:network]];
    }
    
    void hz_pause_expensive_work(void) {
        [HeyzapAds pauseExpensiveWork];
    }
    
    void hz_resume_expensive_work(void) {
        [HeyzapAds resumeExpensiveWork];
    }
    
    void hz_ads_show_debug_logs(void) {
        [HZLog setDebugLevel:HZDebugLevelVerbose];
    }
    
    void hz_ads_hide_debug_logs(void) {
        [HZLog setDebugLevel:HZDebugLevelSilent];
    }
    
    void hz_ads_show_third_party_debug_logs(void) {
        [HZLog setThirdPartyLoggingEnabled:YES];
    }
    
    void hz_ads_hide_third_party_debug_logs(void) {
        [HZLog setThirdPartyLoggingEnabled:NO];
    }
    
    void hz_ads_set_bundle_identifier(const char *bundle_id) {
        if (bundle_id == NULL) { return; }
        
        NSString *bundleID = [NSString stringWithUTF8String:bundle_id];
        [HeyzapAds setBundleIdentifier:bundleID];
    }
    
    
    void hz_demo_set_gender(const char * genderChar) {
        NSString *gender = [NSString stringWithUTF8String:genderChar];
        
        if ([gender isEqualToString:@"MALE"]) {
            [[HeyzapAds demographicInformation] setUserGender:HZUserGenderMale];
        } else if ([gender isEqualToString:@"FEMALE"]) {
            [[HeyzapAds demographicInformation] setUserGender:HZUserGenderFemale];
        } else if ([gender isEqualToString:@"OTHER"]) {
            [[HeyzapAds demographicInformation] setUserGender:HZUserGenderOther];
        } else {
            [[HeyzapAds demographicInformation] setUserGender:HZUserGenderUnknown];
        }
    }
    
    void hz_demo_set_location(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
        CLLocation *location = [[CLLocation alloc] initWithCoordinate:CLLocationCoordinate2DMake(latitude, longitude) altitude:altitude horizontalAccuracy:horizontalAccuracy verticalAccuracy:verticalAccuracy timestamp:[NSDate dateWithTimeIntervalSince1970:timestamp]];
        [[HeyzapAds demographicInformation] setLocation:location];
    }
    
    void hz_demo_set_postal_code(const char * postalCodeChar) {
        NSString *postalCode = (postalCodeChar == NULL ? nil : [NSString stringWithUTF8String:postalCodeChar]);
        [[HeyzapAds demographicInformation] setUserPostalCode:postalCode];
    }
    
    void hz_demo_set_household_income(int householdIncome) {
        [[HeyzapAds demographicInformation] setUserHouseholdIncome:@(householdIncome)];
    }
    
    void hz_demo_set_marital_status(const char * maritalStatusChar) {
        NSString *maritalStatus = [NSString stringWithUTF8String:maritalStatusChar];
        
        if ([maritalStatus isEqualToString:@"SINGLE"]) {
            [[HeyzapAds demographicInformation] setUserMaritalStatus:HZUserMaritalStatusSingle];
        } else if ([maritalStatus isEqualToString:@"MARRIED"]) {
            [[HeyzapAds demographicInformation] setUserMaritalStatus:HZUserMaritalStatusMarried];
        } else {
            [[HeyzapAds demographicInformation] setUserMaritalStatus:HZUserMaritalStatusUnknown];
        }
    }
    
    void hz_demo_set_education_level(const char * educationLevelChar) {
        NSString *educationLevel = [NSString stringWithUTF8String:educationLevelChar];
        
        if ([educationLevel isEqualToString:@"GRADE_SCHOOL"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationGradeSchool];
        } else if ([educationLevel isEqualToString:@"HIGH_SCHOOL_UNFINISHED"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationHighSchoolUnfinished];
        } else if ([educationLevel isEqualToString:@"HIGH_SCHOOL_FINISHED"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationHighSchoolFinished];
        } else if ([educationLevel isEqualToString:@"COLLEGE_UNFINISHED"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationCollegeUnfinished];
        } else if ([educationLevel isEqualToString:@"ASSOCIATE_DEGREE"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationAssociateDegree];
        } else if ([educationLevel isEqualToString:@"BACHELORS_DEGREE"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationBachelorsDegree];
        } else if ([educationLevel isEqualToString:@"GRADUATE_DEGREE"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationGraduateDegree];
        } else if ([educationLevel isEqualToString:@"POSTGRADUATE_DEGREE"]) {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationPostGraduateDegree];
        } else {
            [[HeyzapAds demographicInformation] setUserEducationLevel:HZUserEducationUnknown];
        }
    }
    
    void hz_demo_set_birth_date(const char * yyyyMMdd_dateChar) {
        __block NSDateFormatter *dateFormat;
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            dateFormat = [[NSDateFormatter alloc] init];
            [dateFormat setDateFormat:@"yyyy/MM/dd"];
        });
        
        NSDate *parsedDate = nil;
        if (yyyyMMdd_dateChar != NULL) {
            parsedDate = [dateFormat dateFromString:[NSString stringWithUTF8String:yyyyMMdd_dateChar]];
        }
        
        [[HeyzapAds demographicInformation] setUserBirthDate:parsedDate];
    }
    
    
    BOOL hz_chartboost_enabled(void) {
        return [HeyzapAds isNetworkInitialized:HZNetworkChartboost];
    }
    
    // Calling hz_fetch_chartboost_for_location recursively won't keep the `const char *` in memory
    // Since I want to call it recursively, I immediately convert to an `NSString *` in `hz_fetch_chartboost_for_location`
    void hz_fetch_chartboost_for_location_objc(NSString *location) {
        if (!hz_chartboost_enabled()) {
            HZDLog(@"Chartboost not enabled; retrying in 0.25 seconds");
            dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(0.25 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
                hz_fetch_chartboost_for_location_objc(location);
            });
            return;
        }
        HZDLog(@"Caching Chartboost interstitial for location: %@",location);
        [HZUnityAdapterChartboostProxy cacheInterstitial:location];
    }
    
    void hz_fetch_chartboost_for_location(const char *location) {
        NSString *nsLocation = [NSString stringWithUTF8String:location];
        hz_fetch_chartboost_for_location_objc(nsLocation);
    }
    
    bool hz_chartboost_is_available_for_location(const char *location) {
        NSString *nsLocation = [NSString stringWithUTF8String:location];
        if (!hz_chartboost_enabled()) {
            HZDLog(@"Chartboost ad is not available because it is not enabled");
            return NO;
        }
        const BOOL hasAd = [HZUnityAdapterChartboostProxy hasInterstitial:nsLocation];
        HZDLog(@"Chartboost says it has an ad = %i",hasAd);
        return hasAd;
    }
    
    void hz_show_chartboost_for_location(const char *location) {
        NSString *nsLocation = [NSString stringWithUTF8String:location];
        
        if (!hz_chartboost_enabled()) {
            HZDLog(@"Chartboost not enabled yet; not able to show ad.");
            return;
        }
        HZDLog(@"Requesting Chartboost show interstitial for location: %@",nsLocation);
        [HZUnityAdapterChartboostProxy showInterstitial:nsLocation];
    }
    
    void hz_add_facebook_test_device(const char *device_id) {
        NSString *deviceID = [NSString stringWithUTF8String:device_id];
        
        Class fbAdSettings = NSClassFromString(@"FBAdSettings");
        if ([fbAdSettings respondsToSelector:@selector(addTestDevice:)]) {
            [fbAdSettings performSelector:@selector(addTestDevice:) withObject:deviceID];
        } else {
            HZELog(@"Couldn't find FBAdSettings, or it didn't respond ");
        }
    }
    HZNativeAd *ad;
    NSMutableArray *ads = [[NSMutableArray alloc] init];
    void GetiOSAdsResource(GetResourceCallback getCB){
        [HZNativeAdController fetchAds:30 tag:nil completion:^(NSError *error, HZNativeAdCollection *collection) {
            if (error) {
                NSLog(@"hz_native_fetchAds error = %@",error);
            } else {
                int i=0;
                // Use the `collection` to display ads
                for (i=0; i<collection.ads.count; i++) {
                    ad=[collection.ads objectAtIndex:i];
                    if(ad.portraitCreative.url!=nil&&ad.iconImage.url!=nil){
                        [ads addObject:ad];
                    }
                }
                if(ads.count == 0){
                    getCB(false,"","","","");
                }
                else{
                    ad = [ads objectAtIndex:0];
                    getCB(true,
                          [[ad.iconImage.url absoluteString] UTF8String],
                          [[ad.portraitCreative.url absoluteString] UTF8String],
                          [ad.appName UTF8String],
                          [ad.appDescription UTF8String]);
                    
                    [ad reportImpression];
                }
            }
        }];
    }
    int NativeAdsIndex=0;
    void ChangeNaviteAds(GetResourceCallback getCB){
        if(ads.count!=0){
            NativeAdsIndex++;
            NativeAdsIndex= NativeAdsIndex % ads.count;
            
        ad = [ads objectAtIndex:NativeAdsIndex];
            getCB(true,
                  [[ad.iconImage.url absoluteString] UTF8String],
                  [[ad.portraitCreative.url absoluteString] UTF8String],
                  [ad.appName UTF8String],
                  [ad.appDescription UTF8String]);
            
            [ad reportImpression];
        }
        else{
        getCB(false,"","","","");
        }
        NSLog(@"NativeAdsIndex =%d ads.count= %lu",NativeAdsIndex,(unsigned long)ads.count);
    }
    void ClickTheAds(){
         NSLog(@"kaishi0");
        NSLog(@"kaishi1");
        
        [ad presentAppStoreFromViewController:UnityGetGLViewController()
                                storeDelegate:GetAppController()
                                         completion:^(BOOL result, NSError *error) {
                                             // Dismiss the loading spinner here, if you showed one.
                                             // Apple's SKStoreProductViewController may error out trying to load the app store; you can ignore this error as we fallback to switching to the app store app in this case.
                                             NSLog(@"dismiss");
                                             [UnityGetGLViewController()dismissViewControllerAnimated:YES completion:nil];
                
                                         }];
        
        [ad presentAppStoreFromViewController:UnityGetGLViewController()
                                storeDelegate:GetAppController()
                                   completion:^(BOOL result, NSError *error) {
                                       // Dismiss the loading spinner here, if you showed one.
                                       // Apple's SKStoreProductViewController may error out trying to load the app store; you can ignore this error as we fallback to switching to the app store app in this case.
                                       NSLog(@"dismiss");
                                       [UnityGetGLViewController()dismissViewControllerAnimated:YES completion:nil];
                                       
                                   }];
    }
}
