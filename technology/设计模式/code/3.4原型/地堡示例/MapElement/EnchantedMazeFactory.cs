using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Factory;
using MapElement;

namespace ConsoleApplication1.MapElement
{
    public class EnchantedMazeFactory: MazeFactory
    {
        protected  Spell s;
        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number,s);
        }

        public override Door MakeDoor(Room r1, Room r2)
        {
            return new DoorNeedingSpell(r1,r2);
        }
    }

    public class Spell
    {

    }
}
