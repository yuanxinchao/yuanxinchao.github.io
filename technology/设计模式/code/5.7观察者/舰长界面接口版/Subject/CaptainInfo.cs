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
        private List<P_Captain> _captains = new List<P_Captain>()
        {
            new P_Captain(),
            new P_Captain(),
        };

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
            Timer t = new Timer(2000);
            t.AutoReset = false;
            t.Elapsed += NewCaptGet;//假装新获取舰长
            t.Elapsed += NotifyCaptGet;//通知
            t.Enabled = true;


            
            Timer t2 = new Timer(4000);
            t2.AutoReset = false;
            t2.Elapsed += CaptTrainTimesChange;//假装某个舰长的训练次数变化
            t2.Elapsed += NotifyCaptTrainTimesChange;//通知
            t2.Enabled = true;
        }

        private void CaptTrainTimesChange(object sender, ElapsedEventArgs e)
        {
        }

        private void NewCaptGet(object sender, ElapsedEventArgs e)
        {
            _captains.Add(new P_Captain());
        }


        private List<IObserverCaptain> _observer = new List<IObserverCaptain>();

        public void Attach(IObserverCaptain observerCaptain)
        {
            _observer.Add(observerCaptain);
        }

        public void Detach(IObserverCaptain observerCaptain)
        {
            _observer.Remove(observerCaptain);
        }

        
        private void NotifyCaptGet(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < _observer.Count; i++)
            {
                _observer[i].NewCaptainGet();
            }
        }
        private void NotifyCaptTrainTimesChange(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < _observer.Count; i++)
            {
                _observer[i].CaptTrainTimesChange(1,5);
            }
        }


        public List<P_Captain> GetCaptList()
        {
            return _captains;
        }
    }

    public class P_Captain
    {
        public int id;//唯一id
        public int photo;//头像
        public int train_times;//训练次数
        //...
    }
}
