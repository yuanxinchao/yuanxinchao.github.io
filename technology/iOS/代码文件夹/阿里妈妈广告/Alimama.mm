//
//  Alimama.m
//  Unity-iPhone
//
//  Created by Tomato2 on 16/11/7.
//
//
#include "Alimama.h"
@implementation Alimama

-(void)ShowAliBanner: (BOOL )bo{
    if(bo){
        [UnityGetGLViewController().view addSubview:banners];
        NSLog(@"阿里妈妈banner 播放");
    }
    else{
        [banners removeFromSuperview];
        NSLog(@"阿里妈妈banner 关闭");
    }
}
-(void)InitAlimamaiOS:(NSString*) aliId{
    NSLog(@"初始化阿里妈妈 aliId=%@",aliId);
    banners = [[MMUBanners alloc] initWithSlotId:aliId bannersDelegate:self  browserDelegate:self positionType:MMUPositionTypeDown_middle];
    [UnityGetGLViewController().view addSubview:banners];
    [banners requestBannerAd];
}
// 横幅请求失败的回调
- (void)bannerAdsAllAdFail:(MMUBanners *)bannerAds withError:(NSError *)err{
    NSLog(@"阿里妈妈banner初始化失败%@",err);
    
}
// 横幅展现的回调
- (void)bannerAdsAppear:(MMUBanners *)bannerAds{
    NSLog(@"展示阿里妈妈banner");
    
}
// 横幅点击的回调
- (void)bannerClick:(MMUBanners *)bannerAds{
    NSLog(@"阿里妈妈banner 点击");
}
//返回视图控制器，此代理方法必须实现。
- (UIViewController *)bannerViewControllerForPresentingModalView{
    NSLog(@"阿里妈妈 返回视图控制器");
    return UnityGetGLViewController();
}
// 横幅关闭按钮点击的回调
- (void)bannerClosed:(MMUBanners *)bannerAds{
    NSLog(@"阿里妈妈banner 关闭");
    
}@end
Alimama *alimama;
void InitAlimamaiOS(const char* bo){
    alimama =[Alimama new];
    [alimama InitAlimamaiOS:[NSString stringWithUTF8String:bo]];
}

void ShowAliBanner(const BOOL bo){
     NSLog(@"阿里妈妈banner %@",bo?@"展示":@"隐藏");
    [alimama ShowAliBanner:bo];
}

