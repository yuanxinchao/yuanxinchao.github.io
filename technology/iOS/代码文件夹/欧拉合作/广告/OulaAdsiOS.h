//
//  OulaAnalyzeiOS.h
//  Unity-iPhone
//
//  Created by Tomato2 on 17/4/18.
//
//

#ifndef OulaAdsiOS_h
#define OulaAdsiOS_h

#import <AvidlyAdsSDK/AvidlyAdsSDK.h>
#import <AvidlyAdsSDK/AvidlyAdsSDK.h>
//在要回调的类名上，添加代理
@interface HolaAdsController :NSObject
@end



extern "C"
{
typedef void(*HolaCallback)(const char *callbackInfo);
void InitHola();
void ShowHolaBanner(BOOL bo);
void ShowHolaInterstitial(HolaCallback holaCb);
void ShowHolaRewardVideo(const char *placementId, HolaCallback holaCb);
BOOL IsHolaInterstitialReady();
BOOL IsHolaRewardVideoReady();
void LoadHolaInterstitial();
void LoadHolaIncent();
}





#endif /* OulaAdsiOS_h */
