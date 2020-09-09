using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MacroCommand:Command
    {
        private List<Command> _cmds = new List<Command>();
        public virtual void Add(Command cmd)
        {
            _cmds.Add(cmd);
        }

        public virtual void Remove(Command cmd)
        {
            _cmds.Remove(cmd);
        }

        public override void Excute()
        {
            for (int i = 0; i < _cmds.Count; i++)
            {
                _cmds[i].Excute();
            }
        }
    }
}
