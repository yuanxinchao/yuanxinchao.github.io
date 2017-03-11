//
//  LocalPush.h
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//

#import <UIKit/UIKit.h>

#ifndef LocalPush_h
#define LocalPush_h
@interface LocalPush : UIView
+ (void)registerLocalNotification:(NSInteger)alertTime withNote:(NSString *)note;
+ (void)cancelLocalNotificationWithKey;
@end

extern "C"
{
    void registerLocalNotification(int notifyTime,const char* message);
    void cancelLocalNotification();
    
}

#endif /* LocalPush_h */
