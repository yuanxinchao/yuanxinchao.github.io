using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.Builder;
using ConsoleApplication1.Director;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //客户知道该使用哪个Converter
            ASCIIConverter builder = new ASCIIConverter();
            RTFReader reader = new RTFReader(builder);
            reader.ParseRTF("21Aふジャパンsd2ㅓ춰모쿼1%^$^%$%^-asc/xzc.,asasda");
            builder.GetASCIIText();
        }
    }
}