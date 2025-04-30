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
            TestItems();
            Console.Clear();
        }

        /// <summary>
        /// runs tests on the map and room classes to make sure they generate correctly
        /// </summary>
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

        }

        /// <summary>
        /// runs combat tests to make sure everything scales correctly
        /// </summary>
        private void TestCombat()
        {
            // makes sure player and enemies can spawn
            Creature enemy = new Slime(10);
            Debug.Assert(enemy != null, "Enemy not spawning");
            Player player = new Player("test");
            Debug.Assert(player!= null, "player not spawning");

            // makes sure the player can attack enemies
            player.Attack(enemy);
            Debug.Assert(enemy.health < enemy.maxHealth, "Enemy does not take damage from attack");

            // makes sure the slimes resistance scales correctly and does not exceed the cap.
            int difference = enemy.maxHealth - enemy.health;
            enemy = new Slime(50);
            player.Attack(enemy);
            Debug.Assert(enemy.health < enemy.maxHealth, "Slime Resistance not capping at 75%");
            int difference2 = enemy.maxHealth - enemy.health;
            Debug.Assert(difference > difference2, "Slime Resistance not scaling up");

            // makes sure weapons scale correctly with damage
            Sword weapon = new Sword(5);
            weapon.Use(player);
            enemy = new Slime(50);
            player.Attack(enemy);
            difference = enemy.maxHealth - enemy.health;
            Debug.Assert(difference > difference2, "Player damage not scaling with weapon");
            
            // makes sure enemies can damage the player
            enemy.Attack(player);
            Debug.Assert(player.maxHealth > player.health, "Player not taking damage");
            difference = player.maxHealth - player.health;

            // makes sure enemies damage scales correctly
            player = new Player("test");
            enemy = new Slime(10);
            enemy.Attack(player);
            difference2 = player.maxHealth - player.health;
            Debug.Assert(difference > difference2, "Enemy damage not scaling correctly");

        }
        /// <summary>
        /// tests items and their interactions
        /// </summary>
        private void TestItems()
        {
            // makes sure items can spawn
            Potion potion = new Potion(1);
            Debug.Assert(potion != null, "Potion not spawning");
            Sword sword = new Sword(1);
            Debug.Assert(sword != null, "Sword not spawning");

            // makes sure the potion restores the correct amount health
            Player player = new Player("test");
            player.TakeDamage(20);
            Debug.Assert(player.health < player.maxHealth, "player not taking damage");
            potion.Use(player);
            Debug.Assert(player.health == 95, "Potion not healing the correct amount");

            // makes sure the potions healing sclaes correctly
            potion = new Potion(2);
            player.TakeDamage(20);
            potion.Use(player);
            Debug.Assert(player.health == 95, "Potion healing not scaling correctly");

            // makes sure the sword adds it's damage correctly
            Goblin target = new Goblin(10);
            Debug.Assert(target.health == 60, "goblin health not scaling correctly");

            sword.Use(player);
            player.Attack(target);

            Debug.Assert(target.health == 50, "sword damage not applying to player");

            // makes sure the swords damage scales correctly
            sword = new Sword(2);
            sword.Use(player);
            player.Attack(target);
            Debug.Assert(target.health == 35, "sword damage not scaling correctly");


        }


    }
}
