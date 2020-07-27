using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.CommandSpace
{
    public class QuitCommand:Command
    {
        private Command save;
        public override void Excute()
        {
            save.Excute();
            Quit();
        }

        public override void Unexecute()
        {
        }

        public override bool Reversible()
        {
            return false;
        }

        private void Quit()
        {
        }
    }
}
