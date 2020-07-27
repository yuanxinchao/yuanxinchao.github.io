using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            BombedMazeGame b = new BombedMazeGame();
            Maze m = b.CreateMaze();
        }
    }
}







