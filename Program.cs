using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        // this is where the core program branches from
        /// <summary>
        /// The main program call, runs the program.
        /// </summary>
        /// <param name="args"></param>
        /// 
        static void Main(string[] args)
        {
            // starts the game loop
            Game game = new Game();
            game.Start();
            Console.WriteLine("See you next time Space Cowboy...");
            Console.Write("Press any key to close.");
            Console.ReadKey();
        }
    }
}
