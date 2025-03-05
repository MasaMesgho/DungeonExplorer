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
        static void Main(string[] args)
        {
            // starts the game loop
            Game game = new Game();
            game.Start();
        }
    }
}
