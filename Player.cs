using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }


        public Dictionary <Items,int> inventory = new Dictionary<Items,int>();
        
        

        public Player(string name) 
        {
            Name = name;
            Health = 100;


        }
        public void PickUpItem(Items item)
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
                string tempString = i.Value + " x " + i.Key.Name + "\n" ;
                outputString += tempString;
            }
            if (outputString == "") { outputString = "Empty"; }
            return outputString;
        }
    }
}