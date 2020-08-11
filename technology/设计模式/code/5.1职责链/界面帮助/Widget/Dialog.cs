using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Dialog:Widget
    {
        //base传null说明作为窗口组件，该dialog没有父节点
        //SetHandle 但是有后继者
        public Dialog(HelpHandler h, Topic t = Topic.Undefine) : base(null,t)
        {
            SetHandle(h,t);
        }

        public override void HandleHelp()
        {
            if (HasHelp())
            {
               Console.WriteLine("显示对话框帮助");
            }
            else
            {
                base.HandleHelp();
            }
        }
        
        
    }
}
