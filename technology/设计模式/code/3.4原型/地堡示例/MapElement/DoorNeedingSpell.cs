using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1.MapElement
{
    public class DoorNeedingSpell:Door
    {
        public DoorNeedingSpell()
        {

        }
        public DoorNeedingSpell(Room room1, Room room2) : base(room1, room2)
        {
        }

        public override MapSite Clone()
        {
            return new DoorNeedingSpell();
        }

        public override void Initialize(Room r1, Room r2)
        {
            base.Initialize(r1, r2);
        }
    }
}
