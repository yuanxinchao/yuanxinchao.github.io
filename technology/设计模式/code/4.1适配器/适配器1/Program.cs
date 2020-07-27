using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.Adapter;
using ConsoleApplication1.Target;

namespace ConsoleApplication1
{
    //客户
    public class Program
    {
        static void Main(string[] args)
        {
            IBeing being = new Dog();
            being.Eat();
            being.Move();

            IBeing b = new RobotWrapper();
            b.Eat();
            b.Move();
        }
    }
}