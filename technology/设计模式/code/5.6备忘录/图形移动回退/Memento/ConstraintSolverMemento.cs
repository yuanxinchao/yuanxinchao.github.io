using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class ConstraintSolverMemento
    {
        private List<KeyValuePair<Graphic, Graphic>> _connectList = new List<KeyValuePair<Graphic, Graphic>>();
        public ConstraintSolverMemento(List<KeyValuePair<Graphic, Graphic>> connectList)
        {
            _connectList.Clear();
            for (int i = 0; i < connectList.Count; i++)
            {
                _connectList.Add(connectList[i]);
            }
        }

        //获取内部状态只能原发器(ConstraintSolver)使用  理想情况是只允许生成本备忘录的那个原发器使用
        public List<KeyValuePair<Graphic, Graphic>> GetState()
        {
            return _connectList;
        }
//        public void SetState(State state)
//        {
//            _state = state;
//        }
    }
}
