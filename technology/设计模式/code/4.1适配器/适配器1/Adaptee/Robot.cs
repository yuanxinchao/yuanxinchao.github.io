using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Adaptee
{
    public class Robot
    {
        //充电
        public void Battery()
        {
            Console.WriteLine("充电");
        }

        public void Move()
        {
            Console.WriteLine("空间跳跃");
        }
    }
}
