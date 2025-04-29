using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class EntryRoom : Room
    {
        public EntryRoom(Directions EntryDirection, List<Directions> AvailableDirections, bool Visited)
        {
            description = "a Dark Empty Room, it marks the beginning of the floor.";
        }
    }
}
