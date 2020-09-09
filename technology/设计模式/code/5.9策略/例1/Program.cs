﻿using System;
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
            Compositor _compositor = new SimpleCompositor();
            Composition _composition = new Composition(_compositor);
            _composition.Repair();
        }
    }

}

