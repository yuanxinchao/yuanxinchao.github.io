using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SimpleCompositor:Compositor
    {
        public override void SetComposition(Composition composition)
        {
        }
        public override int Compose(Coord natural, Coord stretch, Coord shrink, int componentCount, int lineWidth, int breaks)
        {
            return 0;
        }
    }
}
