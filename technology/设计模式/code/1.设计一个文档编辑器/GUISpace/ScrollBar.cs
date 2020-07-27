using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public abstract class ScrollBar
    {
        public abstract void ScrollTo(float pos);
    }
    public class MotifScrollBar:ScrollBar
    {
        public override void ScrollTo(float pos)
        {
        }
    }

    public class PMScrollBar : ScrollBar
    {
        public override void ScrollTo(float pos)
        {
        }
    }

    public class MacScrollBar : ScrollBar
    {
        public override void ScrollTo(float pos)
        {
        }
    }
}
