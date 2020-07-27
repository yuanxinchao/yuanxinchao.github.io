using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.VisitorSpace
{
    public abstract class Visitor
    {
        public abstract void VisitCharacter(Character c);
        public abstract void VisitRow(Row r);
        public abstract void VisitImage(Image I);
    }
}
