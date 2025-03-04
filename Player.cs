using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private Dictionary <string,int> inventory = new Dictionary<string,int>();
        
        

        public Player(string name) 
        {
            Name = name;
            Health = 100;

            inventory.Add("Sword", 1);
            inventory.Add("Potion", 1);
        }
        public void PickUpItem(string item)
        {
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
            string outputString = "";
            foreach (var i in this.inventory)
            {
                string tempString = i.Value + " x " + i.Key + "\n" ;
                outputString += tempString;
            }
            if (outputString == "") { outputString = "Empty"; }
            return outputString;
        }
    }
}