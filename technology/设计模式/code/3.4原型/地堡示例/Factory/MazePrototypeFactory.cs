using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.Factory
{
    public class MazePrototypeFactory:MazeFactory
    {
        private Maze _prototypeMaze;
        private Room _prototypeRoom;
        private Wall _prototypeWall;
        private Door _prototypeDoor;

        public MazePrototypeFactory(Maze m,Room r,Wall w,Door d)
        {
            _prototypeMaze = m;
            _prototypeRoom = r;
            _prototypeWall = w;
            _prototypeDoor = d;
        }

        public override Wall MakeWall()
        {
            return (Wall)_prototypeWall.Clone();
        }

        public override Door MakeDoor(Room r1, Room r2)
        {
            var door = (Door)_prototypeDoor.Clone();
            door.Initialize(r1, r2);
            return door;
        }

        public override Room MakeRoom(int number)
        {
            var room = (Room)_prototypeRoom.Clone();
            room.Initialize(number);
            return room;
        }
    }
}
