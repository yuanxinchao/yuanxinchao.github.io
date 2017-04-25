//
//  OulaAnalyzeiOS.h
//  Unity-iPhone
//
//  Created by Tomato2 on 17/4/18.
//
//

#ifndef OulaAnalyzeiOS_h
#define OulaAnalyzeiOS_h

extern "C"
{
    void InitWithProductIdOula(const char* productId,const char* channelId,const char* AppId);
    void LogWithKeyOula(const char* eventKey,const char* eventValue);
    void LogWithOnlyKeyOula(const char* eventKey);
    void LogWithKeyAndDicOula(const char* eventKey,const char* jsondic);

    void CountWithKeyOula(const char* eventKey);
    void GAPLogOula();
    void CustomLogWithKeyOula(const char* eventKey,const char* eventValue);
    void STDebugManagerOula(bool openlog);
    void LogPaymentWithPlayerIdOula(const char* playerId,const char* receiptDataString);
    void FacebookLoginWithGameIdOula(const char* playerId,const char* openId,const char* openToken);
    void guestLoginWithGameIdOula(const char* playerId);
    char* getStaTokenOula();
    char* _getIDFA();
}





#endif /* OulaAnalyzeiOS_h */
