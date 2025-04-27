using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Rooms
{
    internal class GameMap
    {
        private int floor;
        public GameMap(int currentFloor) 
        {
            int Rooms = 3+currentFloor;
        }
    }
}
