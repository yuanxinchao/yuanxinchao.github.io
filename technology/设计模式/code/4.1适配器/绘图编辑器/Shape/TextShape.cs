using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class TextShape:Shape
    {
        private TextView _textView;

        public TextShape()
        {
            _textView = new TextView();
        }

        public void SetTextShape(TextView t)
        {
            _textView = t;
        }
        public override void BoundingBox(Point bottomLeft,Point topRight)
        {
            Coord bottom = null;
            Coord left = null;
            Coord width = null;
            Coord height = null;
            _textView.GetOrigin(bottom,left);
            _textView.GetExtent(width,height);

        }

        public bool IsEmpty()
        {
           return _textView.IsEmpty();
        }

        public override Manipulator CreateManipulator()
        {
            return new Manipulator();
        }
    }
}
