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
        private WindowSystemFactory _wsf = new PMWindowSystemFactory();
        private VisualComponent _contents;
        public Window(VisualComponent contents)
        {
            _contents = contents;
            _imp = _wsf.CreateWindowImp();
        }

        public Window()
        {

        }
        public void SetContents(VisualComponent contents)
        {
            _contents = contents;
        }
        public WindowImp GetWindowImp()
        {
            if (_imp == null)
            {
                _imp = _wsf.CreateWindowImp();
            }

            return _imp;
        }
        public VisualComponent GetView()
        {
            return _contents;
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
        public virtual void Iconify()
        {

        }
        public virtual void Deiconify()
        {

        }


        public virtual void DrawContents()
        {

        }

        public virtual void Open()
        {

        }

        public virtual void Close()
        {

        }


        //转发请求到 implementation
        public virtual void SetOrigin(Point at)
        {
            _imp.DeviceSetOrigin(at);
        }
        public virtual void SetExtent(Point extent)
        {
            _imp.DeviceSetExtent(extent);
        }
        
        public virtual void Raise()
        {
            _imp.DeviceRaise();
        }
        
        public virtual void Lower()
        {
            _imp.DeviceLower();
        }
        
        public virtual void DrawLine(Point start,Point end)
        {
            _imp.DeviceDrawLine(start,end);
        }
        
        public virtual void DrawRect(Point min,Point max)
        {
            _imp.DeviceDrawRect(min,max);
        }
        
        public virtual void DrawPolygon(Point[] points)
        {
            _imp.DeviceDrawPolygon(points);
        }
        public virtual void DrawText(string s,Point p)
        {
            _imp.DeviceDrawText(s,p);
        }
    }

    public class Point
    {

    }

    public class Rect
    {
        public Point point1;
        public Point point2;
    }
}
