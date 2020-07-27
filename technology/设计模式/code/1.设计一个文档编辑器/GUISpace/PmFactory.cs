using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public class PmFactory:GUIFactory
    {
        public override ScrollBar CreateScrollBar()
        {
            return new PMScrollBar();
        }

        public override Button CreateButton()
        {
            return new PMButton();
        }

        public override Menu CreateMenu()
        {
            return new PMMenu();
        }
    }
}
