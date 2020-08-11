using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.GlyphSpace;
using ConsoleApplication1.IteratorSpace;
using ConsoleApplication1.VisitorSpace;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Glyph g = new Character();
            var a = new PreorderIterator<Glyph>(g);


            SpellingCheckingVisitor spVisitor = new SpellingCheckingVisitor();

            for (a.First(); a.IsDone();a.Next())
            {
                g = a.CurrentItem();
                g.Accept(spVisitor);
            }



            //**************----------------**************
            TextView textView = new TextView();
            Window w = new Window();
            w.SetContents(textView);
            w.SetContents(new BorderDecorator(new ScrollDecorator(textView), 2));



            //**************-------------------************
            var row = new Row();
            row.Insert(new ImageProxy("path"),0);

        }
    }

}

