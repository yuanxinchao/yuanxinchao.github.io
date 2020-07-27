using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Factory;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.Factory
{
     public class BombedMazeFactory:MazeFactory
    {
        public override Wall MakeWall()
        {
            return new BombedWall();
        }

        public override Room MakeRoom(int number)
        {
            return new RoomWithABomb(number);
        }
    }
}
