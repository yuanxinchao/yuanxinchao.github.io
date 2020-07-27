using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notation
{
    //半音
    public class HalfNode:MusicalNode
    {
        private int musicalScale;//音阶音调
        private int tone;//音调
        public override void Draw(Position p)
        {
        }

        public override Graphic Clone()
        {
            return new HalfNode();
        }
    }
}
