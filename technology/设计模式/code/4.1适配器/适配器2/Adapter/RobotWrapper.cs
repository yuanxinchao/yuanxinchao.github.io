using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Target;

namespace ConsoleApplication1
{
     public class RobotWrapper:IBeing
    {
        Robot r = new Robot();
        public void Eat()
        {
            r.Battery();
        }

        public void Move()
        {
            r.Move();
        }
    }
}
