using System;
using System.Collections.Generic;
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
            // Initialize the game with one room and one player after getting their name
            currentRoom = new Room(0);
            Console.Write("Please Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name);
        }
        public void Start()
        {
            // uses a boolean variable to initialize the loop
            bool playing = true;
            // loops while the game is running
            while (playing)
            {
                // First gets and writes the description of the room
                Console.WriteLine(currentRoom.getDescription());
                // lets the user know their commands
                Console.WriteLine("The commands are: " +
                    "left, forward, right, inventory, use, health, search, exit");
                // use a while loop to get input from a user until a moving or exit command is used.
                bool moving = false;
                while (!moving)
                {
                    // gets the user input and passes it to the action method below
                    string userInput = Console.ReadLine();
                    string result = action(userInput.ToLower());
                    // once the result of the actionn has been gotten, confirm if they are moving to a new room or exiting the game.
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
            // gets the user input from the main game loop and translates it to a action to be passed back to the main game loop.
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
                    Dictionary<string,int> contents = currentRoom.getItems();
                    if (contents.Count == 0)
                    {
                        Console.WriteLine("The room is Empty.");
                    }
                    else if (contents.ContainsKey("checked"))
                    {
                        Console.WriteLine("You have already searched this room.");
                    }
                    else
                    {
                        Console.WriteLine("You have found:");
                        foreach (var item in contents)
                        {
                            Console.WriteLine("{0} x {1}", item.Value, item.Key);
                            for (int i = 0;i<item.Value;i++)
                            {
                                player.PickUpItem(item.Key);
                            }
                        }
                    }
                    break;
                case "use":
                    Console.WriteLine("What item would you like to use?");
                    string itemInput = Console.ReadLine();
                    Item.useItem(itemInput, player);
                    break;

                default:
                    // if the user enters an unkown command lets them know so they can enter a new one.
                    Console.WriteLine("Command not recognised");
                    break;
            }
            return output;
        }
    }
}