using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.CompilerServices;



namespace DungeonExplorer
{
    public class Game
    {
        // bool that determines if the game is running
        private bool GameRunning;
        /// <summary>
        /// handles menu state switching
        /// </summary>
        private enum MenuState
        {
            None,
            Inventory,
            DetailedInventory,
            Combat,
            Search,
            Item,
            Move
        }
        // variables used within the game class are determined here

        private Player player;

        private Room currentRoom;

        private GameMap map = new GameMap();

        private List<Creature> enemyList = new List<Creature>();

        // Variables for use in inventory interactions
        private List<Item> items = new List<Item>(); 
        private Item item;
        private List<ItemTypes> itemTypes = new List<ItemTypes>();
        private int page;

        // stores the menu state, none is default.
        private MenuState state = MenuState.None;

        // for relaying messages to the player between screen clears.
        private string consoleMessage = "";

        /// <summary>
        /// Initialises the main game loop.
        /// </summary>
        public Game()
        {
            // Initialize the game with a map, one room and one player after getting their name
            currentRoom = map.NewFloor();
            Console.Write("Please Enter your name: ");
            string name = Console.ReadLine();

            // if the user selects "test" as a name, runs the test class
            if (name.ToLower() == "test")
            {
                // call the test and then remove the object after testing is complete.
                Testing test = new Testing();
                test = null;
            }

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
                // shows the user basic stats like name, health, max health and the floor
                Console.WriteLine("Player: {0}[{1}/{2}]      Floor: {3}", player.name, player.health, player.maxHealth, map.floor);
                // First gets and writes the description of the room
                Console.WriteLine(currentRoom.description);
                // outputs any message from the last action.
                // then resets the console message
                Console.Write(consoleMessage);
                consoleMessage = "";
                // then if the menu is on move, tells the user the amount of paths they can choose.
                if (state == MenuState.Move)
                {
                    // switches based on the amount of exits and displays an appropriate message to the user
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
                    // loops through each enemy, adds their name and Hp to a string and displays them to the player
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
                Console.WriteLine(); // just here to add distance

                Menu();

            }
        }

