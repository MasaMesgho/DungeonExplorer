using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal interface IAttack
    {
        /// <summary>
        /// Adds Attack to Creatures
        /// </summary>
        /// <param name="target"> the target being attacked</param>
        /// <returns>A bool of if the attack has killed the target </returns>
        bool Attack(Creature target);
    }
}
