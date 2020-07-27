using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class RobotDog:Robot
    {
        //充电
        public override void Battery()
        {
            Console.WriteLine("机器狗充电");
        }

        public override void Move()
        {
            Console.WriteLine("机器狗跳跃");
        }
    }
}
