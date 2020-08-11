using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class ImageProxy:Glyph
    {
        private string _path;
        private Image _image;
        private Rect _rect;
        public ImageProxy(string path)
        {
            _path = path;
            _rect =new Rect();
        }
        public override void Draw(Window w)
        {
            GetImage().Draw(w);
        }

        public override Rect Bounds()
        {
            return _rect;
        }

        public override bool Intersects(Point w)
        {
            return GetImage().Intersects(w);
        }

        public override void Insert(Glyph g, int index)
        {
            GetImage().Insert(g,index);
        }

        public override void Remove(Glyph g)
        {
            GetImage().Remove(g);
        }

        public override Glyph Child(int index)
        {
            return GetImage().Child(index);
        }

        public override Glyph Parent()
        {
            return GetImage().Parent();
        }

        public override void SetParent(Glyph g)
        {
            GetImage().SetParent(g);
        }

        private Image GetImage()
        {
            if (_image == null)
            {
                _image = new Image(_path);
            }

            return _image;
        }
    }
}
