using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Application:HelpHandler
    {
        public override void HandleHelp()
        {
            //展示帮助列表
            Console.WriteLine("显示帮助列表");
        }

        //base 传null 无后继者
        public Application(Topic t = Topic.Applition) : base(null,t)//最顶层没有后继者
        {

        }
    }
}
