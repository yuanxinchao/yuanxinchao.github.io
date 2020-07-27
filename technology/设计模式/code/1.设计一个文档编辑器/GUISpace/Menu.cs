using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public abstract class Menu
    {
        public abstract void Popup();
    }

    public class MotifMenu : Menu
    {
        public override void Popup()
        {
        }
    }
    public class PMMenu:Menu
    {
        public override void Popup()
        {
        }
    }
    public class MacMenu:Menu
    {
        public override void Popup()
        {
        }
    }
}
