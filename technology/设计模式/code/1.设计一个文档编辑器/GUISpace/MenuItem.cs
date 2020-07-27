using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;
using ConsoleApplication1.CommandSpace;


namespace ConsoleApplication1.GUISapce
{
    public  class MenuItem: GlyphSpace.Glyph
    {
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

        private Command _command;
        public void Click()
        {
            _command.Excute();
        }
    }
}
