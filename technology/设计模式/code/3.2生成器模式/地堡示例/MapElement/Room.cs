using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MapElement
{
    public class Room:MapSite
    {
        private MapSite[] _sides = new MapSite[4];
        private int _roomNumber;

        public Room(int number)
        {
            _roomNumber = number;
        }
        public override void Enter()
        {
        }

        public MapSite GetSide(Direction d)
        {
            return _sides[(int) d];
        }

        public void SetSide(Direction d,MapSite m)
        {
            _sides[(int) d] = m;
        }

        public int GetRoomNumber()
        {
            return _roomNumber;
        }
    }
}
