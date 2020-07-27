using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.IteratorSpace
{
    public class ArrayIterator<T>:Iterator<T>
    {
        public override void First()
        {
        }

        public override void Next()
        {
        }

        public override bool IsDone()
        {
            return false;
        }

        public override T CurrentItem()
        {
            return default(T);
        }
    }
}
