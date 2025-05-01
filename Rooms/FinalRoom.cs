using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class FinalRoom : Room
    {
        /// <summary>
        /// Generates a instance of the Final Room
        /// </summary>
        /// <param name="EntryDir">The entry direction</param>
        /// <param name="AvailableDirections">the aavailable directions to move from</param>
        /// <param name="Visited">if the room has been visited</param>
        public FinalRoom(Directions EntryDir, List<Directions> AvailableDirections, bool Visited, int floor)
        {
            // gives different description based on if the room has been visited before
            // sets all needed variables for the room
            description = "An ancient staircase lies in front of you, leading further down.";
            Floor = floor;
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            Exits.Add(ExitDirection.down);
            dropTable = new DropTable(TableType.Room, floor+1);
            EmptyRoom = Visited;
            // potentially adds items to the room
            if (!EmptyRoom) GenerateItems(3);
        }
    }
}
