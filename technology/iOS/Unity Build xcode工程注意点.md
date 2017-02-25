## Unity Build xcode工程注意点
> * Other link Flag 添加-ObjC
> * Search Path 里将路径里的分号全部去掉
> * Compile Source 里检查是否对应设置ARC模式

---

##other link flag
Targets选项下有Other linker flags的设置，用来填写XCode的链接器参数，如：-ObjC -all_load -force_load等。

还记得我们在学习C程序的时候，从C代码到可执行文件经历的步骤是：

源代码 > 预处理器 > 编译器 > 汇编器 > 机器码 > 链接器 > 可执行文件

在最后一步需要把.o文件和C语言运行库链接起来，这时候需要用到ld命令。源文件经过一系列处理以后，会生成对应的.obj文件，然后一个项目必然会有许多.obj文件，并且这些文件之间会有各种各样的联系，例如函数调用。链接器做的事就是把这些目标文件和所用的一些库链接在一起形成一个完整的可执行文件。

如果要详细研究链接器做了什么，请看：http://www.dutor.net/index.php/2012/02/what-linkers-do/

那么，Other linker flags设置的值实际上就是ld命令执行时后面所加的参数。

下面逐个介绍3个常用参数：

－ObjC：加了这个参数后，链接器就会把静态库中所有的Objective-C类和分类都加载到最后的可执行文件中

－all_load：会让链接器把所有找到的目标文件都加载到可执行文件中，但是千万不要随便使用这个参数！假如你使用了不止一个静态库文件，然后又使用了这个参数，那么你很有可能会遇到ld: duplicate symbol错误，因为不同的库文件里面可能会有相同的目标文件，所以建议在遇到-ObjC失效的情况下使用-force_load参数。

-force_load：所做的事情跟-all_load其实是一样的，但是-force_load需要指定要进行全部加载的库文件的路径，这样的话，你就只是完全加载了一个库文件，不影响其余库文件的按需加载

 

解决方案：修改链接参数（Other Linker Flag）

1.去掉-ObjC -all_load参数(这个参数会强制所有的静态链接库都加载其中的category);

2.改为逐一加载xxxx的各个静态库，即修改链接参数（Other Linker Flag）为如下形式：

-force_load $(BUILT_PRODUCTS_DIR)/xxxxx.a 
## ARC
>如果最后报有关ARC的错误  
>转自：http://www.wanggq.cn/?post=127
Xcode 项目中我们可以使用 ARC 和非 ARC 的混合模式。  
如果你的项目使用的非 ARC 模式，则为 ARC 模式的代码文件加入 -fobjc-arc 标签。  
如果你的项目使用的是 ARC 模式，则为非 ARC 模式的代码文件加入 -fno-objc-arc 标签。  
添加标签的方法：  
打开：你的target -> Build Phases -> Compile Sources.
双击对应的 *.m 文件  
在弹出窗口中输入上面提到的标签 -fobjc-arc / -fno-objc-arc
点击 done 保存
