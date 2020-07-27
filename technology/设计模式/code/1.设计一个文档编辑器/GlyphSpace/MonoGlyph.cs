using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class MonoGlyph : Glyph
    {
        private Glyph _component;


        public override void Draw(Window w)
        {
            _component.Draw(w);
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
    }
}
