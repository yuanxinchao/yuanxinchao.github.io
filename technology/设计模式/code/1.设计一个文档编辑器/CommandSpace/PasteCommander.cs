using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.CommandSpace
{
    public class PasteCommander:Command
    {
        private string buffer;
        public override void Excute()
        {

        }

        public override void Unexecute()
        {
        }

        public override bool Reversible()
        {
            return false;
        }
    }
}
