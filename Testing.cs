using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Testing
    {

        public static void TestMap()
        {
            GameMap map = new GameMap();
            Room testRoom = map.NewFloor();
            Console.WriteLine("Floor {0}", map.floor);
            foreach (int[] floor in map.roomGrid)
            {
                foreach (int room in floor)
                {
                    Console.Write(room+" ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public static void TestCombat()
        {
            Creature enemy = new Slime(10);
            Player player = new Player("test");
            player.Attack(enemy);
        }


    }
}
