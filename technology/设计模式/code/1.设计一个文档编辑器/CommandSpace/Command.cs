using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.CommandSpace
{
    //封装请求

    public abstract class Command
    {
        public abstract void Excute();
        public abstract void Unexecute();
        public abstract bool Reversible();
    }
}
