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
            //简单形式
//            MyClass receiver = new MyClass();
//            Command aCommand = new SimpleCommand(receiver.OnClick); 
//            aCommand.Excute();

//            Command open = new OpenCommand(application);
//            open.Excute();


            Application application = new Application();

            Command open = new OpenCommand(application,"SaveLog.txt");
            Command paste = new PasteCommand(application,"SaveLog.txt","ppppp");
            Command close = new CloseCommand(application.GetDocument("SaveLog.txt"),application);
            MacroCommand cmds = new MacroCommand();

            cmds.Add(open);
            cmds.Add(paste);
            cmds.Add(close);

            cmds.Excute();
        }
    }

}

