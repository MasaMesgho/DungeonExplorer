using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Potion : Item
    {
        // adds in a restores value to the potion
        protected int Restores;

        /// <summary>
        /// creates an instance of the potion.
        /// </summary>
        public Potion(int level)
        {
            this.name = "Health Potion";
            // if the health potion is better than the basic (level 1) shows how advanced it is.
            if (level > 1) this.name += "+ " + (level - 1);
            this.iD = 1;
            this.Restores = 10 + level*5;
            this.description = $"A Level {level} Health Potion, it makes you feel better (restores {Restores} health)";
        }
        /// <summary>
        /// uses the potion, restores the stored amount of health to the player
        /// </summary>
        /// <param name="player"> the current player </param>
        public override void Use(Player player)
        {
            player.changeHealth(Restores);
        }

    }
}
