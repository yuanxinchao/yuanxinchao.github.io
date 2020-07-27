using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.IteratorSpace;

namespace ConsoleApplication1.GlyphSpace
{
    public class Row : Glyph
    {
        private List<Glyph> _children;
        public override void Draw(Window w)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Draw(w);
            }
        }

        public override Rect Bounds()
        {
            return null;
        }

        public override bool Intersects(Point w)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i].Intersects(w))
                    return true;
            }

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
            int i = Math.Min(index, _children.Count - 1);
            return _children[i];
        }

        public override Glyph Parent()
        {
            return null;
        }

        public override void SetParent(Glyph g)
        {
        }

        public override Iterator<Glyph> CreateIterator()
        {
            return new ListIterator<Glyph>(_children);
        }
    }
}
