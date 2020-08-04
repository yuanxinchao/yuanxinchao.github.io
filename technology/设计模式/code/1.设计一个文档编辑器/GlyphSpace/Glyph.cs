using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.IteratorSpace;
using ConsoleApplication1.VisitorSpace;

namespace ConsoleApplication1.GlyphSpace
{
    public abstract class Glyph
    {
        public abstract void Draw(Window w);
        public abstract Rect Bounds();
        public abstract bool Intersects(Point w);
        public abstract void Insert(Glyph g,int index);
        public abstract void Remove(Glyph g);

        public abstract Glyph Child(int index);
        public abstract Glyph Parent();

        public abstract void SetParent(Glyph g);

        public virtual Iterator<Glyph> CreateIterator()
        {
            return new NullIterator<Glyph>();
        }

        public virtual void Accept(Visitor v)
        {
        }
        
    }
}
