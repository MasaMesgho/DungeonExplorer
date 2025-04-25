using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Sword : Item
    {
        // stores the amount of damage the sword does
        protected int Damage;
        public int damage
        {
            get { return Damage; }
            protected set { Damage = value; }
        }

        /// <summary>
        /// creates an instance of the sword item
        /// </summary>
        /// <param name="level"> the level of the sword</param>
        public Sword(int level)
        {
            name = "Sword";
            if (level > 1) name += " +" + (level - 1);
            damage = 5 + (level * 5);
            description = $"A Level {level} sword, it makes enemies unhappy (does {damage} damage)";
            iD = 2;
        }

        /// <summary>
        /// equips the sword to the player
        /// </summary>
        /// <param name="player"> the player to equip the sword to </param>
        public override void Use(Player player)
        {
            player.Equip(this);
        }
    }
}
