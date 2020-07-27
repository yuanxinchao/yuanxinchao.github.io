using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Adaptee;
using ConsoleApplication1.Target;

namespace ConsoleApplication1.Adapter
{
     public class CarWrapper:IBeing
    {
        Car c = new Car();
        public void Eat()
        {
            c.Refuel();
        }

        public void Move()
        {
            c.Run();
        }
    }
}
