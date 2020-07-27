using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.Compose
{
    public abstract class Compositor
    {
        public abstract void Compose();
        public abstract void SetComposition(Composition _composition);
    }
}
