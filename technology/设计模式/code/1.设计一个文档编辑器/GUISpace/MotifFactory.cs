using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public class MotifFactory:GUIFactory
    {
        public override ScrollBar CreateScrollBar()
        {
            return new MotifScrollBar();
        }

        public override Button CreateButton()
        {
            return new MotifButton();
        }

        public override Menu CreateMenu()
        {
            return new MotifMenu();
        }
    }
}
