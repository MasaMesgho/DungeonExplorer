using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace DungeonExplorer
{
    public class Player
    {
        // this class contains all the player information and actions.
        // below are the needed variables
        // uses a private set so it can only be changed within the class for encapsulation
        // uses a public get so the main game loop can display the variables
        public string Name { get; private set; }
        public int Health { get; private set; }
        private int maxHealth;
        // inventory is stored as a <string,int> dictionary, which shows the item(string) and the amount (int)
        private Dictionary <string,int> inventory = new Dictionary<string,int>();

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
        public void PickUpItem(string item)
        {
            // adds an item to the players inventory
            // if the item is already in the inventory adds 1 to the dictionary int value to show the ammount
            // if the item is not in the inventory creates a new dictionary item for it.
            if (this.inventory.ContainsKey(item))
            {
                this.inventory[item]++;
            }
            else
            {
                this.inventory.Add(item, 1);
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
            string outputString;
            if (this.inventory.Count != 0)
            {
                // if the inventory is not empty, loops through all items and their ammunts and passes it back to the game loop
                // joins it all to an output string which is then passed back.
                outputString = "";
                foreach (var i in this.inventory)
                {
                    string tempString = i.Value + " x " + i.Key+"\n";
                    outputString += tempString;
                }
            }
            else
            {
                // if the inventory is empty, returns that information.
                outputString = "Empty";
            }
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
        }

        /// <summary>
        /// Removes an Item from the Player Inventory
        /// </summary>
        /// <param name="itemName">
        /// The Item to be removed.
        /// </param>
        public void removeItem(string itemName)
        {
            // removes an item from a players inventory
            // checks that the item is in the players inventory before removing it.
            // should only be called by something that knows the item is there, so if it isn't raises an exception.
            bool errorCheck = inventory.ContainsKey(itemName);
            Debug.Assert(errorCheck, "Remove item called when " +
                "item not in inventory");
            // either reduces the int variable that shows the ammount of the item by 1 or removes the item if only 1 is left.
            if (errorCheck)
            {
                if (inventory[itemName] > 1)
                {
                    inventory[itemName]--;
                }
                else 
                {
                    inventory.Remove(itemName);
                }
            }
        }

        public void Equip(Item equipment)
        {
            if (checkInventory(equipment.iD))
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