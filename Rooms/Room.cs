using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Room
    {
        // this class contains all information and actions for rooms
        // needed variables are below with a rng generator (rnd)
        private string description;
        private int roomType;
        private Dictionary<string,int> roomInventory = new Dictionary<string, int>();
        private Random rnd = new Random();
        private bool roomChecked;
        /// <summary>
        /// Generates the Room with random attributes if not specified.
        /// </summary>
        /// <param name="roomType">
        /// The Type of room if specifying, leave blank if not specifying.
        /// </param>
        public Room(int tempRoomType = -1)
        {
            // initializes the room
            // generates a random room ID if one is not specified
            if (tempRoomType == -1) { roomType = rnd.Next(1, 4); }
            // uses the set description and set item methods
            // makes sutre the room type is in range.
            Debug.Assert(roomType < 5 && roomType >= 0, "Room Type out of range.");
            setDescription();
            setItems();
            // sets this room to not have been searched.
            this.roomChecked = false;

        }
        /// <summary>
        /// Adds Items to the Rooms Inventory.
        /// </summary>
        private void setItems()
        {
            // Checks the room ID then assigns an item chance, and the amount of tries to get items.
            int amount = 0;
            int chance = 0;
            switch(this.roomType)
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
            // for each attempt at an item, uses the chance to attempt adding an item and adds it to the room Inventory.
            for (int i = 1; i<= amount; i++)
            {
                // uses the item class method Generate item and passes it the chance of an item.
                string tempItem = Item.GenerateItem(chance);
                if (tempItem != "none")
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
        /// <summary>
        /// Sets the Rooms description based on the Room Type.
        /// </summary>
        private void setDescription()
        {
            // gets the room ID and assigns the correct description based on the room entered.
            switch (this.roomType)
            {
                case 0:
                    this.description = "You arrive at the great Entryway to the dungeon" +
                        "\nloose stone and crubling pillars fill you with Anxiety, but the allure of Treasure draws you in" +
                        "\njust inside you see three passages, left, right and forwards, which will you choose?";
                    break;
                case 1:
                    this.description = "A great Hall, " +
                        "filled with forgotten crumbling pillars, " +
                        "three Doorways lead deeper in, left, right and forwards";
                    break;
                case 2:
                    this.description = "A crumbling graveyard, " +
                        "littered with broken caskets, " +
                        "three Doorways lead deeper in, left, right and forwards";
                    break;
                case 3:
                    this.description = "A abandoned tight fitting corridor, " +
                        "crumbling stone walls mark the years passed in this forgotten place, " +
                        "three Doorways lead deeper in, left, right and forwards";
                    break;
                case 4:
                    this.description = "A silent dining room, " +
                        "rotting tables and scattered cutlery the only signs of time you can see, " +
                        "three Doorways lead deeper in, left, right and forwards";
                    break;


            }
        }
        /// <summary>
        /// Gets the current description of the room.
        /// </summary>
        /// <returns>
        /// The Rooms description in string
        /// </returns>
        public string getDescription()
        {
            // if the description is requested, sends the description.
            return this.description;
        }
        /// <summary>
        /// Gets the Items in the rooms inventory if the room has not been checked.
        /// </summary>
        /// <returns>
        /// The Items in the rooms inventory if it has not been checked.
        /// Returns checked if it has already been checked.
        /// </returns>
        public Dictionary<string,int> getItems(bool test = false)
        {
            //creates a temporary dictionary to return if the room has already been searched
            Dictionary<string, int> temp = new Dictionary<string, int>();

            // if this is called as a test, just returns the rooms description
            if (test) { return roomInventory; }
            // if the room is searched, first check if the room has been searched already
            if (roomChecked)
            {
                // adds a checked item to the dictionary which is then handled in the game class
                temp.Add("checked", 0);
                return temp;
            }
            
            // check the roomChecked variable so it can't be searched twice
            roomChecked = true;
            // return every item in the room (will return empty if the room has no items)
            return roomInventory;
        }
    }
}