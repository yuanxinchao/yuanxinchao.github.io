//
//  UMSocialCSDK.cpp
//  UmengGame
//
//  Created by yeahugo on 14-6-26.
//
//
#import <UIKit/UIKit.h>
#include "UMSocialCSDK.h"
//#import "UMSocialUIObject.h"
//#import "UMSocialCObject.h"
#import <UShareUI/UShareUI.h>
#import <UMSocialCore/UMSocialCore.h>
#import "UnityAppController.h"
//#import "UMSocialQQHandler.h"
//#import "UMSocialWechatHandler.h"
//#import "UMSocialLaiwangHandler.h"
//#import "UMSocialYiXinHandler.h"
//#import "UMSocialFacebookHandler.h"
//#import "UMSocialTwitterHandler.h"
//#import "UMSocialInstagramHandler.h"
//#import "UMSocialSinaHandler.h"
#include "Unity/DisplayManager.h"
@interface UMSocialBriage :NSObject<UMSocialShareMenuViewDelegate>
{
@public ShareBoardDismissDelegate callback;
}

+ (UMSocialBriage *)defaultManage;

@end
@implementation UMSocialBriage
+ (UMSocialBriage *)defaultManager
{
    static UMSocialBriage *instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        if (!instance) {
            instance = [[self alloc] init];
        }
    });
    return instance;
}
- (void)UMSocialShareMenuViewDidDisappear
{
    callback();
}
@end

NSString* getNSStringFromCStr(const char* cstr){
    if (cstr) {
        return [NSString stringWithUTF8String:cstr];
    }
    return nil;
}
int const platformlength = 17;
UMSocialPlatformType const platforms[platformlength] = {
    UMSocialPlatformType_Sina
    , UMSocialPlatformType_WechatSession
    , UMSocialPlatformType_WechatTimeLine
    , UMSocialPlatformType_QQ
    , UMSocialPlatformType_Qzone
    , UMSocialPlatformType_Renren
     , UMSocialPlatformType_Facebook
    , UMSocialPlatformType_Twitter
   
};
UMSocialPlatformType getPlatformString(int platform){
    
    return platforms[platform];
}
int getPlatformself(int platform){
    for (int i = 0 ; i<platformlength; i++) {
        if (platform == platforms[i]) {
            return i;
        }
    }
    return -1;
}
UIViewController* getCurrentViewController(){
    
    UIViewController* ctrol = nil;
//     UnityAppController *uni=[[UnityAppController alloc]init];
//    ctrol = uni.window.rootViewController;
    if ( [[UIDevice currentDevice].systemVersion floatValue] < 6.0)
    {
        // warning: addSubView doesn't work on iOS6
        NSArray* array=[[UIApplication sharedApplication]windows];
        UIWindow* win=[array objectAtIndex:0];
        
        UIView* ui=[[win subviews] objectAtIndex:0];
        ctrol=(UIViewController*)[ui nextResponder];
    }
    else
    {
        
        // use this method on ios6
        ctrol=[UIApplication sharedApplication].keyWindow.rootViewController;
    }
    return ctrol;
}

