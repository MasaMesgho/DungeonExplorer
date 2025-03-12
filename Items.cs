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
        public static string GenerateItem(int chance)
        {
            string output = "None";
            Random rnd = new Random();
            bool errorCheck = chance > 0 && chance <= 100;
            Debug.Assert(errorCheck, "Invalid chance variable " +
                "passed to the item Generator");
            int temp = rnd.Next(1, 101);
            if (errorCheck && chance >= temp)
            {
                int itemChance = rnd.Next(1, 5);
                switch (itemChance)
                {
                    case 1:
                    case 2:
                    case 3:
                        output = "Health Potion";
                        break;
                    case 4:
                        output = "Treasure";
                        break;
                    default:
                        Debug.Assert(itemChance > 0 && itemChance < 5,
                            "Error in item chance assignment");
                        break;
                }
            }
            return output;
        }

        public static void useItem(string itemName, Player currentPlayer)
        {
            switch (itemName)
            {
                case "Health Potion":
                    if (currentPlayer.checkInventory(itemName))
                    {
                        Console.WriteLine("You used a health Potion");
                        currentPlayer.changeHealth(30);
                        currentPlayer.removeItem(itemName);
                    }
                    else
                    {
                        Console.WriteLine("you have no Health Potions " +
                            "in your inventory");
                        
                    }
                        break;
                case "Treasure":
                    if (currentPlayer.checkInventory(itemName))
                    {
                        Console.WriteLine("It looks pretty");
                    }
                    else
                    {
                        Console.WriteLine("You have no Treasure in your inventory");
                    }
                    break;
                default:
                    Console.WriteLine("{0} is not a valid item name", itemName);
                    break;
            }

        }
    }
}
