using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SimpleCommand:Command
    {
        public delegate void Act();

        private Act _ac;
        public SimpleCommand(Act ac)
        {
            _ac = ac;
        }

        public override void Excute()
        {
            _ac();
        }
    }
}
