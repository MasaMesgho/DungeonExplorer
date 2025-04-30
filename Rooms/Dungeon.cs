using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Dungeon : Room
    {
        /// <summary>
        /// Generates a instance of the Dungeon Room
        /// </summary>
        /// <param name="EntryDir">The entry direction</param>
        /// <param name="AvailableDirections">the aavailable directions to move from</param>
        /// <param name="Visited">if the room has been visited</param>
        public Dungeon(Directions EntryDir, List<Directions> AvailableDirections, bool Visited, int floor)
        {
            if (!Visited) description = "A dark dungeon, faint traces of previous prisoners remain, maybe there is something of value left behind.";
            else description = "An Empty dungeon, the only thing left is the eerie silence.";
            Floor = floor;
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            dropTable = new DropTable(TableType.Room, floor);
            EmptyRoom = Visited;
            if (!EmptyRoom) GenerateItems(2);
        }
    }
}
