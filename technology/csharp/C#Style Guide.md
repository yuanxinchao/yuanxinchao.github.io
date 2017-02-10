###C#编程规范  转自[raywenderlich](https://github.com/raywenderlich/c-sharp-style-guide#nomenclature)
**PascalCase(帕斯卡拼写法)。**  
即：将描述变量作用所有单词的首字母大写，然后直接连接起来，单词之间没有连接符和下划线。   
**camelCase(骆驼拼写法)**
即：第一个词的首字母小写，后面每个词的首字母大写。

--- 
####Namespaces 命名空间
对于命名空间一律采用PascalCase(帕斯卡拼写法)。  
即：将描述变量作用所有单词的首字母大写，然后直接连接起来，单词之间没有连接符和下划线。  
#####BAD:
`com.raywenderlich.fpsgame.hud.healthbar`
#####GOOD:
`RayWenderlich.FPSGame.HUD.Healthbar`

---
####Classes & Interfaces 类和接口
PascalCase(帕斯卡拼写法)，例如 `RadialSlider `。
####Methods 方法
PascalCase(帕斯卡拼写法)，例如 `DoSomething() `。
####Fields 变量名 
**非静态变量(私有、公开、保护)**:camelCase(骆驼拼写法)  
例如：  

    public class MyClass 
	{
	    public int publicField;
	    int packagePrivate;
	    private int myPrivate;
	    protected int myProtected;
	}
#####BAD:
`private int _myPrivateVariable`
#####GOOD:
`private int myPrivateVariable`		 
**静态变量(私有、公开、保护)**:PascalCase(帕斯卡拼写法)
例如 `public static int TheAnswer = 42;`

---
####Parameters 参数
camelCase(骆驼拼写法)  
#####BAD:
`void doSomething(Vector3 Location)`
#####GOOD:
`void DoSomething(Vector3 location)`  

除非是用于循环的临时变量，否则不要用单字符作为变量名。 

--- 
####Delegates 委托
PascalCase(帕斯卡拼写法)。  
用于事件触发的委托要加上**EventHandler**后缀。
#####BAD:
`public delegate void Click()`
#####GOOD:
`public delegate void ClickEventHandler()`   

其他的则加上**Callback**后缀
#####BAD:
`public delegate void Render()`
#####GOOD:
`public delegate void RenderCallback()` 

---  
####Misc 带有缩略词的变量名
缩略词应该看做成一个单词。例如：
#####BAD:
	XMLHTTPRequest
	String URL
	findPostByID
#####GOOD:
	XmlHttpRequest
	String url
	findPostById  
---
####Declarations 声明
#####Access Level Modifiers  访问修饰符
访问修饰符(public,protected,internal,private)修饰类、方法、变量是应该被明确声明。  
#####Fields & Variables  字段和变量
一行声明一个。
#####BAD:
`string username, twitterHandle;`
#####GOOD:
	string username;
	string twitterHandle;
#####Classes 类
一个文件里面只放一个类。内部类则放在一起。
##### Interfaces 接口
所有的接口必须以**I**开头。
#####BAD:
`RadialSlider`
#####GOOD:
`IRadialSlider `
##### Indentation 缩进
缩进应该使用空格 - 不要使用Tab键。
##### Blocks 块
块缩进使用4个空格。  

*注:Monodevelop中，解决方案选项->源代码->code formatting->Text file->Convert tabs to spaces 可以将tab转换为四个空格，Shift＋Tab则是删除四个空格，很方便*  
#####BAD:
	for (int i = 0; i < 10; i++) 
	{
 	  Debug.Log("index=" + i);
	}
#####GOOD:
	for (int i = 0; i < 10; i++) 
	{
	    Debug.Log("index=" + i);
	}
##### Line Wraps 换行
换行使用四个空格。
#####BAD:
	CoolUiWidget widget =
    		someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);

#####GOOD:
	CoolUiWidget widget =
    	someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
##### Line Length 每行的长度
每行的长度不应该大于100个字符。
##### Vertical Spacing 行间距
* 方法之间用空白行隔开。
* 以功能划分方法。
* 如果一个方法包含太多部分你应该分离成多个方法

---
#### Brace Style 大括号风格
一个大括号独占一行
#####BAD:
	class MyClass {
	    void DoSomething() {
	        if (someTest) {
	          // ...
	        } else {
	          // ...
	        }
	    }
	}

#####GOOD:
	class MyClass
	{
	    void DoSomething()
	    {
	        if (someTest)
	        {
	          // ...
	        }
	        else
	        {
	          // ...
	        }
	    }
	}
条件语句里的内容不管有几行都要用大括号包起来。  
#####BAD:
	if (someTest)
	    doSomething();  
	
	if (someTest) doSomethingElse();
#####GOOD:
	if (someTest) 
	{
	    DoSomething();
	}  
	
	if (someTest)
	{
	    DoSomethingElse();
	}
注：Monodevelop中可以在*解决方案选项->源代码->code formatting*更改大括号的默认设置  

---
#### Switch Statements 分支语句
不要写default case。因为如果你的代码写的正确的话永远也运行不到default case。😂
#####BAD:
	switch (variable) 
	{
	    case 1:
	        break;
	    case 2:
	        break;
	    default:
	        break;
	}
#####GOOD:
	switch (variable) 
	{
	    case 1:
	        break;
	    case 2:
	        break;
	}
---
#### Language 语言
用美式英语。😂
#####BAD:
	string colour = "red";
#####GOOD:
	string color = "red";
---
#### Copyright Statement
The following copyright statement should be included at the top of every source file:  

	/*
	 * Copyright (c) 2017 Razeware LLC
	 * 
	 * Permission is hereby granted, free of charge, to any person obtaining a copy
	 * of this software and associated documentation files (the "Software"), to deal
	 * in the Software without restriction, including without limitation the rights
	 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	 * copies of the Software, and to permit persons to whom the Software is
	 * furnished to do so, subject to the following conditions:
	 * 
	 * The above copyright notice and this permission notice shall be included in
	 * all copies or substantial portions of the Software.
	 * 
	 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	 * THE SOFTWARE.
	 */
---
#### Smiley Face
Smiley faces are a very prominent style feature of the raywenderlich.com site! It is very important to have the correct smile signifying the immense amount of happiness and excitement for the coding topic. The closing square bracket ] is used because it represents the largest smile able to be captured using ASCII art. A closing parenthesis (":)") creates a half-hearted smile, and thus is not preferred.
#####BAD:
:)
#####GOOD:
:]  
>>NOTE: Do not use smileys in your scripts.


