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
            Application ap = new Application(Topic.Applition);
            Dialog d = new Dialog(ap,Topic.Dialog);
            Button b = new Button(d,Topic.Button);
            b.HandleHelp();
        }
    }

}

