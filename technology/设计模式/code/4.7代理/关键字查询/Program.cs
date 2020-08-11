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
            Searchable searcher = new ProxySearch();
            String keyword = "football";
            String result = searcher.search(keyword);
            Console.WriteLine(result);
        }
    }

}