//-----------------自己加的两个方法-------------------//
//初始化key
void InitUmengKeyiOS(const char* QqKey,
                  const char* QqSecret,
                  const char* WeixinKey,
                  const char* WeixinSecret,
                  const char* SinaKey,
                  const char* SinaSecret,
                     const char* UmengKey){
    
    
    [[UMSocialManager defaultManager] openLog:YES];
    NSLog(@"UMeng social version: %@", [UMSocialGlobal umSocialSDKVersion]);
    [UMSocialGlobal shareInstance].type = @"u3d";
    
    NSString *qqKey = [NSString stringWithUTF8String:QqKey];
    NSString *qqSecret = [NSString stringWithUTF8String:QqSecret];
    NSString *weixinKey = [NSString stringWithUTF8String:WeixinKey];
    NSString *weixinSecret = [NSString stringWithUTF8String:WeixinSecret];
    NSString *sinaKey = [NSString stringWithUTF8String:SinaKey];
    NSString *sinaSecret = [NSString stringWithUTF8String:SinaSecret];
    NSString *umengKey = [NSString stringWithUTF8String:UmengKey];
    
    [[UMSocialManager defaultManager]   setUmSocialAppkey:umengKey];
    
    //设置微信的appKey和appSecret
    [[UMSocialManager defaultManager] setPlaform:UMSocialPlatformType_WechatSession appKey:weixinKey appSecret:weixinSecret redirectURL:@"http://mobile.umeng.com/social"];
    //设置分享到QQ互联的appID
    [[UMSocialManager defaultManager] setPlaform:UMSocialPlatformType_QQ appKey:qqKey/*设置QQ平台的appID*/  appSecret:nil redirectURL:@"http://mobile.umeng.com/social"];
    //设置新浪的appKey和appSecret
    [[UMSocialManager defaultManager] setPlaform:UMSocialPlatformType_Sina appKey:sinaKey  appSecret:sinaSecret redirectURL:@"http://sns.whalecloud.com/sina2/callback"];
    
}
//打开分享面板仅分享图片
void openShareWithImageOnlyiOS(int platform[],
                               int platformNum,
                               const char* imagePath,
                               ShareDelegate callback){
    
   
    NSMutableArray *arr = [NSMutableArray arrayWithCapacity:platformNum];
    for (int i = 0;i<platformNum;i++) {
        
        int d = platform[i];
        UMSocialPlatformType type = getPlatformString(d);
        [arr addObject:@(type)];
        
    }
    [UMSocialUIManager setPreDefinePlatforms:[arr copy]];
    
    id image = nil;
    image = [NSString stringWithUTF8String:imagePath];
    UMSocialMessageObject *messageObject = [UMSocialMessageObject messageObject];
    if (image!=nil){
            UMShareImageObject *shareObject = [[UMShareImageObject alloc] init];
            [shareObject setShareImage:image];
            messageObject.shareObject = shareObject;
        }
    [UMSocialUIManager showShareMenuViewInWindowWithPlatformSelectionBlock:^(UMSocialPlatformType platformType, NSDictionary *userInfo) {
        [[UMSocialManager defaultManager] shareToPlatform:platformType messageObject:messageObject currentViewController:getCurrentViewController() completion:^(id data, NSError *error) {
            int code;
            NSString* message;
            if (error) {
                code = error.code;
                NSLog(@"************Share fail with error %@*********",error);
                message =error.description;
            }else{
                code = 200;
                if ([data isKindOfClass:[UMSocialShareResponse class]]) {
                    UMSocialShareResponse *resp = data;
                    //分享结果消息
                    NSLog(@"response message is %@",resp.message);
                    //第三方原始返回的数据
                    NSLog(@"response originalResponse data is %@",resp.originalResponse);
                    code = 200;
                    message =@"success";
                }else{
                    NSLog(@"response data is %@",data);
                    message =@"unkonw fail";
                }
            }
            callback(getPlatformself(platformType), code,[message UTF8String]);
            
        }];
    }];
    
    
}



void authorize(int platform, AuthHandler callback){
    	
      [[UMSocialManager defaultManager]  getUserInfoWithPlatform: getPlatformString(platform) currentViewController:getCurrentViewController() completion:^(id result, NSError *error) {
         NSLog(@" auth call back");
        NSString *message = nil;
        int code = 200;
        NSString *key =@"";
        NSString *value = @"";
        if (error) {
            NSLog(@"Auth fail with error %@", error);
            message = @"Auth fail";
            code = error.code;
            key = [key stringByAppendingString:@"mesaage"];
            value=[value stringByAppendingString:message];
       
        }else{
            if ([result isKindOfClass:[UMSocialUserInfoResponse class]]) {
                UMSocialUserInfoResponse *resp = result;
                // 授权信息
                if (resp.uid) {
                    key = [key stringByAppendingString:@"uid"];
                    value=[value stringByAppendingString:resp.uid];
                }
                
                if (resp.accessToken) {
                    key = [key stringByAppendingString:@",accessToken"];
                    value=[value stringByAppendingString:@","];
                    value=[value stringByAppendingString:resp.accessToken];
                }
                
                if (resp.refreshToken) {
                    key = [key stringByAppendingString:@",refreshToken"];
                    value=[value stringByAppendingString:@","];
                    value=[value stringByAppendingString:resp.refreshToken];
                                  }
                if (resp.name) {
                    key = [key stringByAppendingString:@",name"];
                    value=[value stringByAppendingString:@","];
                    value=[value stringByAppendingString:resp.name];
                }
                if (resp.gender) {
                    key = [key stringByAppendingString:@",gender"];
                    value=[value stringByAppendingString:@","];
                    value=[value stringByAppendingString:resp.gender];
                }
                if (resp.iconurl) {
                    key = [key stringByAppendingString:@",iconurl"];
                    value=[value stringByAppendingString:@","];
                    value=[value stringByAppendingString:resp.iconurl];
                }
                
            }
            else{
                NSLog(@"Auth fail with unknow error");
                key = [key stringByAppendingString:@"mesaage"];
                value=[value stringByAppendingString:@"Auth fail with unknow error"];

            }
        }
        NSLog(@"key=%s",[key UTF8String]);
         NSLog(@"value=%s",[value UTF8String]);
        callback(getPlatformself(platform), code, [key UTF8String], [value UTF8String]);
    }];

}

