//
//  IOSPurchase.m
//  Unity-iPhone
//
//  Created by Tomato2 on 17/1/12.
//
//
#import "IOSPurchase.h"

//在内购项目中创的商品单号
#define ProductID_IAP18 @"com.pepper.ljsp.rmads"//18
#define ProductID_IAP30 @"com.fanqie.niukou.thirtycharge" //30
#define ProductID_IAP68 @"com.fanqie.niukou.sixtycharge" //68
//#define ProductID_IAP9p1000 @"Nada.JPYF04" //1000
//#define ProductID_IAP24p6000 @"Nada.JPYF05" //6000

@interface RechargeVC ()

@end
Paycallback pcb;
int goodnum;
@implementation RechargeVC
+(RechargeVC *)defaultChargeVC{
    static RechargeVC *RechargeVCInstance=nil;
    static dispatch_once_t predicate;
    dispatch_once(&predicate,^{RechargeVCInstance = [[self alloc]init];}
                  );
    return RechargeVCInstance;
}

-(void)addTransactionListener{
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}
-(void)buy:(int)type
{
    buyType = type;
    if ([SKPaymentQueue canMakePayments]) {
        [self RequestProductData];
        NSLog(@"允许程序内付费购买");
    }
    else
    {
        NSLog(@"不允许程序内付费购买");
        UIAlertView *alerView =  [[UIAlertView alloc] initWithTitle:@"提示"
                                                            message:@"您的手机没有打开程序内付费购买"
                                                           delegate:nil cancelButtonTitle:NSLocalizedString(@"关闭",nil) otherButtonTitles:nil];
        
        [alerView show];
        
    }
}
-(void)restore:(int)type
{
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}
-(void)RequestProductData
{
    NSLog(@"---------请求对应的产品信息------------");
    NSArray *product = nil;
    switch (buyType) {
        case IAP18:
            product=[[NSArray alloc] initWithObjects:ProductID_IAP18,nil];
            break;
        case IAP30:
            product=[[NSArray alloc] initWithObjects:ProductID_IAP30,nil];
            break;
        case IAP68:
            product=[[NSArray alloc] initWithObjects:ProductID_IAP68,nil];
            break;
            
        default:
            break;
    }
    NSSet *nsset = [NSSet setWithArray:product];
    SKProductsRequest *request=[[SKProductsRequest alloc] initWithProductIdentifiers: nsset];
    request.delegate=self;
    [request start];
    
}

//<SKProductsRequestDelegate> 请求协议
//收到的产品信息
- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response{
    
    NSLog(@"-----------收到产品反馈信息--------------");
    NSArray *myProduct = response.products;
    NSLog(@"产品Product ID:%@",response.invalidProductIdentifiers);
    NSLog(@"产品付费数量: %d", (int)[myProduct count]);
    // populate UI
    for(SKProduct *product in myProduct){
        NSLog(@"product info");
        NSLog(@"SKProduct 描述信息%@", [product description]);
        NSLog(@"产品标题 %@" , product.localizedTitle);
        NSLog(@"产品描述信息: %@" , product.localizedDescription);
        NSLog(@"价格: %@" , product.price);
        NSLog(@"Product id: %@" , product.productIdentifier);
    }
    SKPayment *payment = nil;
    switch (buyType) {
        case IAP18:
            payment  = [SKPayment paymentWithProductIdentifier:ProductID_IAP18];    //支付18
            break;
        case IAP30:
            payment  = [SKPayment paymentWithProductIdentifier:ProductID_IAP30];    //支付30            break;
        case IAP68:
            payment  = [SKPayment paymentWithProductIdentifier:ProductID_IAP68];    //支付68
            break;
        default:
            break;
    }
    NSLog(@"---------发送购买请求------------");
    [[SKPaymentQueue defaultQueue] addPayment:payment];
    
}
- (void)requestProUpgradeProductData
{
    NSLog(@"------请求升级数据---------");
    NSSet *productIdentifiers = [NSSet setWithObject:@"com.productid"];
    SKProductsRequest* productsRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:productIdentifiers];
    productsRequest.delegate = self;
    [productsRequest start];
    
}
//弹出错误信息
- (void)request:(SKRequest *)request didFailWithError:(NSError *)error{
    NSLog(@"-------弹出错误信息----------");
    UIAlertView *alerView =  [[UIAlertView alloc] initWithTitle:NSLocalizedString(@"Alert",NULL) message:[error localizedDescription]
                                                       delegate:nil cancelButtonTitle:NSLocalizedString(@"Close",nil) otherButtonTitles:nil];
    [alerView show];
    
}

-(void) requestDidFinish:(SKRequest *)request
{
    NSLog(@"----------反馈信息结束--------------");
    
}

-(void) PurchasedTransaction: (SKPaymentTransaction *)transaction{
    NSLog(@"-----PurchasedTransaction----");
    NSArray *transactions =[[NSArray alloc] initWithObjects:transaction, nil];
    [self paymentQueue:[SKPaymentQueue defaultQueue] updatedTransactions:transactions];
}

//<SKPaymentTransactionObserver> 千万不要忘记绑定，代码如下：
//----监听购买结果
//[[SKPaymentQueue defaultQueue] addTransactionObserver:self];

- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions//交易结果
{
    NSLog(@"-----paymentQueue--------");
    for (SKPaymentTransaction *transaction in transactions)
    {
        switch (transaction.transactionState)
        {
            case SKPaymentTransactionStatePurchased:{//交易完成
                [self completeTransaction:transaction];
                NSLog(@"-----交易完成 --------");
                
                UIAlertView *alerView =  [[UIAlertView alloc] initWithTitle:@""
                                                                    message:@"购买成功"
                                                                   delegate:self cancelButtonTitle:NSLocalizedString(@"关闭",nil) otherButtonTitles:nil];
                [alerView setTag:1];
                [alerView show];
                
            } break;
            case SKPaymentTransactionStateFailed://交易失败
            { [self failedTransaction:transaction];
                NSLog(@"-----交易失败 --------");
                UIAlertView *alerView2 =  [[UIAlertView alloc] initWithTitle:@"提示"
                                                                     message:@"购买失败，请重新尝试购买"
                                                                    delegate:self cancelButtonTitle:NSLocalizedString(@"关闭",nil) otherButtonTitles:nil];
                
                [alerView2 setTag:2];
                [alerView2 show];
                
            }break;
            case SKPaymentTransactionStateRestored://已经购买过该商品
                [self restoreTransaction:transaction];
                pcb(goodnum,YES);
                NSLog(@"-----已经购买过该商品 --------");
            case SKPaymentTransactionStatePurchasing:      //商品添加进列表
                NSLog(@"-----商品添加进列表 --------");
                break;
            default:
                break;
        }
    }
}

//alertview回调
-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{
    //    NSLog(@" button index=%ld is clicked.....", (long)buttonIndex);
    //     NSLog(@" alertView tag=%ld is clicked.....", (long)alertView.tag);
    if(buttonIndex==0&&alertView.tag==1){
        pcb(goodnum,YES);
    }
    if(buttonIndex==0&&alertView.tag==2){
        pcb(goodnum,NO);
    }
}
- (void) completeTransaction: (SKPaymentTransaction *)transaction

{
    NSLog(@"-----completeTransaction--------");
    // Your application should implement these two methods.
    NSString *product = transaction.payment.productIdentifier;
    if ([product length] > 0) {
        
        NSArray *tt = [product componentsSeparatedByString:@"."];
        NSString *bookid = [tt lastObject];
        if ([bookid length] > 0) {
            [self recordTransaction:bookid];
            [self provideContent:bookid];
        }
    }
    
    // Remove the transaction from the payment queue.
    
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
}

//记录交易
-(void)recordTransaction:(NSString *)product{
    NSLog(@"-----记录交易--------");
}

//处理下载内容
-(void)provideContent:(NSString *)product{
    NSLog(@"-----下载--------");
}

- (void) failedTransaction: (SKPaymentTransaction *)transaction{
    NSLog(@"失败");
    if (transaction.error.code != SKErrorPaymentCancelled)
    {
        
    }
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
}
-(void) paymentQueueRestoreCompletedTransactionsFinished: (SKPaymentTransaction *)transaction{
    NSLog(@"恢复购买有回调");
//    NSString* productIdentifier = @"";
//    NSLog(@"恢复购买有回调1");
//    switch (transaction.transactionState)
//    {
//            NSLog(@"恢复购买有回调2");
//
//        case SKPaymentTransactionStateRestored:
//        {
//            NSLog(@"恢复购买有回调3");
//            if (transaction.originalTransaction) {
//                productIdentifier = transaction.originalTransaction.payment.productIdentifier;
//                if([productIdentifier isEqualToString: ProductID_IAP18]){
//                    pcb(goodnum,YES);
//                }
//
//            }
//            else {
//                productIdentifier = transaction.payment.productIdentifier;
//                if([productIdentifier isEqualToString: ProductID_IAP18]){
//                    pcb(goodnum,YES);
//                }
//            }
//        }
//        default:
//            break;
//    }
//    pcb(goodnum,NO);
}

- (void) restoreTransaction: (SKPaymentTransaction *)transaction
{
    NSLog(@" 交易恢复处理");
    
}

-(void) paymentQueue:(SKPaymentQueue *) paymentQueue restoreCompletedTransactionsFailedWithError:(NSError *)error{
    NSLog(@"恢复购买失败");
    pcb(goodnum,NO);
}

#pragma mark connection delegate
- (void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data
{
    NSLog(@"%@",  [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding]);
}
- (void)connectionDidFinishLoading:(NSURLConnection *)connection{
    
}

- (void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response{
    switch([(NSHTTPURLResponse *)response statusCode]) {
        case 200:
        case 206:
            break;
        case 304:
            break;
        case 400:
            break;
        case 404:
            break;
        case 416:
            break;
        case 403:
            break;
        case 401:
        case 500:
            break;
        default:
            break;
    }
}

- (void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error {
    NSLog(@"test");
}

-(void)dealloc
{
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];//解除监听
    
}

@end

extern "C" {
    
    
    void InitAndBuy(int num,Paycallback paycallback){
        NSLog(@"购买id为%d的物品",num);
        pcb=paycallback;
        goodnum =num;
        [[RechargeVC defaultChargeVC] addTransactionListener];
        [[RechargeVC defaultChargeVC] buy:num];
       
    }
    void RestoreBuy(int num,Paycallback paycallback){
        NSLog(@"恢复购买id为%d的物品",num);
        pcb=paycallback;
        goodnum =num;
        [[RechargeVC defaultChargeVC] addTransactionListener];
        [[RechargeVC defaultChargeVC] restore:num];
    }
    
    
    
}







