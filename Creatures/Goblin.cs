using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Goblin : Creature
    {
        public Goblin(int level) 
        {
            resistance = 0;
            maxHealth = 10 + (level * 5);
            Damage = 3 + (level * 2);
            health = maxHealth;

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

        public override bool TakeDamage(int amount)
        {
            health -= amount*((100-resistance) / 100);
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
