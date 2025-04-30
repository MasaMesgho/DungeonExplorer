using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.CompilerServices;



namespace DungeonExplorer
{
    public class Game
    {
        private bool GameRunning;

        private enum MenuState
        {
            None,
            Inventory,
            Combat,
            Move
        }

        // has public get for testing purposes and private set so it can only be changed by the game class
        // for encapsulation
        private Player player;

        private Room currentRoom;

        private GameMap map = new GameMap();

        private List<Creature> enemyList;

        private bool waiting;

        private List<Item> items;
        private List<ItemTypes> itemTypes;

        private MenuState state = MenuState.None;

        private string consoleMessage = "";

        /// <summary>
        /// Initialises the main game loop.
        /// </summary>
        public Game()
        {
            // Initialize the game with one room and one player after getting their name
            currentRoom = map.NewFloor();
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
            while (GameRunning)
            {
                Console.Clear();
                // First gets and writes the description of the room
                Console.WriteLine(currentRoom.description);
                // outputs any message from the last action.
                // then resets the console message
                Console.Write(consoleMessage);
                consoleMessage = "";
                // then if the menu is on move, tells the user the amount of paths.
                if (state == MenuState.Move)
                {
                    switch (currentRoom.Exits.Count)
                    {
                        case 1:
                            if (currentRoom.Exits.Contains(ExitDirection.None)) Console.Write("There are no doors, you must turn back.");
                            else Console.WriteLine("You see a path ahead");
                            break;
                        case 2:
                            Console.WriteLine("You see two paths ahead");
                            break;
                        case 3:
                            Console.WriteLine("You see three ahead:");
                            break;
                        case 4:
                            Console.WriteLine("You see four ahead:");
                            break;
                    }
                }
                Console.WriteLine();

                Menu();

            }
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
        private void Menu()
        {

            switch (state)
            {
                case MenuState.None:
                    Console.WriteLine("[0] Exit");
                    Console.WriteLine("[1] Inventory");
                    Console.WriteLine("[2] Search");
                    Console.WriteLine("[3] Move");
                    break;
                case MenuState.Move:
                    Console.WriteLine("[0] Previous menu");
                    int i = 1;
                    foreach (ExitDirection exit in currentRoom.Exits)
                    {
                        Console.WriteLine("[{0}] {1}", i, exit);
                        i++;
                    }
                    Console.WriteLine("[{0}] Back", i);
                    break;
                case MenuState.Combat:
                    break;
            }

            char input = Console.ReadKey().KeyChar;
            switch (state)
            {
                case MenuState.None:
                    if (input == '0') GameOver();
                    if (input == '1')
                    {
                        itemTypes.AddRange(player.InventoryContents());
                        state = MenuState.Inventory;
                    }
                    if (input == '2')
                    {

                    }
                    if (input == '3')
                    {
                        state = MenuState.Move;
                    }
                    break;
                case MenuState.Move:
                    int intInput = Int32.Parse(input.ToString());
                    if (intInput == 0) state = MenuState.None;
                    else if ((intInput) <= currentRoom.Exits.Count)
                    {
                        currentRoom = map.Move(currentRoom.GetExitDirection(currentRoom.Exits[intInput - 1]));
                    }
                    else if (intInput == currentRoom.Exits.Count + 1)
                    {
                        if (currentRoom.type == RoomType.Entry && currentRoom.EntryDirection == Directions.North) consoleMessage = "You cannot go back.\n";
                        else currentRoom = map.Move(currentRoom.EntryDirection);
                    }

                    break;
            }

        }

        public void GameOver()
        {
            GameRunning = false;
            Console.Clear();
        }
    }
}