using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notation
{
    public abstract class Graphic
    {
        public abstract void Draw(Position p);
        public abstract Graphic Clone();
    }
}

public class Position
{

}
