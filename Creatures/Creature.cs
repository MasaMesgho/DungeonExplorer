using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Abstract Class to be inherited by player and enemies
    /// uses interfaces IDamageable and IAttack
    /// </summary>
    public abstract class Creature : IDamageable, IAttack
    {
        // variables shared among all creatures are here
        // health is protected for use only within the child classes for set, but can be seen by other classes using a public get
        protected int Health;
        public int health
        {
            protected set {  Health = value; }
            get { return Health; }
        }
        // damage is only used within the classes so is protected
        protected int Damage;

        // Max health is protected to be accessed only by the child classes
        // max health is public to be accessed outside the class for display purposes

        protected int MaxHealth;
        public int maxHealth
        {
            protected set { MaxHealth = value; }
            get { return MaxHealth; }
        }

        // resistance is protected as it is only used internally by the child classes
        protected int Resistance;
        // name is protected to be only set within the internal classes
        // public get as the information is needed outside of the class
        protected string Name;
        public string name
        {
            protected set {  Name = value; }
            get { return Name; }
        }
        // blank constructor just here for the abstract class
        public Creature() { }

        // here for compliance with the interfaces
        public abstract bool TakeDamage(int amount);

        public abstract bool Attack(Creature target);
    }
}
