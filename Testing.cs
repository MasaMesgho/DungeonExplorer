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
        public Testing()
        {
            TestMap();
            TestCombat();
        }

        private void TestMap()
        {
            GameMap map = new GameMap();
            Room testRoom = map.NewFloor();
            Debug.Assert(map.roomGrid != null, "RoomGrid not generated");
            Debug.Assert(testRoom != null, "Room not generated");
            Debug.Assert(map.floor == 1, "Incorrect Floor");
            Debug.Assert(testRoom.type == RoomType.Entry, "Starting room incorrect type");

            int roomCheck = 0;
            foreach (int[] row in map.roomGrid)
            {
                foreach (int i in row)
                {
                    if (i > 0) roomCheck++;
                }
            }
            Debug.Assert(roomCheck == 7, "Incorrect no. of rooms");

            Console.ReadLine();
        }

        private void TestCombat()
        {
            Creature enemy = new Slime(10);
            Debug.Assert(enemy != null);
            Player player = new Player("test");
            Debug.Assert(player!= null);
            player.Attack(enemy);
            Debug.Assert(enemy.health < enemy.maxHealth, "Enemy does not take damage from attack");
            int difference = enemy.maxHealth - enemy.health;
            enemy = new Slime(50);
            player.Attack(enemy);
            Debug.Assert(enemy.health < enemy.maxHealth, "Slime Resistance not capping at 75%");
            int difference2 = enemy.maxHealth - enemy.health;
            Debug.Assert(difference < difference2, "Slime Resistance not scaling up");
            Sword weapon = new Sword(5);
            weapon.Use(player);
            enemy = new Slime(50);
            player.Attack(enemy);
            difference = enemy.maxHealth - enemy.health;
            Debug.Assert(difference > difference2, "Player damage not scaling with weapon");
        }


    }
}
