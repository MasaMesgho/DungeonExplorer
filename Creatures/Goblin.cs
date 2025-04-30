using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Goblin Enemy class
    /// contains all the stats for a goblin
    /// inherits from creature which inherits the IAttack and IDamageable interfaces
    /// </summary>
    public class Goblin : Creature
    {
        private DropTable Droppable;

        /// <summary>
        /// Creates an instance of the goblin enemy
        /// generates stats based  on the level provided
        /// </summary>
        /// <param name="level"> The enemy Level to be generated </param>
        public Goblin(int level) 
        {
            // scales max health and damage based on level
            Resistance = 0;
            maxHealth = 10 + (level * 5);
            Damage = 3 + (level * 2);
            health = maxHealth;

            // creates an instance of the droptable class for the goblin
            Droppable = new DropTable(TableType.Enemy, level);

            // changes the Goblins name based on level to make it seem like there are different types
            if (level < 5)
            {
                name = "Goblin";
            }
            else if (level < 10)
            {
                name = "Hobgoblin";
            }
            else
            {
                name = "Goblin Lord";
            }
        }

        /// <summary>
        /// takes damage from an attack.
        /// </summary>
        /// <param name="amount"> the amount of incoming damage </param>
        /// <returns> a bool of if the enemy is dead </returns>
        public override bool TakeDamage(int amount)
        {
            double resisted = (Convert.ToDouble(Resistance) / 100) * Convert.ToDouble(amount);
            int calcAmount = (int)(amount - resisted);
            Console.WriteLine("You dealt {0} damage!", calcAmount);
            health -= calcAmount;
            if (health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attacks a Target
        /// </summary>
        /// <param name="target"> The target being attacked </param>
        public override bool Attack(Creature target)
        {
            // calls the targets take damage method, with a if statement for if the damage is fatal.
            if (!target.TakeDamage(Damage))
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// potentially drops an item from the goblins loot-table
        /// </summary>
        /// <returns>an item or null </returns>
        public override Item Drops()
        {
            return Droppable.GetDrop();
        }

    }
}
