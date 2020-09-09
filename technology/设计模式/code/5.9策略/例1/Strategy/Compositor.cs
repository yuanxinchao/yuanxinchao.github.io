using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class Compositor
    {
        protected Compositor()
        {

        }
        public abstract void SetComposition(Composition _composition);
        public abstract int Compose(Coord natural, Coord stretch, Coord shrink, int componentCount, int lineWidth,
            int breaks);

    }

    public class Coord
    {
    }
}
