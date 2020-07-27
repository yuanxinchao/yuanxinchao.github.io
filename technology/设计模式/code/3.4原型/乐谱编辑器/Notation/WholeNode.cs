using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notation
{
    //全音
    public class WholeNode:MusicalNode
    {
        public override void Draw(Position p)
        {
        }

        public override Graphic Clone()
        {
            return new WholeNode();
        }
    }
}
