$ git checkout -b iss53
Switched to a new branch "iss53"
它是下面两条命令的简写：

$ git branch iss53
$ git checkout iss53
git branch --list
git branch -d list
--decorate 标记会让git log显示每个commit的引用(如:分支、tag等) 

[alias]
lg1 = log --graph --abbrev-commit --decorate --format=format:'%C(bold blue)%h%C(reset) - %C(bold green)(%ar)%C(reset) %C(white)%s%C(reset) %C(dim white)- %an%C(reset)%C(bold yellow)%d%C(reset)' --all
lg2 = log --graph --abbrev-commit --decorate --format=format:'%C(bold blue)%h%C(reset) - %C(bold cyan)%aD%C(reset) %C(bold green)(%ar)%C(reset)%C(bold yellow)%d%C(reset)%n''          %C(white)%s%C(reset) %C(dim white)- %an%C(reset)' --all
lg = !"git lg1"
git lg/git lg1

git log --graph --decorate --all

强制合并
我们使用git checkout 将B分支上的系统消息功能添加到A分支上

$ git branch
  * A  
    B
    
$ git checkout B message.html message.css message.js other.js

$ git status
 On branch A
 Changes to be committed:
   (use "git reset HEAD <file>..." to unstage)

    new file:   message.css
    new file:   message.html
    new file:   message.js
    modified:   other.js



合并完成

注意：在使用git checkout某文件到当前分支时，会将当前分支的对应文件强行覆盖

这里新增文件没问题，但是A分支上原有的other.js会被强行覆盖，如果A分支上的other.js有修改，在checkout的时候就会将other.js内容强行覆盖，这样肯定是不行的。如何避免不强制覆盖，往下看。



智能合并
1.使用git checkout 将根据A分支创建一个A_temp分支，避免影响A分支

$ git checkout -b A_temp
Switched to a new branch 'A_temp'
2.将B分支合并到A_temp分支

$ git merge B
Updating 1f73596..04627b5
Fast-forward
 message.css                     | 0
 message.html                    | 0
 message.js                      | 0
 other.js                        | 1 +
 4 files changed, 1 insertion(+)
 create mode 100644 message.css
 create mode 100644 message.html
 create mode 100644 message.js
3.切换到A分支，并使用git checkout 将A_temp分支上的系统消息功能相关文件或文件夹覆盖到A分支


$ git checkout A
Switched to branch 'A'

$ git checkout A_temp message.html message.css message.js other.js

$ git status
 On branch A
 Changes to be committed:
   (use "git reset HEAD <file>..." to unstage)

    new file:   message.css
    new file:   message.html
    new file:   message.js
    modified:   other.js

    
ok，完结，这是工作中使用git合并总结的经验，仅供参考，有错误请指出，谢谢！


git stash: 备份当前的工作区的内容，从最近的一次提交中读取相关内容，让工作区保证和上次提交的内容一致。同时，将当前的工作区内容保存到Git栈中。
git stash pop: 从Git栈中读取最近一次保存的内容，恢复工作区的相关内容。由于可能存在多个Stash的内容，所以用栈来管理，pop会从最近的一个stash中读取内容并恢复。
git stash list: 显示Git栈内的所有备份，可以利用这个列表来决定从那个地方恢复。
git stash clear: 清空Git栈。此时使用gitg等图形化工具会发现，原来stash的哪些节点都消失了。