using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Button:Widget
    {

        //base 调用到HelpHandle里时会将h作为后继者
        public Button(Widget h, Topic t = Topic.Undefine) : base(h, t)
        {

        }


        public override void HandleHelp()
        {
            if (HasHelp())
            {
                Console.WriteLine("显示按钮帮助");
            }
            else
            {
                base.HandleHelp();
            }
        }
    }
}
