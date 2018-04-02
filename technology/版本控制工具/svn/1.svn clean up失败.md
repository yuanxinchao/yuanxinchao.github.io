#### svn clean up失败 ####
svn clean up 的时候报错

	svn: E155009: Failed to run the WC DB work queue associated with 'F:\Devel\bc\dev\trunk\appShare\media\frontend\?_12x15.png', work item 53314 (file-install appShare/media/frontend/?_12x15.png 1 0 1 1)
	svn: E720123: Can't move 'F:\Devel\bc\dev\trunk\.svn\tmp\svn-68A36D23' to 'F:\Devel\bc\dev\trunk\appShare\media\frontend\?_12x15.png': The filename, directory name, or volume label syntax is incorrect.

极有可能是数据库如sqlite 里的db被改动过的原因。  
解决办法：  
mac下：
	
	cd {work-dir-base}
	sqlite3 .svn/wc.db "delete from work_queue"
到工作路径下 执行上述命令即可。  

windows下比较麻烦。需要下载sqlite3.exe 执行上述命令即可。  
下载地址：[sqlite-tools-win32-x86-3180000.zip
(1.56 MiB)](http://www.sqlite.org/download.html)