//
//  LocalPush.m
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//  my name is jiajw

#import "OulaAdsiOS.h"
HolaCallback holaCallback;
@interface HolaAdsController () <KInterstitialManagerDelegate,STAvidlyRewardManagerDelegate,STAvidlyBannerViewDelegate>

@end
@implementation HolaAdsController
//*********rewardVideo*********
-(void)initRewardVideo{
    [AvidlyRewardVideoSDK initSDK];
}
-(void)setRewardDelegate{
    [AvidlyRewardVideoSDK setDelegate:self];
}
-(BOOL)isRewardVideoReady{
    return  [AvidlyRewardVideoSDK ready];
}
-(void)loadIncent{
//    [AvidlyRewardVideoSDK loadAd];
}
-(void)showRewardVideo:(NSString *)placementId{
    [AvidlyRewardVideoSDK showRewardVideo:UnityGetGLViewController() placement:placementId];
}
//*********rewardVideo*********

//*********insterstitial**********
-(void)initInterstitial{
    [AvidlyInterstitialSDK initSDK];
}
-(void)setInterstitialDelegate{
    [AvidlyInterstitialSDK setDelegate:self];
}
-(BOOL)isInterstitialReady{
    return  [AvidlyInterstitialSDK ready];
}
-(void)loadInter{
    NSLog(@"载入插屏广告");
    [AvidlyInterstitialSDK loadAd];
}
-(void)showInterstitialVideo{
    [AvidlyInterstitialSDK show:UnityGetGLViewController()];
}
//*********insterstitial**********

//*********banner**********
BOOL loaded=false;
-(void)initBanner{
     NSLog(@"初始化横幅广告");
    [AvidlyBannerSDK initSDK];
}
-(void)setBannerDelegate{
    bannerView.delegate = self;
    [self performSelector:@selector(createBannerView) withObject:nil/*可传任意类型参数*/ afterDelay:10.0];
}
-(void)loadBanner{
    NSLog(@"载入横幅广告");
    [bannerView loadAd];
}
-(void)showBanner{
       NSLog(@"展示横幅广告");
    [UnityGetGLViewController().view addSubview:bannerView];
}
-(void)hideBanner{
    NSLog(@"隐藏横幅广告");
    [bannerView removeFromSuperview];
}
AvidlyBannerView *bannerView;
-(void)createBannerView{
    NSLog(@"创建横幅广告");
//    NSLog(@"UIScreen hight=%f", [UIScreen mainScreen ].bounds.size.height);
//    NSLog(@"bannerView.bounds.size.height=%f", bannerView.bounds.size.height);
     bannerView= [[AvidlyBannerView alloc] initWithOrigin:CGPointMake(0,[UIScreen mainScreen ].bounds.size.height-50) vc:UnityGetGLViewController()];
    [self loadBanner];
    loaded = true;
}

//*********banner**********

//*******banner回调*********
//实现代理方法，此处在上文的STBannerViewController类中实现
- (void)AvidlyBannerLoadSucceed:(AvidlyBannerView *)bannerView
{
    NSLog(@"横幅广告加载成功");
    if(holaCallback!=NULL){
        holaCallback("LOADEDBANNER");
    }
}

- (void)AvidlyBannerloadError:(AvidlyBannerView *)bannerView error:(NSError *)error
{
    NSLog(@"横幅广告加载失败");
}

- (void)AvidlyBannerClick:(AvidlyBannerView *)bannerView
{
    NSLog(@"横幅广告被点击");
    if(holaCallback!=NULL){
        holaCallback("BANNERCLICK");
    }
}
//*******banner回调*********


//*******插屏回调*********
//实现代理方法，此处在上文的STBannerViewController类中实现
- (void)interstitialAdDidLoad:(id)interstitialAd
{
    NSLog(@"插屏广告加载成功");
    if(holaCallback!=NULL){
        holaCallback("LOADEDINTER");
    }
}

- (void)interstitialAd:(id)interstitialAd didFailWithError:(NSError *)error
{
    NSLog(@"插屏广告加载失败:%@",error);
    if(holaCallback!=NULL){
        holaCallback("LOADINTERFAILED");
    }
}

//广告将要展示
- (void)interstitialAdWillShow:(id)interstitialAd
{
    NSLog(@"广告将要展示");
}

//广告展示
- (void)interstitialAdDidShow:(id)interstitialAd
{
    NSLog(@"广告展示");

}

//广告显示失败
- (void)interstitialAd:(id)interstitialAd showFailWithError:(NSError *)error
{
    NSLog(@"广告显示失败");
}

//广告将要关闭
- (void)interstitialAdWillClose:(id)interstitialAd
{
    NSLog(@"广告将要关闭");
}

//广告关闭
- (void)interstitialAdDidClose:(id)interstitialAd
{
    NSLog(@"广告关闭");
    if(holaCallback!=NULL){
        holaCallback("HIDDENINTER");
    }
}
//*******激励视频回调*********


//*******激励视频回调*********
//激励视频广告打开
- (void)AvidlyRewardVideoAdDidOpen
{
    NSLog(@"激励视频广告打开");
}

//激励视频广告点击
- (void)AvidlyRewardVideoAdDidCilck
{
    NSLog(@"激励视频广告点击");
}

//激励视频广告关闭
- (void)AvidlyRewardVideoAdDidClose
{
    NSLog(@"激励视频广告关闭");
}

//激励视频发放奖励
- (void)AvidlyRewardVideoAdDidRewardUserWithReward:(STAvidlyReward *)reward
{
    NSLog(@"激励视频发放奖励");
    if(holaCallback!=NULL){
        holaCallback("GiveReward");
    }
}

- (void)AvidlyRewardVideoAdDontReward:(NSError *)error
{
    NSLog(@"激励视频未达到发放奖励条件");
    if(holaCallback!=NULL){
        holaCallback("NoReward");
    }
}
//*******激励视频回调*********
@end
HolaAdsController *holaAdsController;

void InitHola(){
    holaAdsController = [[HolaAdsController alloc]init];
    [holaAdsController initBanner];
    [holaAdsController initRewardVideo];
    [holaAdsController initInterstitial];
    [holaAdsController setBannerDelegate];
    [holaAdsController setInterstitialDelegate];
    [holaAdsController setRewardDelegate];

    
}
void ShowHolaBanner(BOOL bo){
    if(bo && loaded){
        [holaAdsController showBanner];
    }else{
        [holaAdsController hideBanner];
    }
}
void ShowHolaInterstitial(HolaCallback holaCb){
    holaCallback = holaCb;
    [holaAdsController showInterstitialVideo];
 
}
void ShowHolaRewardVideo(const char *placementId, HolaCallback holaCb){
    holaCallback = holaCb;
    [holaAdsController showRewardVideo:[NSString stringWithUTF8String:placementId]];
}
void LoadHolaInterstitial(){
   [holaAdsController loadInter];
}
void LoadHolaIncent(){
    [holaAdsController loadIncent];
}
BOOL IsHolaInterstitialReady(){
     return  [holaAdsController isInterstitialReady];
}
BOOL IsHolaRewardVideoReady(){
    return  [holaAdsController isRewardVideoReady];
}