void deleteAuthorization(int platform, AuthHandler callback){
    [[UMSocialManager defaultManager] cancelAuthWithPlatform:getPlatformString(platform) completion:^(id result, NSError *error) {
        NSString *key =@"";
        NSString *value = @"";

        if (!error) {
            key = [key stringByAppendingString:@"mesaage"];
            value=[value stringByAppendingString:@"Auth fail with unknow error"];
            

            
            callback(getPlatformself(platform), 200, [key UTF8String], [value UTF8String]);
        }else {
            key = [key stringByAppendingString:@"mesaage"];
            value=[value stringByAppendingString:@"Auth fail with unknow error"];
            

            
            callback(getPlatformself(platform), -1, [key UTF8String], [value UTF8String]);
        }
        
    }];
}

bool isAuthorized(int platform){
    
   return [[UMSocialDataManager defaultManager ] isAuth:getPlatformString(platform)];
}

id getImageFromFilePath(const char* imagePath){
    id returnImage = nil;
    
    if (imagePath) {
        NSString *imageString = [NSString stringWithUTF8String:imagePath];
        
        if ([imageString hasPrefix:@"http"]) {
            returnImage = imageString;
        }else{
            returnImage = [UIImage imageNamed:imageString];
        }
        
    }
    return returnImage;
//    if (imagePath) {
//        NSString *imageString = getNSStringFromCStr(imagePath);
//        //传入整个路径
//        if ([imageString rangeOfString:@"/"].length > 0){
//            if ([imageString.lowercaseString hasSuffix:@".gif"]) {
//                NSString *path = [[NSBundle mainBundle] pathForResource:[[imageString componentsSeparatedByString:@"."] objectAtIndex:0]
//                                                                 ofType:@"gif"];
//                
//                returnImage = [NSData dataWithContentsOfFile:path];
//            } else{
//                returnImage = [UIImage imageNamed:getNSStringFromCStr(imagePath)];
//            }
//        }
//        else {      //只传文件名
//            if ([imageString.lowercaseString hasSuffix:@".gif"]) {
//                NSString *path = [[NSBundle mainBundle] pathForResource:[[imageString componentsSeparatedByString:@"."] objectAtIndex:0]
//                                                                 ofType:@"gif"];
//                
//                returnImage = [NSData dataWithContentsOfFile:path];
//            } else{
//                returnImage = [UIImage imageNamed:getNSStringFromCStr(imagePath)];
//            }
//        }
////        NSLog(@"return Image is %@",returnImage);
//      //  [UMSocialData defaultData].urlResource.resourceType = UMSocialUrlResourceTypeDefault;
//    }
//    return returnImage;
}
void setDismissCallback(ShareBoardDismissDelegate callback){
    
    [UMSocialUIManager setShareMenuViewDelegate:[UMSocialBriage defaultManager]];
    [UMSocialBriage defaultManager]->callback = callback;

}
void openShareWithImagePath(int platform[], int platformNum, const char* text, const char* imagePath,const char* title,const char* targeturl,ShareDelegate callback)
{
    
    id image = nil;
    NSString* nstargeturl = [NSString stringWithUTF8String:targeturl];
    NSString* nstext = [NSString stringWithUTF8String:text];
    NSString* nstitle = [NSString stringWithUTF8String:title];
    NSMutableArray *arr = [NSMutableArray arrayWithCapacity:platformNum];
    for (int i = 0;i<platformNum;i++) {
        
        int d = platform[i];
        UMSocialPlatformType type = getPlatformString(d);
        [arr addObject:@(type)];
        
    }
  [UMSocialUIManager setPreDefinePlatforms:[arr copy]];
    image = getImageFromFilePath(imagePath);
    UMSocialMessageObject *messageObject = [UMSocialMessageObject messageObject];
    if (nstargeturl==nil||nstargeturl.length==0) {
        if (image==nil) {
            
            messageObject.text =  nstext;
        }else{
            UMShareImageObject *shareObject = [[UMShareImageObject alloc] init];
            [shareObject setShareImage:image];
            messageObject.shareObject = shareObject;
        }
    }else{
        messageObject.text = [NSString stringWithUTF8String:text];
        UMShareWebpageObject *shareObject = [UMShareWebpageObject shareObjectWithTitle:nstitle descr:[NSString stringWithUTF8String:text] thumImage:image];
        //设置网页地址
        shareObject.webpageUrl =nstargeturl;
        
        //分享消息对象设置分享内容对象
        messageObject.shareObject = shareObject;
        
        
    }
    [UMSocialUIManager showShareMenuViewInWindowWithPlatformSelectionBlock:^(UMSocialPlatformType platformType, NSDictionary *userInfo) {
        //********新浪添加URL*********//
        if(platformType==UMSocialPlatformType_Sina){
            NSString *newContent = [NSString stringWithFormat:@"%@ https://%@",nstext,nstargeturl];
            NSLog(@"更改后的分享内容为%@",newContent);
            messageObject.text =  newContent;
        }
        [[UMSocialManager defaultManager] shareToPlatform:platformType messageObject:messageObject currentViewController:getCurrentViewController() completion:^(id data, NSError *error) {
            int code;
            NSString* message;
            if (error) {
                code = error.code;
                NSLog(@"************Share fail with error %@*********",error);
                message =error.description;
            }else{
                code = 200;
                if ([data isKindOfClass:[UMSocialShareResponse class]]) {
                    UMSocialShareResponse *resp = data;
                    //分享结果消息
                    NSLog(@"response message is %@",resp.message);
                    //第三方原始返回的数据
                    NSLog(@"response originalResponse data is %@",resp.originalResponse);
                    code = 200;
                    message =@"success";
                }else{
                    NSLog(@"response data is %@",data);
                    message =@"unkonw fail";
                }
            }
            callback(getPlatformself(platformType), code,[message UTF8String]);
            
        }];
    }];
//    [UMSocialUIManager showShareMenuViewInWindowWithPlatformSelectionBlock:^(UMShareMenuSelectionView *shareSelectionView, UMSocialPlatformType platformType) {
//        
//        [[UMSocialManager defaultManager] shareToPlatform:platformType messageObject:messageObject currentViewController:getCurrentViewController() completion:^(id data, NSError *error) {
//            int code;
//            NSString* message;
//            if (error) {
//                code = error.code;
//                NSLog(@"************Share fail with error %@*********",error);
//                message =@"************Share fail with error %@*********";
//            }else{
//                code = 200;
//                if ([data isKindOfClass:[UMSocialShareResponse class]]) {
//                    UMSocialShareResponse *resp = data;
//                    //分享结果消息
//                    NSLog(@"response message is %@",resp.message);
//                    //第三方原始返回的数据
//                    NSLog(@"response originalResponse data is %@",resp.originalResponse);
//                    code = 200;
//                    message =@"success";
//                }else{
//                    NSLog(@"response data is %@",data);
//                    message =@"unkonw fail";
//                }
//            }
//            callback(getPlatformself(platformType), code,[message UTF8String]);
//            
//        }];
//        
//    }];
}






