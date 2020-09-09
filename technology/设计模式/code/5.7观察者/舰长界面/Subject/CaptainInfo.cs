using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication1
{
    public class CaptainInfo
    {
        private List<P_Captain> _captains = new List<P_Captain>();

        private static CaptainInfo _instance;
        public static CaptainInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CaptainInfo();
                }

                return _instance;
            }
        }

        private CaptainInfo()
        {
            Timer t = new Timer(5000);
            t.Elapsed += Notify;
            t.Enabled = true;
        }


        private List<IObserver> _observerList = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observerList.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observerList.Remove(observer);
        }

        private void Notify(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < _observerList.Count; i++)
            {
                _observerList[i].Update();
            }
        }
    }

    public class P_Captain
    {
        public int id;
    }
}
