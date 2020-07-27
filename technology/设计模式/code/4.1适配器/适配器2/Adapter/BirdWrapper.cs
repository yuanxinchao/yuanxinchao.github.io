using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Target;

namespace ConsoleApplication1
{
     public class BirdWrapper:IBeing
     {
         private Bird b = new Bird();
        public void Eat()
        {
            b.Eat();
        }

        public void Move()
        {
            b.Fly();
        }
    }
}
