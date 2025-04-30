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
            DetailedInventory,
            Combat,
            Search,
            Move
        }

        // has public get for testing purposes and private set so it can only be changed by the game class
        // for encapsulation
        private Player player;

        private Room currentRoom;

        private GameMap map = new GameMap();

        private List<Creature> enemyList = new List<Creature>();

        private bool waiting;

        private List<Item> items = new List<Item>();
        private List<ItemTypes> itemTypes = new List<ItemTypes>();

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
                Console.WriteLine("Player: {0}[{1}/{2}]      Floor: {1}", player.name, player.health, player.maxHealth, map.floor);
                // First gets and writes the description of the room
                Console.WriteLine(currentRoom.description);
                // outputs any message from the last action.
                // then resets the console message
                Console.Write(consoleMessage);
                consoleMessage = "";
                // then if the menu is on move, tells the user the amount of paths they can choose.
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

                // if there are enemies, set the menu to combat if it is not in the inventory and display the enemies names+health
                if (enemyList.Count > 0)
                {
                    if (state == MenuState.None) state = MenuState.Combat;
                    string enemyNames = "";
                    string enemyHP = "";
                    int i = 0;
                    foreach (Creature enemy in enemyList)
                    {
                        if (i > 0) enemyNames += enemy.name + " ("+i+")   ";
                        else enemyNames += enemy.name + "   ";
                        enemyHP += "[" + enemy.health + "/" + enemy.maxHealth + "]  ";
                        i++;
                    }
                    Console.WriteLine(enemyNames);
                    Console.WriteLine(enemyHP);
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
            int i;
            // shows the user the commands they can take
            // switches based on menu state
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
                    i = 1;
                    foreach (ExitDirection exit in currentRoom.Exits)
                    {
                        Console.WriteLine("[{0}] {1}", i, exit);
                        i++;
                    }
                    Console.WriteLine("[{0}] Back", i);
                    break;
                case MenuState.Combat:
                    Console.WriteLine("[0] Exit");
                    Console.WriteLine("[1] Inventory");
                    i = 0;
                    foreach(Creature enemy in enemyList)
                    {
                        Console.Write("[{0}] Attack: {1}", i + 2, enemyList[i].name);
                        if (i > 0) Console.Write("({0})", i);
                        Console.WriteLine();
                        i++;
                    }
                    break;
            }

            char input = Console.ReadKey().KeyChar;
            // try to convert the input to a int.
            // using try loop to avoid crashes
            int intInput;
            try 
            {
                intInput = Int32.Parse(input.ToString());
            }
            catch
            {
                // sets intInput to -1 for errorhandling
                intInput = -1;
            }
            // handles the users input
            // switches based on menu state
            switch (state)
            {
                case MenuState.None:
                    if (intInput == 0)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Are you sure you want to exit? (y/n)");
                            char exitCheck = Console.ReadKey().KeyChar;
                            if (exitCheck == 'y')
                            {
                                GameOver("");
                                break;
                            }
                            if (exitCheck == 'n') break;
                        }
                    }
                    if (input == '1')
                    {
                        if (player.InventorySize != 0)
                        {
                            itemTypes.AddRange(player.InventoryContents());
                            state = MenuState.Inventory;
                        }
                        else consoleMessage = "You have no items in your inventory";
                    }
                    if (input == '2')
                    {
                        if (currentRoom.roomInventory.Count > 0)
                        {
                            state = MenuState.Search;
                            items.AddRange(currentRoom.roomInventory);
                        }
                        else consoleMessage = "The Room is empty";
                    }
                    if (input == '3')
                    {
                        state = MenuState.Move;
                    }
                    break;
                case MenuState.Move:
                    if (intInput == 0) state = MenuState.None;
                    else if (intInput <= currentRoom.Exits.Count && intInput != -1)
                    {
                        currentRoom = map.Move(currentRoom.GetExitDirection(currentRoom.Exits[intInput - 1]));
                        state = MenuState.None;
                        enemyList = currentRoom.EnemyEncounter();
                        if (enemyList != null) consoleMessage = "Enemies have appeared!\n";
                    }
                    else if (intInput == currentRoom.Exits.Count + 1)
                    {
                        if (currentRoom.type == RoomType.Entry && currentRoom.EntryDirection == Directions.North) consoleMessage = "You cannot go back.\n";
                        else currentRoom = map.Move(currentRoom.EntryDirection);
                        state = MenuState.None;
                    }

                    break;
                case MenuState.Combat:
                    if (intInput == 0)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to exit? (y/n)");
                            char exitCheck = Console.ReadKey().KeyChar;
                            if (exitCheck == 'y')
                            {
                                GameOver("");
                                break;
                            }
                            if (exitCheck == 'n') break;
                        }
                    }
                    if (input == '1')
                    {
                        if (player.InventorySize != 0)
                        {
                            itemTypes.AddRange(player.InventoryContents());
                            state = MenuState.Inventory;
                        }
                        else consoleMessage = "You have no items in your inventory";
                    }
                    if(intInput > 1 && intInput-1 <= enemyList.Count)
                    {
                        if (player.Attack(enemyList[intInput-2]))
                        {
                            currentRoom.AddItem(enemyList[intInput-2].Drops());
                            enemyList.RemoveAt(intInput - 2);
                            if (enemyList.Count == 0) state = MenuState.None;
                        }
                        if (enemyList.Count > 0)
                        {
                            foreach (Creature enemy in enemyList)
                            {
                                if (enemy.Attack(player)) GameOver($"You were slain by a {enemy.name}");
                            }
                        }
                        else
                        {
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }

                    break;
            }

        }

        public void GameOver(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            GameRunning = false;
        }
    }
}