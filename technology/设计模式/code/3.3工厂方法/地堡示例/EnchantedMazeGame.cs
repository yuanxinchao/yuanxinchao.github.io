using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;
using MapElement;

namespace ConsoleApplication1
{

    public class EnchantedMazeGame:MazeGame
    {
        Spell s = new Spell();
        public override Room MakeRoom(int n)
        {
            return new EnchantedRoom(n,s);
        }

        public override Door MakeDoor(Room r1, Room r2)
        {
            return new DoorNeedingSpell(r1,r2);
        }
    }


}
