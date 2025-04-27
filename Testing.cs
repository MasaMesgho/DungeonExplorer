using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Testing
    {
        /// <summary>
        /// makes sure all the rooms public atributes are filled.
        /// </summary>
        /// <param name="testRoom">
        /// the room to be tested
        /// </param>
        public static void TestRoom(Room testRoom)
        {
            // makes sure all room attributes are filled.
            // does not check inventory as this can be empty
            Debug.Assert(testRoom != null, "No Room found");
            Debug.Assert(testRoom.getDescription() != null, "Room Has no description");
        }

        /// <summary>
        /// Makes sure all public attributes for player work correctly.
        /// </summary>
        /// <param name="testPlayer">
        /// the player to be tested.
        /// </param>
        public static void TestPlayer(Player testPlayer)
        {
            // makes sure all player public attributes are filled
            Debug.Assert(testPlayer != null, "No player found");
            Debug.Assert(testPlayer.name != null, "Player has no name");
            Debug.Assert(testPlayer.health != 0, "Player has no health");
            Debug.Assert(testPlayer.InventoryContents() != null, "Player Contents method failed.");
                   
        }

        public static void TestGame(Game game)
        {
            // makes sure all room attributes are filled
            Debug.Assert(game != null, "Game not found");
            Debug.Assert(game.player != null, "Game has no player");
            Debug.Assert(game.currentRoom != null, "Game has no Room");
        }

    }
}
