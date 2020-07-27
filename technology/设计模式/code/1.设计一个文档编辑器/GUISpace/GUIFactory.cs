using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public abstract class GUIFactory
    {
        public abstract ScrollBar CreateScrollBar();
        public abstract Button CreateButton();
        public abstract Menu CreateMenu();
    }
}
