//
//  LocalPush.m
//  myOwn
//
//  Created by myOwn on 16/4/8.
//  Copyright © 2016年 myOwn. All rights reserved.
//  my name is jiajw

extern "C"{
    void iOSShareText(const char * content){
        
        NSString *textToShare = [NSString stringWithUTF8String: content];
        
//        UIImage *imageToShare = [UIImage imageNamed:@"iosshare.jpg"];
        
        NSURL *urlToShare = [NSURL URLWithString:@"http://www.baidu.com"];
        
//        NSArray *activityItems = @[textToShare, imageToShare, urlToShare];
        NSArray *activityItems = @[textToShare, urlToShare];
        
        
        UIActivityViewController *activityVC = [[UIActivityViewController alloc]initWithActivityItems:activityItems applicationActivities:nil];
        
        activityVC.excludedActivityTypes = @[UIActivityTypePostToFacebook,UIActivityTypePostToTwitter, UIActivityTypePostToWeibo,UIActivityTypeMessage,UIActivityTypeMail,UIActivityTypePrint,UIActivityTypeCopyToPasteboard,UIActivityTypeAssignToContact,UIActivityTypeSaveToCameraRoll,UIActivityTypeAddToReadingList,UIActivityTypePostToFlickr,UIActivityTypePostToVimeo,UIActivityTypePostToTencentWeibo,UIActivityTypeAirDrop,UIActivityTypeOpenInIBooks];
        
  
        
        UIActivityViewControllerCompletionHandler myBlock = ^(NSString *activityType,BOOL completed) {
            
            NSLog(@"activityType :%@", activityType);
            
            if (completed)  {
                
                NSLog(@"completed");
            }  
            else  {
                NSLog(@"cancel"); 
            }
        };
        
        activityVC.completionHandler =myBlock;
    }
}