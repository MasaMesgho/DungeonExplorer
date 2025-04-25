using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DungeonExplorer
{

    internal class Item
    {
        /// <summary>
        /// Potentially generates a random item based on a provided chance.
        /// </summary>
        /// <param name="chance">
        /// The chance of generating a item.
        /// </param>
        /// <returns>
        /// An Item or "none" if the chance fails
        /// </returns>
        public static string GenerateItem(int chance)
        {
            // method that potentially generates an item
            // gets a chance of an item from the request
            // then returns a type of item determined by chance
            string output = "none";
            Random rnd = new Random();
            // first makes sure there is no error in the chance variable using Debug.Assert and a bool variable.
            bool errorCheck = chance > 0 && chance <= 100;
            Debug.Assert(errorCheck, "Invalid chance variable " +
                "passed to the item Generator");
            // uses the random class to see if the user gets an item.
            int temp = rnd.Next(1, 101);
            if (errorCheck && chance >= temp)
            {
                // uses a 1,5 next to assign 25% chances for the item type.
                int itemChance = rnd.Next(1, 5);
                switch (itemChance)
                {
                    case 1:
                    case 2:
                    case 3:
                        output = "health potion";
                        break;
                    case 4:
                        output = "treasure";
                        break;
                    default:
                        // if the itemChance is out of range raise an exception.
                        Debug.Assert(itemChance > 0 && itemChance < 5,
                            "Error in item chance assignment");
                        break;
                }
            }
            return output;
        }
        /// <summary>
        /// Uses an Item on the specified player
        /// </summary>
        /// <param name="itemName">
        /// The Item to be used.
        /// </param>
        /// <param name="currentPlayer">
        /// The player the item is being used on.
        /// </param>
        public static void useItem(string itemName, Player currentPlayer)
        {
            // a public method for using an item
            // first gets the item name and affected player, needs to be passed here for it to use player methods.
            switch (itemName)
            {
                case "health potion":
                    // for a health potion, uses the player method to make sure they have it in their inventory
                    if (currentPlayer.checkInventory(itemName))
                    {
                        // tells the user they used the potion
                        // then uses the method changeHealth to add 30 health.
                        // and uses the method removeItem to remove it from the players inventory
                        Console.WriteLine("You used a health Potion");
                        currentPlayer.changeHealth(30);
                        currentPlayer.removeItem(itemName);
                    }
                    else
                    {
                        // if they don't have any potions, let them know.
                        Console.WriteLine("you have no Health Potions " +
                            "in your inventory");
                        
                    }
                        break;
                case "treasure":
                    if (currentPlayer.checkInventory(itemName))
                    {
                        // there is no use case so just tells them it's pretty.
                        Console.WriteLine("It looks pretty");
                    }
                    else
                    {
                        // lets them know they have no treasure
                        Console.WriteLine("You have no Treasure in your inventory");
                    }
                    break;
                default:
                    // uses default to tell them that item name is not valid
                    Console.WriteLine("{0} is not a valid item name", itemName);
                    break;
            }

        }
    }
}
