//
//  U3dInteractive.h
//  Unity-iPhone
//
//  Created by Tomato2 on 16/11/7.
//
//
#import <Foundation/Foundation.h>
#ifndef U3dInteractive_h
#define U3dInteractive_h
@interface U3dInteractive : NSObject
{
    NSString*			_appid;
}
@property (copy, nonatomic) NSString*			appid;

-(void)RateApp: (NSString *)appleid;
-(void)checkNetType;
@end
extern "C"{
    typedef void (*RateCB)(bool bo);
//    typedef void (*UserInfoCB)(const char* idfa,const char* resolution,const char* phoneVersion,const char* phoneModel);
void _RateApp1(const char* Appid,RateCB rateCB);
void _CopyURL (const char* url);
void _OpenWX ();
//void _GetUserInfo(UserInfoCB callback);
char* _getIDFA(); //需添加adsupport.Framework
char* _getResolution();
char* _getPhoneVersion();
char* _getPhoneModel();
char* _getIMSI();
void _getNetType();
   
}
#endif /* U3dInteractive_h */
