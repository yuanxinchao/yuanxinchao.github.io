using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Adaptee
{
    public class Car
    {
        public void Refuel()
        {
            Console.WriteLine("加油");
        }

        public void Run()
        {
            Console.WriteLine("开的飞快");
        }

    }
}
