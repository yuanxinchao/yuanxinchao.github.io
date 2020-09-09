using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
     public interface IObserverCaptain
     {
         //新的舰长活获得
         void NewCaptainGet();

         //舰长训练次数变化
         void CaptTrainTimesChange(int cid,int trainTimes);
     }
}
