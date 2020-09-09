using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class ConnectCommand
    {
        //命令里保存了状态
        ////不能对state进行审查，仅是保存
        private IMemento _state;
        private Graphic _a;
        private Graphic _b;
        public ConnectCommand(Graphic a, Graphic b)
        {
            _a = a;
            _b = b;
        }

        public void Excute()
        {
            //保存状态
            ConstraintSolver solver = ConstraintSolver.Instance();
            _state = solver.CreateMemento();
            solver.AddConstraint(_a,_b);
            solver.Solve();
        }

        public void Unexcute()
        {
            ConstraintSolver solver = ConstraintSolver.Instance();
            solver.RemoveConstraint(_a,_b);
            solver.SetMemento(_state);
            solver.Solve();
        }


    }
}
