using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Composition
    {
        private int _componentCount;
        private int _lineWidth;
        private int _lineBreaks;
        private int _lineCount;

        private Compositor _compositor;
        private Component _components;
        public Composition(Compositor compositor)
        {
            _compositor = compositor;
        }

        public void Repair()
        {
            Coord natural = null;
            Coord stretchability= null;
            Coord shrinkability= null;
            int componentCount = 1;
            int breaks =1;

            //准备数据

            //计算换行位置
            int breakCount;
            breakCount = _compositor.Compose(natural,stretchability,shrinkability,componentCount,_lineWidth,breaks);

            //根据breakCount换行
        }

    }
}
