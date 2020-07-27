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

        }
    }

}

