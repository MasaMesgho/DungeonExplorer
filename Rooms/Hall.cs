using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Hall : Room
    {
        /// <summary>
        /// Generates a instance of the Hall Room
        /// </summary>
        /// <param name="EntryDir">The entry direction</param>
        /// <param name="AvailableDirections">the aavailable directions to move from</param>
        /// <param name="Visited">if the room has been visited</param>
        public Hall(Directions EntryDir, List<Directions> AvailableDirections, bool Visited, int floor)
        {
            // gives different description based on if the room has been visited before
            // sets all needed variables for the room
            if (!Visited) description = "An old Hall, decaying furniture fills the room, there may be something of use.";
            else description = "An Empty old Hall, nothing of value remains.";
            Floor = floor;
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            dropTable = new DropTable(TableType.Room, floor);
            EmptyRoom = Visited;
            // potentially adds items to the room
            if (!EmptyRoom) GenerateItems(3);
        }

    }
}
