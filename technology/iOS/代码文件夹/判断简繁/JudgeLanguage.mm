char* StringCopy (const char* string)
{
    
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    
    strcpy(res, string);
    
    return res;
    
}
extern "C"
{
    // ios手机的当前语言 "en"、“zh"、“zh-Hans"、"zh-Hant"
    char* CurIOSLang()
    {
       NSString * language = [[NSLocale preferredLanguages] objectAtIndex:0];
      return StringCopy([language UTF8String]);
    }
}
