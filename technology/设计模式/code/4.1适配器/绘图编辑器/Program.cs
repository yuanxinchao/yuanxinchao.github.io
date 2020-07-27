using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace ConsoleApplication1
{
    //客户
    public class Program
    {
        static void Main(string[] args)
        {
            Point _nowBottomLeft = null;
            Point _nowTopRight = null;

            Shape _shape = new TextShape();
            _shape.BoundingBox(_nowBottomLeft,_nowTopRight);
        }
    }
}