using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Potion : Item
    {
        // adds in a value for the amount the potion restores
        protected int Restores;

        /// <summary>
        /// Creates an instance of the potion item
        /// </summary>
        /// <param name="level"> the level of the health potion </param>
        public Potion(int level)
        {
            name = "Health Potion";
            // if the health potion is better than the basic (level 1) shows how advanced it is.
            if (level > 1) name += "+ " + (level - 1);
            iD = Int32.Parse($"1{level}");
            Restores = 10 + level*5;
            description = $"A Level {level} Health Potion, it makes you feel better (restores {Restores} health)";
            itemType = ItemTypes.potion;
        }
        /// <summary>
        /// uses the potion, restores the stored amount of health to the player
        /// </summary>
        /// <param name="player"> the current player </param>
        public override void Use(Player player)
        {
            player.gainHealth(Restores);
        }

    }
}
