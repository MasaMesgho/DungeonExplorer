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
            description = "A Dark Dungeon, old traces of prisoners, long gone remain.";
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            dropTable = new DropTable(TableType.Room, floor);
            EmptyRoom = Visited;
            if (!EmptyRoom) GenerateItems(3);
        }

    }
}
