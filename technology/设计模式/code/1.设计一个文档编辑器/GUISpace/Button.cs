using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.GUISapce
{
    public abstract class Button
    {
        public abstract void Press();
    }
    public class MotifButton:Button
    {
        public override void Press()
        {
        }
    }
    public class PMButton:Button
    {
        public override void Press()
        {
        }
    }
    public class MacButton:Button
    {
        public override void Press()
        {
        }
    }
}
