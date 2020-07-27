using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.IteratorSpace;
using ConsoleApplication1.VisitorSpace;

namespace ConsoleApplication1.GlyphSpace
{
    public class Character:Glyph
    {
        private char c;
        public override void Draw(Window w)
        {
            w.DrawCharacter(c);
        }

        public override Rect Bounds()
        {
            return null;
        }

        public override bool Intersects(Point w)
        {
            return false;
        }

        public override void Insert(Glyph g, int index)
        {
        }

        public override void Remove(Glyph g)
        {
        }

        public override Glyph Child(int index)
        {
            return null;
        }

        public override Glyph Parent()
        {
            return null;
        }

        public override void SetParent(Glyph g)
        {
        }

        public char GetCharCode()
        {
            return c;
        }

        public override void Accept(Visitor v)
        {
            base.Accept(v);
            v.VisitCharacter(this);
        }
    }
}
