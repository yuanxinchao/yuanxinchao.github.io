using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class DialogWindow:Window
    {
        private Window _w;
        public override void Lower()
        {
            base.Lower();
            _w.Lower();
        }

        public DialogWindow(VisualComponent contents) : base(contents)
        {
        }
    }
}
