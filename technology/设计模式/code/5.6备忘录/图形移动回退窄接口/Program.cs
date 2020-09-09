using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graphic a = new Graphic();
            Graphic b = new Graphic();
            ConnectCommand connectCommand = new ConnectCommand(a,b);
            connectCommand.Excute();

            MoveCommand cmd =new MoveCommand(b,new Point(100,100));
            cmd.Excute();

            cmd.Unexcute();
        }
    }

}

