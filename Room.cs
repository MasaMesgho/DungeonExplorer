using System;
using System.Configuration;

namespace DungeonExplorer
{
    public class Room
    {
        // this class contains all information and actions for rooms
        // needed variables are below with a rng generator (rnd)
        private string description;
        private int roomID;
        private string item;
        private Random rnd = new Random();
        private bool roomChecked;

        public Room(int roomID = 5)
        {
            // initializes the room
            // generates a random room ID if one is not specified
            if (roomID == 5) { this.roomID = rnd.Next(1, 4); }
            // uses the set description and set item methods
            setDescription();
            setItem();
            // sets this room to not have been searched.
            this.roomChecked = false;

        }

        private void setItem()
        {
            // use the random generator to assign 25% chances, then assign items based on the type of room.
            // if the item is out of bounds, will add a bug into the inventory.
            int chance = rnd.Next(1, 5);
            switch(this.roomID)
            {
                case 0:
                    this.item = "None";
                    break;
                case 1:
                    switch(chance)
                    {
                        case 1:
                            this.item = "None";
                            break;
                        case 2:
                            this.item = "None";
                            break;
                        case 3:
                            this.item = "Health Potion";
                            break;
                        case 4:
                            this.item = "Treasure";
                            break;
                        default:
                            this.item = "Bug in the code!";
                                break;
                    }
                    break;
                case 2:
                    switch (chance)
                    {
                        case 1:
                            this.item = "None";
                            break;
                        case 2:
                            this.item = "None";
                            break;
                        case 3:
                            this.item = "Health Potion";
                            break;
                        case 4:
                            this.item = "Treasure";
                            break;
                        default:
                            this.item = "Bug in the code!";
                                break;
                    }
                    break;
                case 3:
                    switch (chance)
                    {
                        case 1:
                            this.item = "None";
                            break;
                        case 2:
                            this.item = "None";
                            break;
                        case 3:
                            this.item = "Health Potion";
                            break;
                        case 4:
                            this.item = "Treasure";
                            break;
                        default:
                            this.item = "Bug in the code!";
                                break;
                    }
                    break;
                case 4:
                    switch (chance)
                    {
                        case 1:
                            this.item = "None";
                            break;
                        case 2:
                            this.item = "None";
                            break;
                        case 3:
                            this.item = "Health Potion";
                            break;
                        case 4:
                            this.item = "Treasure";
                            break;
                        default:
                            this.item = "Bug in the code!";
                                break;
                    }
                    break;
                default:
                    // if the room ID is out of bounds, adds a bug item for bug testing. (should be no way to obtain this item)
                    this.item = "Bug in the code!";
                    break;
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

        public string getItem()
        {
            // if the room is searched, first check if the room has been searched already
            if (roomChecked) { return "checked"; }
            // check the roomChecked variable so it can't be searched twice
            this.roomChecked = true;
            // return any item in the room (will return empty if the room has no item)
            return this.item;
        }
    }
}