using System;
using System.Media;
using System.Runtime.CompilerServices;



namespace DungeonExplorer
{
    internal class Game
    {
        public Random rnd = new Random();
        public Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialize the game with one room and one player
            currentRoom = new Room(0);
            Console.Write("Please Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name);
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
                bool moving = false;
                while (!moving)
                {
                    string userInput = Console.ReadLine();
                    string result = action(userInput.ToLower());
                    switch (result)
                    {
                        case "false":
                            break;
                        case "true":
                            moving = true;
                            break;
                        case "exit":
                            moving = true;
                            playing = false;
                            break;
                    }
                    
                }
            }
        }

        private string action(string userInput)
        {
            string output = "false";
            switch (userInput)
            {
                case "left":
                    currentRoom = new Room();
                    output = "true";
                    break;

                case "forward":
                    currentRoom = new Room();
                    output = "true";
                    break;

                case "right":
                    currentRoom = new Room();
                    output = "true";
                    break;

                case "inventory":
                    Console.WriteLine("{0}", player.InventoryContents());
                    break;

                case "health":
                    Console.WriteLine("Current Health: {0}", player.Health);
                    break;

                case "exit":
                    output = "exit";
                    break;

                case "search":
                    string contents = currentRoom.getItem();
                    if (contents == "None")
                    {
                        Console.WriteLine("The room is Empty.");
                    }
                    else if (contents == "checked")
                    {
                        Console.WriteLine("You have already searched this room.");
                    }
                    else
                    {
                        Console.WriteLine("You have found 1 x {0}!", contents);
                        player.PickUpItem(contents);
                    }
                    break;

                default:
                    Console.WriteLine("Command not recognised");
                    break;
            }
            return output;
        }
    }
}