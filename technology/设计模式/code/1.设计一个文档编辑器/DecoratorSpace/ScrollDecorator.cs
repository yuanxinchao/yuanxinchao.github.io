using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ScrollDecorator : Decorator
    {
        private float scrollPostiton;
        public ScrollDecorator(VisualComponent component) : base(component)
        {
        }
        public override void Draw()
        {
            base.Draw();
            ScrollTo(scrollPostiton);
        }

        public void ScrollTo(float pos)
        {

        }


    }
}
