using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.Factory
{
    public class MazeFactory
    {
        public virtual  Maze MakeMaze()
        {
            return new Maze();
        }

        public virtual Wall MakeWall()
        {
            return new Wall();
        }

        public virtual Room MakeRoom(int number)
        {
            return new Room(number);
        }

        public virtual Door MakeDoor(Room r1, Room r2)
        {
           return new Door(r1, r2);
        }
    }
}
