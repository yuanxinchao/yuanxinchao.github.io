using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.IteratorSpace
{
    public class ListIterator<T>:Iterator<T> 
    {
        private List<T> _list;
        private int i;
        public ListIterator(List<T> list)
        {
            _list = list;
        }
        public override void First()
        {
            i = 0;
        }

        public override void Next()
        {
            i++;
        }

        public override bool IsDone()
        {
            return i >= _list.Count;
        }

        public override T CurrentItem()
        {
            return _list[i];
        }
    }
}
