using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Dungeon : Room, ILootable
    {
        public Dungeon(Directions EntryDirection, List<Directions> AvailableDirections, bool Visited)
        {

        }
        /// <summary>
        /// Loots the room
        /// </summary>
        /// <returns> all items in the room </returns>
        public Item[] Loot()
        {
            return RoomInventory;
        }
    }
}
