using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Items
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Items(string name) 
        {
            Name = name;
        }
    }
}
