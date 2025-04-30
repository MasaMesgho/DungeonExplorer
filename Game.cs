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
                switch (currentRoom.Exits.Count)
                {
                    case 0:
                        Console.WriteLine("There are no exits here, you have to turn back");
                        break;
                    case 1:
                        Console.Write("You see a lone door ");
                        break;
                    case 2:
                    case 3:
                    case 4:
                        Console.Write("You see doors ");
                        break;
                }
                if (currentRoom.Exits.Contains(ExitDirection.left)) Console.Write("Left ");
                if (currentRoom.Exits.Contains(ExitDirection.forward)) Console.Write("Ahead ");
                if (currentRoom.Exits.Contains(ExitDirection.right)) Console.Write("Right ");
                if (currentRoom.Exits.Count > 0) Console.WriteLine();
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
                    Console.WriteLine("[1] Left");
                    Console.WriteLine("[2] Forward");
                    Console.WriteLine("[3] Right");
                    Console.WriteLine("[4] Down");
                    Console.WriteLine("[5] Back");
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
                    if (input == '0') state = MenuState.None;
                    if (input == '1')
                    {
                        if (currentRoom.Exits.Contains(ExitDirection.left)) currentRoom = map.Move(currentRoom.GetExitDirection(ExitDirection.left));
                        else Console.WriteLine("there is no way left.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        state = MenuState.None;
                    }
                    if (input == '2')
                    {
                        if (currentRoom.Exits.Contains(ExitDirection.forward)) currentRoom = map.Move(currentRoom.GetExitDirection(ExitDirection.forward));
                        else Console.WriteLine("there is no way forwards.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        state = MenuState.None;
                    }
                    if (input == '3')
                    {
                        if (currentRoom.Exits.Contains(ExitDirection.right)) currentRoom = map.Move(currentRoom.GetExitDirection(ExitDirection.right));
                        else Console.WriteLine("there is no way right.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        state = MenuState.None;
                    }
                    if (input == '4')
                    {
                        if (currentRoom.Exits.Contains(ExitDirection.down)) currentRoom = map.NewFloor();
                        else Console.WriteLine("there is no way down.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        state = MenuState.None;
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