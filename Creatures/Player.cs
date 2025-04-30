using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace DungeonExplorer
{
    public class Player : Creature
    {

        public Item Weapon;

        // inventory is a array of items
        private List<Item> Inventory = new List<Item>();
        public int InventorySize
        {
            get; private set;
        }

        public Player(string playerName)
        {
            // assigns the name the user enters from the game loop, then assigns the starting health (100)
            name = playerName;
            maxHealth = 100;
            health = maxHealth;
            Resistance = 0;
            Damage = 5;
        }

        /// <summary>
        /// Gets the types of items in the players inventory.
        /// </summary>
        /// <returns>
        /// The types of items in the inventory
        /// </returns>
        public List<ItemTypes> InventoryContents()
        {
            // returns the inventory contents.
            // first checks if the inventory is empty
            List<ItemTypes> items= new List<ItemTypes>();
            bool hasItems = Inventory.Count() != 0;
            // if the inventory has items in it
            if (hasItems)
            {
                bool hasPotions = this.Inventory.Exists(item => item.itemType == ItemTypes.potion);
                bool hasEquip = Inventory.Exists(item => item.itemType == ItemTypes.weapon);

                if (hasPotions) items.Add(ItemTypes.potion);
                if (hasEquip) items.Add(ItemTypes.weapon);
            }
            else items.Add(ItemTypes.None);
            return items;
        }

        /// <summary>
        /// Gets all the items of a given type in the players inventory
        /// </summary>
        /// <param name="type">the type of item to be returned </param>
        /// <returns> a list of items of the requested type </returns>
        public List<Item> InventoryContents(ItemTypes type)
        {
            // creates a emty item list
            // then uses LINQ to fill it with the given type and then sort it.
            List<Item> items = new List<Item>();
            items = Inventory.Where(item => item.itemType == type).ToList();
            items.OrderByDescending(item => item.iD).FirstOrDefault();
            return items;
        }

        /// <summary>
        /// Checks the players inventory for the item specified
        /// </summary>
        /// <param name="itemName">
        /// The Item that is being checked for.
        /// </param>
        /// <returns>
        /// True if the item is there, false if not.
        /// </returns>
        public bool checkInventory(Item item)
        {
            // a method to see if an item is in the players inventory.
            // returns a bool

            // needs to be reworked after item rework
            if (Inventory.Contains(item)) return true;
            else return false;
        }

        /// <summary>
        /// Removes an Item from the Player Inventory
        /// </summary>
        /// <param name="itemName">
        /// The Item to be removed.
        /// </param>
        public void removeItem(Item item)
        {
            // removes an item from a players inventory
            // checks that the item is in the players inventory before removing it.
            // should only be called by something that knows the item is there, so if it isn't raises an exception.
            bool errorCheck = Inventory.Contains(item);

            Debug.Assert(errorCheck, "Remove item called when item not in inventory");

            // if the item is in the inventory, removes it.
            if (errorCheck) Inventory.Remove(item);
        }

        /// <summary>
        /// Adds a item to the players inventory.
        /// </summary>
        /// <param name="item">
        /// The Item to be added.
        /// </param>
        public bool PickUpItem(Item item)
        {
            // adds an item to the players inventory if it has a space for it.
            if (Inventory.Count <= 20)
            {
                Inventory.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// equips an item to the player
        /// </summary>
        /// <param name="equipment">the item being equipped </param>
        public void Equip(Item equipment)
        {
            // uses a switch case for types of equipment
            switch (equipment.itemType)
            {
                case ItemTypes.weapon:
                    Sword temp = equipment as Sword;
                    Weapon = temp;
                    Damage = temp.damage;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// increases the player health according to the input int
        /// </summary>
        /// <param name="change"></param>
        /// <returns>
        /// A bool that signifies if the player is alive after the change.
        /// </returns> 
        public void gainHealth(int change)
        {
            Health += change;

            // if the user has gained health over their max, sets it to the max health
            if (Health > maxHealth) Health = maxHealth;
        }

        /// <summary>
        /// reduces the players health by the given amount
        /// </summary>
        /// <param name="amount">the amount it is being reduced </param>
        /// <returns> if the player has health left </returns>
        public override bool TakeDamage(int amount)
        {
            double resisted = (Convert.ToDouble(Resistance) / 100) * Convert.ToDouble(amount);
            int calcAmount = (int)(amount - resisted);
            Console.WriteLine("You took {0} damage!", calcAmount);
            health -= calcAmount;
            // return if there is health left
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
        /// Attacks a given target
        /// </summary>
        /// <param name="target">the target being attacked </param>
        /// <returns> if the attack has killed the target </returns>
        public override bool Attack(Creature target)
        {
            // call the targets take damage, if it has health left return false, otherwise return true
            if (!target.TakeDamage(Damage))
            {
                return true;
            }
            return false;
        }

    }
}