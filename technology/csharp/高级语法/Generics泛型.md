#### Generics 泛型
2.0 版 C# 语言和公共语言运行时 (CLR) 中增加了泛型。 泛型将类型参数的概念引入 .NET Framework，类型参数使得设计如下类和方法成为可能：**这些类和方法将一个或多个类型的指定推迟到客户端代码声明并实例化该类或方法的时候。**  
比如：我要把它转换成字符串，重点在转换成字符串这个方法，不考虑它的类型。所以这个类型它必须要能够转换成字符串，或者有这个方法。因此要对它进行约束。		
##### 泛型声明好之后的调用
调用的时候，客户端代码必须通过指定尖括号中的类型参数来声明和实例化构造类型。即：要将具体的**T**类型用实际类型替换掉  
假如声明是这样的：

	public class GenericList<T> 
	{
		public GenericList() 
		{
			head = null;
		}
	｝
那么调用就是这样的：  

	GenericList<float> list1 = new GenericList<float>();
	GenericList<ExampleClass> list2 = new GenericList<ExampleClass>();
	GenericList<ExampleStruct> list3 = new GenericList<ExampleStruct>();  
##### Constraints 约束  
在定义泛型类时，可以对客户端代码能够在实例化类时用于类型参数的类型种类施加限制。下图列出了六种类型的约束：  
![learnPic1](./learnPic/learnPic1.png)  
一条有用的规则是，应用尽可能最多的约束，但仍使您能够处理必须处理的类型。 例如，如果您知道您的泛型类仅用于引用类型，则应用类约束。 这可以防止您的类被意外地用于值类型，并允许您对 T 使用 as 运算符以及检查空值。
##### default 关键字
无法确定**T**为引用类型，值类型，结构类型时，想赋null或0时可以使用`T temp = default(T);`
