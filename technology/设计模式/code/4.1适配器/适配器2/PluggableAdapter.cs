using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApplication1
{

    //可插入适配器
    public class PluggableAdapter
    {
        private Thing adaptee;
        private Dictionary<string,MethodInfo> map = new Dictionary<string, MethodInfo>();//key class name  value method name

        public PluggableAdapter(Thing adaptee)//依赖注入
        {
            this.adaptee = adaptee;

        }

        public void Register(string className, string method)
        {
            var classN = "ConsoleApplication1." + className;
            try
            {
                Type t = Type.GetType(classN);
                MethodInfo m = t.GetMethod(method);
                map.Add(className,m);

//                Console.WriteLine(m.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Register wrong find no class= "+className + "---method="+method);
                throw;
            }
        }


        public void EatBy(string className)
        {
            try
            {
                var classN = "ConsoleApplication1." + className;
                Type t = Type.GetType(classN);
                //新建一个Class实例
                ConstructorInfo magicConstructor = t.GetConstructor(Type.EmptyTypes);
                var instance = magicConstructor.Invoke(new object[]{});

                adaptee.GetFood();

                if(map.ContainsKey(className))
                {
                    map[className].Invoke(instance,new object[]{});
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
