using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace DungeonExplorer
{
    public class Player
    {
        // this class contains all the player information and actions.
        // below are the needed variables
        public string Name { get; private set; }
        public int Health { get; private set; }
        private int maxHealth;
        private Dictionary <string,int> inventory = new Dictionary<string,int>();

        public Player(string name) 
        {
            Name = name;
            Health = 70;
            maxHealth = 100;
            // assigns the name the user enters from the game loop, then assigns the starting health (100)
        }
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
        public string InventoryContents()
        {
            // returns the inventory contents.
            // first checks if the inventory is empty
            string outputString;
            if (this.inventory.Count != 0)
            {
                // if the inventory is not empty, loops through all items and their ammunts and passes it back to the game loop
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

        public bool checkInventory(string itemName)
        {
            if (this.inventory.ContainsKey(itemName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool changeHealth(int change)
        {
            bool healthLeft = true;
            Health += change;
            if (Health <= 0)
            {
                healthLeft = false;
                return healthLeft;
            }
            else if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            return healthLeft;
        }
    }
}