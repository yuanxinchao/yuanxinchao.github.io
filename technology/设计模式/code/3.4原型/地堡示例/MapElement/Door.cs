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

        public Door()
        {

        }
        public Door(Door other)
        {
            _room1 = other._room1;
            _room2 = other._room2;
        }
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

        public override MapSite Clone()
        {
//            var d = new Door();
//            d._room1 = _room1.Clone();
//            d._room2 = _room2.Clone();

            var d = new Door(this);
            return d;
        }

        public virtual void Initialize(Room r1, Room r2)
        {
            _room1 = r1;
            _room2 = r2;
        }
    }
}
