using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Builder
{
    public class CountingMazeBuilder:MazeBuilder
    {
        private int rooms;
        private int doors;
        public CountingMazeBuilder()
        {
            rooms = 0;
            doors = 0;
        }

        public override void BuildRoom(int room)
        {
            rooms++;
        }

        public override void BuilldDoor(int roomFrom, int roomTo)
        {
            doors++;
        }

        public void GetCounts(out int roomNum, out int doorNum)
        {
            roomNum = rooms;
            doorNum = doors;
        }
    }
}
