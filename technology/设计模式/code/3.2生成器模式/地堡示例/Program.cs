using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.Builder;
using ConsoleApplication1.MapElement;
using Director;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            MazeGame game = new MazeGame();
            StandarMazeBuilder builder = new StandarMazeBuilder();
            game.CreateMaze(builder);
            Maze m = builder.GetMaze();


            CountingMazeBuilder countingMazeBuilder = new CountingMazeBuilder();
            game.CreateMaze(countingMazeBuilder);
            int roomNum;
            int doorNum;
            countingMazeBuilder.GetCounts(out roomNum,out doorNum);

            Console.WriteLine(roomNum);
            Console.WriteLine(doorNum);
        }
    }
}







