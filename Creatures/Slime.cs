using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Slime : Creature
    {
        public Slime(int level)
        {
            resistance = 8+(level*2);
            if (resistance > 75) resistance = 75;
            maxHealth = 10 + (level * 2);
            health = maxHealth;
            Damage = 3 + (level * 2);

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

        public override bool TakeDamage(int amount)
        {
            health -= amount * ((100 - resistance) / 100);
            if (health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Attack(Creature target)
        {
            if (!target.TakeDamage(Damage))
            {
                Console.WriteLine($"Your vision fades, your journey ended by a {name}");
            }
        }
    }
}
