using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public class MacFactory:GUIFactory
    {
        public override ScrollBar CreateScrollBar()
        {
            return new MacScrollBar();
        }

        public override Button CreateButton()
        {
            return new MacButton();
        }

        public override Menu CreateMenu()
        {
            return new MacMenu();
        }
    }
}
