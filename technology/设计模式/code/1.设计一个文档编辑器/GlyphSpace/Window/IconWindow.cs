using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class IconWindow:Window
    {
        private string _bitmapName;
        public IconWindow(VisualComponent contents) : base(contents)
        {
        }

        public override void DrawContents()
        {
            WindowImp imp = GetWindowImp();
            if (imp != null)
            {
                imp.DeviceBitMap(_bitmapName,new Point(), new Point());
            }
        }
    }
}
