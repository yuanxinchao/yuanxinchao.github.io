using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace MapElement
{
    //魔法门需要输密码
    public class EnchantedRoom:Room
    {
        public EnchantedRoom(int number,Spell s) : base(number)
        {

        }
    }

    public class Spell
    {

    }
}
