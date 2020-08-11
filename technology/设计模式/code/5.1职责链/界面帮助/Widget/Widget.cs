using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Widget:HelpHandler
    {
        //窗口组件的父节点
        private Widget _parent;
        protected Widget(Widget h, Topic t = Topic.Undefine) : base(h, t)
        {
            _parent = h;
        }

    }
}
