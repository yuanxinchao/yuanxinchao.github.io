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
            MazeGame m  = new MazeGame();
            MazeFactory f = new BombedMazeFactory();
            m.CreateMaze(f);

        }
    }

}







