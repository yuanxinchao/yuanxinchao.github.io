using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Factory;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1
{
    public class MazeGame
    {
        public Maze CreateMaze(MazeFactory factory)
        {
            Maze maze = factory.MakeMaze();
            Room r1 = factory.MakeRoom(1);
            Room r2 = factory.MakeRoom(2);
            Door aDoor = factory.MakeDoor(r1,r2);

            maze.AddRoom(r1);
            maze.AddRoom(r2);

            r1.SetSide(Direction.North,factory.MakeWall());
            r1.SetSide(Direction.East,aDoor);
            r1.SetSide(Direction.South,factory.MakeWall());
            r1.SetSide(Direction.West,factory.MakeWall());
            r2.SetSide(Direction.North,factory.MakeWall());
            r2.SetSide(Direction.East,factory.MakeWall());
            r2.SetSide(Direction.South,factory.MakeWall());
            r2.SetSide(Direction.West,aDoor);


            return maze;
        }
    }
}
