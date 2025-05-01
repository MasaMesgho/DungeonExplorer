using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    internal class Program
    {
        public static Random rnd = new Random();
        // this is where the core program branches from
        /// <summary>
        /// The main program call, runs the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // see if the player wants to test before running.
            Console.WriteLine("Welcome To Dungeon Explorer \nPress 'y' to run tests or any other key to start playing.");
            char input = Console.ReadKey().KeyChar;
            if (input == 'y' || input == 'Y')
            {
                // call the test and then remove the object after testing is complete.
                Testing test = new Testing();
                test = null;
                Console.Clear();
            }
            // starts the game loop
            Game game = new Game();
            game.Start();
            Console.WriteLine("See you Space Cowboy..."); // cowboy bebop reference
            Console.Write("Press any key to close.");
            Console.ReadKey();
        }
    }
}
