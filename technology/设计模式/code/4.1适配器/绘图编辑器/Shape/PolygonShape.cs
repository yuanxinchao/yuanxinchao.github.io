using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class PolygonShape:Shape
    {
        public override void BoundingBox(Point bottomLeft, Point topRight)
        {
            base.BoundingBox(bottomLeft, topRight);
        }

        public override Manipulator CreateManipulator()
        {
            return base.CreateManipulator();
        }
    }
}
