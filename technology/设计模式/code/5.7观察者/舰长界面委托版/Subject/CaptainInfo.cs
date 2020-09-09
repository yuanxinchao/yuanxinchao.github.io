﻿using System;
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
        

        public List<P_Captain> GetCaptList()
        {
            return _captains;
        }

        //***********通知部分**************//
        public delegate void CaptGetDelegate();

        private CaptGetDelegate OnCaptGet;

        public delegate void CaptTrainDelegate(int cid,int times);

        private CaptTrainDelegate OnCaptTrainTimesChange;

        public void AddListenerCaptGet(CaptGetDelegate newCaptainGet)
        {
            OnCaptGet += newCaptainGet;
        }

        public void AddListenerCaptTrain(CaptTrainDelegate captTrainTimesChange)
        {
            OnCaptTrainTimesChange += captTrainTimesChange;
        }
        public void RemoveCaptGet(CaptGetDelegate newCaptainGet)
        {
            OnCaptGet -= newCaptainGet;
        }

        public void RemoveCaptTrain(CaptTrainDelegate captTrainTimesChange)
        {
            OnCaptTrainTimesChange -= captTrainTimesChange;
        }
        private void NotifyCaptGet(object sender, ElapsedEventArgs e)
        {
            OnCaptGet();
        }
        private void NotifyCaptTrainTimesChange(object sender, ElapsedEventArgs e)
        {
            OnCaptTrainTimesChange(1, 5);
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
