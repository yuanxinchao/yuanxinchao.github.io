//
//  UMSocialCSDK.h
//  UmengGame
//
//  Created by yeahugo on 14-6-26.
//
//

#ifndef __UmengGame__UMSocialCSDK__
#define __UmengGame__UMSocialCSDK__


extern "C"
{
    
typedef void (*AuthHandler)(int platform, int stCode, const char * key,const char * value);
typedef void (*ShareDelegate)(int platform, int stCode, const char * errorMsg);

typedef void (*ShareBoardDismissDelegate)();




//授权某社交平台
void authorize(int platform, AuthHandler callback);

//解除某平台授权
void deleteAuthorization(int platform, AuthHandler callback);

//是否已经授权某平台
bool isAuthorized(int platform);

//打开分享面板
void openShareWithImagePath(int platform[], int platformNum, const char* text, const char* imagePath,const char* title,const char* targeturl,ShareDelegate callback);
//设置取消监听
void setDismissCallback(ShareBoardDismissDelegate callback);
//直接分享到各个社交平台
void directShare(const char* text, const char* imagePath, const char* title,const char* targeturl,int platform, ShareDelegate callback);
    
//-----------------自己加的两个方法-------------------//
//初始化key
void InitUmengKeyiOS(const char* QqKey,
                  const char* QqSecret,
                  const char* WeixinKey,
                  const char* WeixinSecret,
                  const char* SinaKey,
                  const char* SinaSecret,
                      const char* UmengKey);
//打开分享面板仅分享图片
void openShareWithImageOnlyiOS(int platform[],
                               int platformNum,
                               const char* imagePath,
                               ShareDelegate callback);
//------------------------------------------------//
    

//是否安装某应用
BOOL IsInstalliOS(int platformNum);

}
#endif /* defined(__UmengGame__UMSocialCSDK__) */
