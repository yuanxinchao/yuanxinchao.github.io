using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public class ApplicationWindow :Window
    {
        public ApplicationWindow(VisualComponent contents) : base(contents)
        {
            

        }

        public override void DrawContents()
        {
            GetView().Draw();
        }
    }
}
