using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;
namespace ConsoleApplication1.IteratorSpace
{
    //不揭示其底层的数据结构
    //封装变化
    //暴露遍历一个数据结构的最基本要素，而不是整个结构
    public abstract class Iterator<T>
    {
        public abstract void First();
        public abstract void Next();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }
}
