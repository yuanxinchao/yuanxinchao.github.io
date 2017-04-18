//
//  LocalPush.m
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//  my name is jiajw

#import "OulaAnalyzeiOS.h"
#import <HolaStatisticalSDK/HolaStatisticalSDK.h>
NSString *ToNs(const char* key){
    return [NSString stringWithUTF8String:key];
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
