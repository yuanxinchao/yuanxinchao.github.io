using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Target
{
    public class Dog:IBeing
    {
        private int satiety;//饱食度
        private int pos;
        public void Eat()
        {
            satiety += 20;
            Console.WriteLine("吃了一个骨头");
        }

        public void Move()
        {
            pos += 10;
            Console.WriteLine("往前跑了一段");
        }
    }
}
