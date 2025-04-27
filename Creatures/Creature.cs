using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    abstract class Creature : IDamageable, IAttack
    {
        protected int Health;
        public int health
        {
            protected set {  Health = value; }
            get { return Health; }
        }

        protected int Damage;

        protected int MaxHealth;
        public int maxHealth
        {
            protected set {  MaxHealth = value; }
            get { return MaxHealth; }
        }

        protected int Resistance;
        public int resistance
        {
            protected set {  Resistance = value; }
            get { return Resistance; }
        }

        protected string Name;
        public string name
        {
            protected set {  Name = value; }
            get { return Name; }
        }

        public Creature() { }

        public abstract bool TakeDamage(int amount);

        public abstract void Attack(Creature target);
    }
}
