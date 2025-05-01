using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// holds statistics of the umber of rooms visited and how many enemies are slain
    /// </summary>
    public class Statistics
    {
        public int enemiesSlain;
        public int roomsVisited;
        public Statistics() 
        {
            enemiesSlain = 0;
            roomsVisited = 0;
        }
        /// <summary>
        /// returns the current stats as a string
        /// </summary>
        /// <returns>the current stats </returns>
        public string GetStats()
        {
            string stats = "";
            stats+= $"Enemies Slain: {enemiesSlain}\n";
            stats += $"Rooms Visited: {roomsVisited}\n";
            return stats;
        }

    }
}
