using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class _D_CaptainHome:Dialog,IObserver
    {

        private CaptainInfo _info;
        public _D_CaptainHome(CaptainInfo info)
        {
            _info = info;
            info.Attach(this);
        }

        public override void Destory()
        {
            base.Destory();
            _info.Detach(this);
        }

        public void Update()
        {
            Console.WriteLine("\n更新" + this.GetType());
        }
    }
}