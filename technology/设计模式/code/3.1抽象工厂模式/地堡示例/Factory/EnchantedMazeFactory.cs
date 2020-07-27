using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;
using MapElement;

namespace ConsoleApplication1.Factory
{
    //施了魔法的
    public class EnchantedMazeFactory:MazeFactory
    {
        Spell s = new Spell();
        public override Maze MakeMaze()
        {
            return base.MakeMaze();
        }

        public override Wall MakeWall()
        {
            return base.MakeWall();
        }

        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number,s);
        }

        public override Door MakeDoor(Room r1, Room r2)
        {
            return new DoorNeedingSpell(r1,r2);
        }
    }
}
