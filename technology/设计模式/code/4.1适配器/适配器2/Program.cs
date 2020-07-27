using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.Target;

namespace ConsoleApplication1
{
    //客户
    public class Program
    {
        static void Main(string[] args)
        {
//            IBeing being = new Dog();
//            being.Eat();
//            being.Move();
//
//            IBeing b = new RobotWrapper();
//            b.Eat();
//            b.Move();

            Thing t = new Thing();
            PluggableAdapter adapter = new PluggableAdapter(t);
            adapter.Register("Bird","Eat");
            adapter.Register("BirdWrapper","Eat");
            adapter.Register("RobotDog","Battery");
            adapter.EatBy("Bird");
            adapter.EatBy("BirdWrapper");
            adapter.EatBy("RobotDog");
        }
    }
}