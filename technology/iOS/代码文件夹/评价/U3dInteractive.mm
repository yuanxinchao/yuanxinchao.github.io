//
//  U3dInteractive.m
//  Unity-iPhone
//
//  Created by Tomato2 on 16/11/7.
//
//
#include "U3dInteractive.h"
//获取IDFA
#import <AdSupport/AdSupport.h>
//获取设备名称
#import <sys/utsname.h>
//获取imsi
#import <CoreTelephony/CTCarrier.h>
#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <UIKit/UIKit.h>
//获取网络类型
RateCB Global_rateCB;
@implementation U3dInteractive

@synthesize appid = _appid;
//评价框的回调
//-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{
//    //    NSLog(@" button index=%ld is clicked.....", (long)buttonIndex);
//    //     NSLog(@" alertView tag=%ld is clicked.....", (long)alertView.tag);
//    if(buttonIndex==1&&alertView.tag==0){
//        Global_rateCB(true);
//        NSString *str = [NSString stringWithFormat:@"itms-apps://itunes.apple.com/app/id%@",self.appid];
//        
//        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:str]];
//    }else{
//        Global_rateCB(false);
//    }
//}
-(void)GotoRate:(bool )bo{
    if(bo){
        Global_rateCB(true);
        NSString *str = [NSString stringWithFormat:@"itms-apps://itunes.apple.com/app/id%@",self.appid];
        
        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:str]];

        
    }else{
        Global_rateCB(false);
    }
}
////评价的方法
//-(void)RateApp:(NSString *)appleid{
//    self.appid=appleid;
//    NSString *appName = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleDisplayName"];
//    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:[NSString stringWithFormat:@"去给'%@'打分吧！",appName]
//                                                        message:@"您的评价对我们很重要"
//                                                       delegate:self
//                                              cancelButtonTitle:nil
//                                              otherButtonTitles:@"稍后评价",@"去评价",nil];
//    
//    [alertView show];
//}

