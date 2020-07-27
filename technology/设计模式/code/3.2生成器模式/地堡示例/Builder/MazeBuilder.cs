using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.Builder
{
    public class MazeBuilder
    {
        public virtual void BuildMaze()
        {

        }
        public virtual void BuildRoom(int room)
        {

        }
        public virtual void BuilldDoor(int roomFrom, int roomTo)
        {

        }
        public virtual Maze GetMaze()
        {
            throw new Exception("should no use directly");
        }
    }

}
