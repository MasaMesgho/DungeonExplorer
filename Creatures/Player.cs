using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using DungeonExplorer.Creatures;

namespace DungeonExplorer
{
    public class Player : Creature
    {

        public Item weapon;

        // inventory is a array of items with a max size of 10
        private Item[] inventory = new Item[10];

        public Player(string name) 
        {
            Name = name;
            Health = 70;
            maxHealth = 100;
            // assigns the name the user enters from the game loop, then assigns the starting health (100)
        }
        /// <summary>
        /// Adds a item to the players inventory.
        /// </summary>
        /// <param name="item">
        /// The Item to be added.
        /// </param>
        public void PickUpItem(Item item)
        {
            // adds an item to the players inventory if it has a space for it.
            if (inventory.Contains(null))
            {
                inventory.Append(item);
            }
        }
        /// <summary>
        /// Gets the players current Inventory as a string.
        /// </summary>
        /// <returns>
        /// The Inventory Contents
        /// </returns>
        public string InventoryContents()
        {
            // returns the inventory contents.
            // first checks if the inventory is empty
            string outputString = "";
            bool empty = true;
            foreach (Item item in inventory)
            {
                empty = false;
                outputString += item.name + "\n";

            }
            if (empty) outputString = "Your inventory is Empty...";
            return outputString;
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
        public bool checkInventory(int itemID)
        {
            // a method to see if an item is in the players inventory.
            // returns a bool

            // needs to be reworked after item rework
            return false;
        }

        /// <summary>
        /// Removes an Item from the Player Inventory
        /// </summary>
        /// <param name="itemName">
        /// The Item to be removed.
        /// </param>
        public void removeItem(int itemID)
        {
            // removes an item from a players inventory
            // checks that the item is in the players inventory before removing it.
            // should only be called by something that knows the item is there, so if it isn't raises an exception.
            bool errorCheck = false;
            foreach (Item item in inventory)
            {
                if (item.iD == itemID) errorCheck = true;
            }
            Debug.Assert(errorCheck, "Remove item called when " +
                "item not in inventory");
            // either reduces the int variable that shows the ammount of the item by 1 or removes the item if only 1 is left.
            if (errorCheck)
            {
                int i = 0;
                bool removed = false;
                foreach (Item item in inventory)
                {
                    if (!removed && item.iD == itemID) inventory[i] = null;
                    i++;
                }
            }
        }

        public void Equip(Item equipment)
        {
            if (checkInventory(equipment.iD))
            {
                switch (equipment.itemType)
                {
                    case ItemTypes.weapon:
                        weapon = equipment;
                        break;
                    default:
                        break;

                }
            }
            else 
            {
                
            }
        }

        /// <summary>
        /// adjusts the player health according to the input int
        /// </summary>
        /// <param name="change"></param>
        /// <returns>
        /// A bool that signifies if the player is alive after the change.
        /// </returns> 
        public bool changeHealth(int change)
        {
            // returns a bool for use in taking damage (not implemented) to check if it is fatal
            // by default the return is true as the player should be alive before this is called
            bool healthLeft = true;
            // changes the players health
            Health += change;
            if (Health <= 0)
            {
                // if the player has no health left, return false
                healthLeft = false;
            }
            else if (Health > maxHealth)
            {
                // if the user got more health than they can have.
                // set it to the max health value.
                Health = maxHealth;
            }
            return healthLeft;
        }
    }
}