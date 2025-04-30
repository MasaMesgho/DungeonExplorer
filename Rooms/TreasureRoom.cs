using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class TreasureRoom : Room
    {
        /// <summary>
        /// Generates a instance of the Treasure Room
        /// </summary>
        /// <param name="EntryDir">The entry direction</param>
        /// <param name="AvailableDirections">the aavailable directions to move from</param>
        /// <param name="Visited">if the room has been visited</param>
        public TreasureRoom(Directions EntryDir, List<Directions> AvailableDirections, bool Visited, int floor)
        {
            // gives different description based on if the room has been visited before
            // sets all needed variables for the room
            if (!Visited) description = "A Room full of Decaying chest and treasure, there may be something left.";
            else description = "An Empty Treasure hall, nothing is left.";
            Floor = floor;
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            dropTable = new DropTable(TableType.Room, floor+1);
            EmptyRoom = Visited;
            // potentially adds items to the room
            if (!EmptyRoom) GenerateItems(5);
        }
    }
}
