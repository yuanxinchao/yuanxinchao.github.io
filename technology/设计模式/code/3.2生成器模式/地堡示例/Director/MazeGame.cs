using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using ConsoleApplication1.Builder;

namespace Director
{
    public class MazeGame
    {
        public Maze CreateMaze(MazeBuilder builder)
        {
            builder.BuildMaze();

            builder.BuildRoom(1);
            builder.BuildRoom(2);
            builder.BuilldDoor(1,2);

            return builder.GetMaze();
        }

        public Maze CreateComplexMaze(MazeBuilder builder)
        {
            for (int i = 0; i < 100; i++)
            {
                builder.BuildRoom(i + 1);
            }

            return builder.GetMaze();
        }
    }
}