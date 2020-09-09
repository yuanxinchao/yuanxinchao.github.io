using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //原发器
    public class ConstraintSolver
    {
        private static ConstraintSolver _instance;


        //**********状态***********//
        private List<KeyValuePair<Graphic, Graphic>> _connectList = new List<KeyValuePair<Graphic, Graphic>>();
        //*************************//

        public static ConstraintSolver Instance()
        {
            if (_instance == null)
            {
                _instance = new ConstraintSolver();
            }
            return _instance;
        }

        public void Solve()
        {

        }

        public void AddConstraint(Graphic startConnection,Graphic endConnection)
        {
            var kv = new KeyValuePair<Graphic, Graphic>(startConnection, endConnection);
            _connectList.Add(kv);
        }

        public void RemoveConstraint(Graphic startConnection,Graphic endConnection)
        {
            for (int i = _connectList.Count -1; i >=0; i--)
            {
                var kv = _connectList[i];
                if (kv.Key == startConnection && kv.Value == endConnection)
                    _connectList.RemoveAt(i);
            }
        }
        //请求一个备忘录
        //记录状态
        public ConstraintSolverMemento CreateMemento()
        {
            return new ConstraintSolverMemento(_connectList);
        }
        //管理者(Command)将命令执行前储存的备忘录送回原发器(ConstraintSolver)
        //恢复状态
        public void SetMemento(ConstraintSolverMemento m)
        {
            _connectList = m.GetState();
        }
    }
}
