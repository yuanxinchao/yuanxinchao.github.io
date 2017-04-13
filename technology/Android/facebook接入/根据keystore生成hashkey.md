Tomato2deMac-mini:document tomato2$ keytool -exportcert -alias tomato -keystore tomatojoy.keystore | openssl sha1 -binary | openssl base64
输入密钥库口令:  tomatojoy
xJvbwLFW5O8FKTNVY+LD7DHZN7o=  
⚠️：-alias tomato  
alias要对应alias
-keystore对应到路径文件