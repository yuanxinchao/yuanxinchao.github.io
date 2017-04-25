//
//  LocalPush.m
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//  my name is jiajw

#import "OulaAnalyzeiOS.h"
#import <Foundation/Foundation.h>
//获取IDFA
#import <AdSupport/AdSupport.h>
#import <HolaStatisticalSDK/HolaStatisticalSDK.h>
NSString *ToNs(const char* key){
    return [NSString stringWithUTF8String:key];
}
//转为可以return到u3d的char型
char* CopyString (const char* string)
{
    
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    
    strcpy(res, string);
    
    return res;
    
}
void InitWithProductIdOula(const char* productId,const char* channelId,const char* AppId){
    
    [HolaAnalysis initWithProductId:ToNs(productId) ChannelId:ToNs(channelId) AppID:ToNs(AppId)];
}
void LogWithKeyOula(const char* eventKey,const char* eventValue){
    [HolaAnalysis logWithKey:ToNs(eventKey) value:ToNs(eventValue)];
}
void LogWithOnlyKeyOula(const char* eventKey){
    [HolaAnalysis logWithKey:ToNs(eventKey) value:nil];
}
void CountWithKeyOula(const char* eventKey){
     [HolaAnalysis countWithKey:ToNs(eventKey)];
}
void GAPLogOula(){
[HolaAnalysis GAPLog];
}
void LogWithKeyAndDicOula(const char* eventKey,const char* jsondic){
    NSString *nsJsonDic =ToNs(jsondic);
    NSData *jsonData = [nsJsonDic dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err) {
        NSLog(@"json解析失败：%@",err);
    }
    else{
        NSLog(@"json解析成功%@",dic);
        [HolaAnalysis logWithKey:ToNs(eventKey) value:dic];
    }
}
char* getStaTokenOula(){
    NSString *nsStaToken  = [HolaAnalysis staToken];
    const char *staToke = [nsStaToken UTF8String];
    NSLog(@"staToke=%s",staToke);
    return CopyString(staToke);
}
void CustomLogWithKeyOula(const char* eventKey,const char* eventValue){
     [HolaAnalysis customLogWithKey:ToNs(eventKey) value:ToNs(eventValue)];
}
void STDebugManagerOula(bool openlog){
[[STDebugManager shareDebugManager] setIsDebug:openlog?YES:NO];

}
void LogPaymentWithPlayerIdOula(const char* playerId,const char* receiptDataString){
    
    [HolaAnalysis LogPaymentWithPlayerId:ToNs(playerId) receiptDataString:ToNs(receiptDataString)];
}
void FacebookLoginWithGameIdOula(const char* playerId,const char* openId,const char* openToken){
    [HolaAnalysis facebookLoginWithGameId:ToNs(playerId) OpenId:ToNs(openId)  OpenToken:ToNs(openToken)];
}
void guestLoginWithGameIdOula(const char* playerId){
[HolaAnalysis guestLoginWithGameId:ToNs(playerId)];
}

char* _getIDFA(){
    NSString *adId = [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
    NSLog(@"IDFA=%@",adId);
    return CopyString([adId UTF8String]);
}

