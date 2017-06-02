## linq查询表达式
使用linq表达式在c#内部进行对一些数据操作会出奇的简单，下面简单介绍一下  
#### from  ####
* 查询表达式必须以 from 子句开头
*  from 子句中引用的数据源的类型必须为 IEnumerable、IEnumerable<T> 或一种派生类型（如 IQueryable<T>）。

---
#### select  ####
*  查询表达式必须以 select 子句或 group 子句结束
*  select 子句可以指定将在执行查询时产生的值。  

---
#### where  ####
* 指定将在查询表达式中返回数据源中的哪些元素  
* 一个查询表达式可以包含多个 where 子句

---
#### let ####

* 起到替代作用  

如下面的两查询语句结果相同

	var earlyBirdQuery =
	from sentence in strings
	let words = sentence.Split(' ')
	from word in words
	let w = word.ToLower()
	where w[0] == 'a'
	select word;

	var earlyBirdQuery =
	from sentence in strings
	from word in sentence.Split(' ')
	let w = word.ToLower()
	where w[0] == 'a'
	select word;

---
#### orderby  ####
* orderby 子句可使返回的序列或子序列（组）按升序或降序排序
* 可以指定多个键，即可以指定多个次要排序
* 降序descending和升序ascending  

-

	IEnumerable<Student> sortedStudents =
	    from student in students
	    orderby student.Last ascending, student.First ascending
	    select student;

---
#### group  ####
* 根据特定键值对数据进行分组
* 分组结果为 IGrouping<TKey, TElement> 类型  

如：

	string[] groupingQuery = { "carrots", "cabbage", "broccoli", "beans", "barley" };
	IEnumerable<IGrouping<char, string>> queryFoodGroups =
	    from item in groupingQuery
	    group item by item[0];
	foreach(IGrouping<char, string> agroup in queryFoodGroups)
	{
	    foreach (string st in agroup)
	    {
	        Debug.Log(agroup.Key+"-"+st);
	    }
	}

---
#### join  ####
* 以相同key配对联接两表中数据
* equals 左键使用外部源序列，而右键使用内部源序列。  

如：  

	var innerJoinQuery =
	        from category in categories
	        join prod in products on category.ID equals prod.CategoryID
	        select new { ProductName = prod.Name, Category = category.Name }; //produces flat sequence
示例演示一个简单的内部同等联接。 此查询产生一个“产品名称/类别”对平面序列。 同一类别字符串将出现在多个元素中。 如果 categories 中的某个元素不具有匹配的 products，则该类别不会出现在结果中。
#### into  ####
* 使用 into 上下文关键字创建一个临时标识符，与let有异曲同工之妙
* 具有推断类型 IGrouping

如：

	string[] words = { "apples", "blueberries", "oranges", "bananas", "apricots"};
	
	        // Create the query.
	        var wordGroups1 =
	            from w in words
	            group w by w[0] into fruitGroup
	            where fruitGroup.Count() >= 2
	            select new { FirstLetter = fruitGroup.Key, Words = fruitGroup.Count() };

---
#### 简单的查询 ####
	List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
	
	// The query variable can also be implicitly typed by using var
	IEnumerable<int> filteringQuery =
	    from num in numbers
	    where num < 3 || num > 7
	    orderby num ascending
	    select num;
	foreach(int num in filteringQuery)
	{
	    Debug.Log(num);
	
	}
   



#### 创建嵌套组 ####  

	 var queryNestedGroups =
                from student in students
                group student by student.Year into newGroup1
                from newGroup2 in
                    (from student in newGroup1
                     group student by student.LastName)
                group newGroup2 by newGroup1.Key;  


#### 动态指定谓词where筛选器 ####

	class DynamicPredicates : StudentClass
	{
	    static void Main(string[] args)
	    {
	        string[] ids = { "111", "114", "112" };
	
	        Console.WriteLine("Press any key to exit.");
	        Console.ReadKey();
	    }
	
	    static void QueryByID(string[] ids)
	    {
	        var queryNames =
	            from student in students
	            let i = student.ID.ToString()
	            where ids.Contains(i)
	            select new { student.LastName, student.ID };
	
	        foreach (var name in queryNames)
	        {
	            Console.WriteLine("{0}: {1}", name.LastName, name.ID);
	        }
	    }
	}


>* 注意： 在您循环访问 foreach 语句中的查询变量之前，不会执行查询(除非是单值)。 有关更多信息，请参见 Introduction to LINQ Queries (C#)。[这](https://msdn.microsoft.com/zh-cn/library/bb397676.aspx)  

	// Data source.
	string[] files = { "fileA.txt", "fileB.txt", "fileC.txt" };
	
	// Demonstration query that throws.
	var exceptionDemoQuery =
	    from file in files
	    let n = SomeMethodThatMightThrow(file)
	    select n;
	
	// Runtime exceptions are thrown when query is executed.
	// Therefore they must be handled in the foreach loop.
	try
	{
	    foreach (var item in exceptionDemoQuery)
	    {
	        Console.WriteLine("Processing {0}", item);
	    }
	}
	
	// Catch whatever exception you expect to raise
	// and/or do any necessary cleanup in a finally block
	catch (InvalidOperationException e)
	{
	    Console.WriteLine(e.Message);
	}

---
## 排序 ##
对于已经存在的一个list

	List<Student> student = new List<Student>();
	Student stu1 = new Student();
	stu1.score = 100;
	stu1.name = "yuan";
	student.Add(stu1);
	Student stu2 = new Student();
	stu2.score = 80;
	stu2.name = "kai";
	student.Add(stu2);
	
	Student stu3 = new Student();
	stu3.score = 120;
	stu3.name = "rui";
	student.Add(stu3);
	
	Student stu4 = new Student();
	stu4.score = 120;
	stu4.name = "fu";
	student.Add(stu4);
以score为主要排序方式，name为次要排序方式，排序代码为：  

	IEnumerable<Student> query = student.OrderBy(stu => stu.score).ThenBy(stu => stu.name);

	//与上述代码同一效果
	IEnumerable<Student> query =
	        from stu in student
	        orderby stu.score descending,stu.name descending
	        select stu;

如果想只对前3个学生进行排序：  

	IEnumerable<Student> studentQuery =
	        (from stu in student
             select stu).Take(num);
	IEnumerable<Student> stuQue =
	        from stu in studentQuery
	        orderby stu.score descending,stu.name descending
	        select stu;
