using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class Widget
    {
        public delegate void WidgetChange(Widget w);
        public WidgetChange widgetChange;
        public virtual void Change()
        {
            widgetChange(this);
        }
        public virtual void HandleMouse(MouseEvent mouseEvent)
        {

        }
    }
}
