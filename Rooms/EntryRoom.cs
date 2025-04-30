using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class EntryRoom : Room
    {
        /// <summary>
        /// Generates a instance of the Entry Room
        /// </summary>
        /// <param name="EntryDir">The entry direction</param>
        /// <param name="AvailableDirections">the aavailable directions to move from</param>
        /// <param name="Visited">if the room has been visited</param>
        public EntryRoom(Directions EntryDir, List<Directions> AvailableDirections, bool Visited, int floor)
        {
            if (!Visited) description = "A new floor with new dangers lurking inside.";
            else description = "A familiar room, you started your exploration of this floor here";
                type = RoomType.Entry;
            EntryDirection = EntryDir;
            AddExits(AvailableDirections);
            EmptyRoom = Visited;
        }
    }
}
