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
            description = "An ancient staircase lies in front of you, leading further down.";
            EntryDirection = EntryDir;
            AvailableDirections.Add(Directions.Down);
            AddExits(AvailableDirections);
            dropTable = new DropTable(TableType.Room, floor+1);
            EmptyRoom = Visited;
            if (!EmptyRoom) GenerateItems(3);
        }
    }
}
