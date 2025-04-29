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
        public Slime(int level)
        {
            Resistance = 8+(level*2);
            if (Resistance > 75) Resistance = 75;
            maxHealth = 10 + (level * 2);
            health = maxHealth;
            Damage = 3 + (level * 2);
            Droppable = new DropTable(TableType.Enemy, level);

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
            health -= amount * ((100 - Resistance) / 100);
            if (health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Attack(Creature target)
        {
            if (!target.TakeDamage(Damage))
            {
                return true;
            }
            return false;
        }

        public Item drops()
        {
            return Droppable.GetDrop();
        }

    }
}
