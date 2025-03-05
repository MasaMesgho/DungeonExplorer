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
                    this.item = "Bug in the code!";
                    break;
            }
        }

        private void setDescription()
        {
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
                    this.description = "A unkown sight, a vortex marking a impossible place, three Doorways lead deeper in, left, right and forwards";
                    break;


            }
        }

        public string getDescription()
        {
            
            return this.description;
        }

        public string getItem()
        {
            if (roomChecked) { return "checked"; }
            this.roomChecked = true;
            return this.item;
        }
    }
}