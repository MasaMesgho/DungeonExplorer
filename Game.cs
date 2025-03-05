using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Xml.Linq;


//wow, much comment, very cool

namespace DungeonExplorer
{
    internal class Game
    {
        public Random rnd = new Random();
        private Player player;
        private Room currentRoom = new Room(0);


        public Items Sword;
        public Items Potion;
        public Game()
        {
            // Initialize the game with one room and one player
            Console.WriteLine("Please Enter your name");
            string name = Console.ReadLine();
            player = new Player(name);

            //Initalize items
            Sword = new Items("Sword");
            Potion = new Items("Potion");

            player.inventory.Add(Sword, 1);
            player.inventory.Add(Potion, 1);
        }
        public void Start()
        {


            
            // Change the playing logic into true and populate the while loop
            bool playing = true;
            while (playing)
            {
                // Code your playing logic here
                Console.WriteLine(currentRoom.getDescription());
                Console.WriteLine("The commands are: left, forward, right, inventory, health, search, exit");
                bool waitingInput = true;
                while (waitingInput)
                {
                    string userInput = Console.ReadLine();
                    userInput = userInput.ToLower();
                    switch (userInput)
                    {
                        case "left":
                            currentRoom = new Room();
                            waitingInput = false;
                            break;

                        case "forward":
                            currentRoom = new Room();
                            waitingInput = false;
                            break;

                        case "right":
                            currentRoom = new Room();
                            waitingInput = false;
                            break;

                        case "inventory":
                            Console.WriteLine("{0}", player.InventoryContents());
                            break;

                        case "health":
                            Console.WriteLine("Current Health: {0}", player.Health);
                            break;

                        case "exit":
                            playing = false;
                            waitingInput = false;
                            break;

                        /* case "search":
                            string contents = currentRoom.getItem();
                            if (contents=="None")
                            {
                                Console.WriteLine("The room is Empty.");
                            }
                            else if (contents=="checked")
                            {
                                Console.WriteLine("You have already searched this room.");
                            }
                            else
                            {
                                Console.WriteLine("You have found 1 x {0}!", contents);
                                player.PickUpItem(contents);
                            }
                            break;
                        */

                        default:
                            Console.WriteLine("Command not recognised");
                            break;
                    }
                }
            }
        }
    }
}