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
@end

void registerLocalNotification(int notifyTime,const char* message){
    NSString *pushMessage = [NSString stringWithUTF8String:message];
    NSInteger alertTime =notifyTime;
    [LocalPush registerLocalNotification:alertTime withNote:pushMessage];
}
void cancelLocalNotification(){
    [LocalPush cancelLocalNotificationWithKey];
}