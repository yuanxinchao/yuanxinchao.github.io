using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MapElement
{
    public class BombedWall:Wall
    {
        public BombedWall()
        {

        }

        public BombedWall(BombedWall w)
        {

        }

        public override MapSite Clone()
        {
            return new BombedWall(this);
        }
    }
}
