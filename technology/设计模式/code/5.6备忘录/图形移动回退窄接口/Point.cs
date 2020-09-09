using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator -(Point p)
        {
            p.x = -p.x;
            p.y = -p.y;
            return p;
        }
    }
}
