using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.CompilerServices;



namespace DungeonExplorer
{
    public class Game
    {
        private bool GameRunning;

        // has public get for testing purposes and private set so it can only be changed by the game class
        // for encapsulation
        public Player player { get; private set; }
        public Room currentRoom { get; private set; }

        /// <summary>
        /// Initialises the main game loop.
        /// </summary>
        public Game()
        {
            // Initialize the game with one room and one player after getting their name
            //currentRoom = new GameMap(0);
            Console.Write("Please Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name);
        }

        /// <summary>
        /// Starts the main Game Loop
        /// </summary>
        public void Start()
        {
            // uses a boolean variable to initialize the loop
            GameRunning = true;
            // loops while the game is running
            /*while (GameRunning)
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
                    //string result = action(userInput.ToLower());
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
                            GameRunning = false;
                            break;
                    }
                }
            }*/
        }

        /// <summary>
        /// Determines the type of action and calls the relevant methods.
        /// </summary>
        /// <param name="userInput">
        /// The users input.
        /// </param>
        /// <returns>
        /// If a move action has been taken or if the user wants to exit.
        /// </returns>
        /*private string action(string userInput)
        {
            // gets the user input from the main game loop and translates it to a action to be passed back to the main game loop.
            // currently when moving, can move any direction and will generate a new random room.
            string output = "false";
            switch (userInput)
            {
                case "left":
                    currentRoom = new Room();
                    Testing.TestRoom(currentRoom);
                    output = "true";
                    break;

                case "forward":
                    currentRoom = new Room();
                    Testing.TestRoom(currentRoom);
                    output = "true";
                    break;

                case "right":
                    currentRoom = new Room();
                    Testing.TestRoom(currentRoom);
                    output = "true";
                    break;

                case "inventory":
                    // uses the inventory contents command from the player class
                    Console.WriteLine("{0}", player.InventoryContents());
                    break;

                case "health":
                    // gets the current health from the player class
                    Console.WriteLine("Current Health: {0}", player.health);
                    break;

                case "exit":
                    // breaks the main game loop by passing an exit command to it.
                    output = "exit";
                    break;

                case "search":
                    // first gets the room contents using the room method getItems
                    Dictionary<string,int> contents = currentRoom.getItems();
                    // if the room has no items in it (a seached room does not have an empty dictionary.
                    if (contents.Count == 0)
                    {
                        // tell the user the room is empty.
                        Console.WriteLine("The room is Empty.");
                    }
                    else if (contents.ContainsKey("checked"))
                    {
                        // if the room contains a checked key (added by the room method
                        // tell the user they can't search the same room twice
                        Console.WriteLine("You have already searched this room.");
                    }
                    else
                    {
                        // if the dictionary has elements that are not "checked" let the user know what they found.
                        Console.WriteLine("You have found:");
                        foreach (var item in contents)
                        {
                            // loop through every item in the array to let the user know what they have added
                            Console.WriteLine("{0} x {1}", item.Value, item.Key);
                            // add the item x times (where x is the amount in the room) to the players inventory
                            for (int i = 0;i<item.Value;i++)
                            {
                                player.PickUpItem(new Potion(1));
                            }
                        }
                    }
                    break;
                case "use":
                    // if the user uses the use command, get another input for the item type
                    Console.WriteLine("What item would you like to use?");
                    string itemInput = Console.ReadLine().ToLower();
                    // uses the item class method use item, also passes the current player for any effects to be used.
                    
                    break;

                default:
                    // if the user enters an unkown command lets them know so they can enter a new one.
                    Console.WriteLine("Command not recognised");
                    break;
            }
            return output;
        }*/

        public void GameOver()
        {
            GameRunning = false;
        }
    }
}