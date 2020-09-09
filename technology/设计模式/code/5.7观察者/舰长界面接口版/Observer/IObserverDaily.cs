using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
     public interface IObserverDaily
     {
         //体力购买次数变化
         void TiliBuyNumChange();
         //保释次数变化
         void BailTimesChange();
     }
}
