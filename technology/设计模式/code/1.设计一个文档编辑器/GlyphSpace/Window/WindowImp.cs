using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    //Bridge 模式
    //分离了类层次，一个支持窗口的逻辑概念，一个描述了窗口的不同实现
    //封装变化
    public abstract class WindowImp
    {
        public abstract void DeviceRaise();
        public abstract void DeviceRect(Rect rect);
    }
}
