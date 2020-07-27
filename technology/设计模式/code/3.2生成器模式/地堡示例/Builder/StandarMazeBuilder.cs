using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.Builder
{
    public class StandarMazeBuilder :MazeBuilder
    {
        Maze _currentMaze;
        public override void BuildMaze()
        {
            _currentMaze = new Maze();
        }

        public override void BuildRoom(int num)
        {
            if (_currentMaze.RoomNo(num,true) == null)
            {
                Room r = new Room(num);

                _currentMaze.AddRoom(r);

                r.SetSide(Direction.North,new Wall());
                r.SetSide(Direction.South,new Wall());
                r.SetSide(Direction.East,new Wall());
                r.SetSide(Direction.West,new Wall());
            }
        }

        public override void BuilldDoor(int roomFrom, int roomTo)
        {
            var r1 = _currentMaze.RoomNo(roomFrom);
            var r2 = _currentMaze.RoomNo(roomTo);
            Door d = new Door(r1,r2);

            r1.SetSide(CommonWall(r1,r2),d);
            r2.SetSide(CommonWall(r2,r1),d);
        }

        public override Maze GetMaze()
        {
            return _currentMaze;
        }

        public Direction CommonWall(Room r1, Room r2)
        {
            //TODO 计算commonwall
            return default(Direction);
        }
    }
}
