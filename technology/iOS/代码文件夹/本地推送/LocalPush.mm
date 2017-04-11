//
//  LocalPush.m
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//  my name is jiajw

#import "LocalPush.h"

@implementation LocalPush

+ (void)registerLocalNotification:(NSInteger)alertTime withNote:(NSString *)note{
    UILocalNotification *notification = [[UILocalNotification alloc] init];
    //设置alertTime秒后进行推送
    NSDate *fireDate = [NSDate dateWithTimeIntervalSinceNow:alertTime];
    
    //*****设置时间为11:30 － 12：30 或下午5:30-6：30避免影响用户
    int notiHour =11;
    int minute = 30;
    int interTime1 =  arc4random() % (60*60);
    int interTime2 =6*60*60+ arc4random() % (7*60*60 - 6*60*60+1);
    NSCalendar *gregorian = [[NSCalendar alloc] initWithCalendarIdentifier:NSGregorianCalendar];
    unsigned unitFlags = NSYearCalendarUnit | NSMonthCalendarUnit |  NSDayCalendarUnit | NSHourCalendarUnit;
    
    NSDateComponents *weekdayComponents =
    [gregorian components:unitFlags fromDate:fireDate];
    [weekdayComponents setHour:notiHour];
    [weekdayComponents setMinute: minute];
    fireDate = [gregorian dateFromComponents:weekdayComponents];
    
    int random = arc4random() % 2;
    if(random == 0){
        fireDate = [fireDate dateByAddingTimeInterval:interTime1];
    }
    else{
         fireDate = [fireDate dateByAddingTimeInterval:interTime2];
    }
    
    NSDateFormatter *formater = [[NSDateFormatter alloc] init];
    [formater setDateFormat:@"yyyy-MM-dd HH:mm:ss "];//设置时间显示的格式，此处使用的formater格式要与字符串格式完全一致，否则转换失败
    NSString *dateStr = [formater stringFromDate:fireDate];//将日期转换成字符串
    NSLog(@"sjsj%@",dateStr);

    //******************
    
    
    notification.fireDate = fireDate;
    //设置重复的间隔
    notification.repeatInterval = 0;
    //时区
    notification.timeZone = [NSTimeZone defaultTimeZone];

    //通知内容
    notification.alertBody = note;
    notification.applicationIconBadgeNumber = 1;
    notification.soundName = UILocalNotificationDefaultSoundName;
    
    //iOS8后,需要添加注册，才能得到授权
    if ([[UIApplication sharedApplication] respondsToSelector:@selector(registerUserNotificationSettings:)]) {
        UIUserNotificationType type = UIUserNotificationTypeAlert | UIUserNotificationTypeBadge | UIUserNotificationTypeSound;
        UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:type categories:nil];
        
        [[UIApplication sharedApplication] registerUserNotificationSettings:settings];
        //通知重复提示的单位，可以是天，周，日
        notification.repeatInterval  = 0;
        
    }else{
        notification.repeatInterval = NSCalendarUnitDay;
    }
    //执行通知注册
    [[UIApplication sharedApplication] scheduleLocalNotification:notification];
    NSLog(@"新建一个本地推送:%@",note);
}

+ (void)cancelLocalNotificationWithKey{
    NSLog(@"清除本地推送");
    [[UIApplication sharedApplication] setApplicationIconBadgeNumber: 0];
    [[UIApplication sharedApplication] cancelAllLocalNotifications];
}
+ (void) ShowTime:(NSData *)dataToShow {
    

}
@end

void registerLocalNotification(int notifyTime,const char* message){
    NSString *pushMessage = [NSString stringWithUTF8String:message];
    NSInteger alertTime =notifyTime;
    [LocalPush registerLocalNotification:alertTime withNote:pushMessage];
}
void cancelLocalNotification(){
    [LocalPush cancelLocalNotificationWithKey];
}