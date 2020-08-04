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

        public abstract void DeviceSetOrigin(Point at);
        public abstract void DeviceSetExtent(Point extent);
        public abstract void DeviceLower();
        public abstract void DeviceDrawLine(Point start, Point end);
        public abstract void DeviceDrawRect(Point min, Point max);
        public abstract void DeviceDrawPolygon(Point[] points);
        public abstract void DeviceDrawText(string s, Point point);
        public abstract void DeviceBitMap(string bitMapName, Point point1,Point point2);
    }
}
