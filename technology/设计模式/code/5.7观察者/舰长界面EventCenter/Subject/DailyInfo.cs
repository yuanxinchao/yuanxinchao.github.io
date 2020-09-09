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
            t.Elapsed += EventCenter.Instance.NotifyBailTimesChange;//通知
            t.Enabled = true;


            
            Timer t2 = new Timer(8000);
            t2.AutoReset = false;
            t2.Elapsed += TiliBuyNumChange;//假装体力购买次数变化
            t2.Elapsed += EventCenter.Instance.NotifyTiliChange;//通知
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




    }

    public class P_Daily
    {
        public int tili_buy;  //每日购买体力次数
        public int day_captain_bail_time;//每日保释次数
        //.....
    }
}