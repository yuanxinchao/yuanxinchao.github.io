using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MapElement
{
//迷宫组件
    public abstract class MapSite
    {
        public abstract void Enter();
    }
}
public enum Direction
{
    North,
    South,
    East,
    West,
}
