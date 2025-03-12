using System;
using System.Collections.Generic;
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
        itemType thisItemType;
        private string Name;
        public string name
        {
            get { return Name; }
        }
        public string useItem(Player currentPlayer)
        {
            string output = "";
            switch (thisItemType)
            {
                case itemType.Equipment:
                    output = "Not currently in use";
                    break;
                case itemType.Consumable:
                    switch (Name)
                    {
                        case "Health Potion":
                            currentPlayer.changeHealth(30);
                            output = "Your health is restored.";
                            break;
                    }
                    break;
                case itemType.Misc:
                    output = ("You don't know how to use this " + Name);
                    break;
                default:

                    output = "You encountered an error";
                    break;
            }
            return output;
        }

    }
}
