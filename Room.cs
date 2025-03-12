using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;

namespace DungeonExplorer
{
    public class Room
    {
        // this class contains all information and actions for rooms
        // needed variables are below with a rng generator (rnd)
        private string description;
        private int roomID;
        private Dictionary<string,int> roomInventory = new Dictionary<string, int>();
        private Random rnd = new Random();
        private bool roomChecked;

        public Room(int roomID = 5)
        {
            // initializes the room
            // generates a random room ID if one is not specified
            if (roomID == 5) { this.roomID = rnd.Next(1, 4); }
            // uses the set description and set item methods
            setDescription();
            setItems();
            // sets this room to not have been searched.
            this.roomChecked = false;

        }

        private void setItems()
        {
            // use the random generator to assign 25% chances, then assign items based on the type of room.
            // if the item is out of bounds, will add a bug into the inventory.
            int amount = 0;
            int chance = 0;
            switch(this.roomID)
            {
                case 0:
                    amount = 0;
                    chance = 0;
                    break;
                case 1:
                    amount = 1;
                    chance = 60;
                    break;
                case 2:
                    amount = 2;
                    chance = 40;
                    break;
                case 3:
                    amount = 3;
                    chance = 60;
                    break;
                case 4:
                    amount = 4;
                    chance = 30;
                    break;
            }
            for (int i = 1; i<= amount; i++)
            {
                string tempItem = Item.GenerateItem(chance);
                if (tempItem != "None")
                {
                    if (roomInventory.ContainsKey(tempItem))
                    {
                        roomInventory[tempItem]++;
                    }
                    else
                    {
                        roomInventory.Add(tempItem, 1);
                    }
                }
            }
        }

        private void setDescription()
        {
            // gets the room ID and assigns the correct description based on the room entered.
            switch (this.roomID)
            {
                case 0:
                    this.description = "You arrive at the great Entryway to the dungeon" +
                        "\nloose stone and crubling pillars fill you with Anxiety, but the allure of Treasure draws you in" +
                        "\njust inside you see three passages, left, right and forwards, which will you choose?";
                    break;
                case 1:
                    this.description = "A great Hall, filled with forgotten crumbling pillars, three Doorways lead deeper in, left, right and forwards";
                    break;
                case 2:
                    this.description = "A crumbling graveyard, littered with broken caskets, three Doorways lead deeper in, left, right and forwards";
                    break;
                case 3:
                    this.description = "A abandoned tight fitting corridor, crumbling stone walls mark the years passed in this forgotten place, three Doorways lead deeper in, left, right and forwards";
                    break;
                case 4:
                    this.description = "A silent dining room, rotting tables and scattered cutlery the only signs of time you can see, three Doorways lead deeper in, left, right and forwards";
                    break;
                default:
                    // if the room is out of bounds, goes to a errored area, should be no way to reach this room.
                    this.description = "A unkown sight, a vortex marking a impossible place, three Doorways lead deeper in, left, right and forwards";
                    break;


            }
        }

        public string getDescription()
        {
            // if the description is requested, sends the description.
            return this.description;
        }

        public Dictionary<string,int> getItems()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            // if the room is searched, first check if the room has been searched already
            if (roomChecked)
            {
                temp.Add("checked", 0);
                return temp;
            }
            
            // check the roomChecked variable so it can't be searched twice
            this.roomChecked = true;
            // return any item in the room (will return empty if the room has no item)
            return this.roomInventory;
        }
    }
}