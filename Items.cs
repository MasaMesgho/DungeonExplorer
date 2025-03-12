using System;
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

    public enum itemType
    {
        Equipment,
        Consumable,
        Misc
    }

    internal class Item
    {
        public static string GenerateItem(int chance)
        {
            string output = "None";
            Random rnd = new Random();
            bool errorCheck = chance > 0 && chance <= 100;
            Debug.Assert(errorCheck, "Error in chance variable " +
                "passed to the item Generator");
            if (!errorCheck && chance > rnd.Next(1,101))
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
                    }
                    else
                    {
                        Debug.Assert(false, "Item not in Inventory");
                        
                    }
                        break;
                case "Treasure":

            }
        }
    }
}
