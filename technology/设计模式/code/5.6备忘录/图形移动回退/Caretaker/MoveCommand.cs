using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //管理器
    public class MoveCommand
    {
        //命令里保存了状态
        //不能对state进行审查，仅是保存
        private ConstraintSolverMemento _state;

        private Point _delta;
        private Graphic _target;
        public MoveCommand(Graphic target, Point delta)
        {
            _target = target;
            _delta = delta;
        }

        public void Excute()
        {
            //保存状态
            ConstraintSolver solver = ConstraintSolver.Instance();
            _state = solver.CreateMemento();
            _target.Move(_delta);
            solver.Solve();
        }

        public void Unexcute()
        {
            ConstraintSolver solver = ConstraintSolver.Instance();
            _target.Move(-_delta);
            solver.SetMemento(_state);
            solver.Solve();
        }


    }
}
