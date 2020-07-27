using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Compose;
using ConsoleApplication1.IteratorSpace;

namespace ConsoleApplication1.GlyphSpace
{
    public class Composition : Glyph
    {
        private List<Glyph> _children;
        private Compositor _compositor;

        public void SetComposition(Compositor compositor)
        {
            _compositor = compositor;
        }

        public override void Draw(Window w)
        {
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
            //To do insert


            _compositor.SetComposition(this);
            _compositor.Compose();
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

        public override Iterator<Glyph> CreateIterator()
        {
            return new PreorderIterator<Glyph>(this);
        }
    }
}
