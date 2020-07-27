using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1
{
    public class BombedMazeGame:MazeGame
    {
        public override Wall MakeWall()
        {
            return new BombedWall();
        }

        public override Room MakeRoom(int n)
        {
            return new RoomWithABomb(n);
        }
    }
}
