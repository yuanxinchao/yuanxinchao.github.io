﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class _D_CaptainHome:Dialog,IObserverCaptain,IObserverDaily
    {
        public _D_CaptainHome()
        {
            CaptainInfo.Instance.Attach(this);
            DailyInfo.Instance.Attach(this);
        }

        public override void Destory()
        {
            base.Destory();
            CaptainInfo.Instance.Detach(this);
            DailyInfo.Instance.Detach(this);
        }


        public void NewCaptainGet()
        {
            Console.WriteLine(GetType()+" 刷新{0}个舰长",CaptainInfo.Instance.GetCaptList().Count);
        }

        public void CaptTrainTimesChange(int cid, int trainTimes)
        {
            Console.WriteLine(GetType()+" id={0}的舰长剩余训练次数{1}",cid,trainTimes);
        }

        public void TiliBuyNumChange()
        {
            Console.WriteLine(GetType()+" 买了{0}次体力",DailyInfo.Instance.GetDailyInfo().day_captain_bail_time);
        }

        public void BailTimesChange()
        {
            Console.WriteLine(GetType()+" 保释了{0}次",DailyInfo.Instance.GetDailyInfo().day_captain_bail_time);
        }
    }
}