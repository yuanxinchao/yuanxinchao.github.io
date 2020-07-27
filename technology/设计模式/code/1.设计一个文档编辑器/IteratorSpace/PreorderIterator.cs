using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.IteratorSpace
{
    public class PreorderIterator<T>:Iterator<T> where T:class
    {
        private Glyph _root;

        Stack<Iterator<Glyph>> _iterators;
        public PreorderIterator(Glyph g)
        {
            _root = g;
        }
        public override void First()
        {
            Iterator<Glyph> i = _root.CreateIterator();

            if(i == null)
                return;

            i.First();
            _iterators.Clear();
            _iterators.Push(i);
        }

        public override void Next()
        {
            Iterator<Glyph> i = _iterators.Peek().CurrentItem().CreateIterator();
            i.First();
            _iterators.Push(i);
            while (_iterators.Count>0 &&_iterators.Peek().IsDone())
            {
                _iterators.Pop();
                _iterators.Peek().Next();
            }

        }

        public override bool IsDone()
        {
            return _iterators.Count == 0;
        }

        public override T CurrentItem()
        {
            if (_iterators.Count > 0)
            {
                return _iterators.Peek().CurrentItem() as T;
            }
            return default(T);
        }
    }
}
