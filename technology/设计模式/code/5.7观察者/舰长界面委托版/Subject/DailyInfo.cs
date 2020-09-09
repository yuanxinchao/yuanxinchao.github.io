using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication1
{
    public class DailyInfo
    {
        private P_Daily _daily = new P_Daily()
        {
            day_captain_bail_time = 5,
        };
        private static DailyInfo _instance;
        public static DailyInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DailyInfo();
                }

                return _instance;
            }
        }

        private DailyInfo()
        {
            
            Timer t = new Timer(6000);
            t.AutoReset = false;
            t.Elapsed += BailTimesChange;//假装保释次数变化
            t.Elapsed += NotifyBailTimesChange;//通知
            t.Enabled = true;


            
            Timer t2 = new Timer(8000);
            t2.AutoReset = false;
            t2.Elapsed += TiliBuyNumChange;//假装体力购买次数变化
            t2.Elapsed += NotifyTiliChange;//通知
            t2.Enabled = true;
        }

        private void TiliBuyNumChange(object sender, ElapsedEventArgs e)
        {
            _daily.tili_buy = 3;
        }

        private void BailTimesChange(object sender, ElapsedEventArgs e)
        {
            _daily.day_captain_bail_time = 4;
        }


        public P_Daily GetDailyInfo()
        {
            return _daily;
        }



        //***********通知部分**************//
        public delegate void TiliBuyNumDelegate();

        private TiliBuyNumDelegate OnTiliBuyChange;

        public delegate void BailTimesDelegate();

        private BailTimesDelegate OnBailTimesChange;

        public void AddListenerTiliBuyNumChange(TiliBuyNumDelegate tiliBuyNumChange)
        {
            OnTiliBuyChange += tiliBuyNumChange;
        }

        public void AddListenerBailTimesChange(BailTimesDelegate bailTimesChange)
        {
            OnBailTimesChange += bailTimesChange;
        }
        public void RemoveTiliBuyNumChange(TiliBuyNumDelegate tiliBuyNumChange)
        {
            OnTiliBuyChange -= tiliBuyNumChange;
        }

        public void RemoveBailTimesChange(BailTimesDelegate bailTimesChange)
        {
            OnBailTimesChange -= bailTimesChange;
        }
        private void NotifyTiliChange(object sender, ElapsedEventArgs e)
        {
            OnTiliBuyChange();
        }
        private void NotifyBailTimesChange(object sender, ElapsedEventArgs e)
        {
            OnBailTimesChange();
        }


    }

    public class P_Daily
    {
        public int tili_buy;  //每日购买体力次数
        public int day_captain_bail_time;//每日保释次数
        //.....
    }
}