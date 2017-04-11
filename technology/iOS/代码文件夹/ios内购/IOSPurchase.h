//
//  IOSPurchase.h
//  Unity-iPhone
//
//  Created by Tomato2 on 17/1/12.
//
//

#ifndef IOSPurchase_h
#define IOSPurchase_h
#import <StoreKit/StoreKit.h>
enum{
    IAP2=2,
    IAP3,
    IAP4,
    IAP5,
    IAP6,
    IAP7,
    IAP8,
    IAP9,
    IAP10,
    IAP11,
    IAP12,
    IAP13
}buyCoinsTag;
extern "C"{
    typedef  void(*Paycallback)(int num,BOOL bo);
}
@interface RechargeVC :NSObject <SKPaymentTransactionObserver,SKProductsRequestDelegate >

{
    int buyType;
}
+ (RechargeVC *)defaultChargeVC;
- (void) requestProUpgradeProductData;

-(void)RequestProductData;

-(void)buy:(int)type;

- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions;

-(void) PurchasedTransaction: (SKPaymentTransaction *)transaction;

- (void) completeTransaction: (SKPaymentTransaction *)transaction;

- (void) failedTransaction: (SKPaymentTransaction *)transaction;

-(void) paymentQueueRestoreCompletedTransactionsFinished: (SKPaymentTransaction *)transaction;

-(void) paymentQueue:(SKPaymentQueue *) paymentQueue restoreCompletedTransactionsFailedWithError:(NSError *)error;

- (void) restoreTransaction: (SKPaymentTransaction *)transaction;

-(void)provideContent:(NSString *)product;

-(void)recordTransaction:(NSString *)product;

@end
#endif /* IOSPurchase_h */