        /// <summary>
        /// Handles menu interactions and display.
        /// </summary>
        private void Menu()
        {
            int i;
            // shows the user the commands they can take
            // switches based on menu state
            // always has a way to either quit or return to a menu that can quit.
            switch (state)
            {
                // if it's the base menu, just shows 4 basic options that navigate to other menus
                case MenuState.None:
                    Console.WriteLine("[0] Exit");
                    Console.WriteLine("[1] Inventory");
                    Console.WriteLine("[2] Search");
                    Console.WriteLine("[3] Move");
                    break;

                // if it is the move menu, shows available paths for the user to take
                case MenuState.Move:
                    Console.WriteLine("[0] Back");
                    i = 1;
                    // loops through each exit direction and adds it to the menu
                    foreach (ExitDirection exit in currentRoom.Exits)
                    {
                        Console.WriteLine("[{0}] {1}", i, exit);
                        i++;
                    }
                    // adds a way for the user to return to the previous room
                    Console.WriteLine("[{0}] Previous Room", i);
                    break;

                // for the combat menu, has it's own exit and can use the inventory
                // shows the different enemies that can be attacked.
                case MenuState.Combat:
                    Console.WriteLine("[0] Exit");
                    Console.WriteLine("[1] Inventory");

                    // shows the commands to attack each creature in the enemy list
                    i = 0;
                    // loops through each creature and adds it to the commands list
                    foreach(Creature enemy in enemyList)
                    {
                        Console.Write("[{0}] Attack: {1}", i + 2, enemyList[i].name);
                        if (i > 0) Console.Write("({0})", i);
                        Console.WriteLine();
                        i++;
                    }
                    break;

                // the search menu (only accessible when there are items in the rooms inventory)
                // shows all items in the rooms inventory for the player to pick up.
                case MenuState.Search:
                    Console.WriteLine("[0] Back");
                    i = 1;
                    // simple loop to add a command for each item in the inventory. this has a maximum of 8 items so this basic loop works fine
                    foreach (Item item in currentRoom.roomInventory)
                    {
                        Console.WriteLine("[{0}] Pick Up {1}", i, item.name);
                        i++;
                    }
                    break;

                // the inventory shows the item catergories from the players inventory
                case MenuState.Inventory:
                    Console.WriteLine("[0] Back");
                    i = 1;
                    // loops through the available item types and adds a command for each
                    foreach (ItemTypes type in itemTypes)
                    {
                        Console.WriteLine("[{0}] {1}", i, type);
                        i++;
                    }
                    break;

                // this inventory shows the items of the chosen type to the player
                // it has a page system so that if the player has a full inventory(20 items)
                // it can still display them
                case MenuState.DetailedInventory:
                    Console.WriteLine("[0] Back");
                    i = 1;
                    // if it needs to show pages
                    if (items.Count > 7)
                    {
                        // if there needs to be a next page option
                        if (items.Count > 7+page*7)
                        {
                            Console.WriteLine("[{0}] Next Page",i);
                            i++;
                        }
                        // if there needs to be a previous page option
                        if (page > 0)
                        {
                            Console.WriteLine("[{0}] previous Page",i);
                            i++;
                        }
                        // uses a index of 'x' with a maximum of 7 higher so it displays the correct no. of items
                        int x = page * 7;
                        int max = x+7;
                        // loops through items while there is room left on the page, and there are still items to add.
                        while (x <= max && x<=items.Count-1)
                        {
                            Console.WriteLine("[{0}] {1}", i, items[x].name);
                            i++;
                            x++;
                        }
                    }
                    else
                    {
                        // if it doesn't need to show pages, just loops through the items.
                        i = 1;
                        foreach (Item temp in items)
                        {
                            Console.WriteLine("[{0}] {1}", i, temp.name);
                            i++;
                        }
                    }
                    break;

                // displays the item options (use or discard), this alsos come with a item description in the console message above these options.
                case MenuState.Item:
                    Console.WriteLine("[0] Back");
                    Console.WriteLine("[1] Use");
                    Console.WriteLine("[2] Discard");
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
                // on the base menu
                case MenuState.None:
                    // if the user has chosen to exit
                    if (input == '0')
                    {
                        // makes sure the user wants to quit by using a y/n loop that either calls game over or just breaks the loop
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

                    // if the inventory option is chosen
                    if (input == '1')
                    {
                        // makes sure the player has items in the inventory
                        if (player.InventorySize != 0)
                        {
                            // loads the item types of the players inventory into the itemTypes variable and changes the menu state to inventory
                            itemTypes = player.InventoryContents();
                            state = MenuState.Inventory;
                        }
                        // otherwise doesn't do anything except let the player know they have no items in their inventory
                        else consoleMessage = "You have no items in your inventory";
                    }

                    // if the user chooses to search the room
                    if (input == '2')
                    {
                        // checks that the room has items to be found
                        if (currentRoom.roomInventory.Count > 0)
                        {
                            // sets the state to search and loads the items into the items variable
                            state = MenuState.Search;
                            items = currentRoom.roomInventory;
                        }
                        // otherwise does nothing except let the player know there are no items to search
                        else consoleMessage = "The Room is empty";
                    }
                    // if the user chose to move, load the move menu
                    if (input == '3')
                    {
                        state = MenuState.Move;
                    }
                    break;

                // if the player is in the move menu
                case MenuState.Move:
                    // if they chose to return to the previous menu, set the menu to none(default)
                    if (input == '0') state = MenuState.None;
                    // otherwise if they chose an option for a room exit, and intInput is in a valid range
                    else if (intInput <= currentRoom.Exits.Count && intInput > 0)
                    {
                        // set the current room to the new room returned by the map.move method
                        currentRoom = map.Move(currentRoom.GetExitDirection(currentRoom.Exits[intInput - 1]));
                        // set the menu to default
                        state = MenuState.None;
                        // see if any enemies are encountered
                        enemyList = currentRoom.EnemyEncounter();
                        // if enemies are encountered, sets a console message that lets the player know.
                        if (enemyList.Count != 0) consoleMessage = "Enemies have appeared!\n";
                    }
                    // if the user chose the previous room option
                    else if (intInput == currentRoom.Exits.Count + 1)
                    {
                        // if they have just entered a new floor, lets them know they can't go back
                        // otherwise moves them to a previous room.
                        // (not actually the previous room, it is a new room of the same type with the visited bool true)
                        if (currentRoom.type == RoomType.Entry && currentRoom.EntryDirection == Directions.North) consoleMessage = "You cannot go back.\n";
                        else currentRoom = map.Move(currentRoom.EntryDirection);
                        // sets the menu to the default state
                        state = MenuState.None;
                    }

                    break;

                // if in the combat menu
                case MenuState.Combat:
                    if (input == '0')
                    {
                        // same while loop found in default menu, to make sure the user wants to exit.
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
                    // same as the default menu, makes sure the player has items in their inventory, if so, opens it.
                    if (input == '1')
                    {
                        if (player.InventorySize != 0)
                        {
                            itemTypes = player.InventoryContents();
                            state = MenuState.Inventory;
                        }
                        else consoleMessage = "You have no items in your inventory";
                    }
                    // if the user instead chose a enemy to attack
                    if(intInput > 1 && intInput-1 <= enemyList.Count)
                    {
                        // attacks the target and checks if the target has been slain.
                        if (player.Attack(enemyList[intInput-2]))
                        {
                            // if the target was slain, adds their drop to the room inventory
                            // lets the player know they slew a enemy
                            // and removes the enemy from the list
                            currentRoom.AddItem(enemyList[intInput-2].Drops());
                            consoleMessage = $"You have Slain the {enemyList[intInput - 2].name}!\n";
                            enemyList.RemoveAt(intInput - 2);
                            // if there are no enemies left, reset the menu to default.
                            if (enemyList.Count == 0) state = MenuState.None;
                        }

                        // if there are enemies left after the player attack
                        if (enemyList.Count > 0)
                        {
                            // each enemy gets to attack, checking if they have killed the player each time.
                            foreach (Creature enemy in enemyList)
                            {
                                if (enemy.Attack(player)) GameOver($"You were slain by a {enemy.name}\n");
                            }
                        }
                        // this just holds the input so the player can see the damage they dealt/took
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }

                    break;

                // if the menu is in the search state
                case MenuState.Search:
                    //same as before moves them back to default menu
                    if (input == '0') state = MenuState.None;
                    // if they selected a item to aquire
                    else if (intInput <= items.Count)
                    {
                        // loads the item chosen to a temp variable
                        Item temp = items[intInput - 1];
                        // tries to pickup the item
                        if (player.PickUpItem(temp))
                        {
                            // if the item is collected, removes it from the items list and the current rooms inventory
                            currentRoom.RemoveItem(temp);
                            items.Remove(temp);
                            // lets the user know they collected the item
                            consoleMessage = "you picked up the " + temp.name;
                        }
                        // if the item can't be picked up (full inventory)
                        else
                        {
                            // let the user know they need to remove a item from their inventory and sets the menu to default
                            consoleMessage = "You have too many items in your inventory\ntry using or discarding an item.";
                            state = MenuState.None;
                        }
                        // if there are no items left, goes back to the default menu
                        if (currentRoom.roomInventory.Count == 0) state = MenuState.None;
                    }
                    break;

                // if the user is in the inventory menu
                case MenuState.Inventory:
                    if (input == '0') state = MenuState.None;

                    // if they selected a item type to load
                    if (intInput > 0 &&intInput <= itemTypes.Count)
                    {
                        // loads all the items into the temporary list and changes the menu to the detailed inventory
                        items = player.InventoryContents(itemTypes[intInput - 1]);
                        state = MenuState.DetailedInventory;
                        // sets the page to 0 (for use in navigating large inventories)
                        page = 0;
                    }
                    break;

                // if the user is in the detailed inventory
                case MenuState.DetailedInventory:
                    if (input == '0') state = MenuState.Inventory;

                    // if there are more than 7 items and the input is valid.
                    if (items.Count > 7 && intInput>0)
                    {
                        // x stores where in the array it is
                        int x;
                        // i stores the place of where the menu inputs are
                        i = 1;
                        // if the user could have chosen next page
                        if (items.Count > 7 + page * 7)
                        {
                            // if they chose next page, move the page up by 1.
                            if (intInput == i)
                            {
                                page++;
                                break;
                            }
                            // otherwise move the placeholder along
                            i++;
                        }
                        // if they could have chosen previous page
                        if (page > 0)
                        {
                            // if they did choose previous page, move page down 1
                            if (intInput == i)
                            {
                                page++;
                                break;
                            }
                            // otherwise move the placeholder along 1 and then subtract the placeholder from how many menu options were above the users choice
                            i++;
                            i = ( page * 7 ) - i;
                        }
                        // if they did not choose any of the above options, and the placeholder+their input is a valid selection
                        if (i + intInput <= items.Count)
                        {
                            //add the placeholder to their choice and get the item, and move them to the item menu
                            item = items[intInput + i];
                            state = MenuState.Item;
                            // give the user the items description
                            consoleMessage = item.description;
                        }
                    }
                    else
                    {
                        //otherwise just get their choice from the menu, load that item, give the user the items description and load the item menu
                        if (intInput > 0 && intInput <= items.Count)
                        {
                            item = items[intInput - 1];
                            state = MenuState.Item;
                            consoleMessage = item.description;
                        }
                    }
                    break;

                // if they are in the item menu
                case MenuState.Item:
                    // reset page to 0
                    page = 0;
                    // if they chose to go back, load the detailed inventory menu
                    if (input == '0') state = MenuState.DetailedInventory;
                    // if they choose to use the item
                    if (input == '1')
                    {
                        // set the menu to default, use the item and remove it from the players inventory
                        state = MenuState.None;
                        item.Use(player);
                        player.removeItem(item);
                    }
                    // if they choose to discard the item
                    if (input == '2')
                    {
                        // remove the item and set the menu to default
                        state = MenuState.None;
                        player.removeItem(item);
                    }
                    break;
            }

        }
        /// <summary>
        /// ends the main game loop
        /// </summary>
        /// <param name="message"> the death message to give the player </param>
        public void GameOver(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            GameRunning = false;
        }
    }
}