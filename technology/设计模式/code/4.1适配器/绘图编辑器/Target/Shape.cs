using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
     public class Shape
    {
        public virtual void BoundingBox(Point bottomLeft,Point topRight)
        {

        }

        public virtual Manipulator CreateManipulator()
        {
            return null;
        }
    }

     public class Point
     {

     }
}
