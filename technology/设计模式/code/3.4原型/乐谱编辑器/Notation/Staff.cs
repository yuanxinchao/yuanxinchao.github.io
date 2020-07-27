using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notation
{
    //五线谱
    public class Staff:Graphic
    {
        public override void Draw(Position p)
        {
        }

        public override Graphic Clone()
        {
            return new Staff();
        }
    }
}
