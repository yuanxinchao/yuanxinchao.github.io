using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication1
{
    public class EventCenter
    {
        private static EventCenter _instance;
        public static EventCenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventCenter();
                }

                return _instance;
            }
        }

        //***********每日次数通知**************//

        private Action OnTiliBuyChange;


        private Action OnBailTimesChange;

        public void AddListenerTiliBuyNumChange(Action tiliBuyNumChange)
        {
            OnTiliBuyChange += tiliBuyNumChange;
        }

        public void AddListenerBailTimesChange(Action bailTimesChange)
        {
            OnBailTimesChange += bailTimesChange;
        }
        public void RemoveTiliBuyNumChange(Action tiliBuyNumChange)
        {
            OnTiliBuyChange -= tiliBuyNumChange;
        }

        public void RemoveBailTimesChange(Action bailTimesChange)
        {
            OnBailTimesChange -= bailTimesChange;
        }
        public void NotifyTiliChange(object sender, ElapsedEventArgs e)
        {
            OnTiliBuyChange();
        }
        public void NotifyBailTimesChange(object sender, ElapsedEventArgs e)
        {
            OnBailTimesChange();
        }



        
        //***********舰长部分通知**************//

        private Action OnCaptGet;


        private Action<int,int> OnCaptTrainTimesChange;

        public void AddListenerCaptGet(Action newCaptainGet)
        {
            OnCaptGet += newCaptainGet;
        }

        public void AddListenerCaptTrain(Action<int,int> captTrainTimesChange)
        {
            OnCaptTrainTimesChange += captTrainTimesChange;
        }
        public void RemoveCaptGet(Action newCaptainGet)
        {
            OnCaptGet -= newCaptainGet;
        }

        public void RemoveCaptTrain(Action<int,int> captTrainTimesChange)
        {
            OnCaptTrainTimesChange -= captTrainTimesChange;
        }
        public void NotifyCaptGet(object sender, ElapsedEventArgs e)
        {
            OnCaptGet();
        }
        public void NotifyCaptTrainTimesChange(object sender, ElapsedEventArgs e)
        {
            OnCaptTrainTimesChange(1, 5);
        }
    }
}
