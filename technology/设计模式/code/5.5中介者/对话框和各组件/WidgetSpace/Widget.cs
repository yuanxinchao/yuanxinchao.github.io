using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class Widget
    {

        //Widget是窗口组件的抽象基类。一个窗口组件知道它的导控者(中介者)
        protected DialogDirector _director;

        public virtual void Change()
        {
            _director.WidgetChanged(this);
        }

        public virtual void HandleMouse(MouseEvent mouseEvent)
        {

        }

    }
}
