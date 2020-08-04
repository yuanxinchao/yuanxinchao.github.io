using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    //文本 图片 都可以继承VisualComponent (如将文本，图片，利用组合(Composite)模式组合起来)
    public class VisualComponent
    {
        public virtual void Draw()
        {
        }

        public virtual void Resize()
        {

        }
    }
}
