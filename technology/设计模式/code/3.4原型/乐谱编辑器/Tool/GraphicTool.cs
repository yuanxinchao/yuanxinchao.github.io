using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notation;

namespace Tool
{
    public class GraphicTool : Tool
    {
        //原型
        private Graphic _prototype;
        public override void Manipulate()
        {
            

            //模拟玩家拖拽一个音符用以复制
            Position p; 
            UserDragsMouse(out _prototype,out p);
            while (p !=null)
            {
                var node = _prototype.Clone();
                node.Draw(p);
            }
        }

        public void UserDragsMouse(out Graphic _prototype,out Position position)
        {
            //假数据
            _prototype = new HalfNode();
            position = new Position();
        }
    }
}
