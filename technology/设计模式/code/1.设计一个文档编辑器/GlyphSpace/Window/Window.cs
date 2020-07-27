using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class Window
    {
        private Glyph g;
        private WindowImp _imp;
        private WindowSystemFactory _wsf;
        public Window()
        {
            _imp = _wsf.CreateWindowImp();
        }
        public void DrawCharacter(char c)
        {
            
        }
        public void DrawImg(string path)
        {
            
        }

        public virtual void Redraw()
        {
            g.Draw(this);
        }
        public virtual void Raise()
        {
            _imp.DeviceRaise();
        }
        public virtual void Lower()
        {

        }
        public virtual void Iconify()
        {

        }
        public virtual void Deiconify()
        {

        }

        public virtual void DrawLine()
        {

        }

        public virtual void DrawRect(Rect r)
        {
            _imp.DeviceRect(r);
        }
        public virtual void DrawPolygon()
        {

        }
        public virtual void DrawText()
        {

        }
    }

    public class Point
    {

    }

    public class Rect
    {

    }
}
