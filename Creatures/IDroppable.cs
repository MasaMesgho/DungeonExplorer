﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal interface IDroppable
    {
        /// <summary>
        /// Drops an item.
        /// </summary>
        /// <returns></returns>
        Item Drops();
    }
}
