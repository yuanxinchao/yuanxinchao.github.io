using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MapElement
{
    public class Door:MapSite
    {
        private Room _room1;
        private Room _room2;
        private bool _isOpen;
        public Door(Room room1, Room room2)
        {
            _room1 = room1;
            _room2 = room2;
        }
        public override void Enter()
        {
        }

        public bool IsOpen()
        {
            return _isOpen;
        }
    }
}
