## Lanbda表达式 ##
可以将Lambda表达式看成匿名方法语法的拓展。  
首先要假设一个前提  

    delegate int TwoIntOperateDelegate(int paramA, int paramB);
    static void PerformOperations(TwoIntOperateDelegate del)
    {

    }
 
这样在调用的时候就可以使用  

	PerformOperations((paramA, paramB) => paramA + paramB);

	PerformOperations((int paramA,int paramB) => paramA + paramB);

	PerformOperations((int paramA, int paramB) => { return paramA + paramB; });

	PerformOperations(delegate(int paramA,int paramB)
        {
           return paramA + paramB;
        });
上面几种方法都是可以的，如果只有一个隐式类型化的参数就可以省略括号。如`paramA => paramA + paramA);`没有参数就使用空括号。  
分析第二种写法和第三种写法，可以发现，即使不用return，编译器根据先前delegate的声明也会推断出这个表达式的结果就是方法的返回类型。  
#### 不得不提一下的Action ####  
> * Action 不带参数，返回void
> * Action<>  最多8个参数，返回void
> *Func<>  最多8个参数，返回不是void，且返回类型始终在列表最后。如`Func<int,String> fc;`参数为一个int型，返回值为string型。  

所以DOTween里的一个函数
    `DOTween.To(() => myFloat, x => myFloat = x, 52, 1);`  
可能的声明是这样的
	`public static void To(Func<int> fc1, Action<int>fc2,int aid,int time)`  
当然，远不会这样简单