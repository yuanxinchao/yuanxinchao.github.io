## Resharper卡顿问题

虽然Resharper给我们编写代码带来了很多便利，但是同时也会让我们的vs十分卡顿，这里有几个设置可以让我们在享受ReSharper带来的方便的同时让其更加流畅。

出处：https://www.cnblogs.com/zhaoqingqing/p/3896826.html

1.**Visual Studio的配置**

ReSharper的与其他Visual Studio加载项和扩展可能会发生冲突-如果速度变慢，请尝试禁用的加载项等一个接一个，检查它是否有利于加速VS与ReSharper的。下面是与其他加载项已知的兼容性问题的一些例子：

- - Productivity Power Tools
  - VSCommands

此外，您还可以尝试下关闭下列选项“工具|选项|环境|常规”[Tools | Options | Environment | General]：

- 基于客户端性能自动调整视觉体验
- 使用硬件图形加速（如果可用）
- - Automatically adjust visual experience based on client performance
  - Use hardware graphics acceleration if available

2.**ReSharper的配置**

虽然ReSharper的提供了不少功能强大且实用的功能，其中一些可以调整或改善的速度方面处于关闭状态。下面是一些例子：

- 关闭在解决方案范围的分析（SWA）“的ReSharper |选项|代码检查|设置”，“分析整体解决方案的错误”复选框
- 切换到Visual Studio中的智能感知“的ReSharper |选项|环境|智能感知|常规”对话框
- 清除高速缓存中“的ReSharper |选项|环境|大将军”目前的解决方案对话框
- - Turn off Solution Wide Analysis (SWA) in "ReSharper | Options | Code Inspection | Settings", 'Analyze errors in whole solution' checkbox
  - Switch back to Visual Studio IntelliSense in "ReSharper | Options | Environment | IntelliSense | General" dialog
  - Clearing caches for current solution in "ReSharper | Options | Environment | General" dialog

3.**更详细的操作请看**

Jb官网相关https://www.jetbrains.com/help/resharper/2019.3/Speeding_Up_ReSharper.html