void directShare(const char* text, const char* imagePath, const char* title,const char* targeturl,int platform, ShareDelegate callback){
    
    id image = nil;
    NSString* nstargeturl = [NSString stringWithUTF8String:targeturl];
    NSString* nstext = [NSString stringWithUTF8String:text];
    NSString* nstitle = [NSString stringWithUTF8String:title];
    
    image = getImageFromFilePath(imagePath);
    UMSocialMessageObject *messageObject = [UMSocialMessageObject messageObject];
    if (nstargeturl==nil||nstargeturl.length==0) {
        if (image==nil) {
            
            messageObject.text =  nstext;
        }else{
            UMShareImageObject *shareObject = [[UMShareImageObject alloc] init];
            [shareObject setShareImage:image];
            messageObject.shareObject = shareObject;
        }
    }else{
        messageObject.text = [NSString stringWithUTF8String:text];
        UMShareWebpageObject *shareObject = [UMShareWebpageObject shareObjectWithTitle:nstitle descr:[NSString stringWithUTF8String:text] thumImage:image];
        //设置网页地址
        shareObject.webpageUrl =nstargeturl;
        
        //分享消息对象设置分享内容对象
        messageObject.shareObject = shareObject;
        
        
    }
    //********新浪添加URL*********//
    if(platform==UMSocialPlatformType_Sina){
        NSString *newContent = [NSString stringWithFormat:@"%@ https://%@",nstext,nstargeturl];
        NSLog(@"更改后的分享内容为%@",newContent);
        messageObject.text =  newContent;
    }
    [[UMSocialManager defaultManager] shareToPlatform:getPlatformString(platform) messageObject:messageObject currentViewController:getCurrentViewController() completion:^(id data, NSError *error) {
        int code;
        NSLog(@"dddddd");
        NSString* message;
        if (error) {
            code = error.code;
            NSLog(@"************Share fail with error %@*********",error);
            message =error.description;
        }else{
            code = 200;
            if ([data isKindOfClass:[UMSocialShareResponse class]]) {
                UMSocialShareResponse *resp = data;
                //分享结果消息
                NSLog(@"response message is %@",resp.message);
                //第三方原始返回的数据
                NSLog(@"response originalResponse data is %@",resp.originalResponse);
                code = 200;
                message =@"success";
            }else{
                NSLog(@"response data is %@",data);
                message =@"unkonw fail";
            }
        }
        callback(platform, code, [message UTF8String]);
        
    }];
}
