using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.MapElement;

namespace ConsoleApplication1
{
    public class Maze
    {
        Dictionary<int,Room> _dic = new Dictionary<int, Room>();
        public void AddRoom(Room room)
        {
            _dic[room.GetRoomNumber()] = room;
        }

        public Room RoomNo(int number,bool canBeNull = false)
        {
            Room r;
            _dic.TryGetValue(number, out r);


            if (r == null && !canBeNull)
                throw new Exception("RoomNo find no room="+number);

            return r;
        }

    }

}
