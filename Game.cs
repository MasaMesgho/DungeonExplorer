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
                Console.WriteLine("The commands are: left, forward, right, inventory, health, search, exit");
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
                    // if the user enters an unkown command lets them know so they can enter a new one.
                    Console.WriteLine("Command not recognised");
                    break;
            }
            return output;
        }
    }
}