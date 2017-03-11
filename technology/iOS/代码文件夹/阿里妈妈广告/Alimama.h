//
//  Alimama_h
//  Unity-iPhone
//
//  Created by Tomato2 on 16/11/7.
//
//
#import <Foundation/Foundation.h>
#import "MMUBanners.h"

#ifndef Alimama_h
#define Alimama_h
@interface Alimama : NSObject<MMUBannersDelegate,MMUBrowserDelegate>
{
   MMUBanners *banners;
}
-(void)ShowAliBanner: (BOOL )bo;
-(void)InitAlimamaiOS:(NSString*) aliId;
@end
extern "C"{
void ShowAliBanner(const BOOL bo);
void InitAlimamaiOS(const char* aliId);
}
#endif /* Alimama_h */
