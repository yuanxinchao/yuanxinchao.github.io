using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.Factory;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            MazeGame game  = new MazeGame();
            MazeFactory simpleMazeFactory = new MazePrototypeFactory(new Maze(), new Room(),new Wall(), new Door() );
            Maze m = game.CreateMaze(simpleMazeFactory);


            MazeFactory bombedMazeFactory = new MazePrototypeFactory(new Maze(), new RoomWithABomb(),new BombedWall(), new DoorNeedingSpell() );
            Maze m2 = game.CreateMaze(bombedMazeFactory);
        }
    }

}







