using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GlyphSpace
{
    public abstract class WindowSystemFactory
    {
        public abstract WindowImp CreateWindowImp();
        public abstract ColorImp CreateColorImp();
    }

    public class PMWindowSystemFactory:WindowSystemFactory
    {
        public override WindowImp CreateWindowImp()
        {
            return new PMWindowImp();
        }

        public override ColorImp CreateColorImp()
        {
            return null;
        }
    }
    public class MacWindowSystemFactory:WindowSystemFactory
    {
        public override WindowImp CreateWindowImp()
        {
            return new MacWindowImp();
        }

        public override ColorImp CreateColorImp()
        {
            return null;
        }
    }
    public class XWindowSystemFactory:WindowSystemFactory
    {
        public override WindowImp CreateWindowImp()
        {
            return new XWindowImp();
        }

        public override ColorImp CreateColorImp()
        {
            return null;
        }
    }
}
