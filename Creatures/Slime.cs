using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Slime : Creature
    {
        private DropTable Droppable;
        /// <summary>
        /// Creates an instance of the slime class
        /// </summary>
        /// <param name="level"> the level of the slime </param>
        public Slime(int level)
        {
            // scales max health, resistance and damage based on level
            Resistance = 8+(level*2);
            if (Resistance > 75) Resistance = 75; // caps resistance at 75% so there is no instance of 100% resistance
            maxHealth = 10 + (level * 2);
            health = maxHealth;
            Damage = 3 + (level * 2);
            // creates a droptable instance for the slime
            Droppable = new DropTable(TableType.Enemy, level);

            // changes the Slimes name based on level to make it seem like there are different types
            if (level < 5)
            {
                name = "Slime";
            }
            else if (level < 10)
            {
                name = "Slime Abomination";
            }
            else 
            {
                name = "Enormous Slime";
            }
        }

        /// <summary>
        /// takes damage from an attack.
        /// </summary>
        /// <param name="amount"> the amount of incoming damage </param>
        /// <returns> a bool of if the enemy is dead </returns>
        public override bool TakeDamage(int amount)
        {
            // uses doubles to get the amount of damage resisted then subtracts that from the amount and removes the new amount from the creatures health.
            double resisted = (Convert.ToDouble(Resistance) / 100)*Convert.ToDouble(amount);
            int calcAmount = (int)(amount-resisted);
            Console.WriteLine("You dealt {0} damage!", calcAmount);
            health -= calcAmount;
            // checks to see if the creature is alive and returns that information
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
            // checks to see if the target is dead and returns it
            if (!target.TakeDamage(Damage))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// potentially drops an item from the goblins loot-table
        /// </summary>
        /// <returns>an item or null </returns>
        public override Item Drops()
        {
            // generates a drop and returns it
            return Droppable.GetDrop();
        }

    }
}