//评价的方法
-(void)RateApp:(NSString *)appleid{
    self.appid=appleid;
    NSString *appName = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleDisplayName"];
    UIAlertController *alertController =[UIAlertController
                                   alertControllerWithTitle:[NSString stringWithFormat:@"去给'%@'打分吧！",appName]
                                   message:@"您的评价对我们很重要"
                                   preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction *cancelAction = [UIAlertAction
                                   actionWithTitle:NSLocalizedString(@"稍后评价", @"Cancel action")
                                   style:UIAlertActionStyleCancel
                                   handler:^(UIAlertAction *action)
                                   {
                                       NSLog(@"Cancel action");
                                       [self GotoRate:false];
                                   }];
    
    UIAlertAction *okAction = [UIAlertAction
                               actionWithTitle:NSLocalizedString(@"去评价", @"OK action")
                               style:UIAlertActionStyleDefault
                               handler:^(UIAlertAction *action)
                               {
                                   NSLog(@"OK action");
                                   [self GotoRate:true];
                               }];

    [alertController addAction:cancelAction];
    [alertController addAction:okAction];
    
    [UnityGetGLViewController() presentViewController:alertController animated:YES completion:nil];
}

- (void)dealloc{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

+ (NSString *)iphoneType {
    struct utsname systemInfo;
    
    uname(&systemInfo);
    
    NSString *platform = [NSString stringWithCString:systemInfo.machine encoding:NSASCIIStringEncoding];
    
    if ([platform isEqualToString:@"iPhone1,1"]) return @"iPhone2G";
    
    if ([platform isEqualToString:@"iPhone1,2"]) return @"iPhone3G";
    
    if ([platform isEqualToString:@"iPhone2,1"]) return @"iPhone3GS";
    
    if ([platform isEqualToString:@"iPhone3,1"]) return @"iPhone4";
    
    if ([platform isEqualToString:@"iPhone3,2"]) return @"iPhone4";
    
    if ([platform isEqualToString:@"iPhone3,3"]) return @"iPhone4";
    
    if ([platform isEqualToString:@"iPhone4,1"]) return @"iPhone4S";
    
    if ([platform isEqualToString:@"iPhone5,1"]) return @"iPhone5";
    
    if ([platform isEqualToString:@"iPhone5,2"]) return @"iPhone5";
    
    if ([platform isEqualToString:@"iPhone5,3"]) return @"iPhone5c";
    
    if ([platform isEqualToString:@"iPhone5,4"]) return @"iPhone5c";
    
    if ([platform isEqualToString:@"iPhone6,1"]) return @"iPhone5s";
    
    if ([platform isEqualToString:@"iPhone6,2"]) return @"iPhone5s";
    
    if ([platform isEqualToString:@"iPhone7,1"]) return @"iPhone6Plus";
    
    if ([platform isEqualToString:@"iPhone7,2"]) return @"iPhone6";
    
    if ([platform isEqualToString:@"iPhone8,1"]) return @"iPhone6s";
    
    if ([platform isEqualToString:@"iPhone8,2"]) return @"iPhone6sPlus";
    
    if ([platform isEqualToString:@"iPhone8,4"]) return @"iPhoneSE";
    
    if ([platform isEqualToString:@"iPod1,1"]) return @"iPodTouch1G";
    
    if ([platform isEqualToString:@"iPod2,1"]) return @"iPodTouch2G";
    
    if ([platform isEqualToString:@"iPod3,1"]) return @"iPodTouch3G";
    
    if ([platform isEqualToString:@"iPod4,1"]) return @"iPodTouch4G";
    
    if ([platform isEqualToString:@"iPod5,1"]) return @"iPodTouch5G";
    
    if ([platform isEqualToString:@"iPad1,1"]) return @"iPad1G";
    
    if ([platform isEqualToString:@"iPad2,1"]) return @"iPad2";
    
    if ([platform isEqualToString:@"iPad2,2"]) return @"iPad2";
    
    if ([platform isEqualToString:@"iPad2,3"]) return @"iPad2";
    
    if ([platform isEqualToString:@"iPad2,4"]) return @"iPad2";
    
    if ([platform isEqualToString:@"iPad2,5"]) return @"iPadMini1G";
    
    if ([platform isEqualToString:@"iPad2,6"]) return @"iPadMini1G";
    
    if ([platform isEqualToString:@"iPad2,7"]) return @"iPadMini1G";
    
    if ([platform isEqualToString:@"iPad3,1"]) return @"iPad3";
    
    if ([platform isEqualToString:@"iPad3,2"]) return @"iPad3";
    
    if ([platform isEqualToString:@"iPad3,3"]) return @"iPad3";
    
    if ([platform isEqualToString:@"iPad3,4"]) return @"iPad4";
    
    if ([platform isEqualToString:@"iPad3,5"]) return @"iPad4";
    
    if ([platform isEqualToString:@"iPad3,6"]) return @"iPad4";
    
    if ([platform isEqualToString:@"iPad4,1"]) return @"iPadAir";
    
    if ([platform isEqualToString:@"iPad4,2"]) return @"iPadAir";
    
    if ([platform isEqualToString:@"iPad4,3"]) return @"iPadAir";
    
    if ([platform isEqualToString:@"iPad4,4"]) return @"iPadMini 2G";
    
    if ([platform isEqualToString:@"iPad4,5"]) return @"iPadMini 2G";
    
    if ([platform isEqualToString:@"iPad4,6"]) return @"iPadMini 2G";
    
    if ([platform isEqualToString:@"i386"])   return @"iPhoneSimulator";
    
    if ([platform isEqualToString:@"x86_64"])  return @"iPhoneSimulator";
    
    return platform;
    
}
@end
//转为可以return到u3d的char型
char* MakeStringCopy (const char* string)
{
    
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    
    strcpy(res, string);
    
    return res;
    
}
//评价app
void _RateApp1(const char* Appid,RateCB rateCB){
     NSLog(@"caonimabi");
    Global_rateCB=rateCB;
     NSString *appid = [NSString stringWithUTF8String: Appid] ;
    U3dInteractive *rateapp = [[U3dInteractive alloc]init];
    [rateapp RateApp:appid];
}

//将内容复制到剪贴板
void _CopyURL (const char* url){
    static NSObject *iosClipboard;
    NSString *text = [NSString stringWithUTF8String: url] ;
    
    if(iosClipboard == NULL)
    {
        iosClipboard = [[NSObject alloc] init];
    }
    
    UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
    pasteboard.string = text;

}
//打开微信
void _OpenWX (){
    NSString* strIdentifier = @"weixin://";
    BOOL isExsit = [[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:strIdentifier]];
    if(isExsit) {
        NSLog(@"App %@ installed", strIdentifier);
        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:strIdentifier]];
    }
}
//获取IDFA
char* _getIDFA(){
    NSString *adId = [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
    return MakeStringCopy([adId UTF8String]);
}
//获取分辨率
char* _getResolution(){

    CGRect rect_screen = [[UIScreen mainScreen] bounds];
    CGFloat scale_screen= [[UIScreen mainScreen] scale];
    CGSize size_screen = rect_screen.size;
    int width =(int)size_screen.width*scale_screen;
    int height =(int)size_screen.height*scale_screen;
    NSString *stringwidth = [NSString stringWithFormat:@"%d",width];
    NSString *stringheight = [NSString stringWithFormat:@"%d",height];
    NSString *resolution = [[NSString alloc] initWithFormat:@"%@*%@",stringwidth,stringheight];
     return MakeStringCopy([resolution UTF8String]);
}
//获取手机系统版本
char* _getPhoneVersion(){
    NSString* phoneVersion = [[UIDevice currentDevice] systemVersion];
     return MakeStringCopy([phoneVersion UTF8String]);
}
//获取手机类型
char* _getPhoneModel(){
    NSString* phoneModel = [U3dInteractive iphoneType];
    NSLog(@"手机型号:%@",phoneModel);
     return MakeStringCopy([phoneModel UTF8String]);
}
//获取手机卡IMSI 进而获取手机运营商
char* _getIMSI(){
    CTTelephonyNetworkInfo *info = [[CTTelephonyNetworkInfo alloc] init];
    
    CTCarrier *carrier = [info subscriberCellularProvider];
    
    NSString *mcc = [carrier mobileCountryCode];
    NSString *mnc = [carrier mobileNetworkCode];
    
    NSString *imsi = [NSString stringWithFormat:@"%@%@", mcc, mnc];
    return MakeStringCopy([imsi UTF8String]);
}
char* _getNetType(){
    

}