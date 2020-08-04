using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class BorderDecorator : Decorator
    {
        private int borderWidth;

        public BorderDecorator(VisualComponent v, int borderWidth) :base(v)
        {
            this.borderWidth = borderWidth;
        }

        public override void Draw()
        {
            base.Draw();
            DrawBorder(borderWidth);
        }

        private void DrawBorder(int borderWidth)
        {

        }
    }
}
