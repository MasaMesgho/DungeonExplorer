using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public enum Directions
    {
        North,
        West,
        East,
        South
    }

    internal class GameMap
    {
        public int Floor
        {
            get; private set;
        }

        private List<RoomType> Rooms = new List<RoomType>();

        List<int[]> RoomGrid = new List<int[]>();

        public GameMap()
        {
            Floor = 1;
            GenerateRooms();
            CreateConnections();
        }

        /// <summary>
        /// Clears the Floor and rebuilds it
        /// </summary>
        public void NewFloor()
        {
            Floor = Floor + 1;
            Rooms.Clear();
            GenerateRooms();
            CreateConnections();
        }

        /// <summary>
        /// Generates the Rooms for the Map
        /// </summary>
        private void GenerateRooms()
        {
            for (int i = 0; i < 5; i++)
            {
                int chance = Program.rnd.Next(0, 9);
                if (chance < 4)
                {
                    Rooms.Add(RoomType.dungeon);
                }
                else if (chance < 9 || Rooms.Contains(RoomType.treasure))
                {
                    Rooms.Add(RoomType.hall);
                }
                else
                {
                    Rooms.Add(RoomType.treasure);
                }
            }
        }

        private void CreateConnections()
        {
            List<int[]> RoomLocations = new List<int[]>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; i++)
                {
                    RoomGrid[i][j] = 0;
                }
            }
            RoomGrid[0][2] = 1;
            int[] newLocation = new int[2];
            newLocation[0] = 0;
            newLocation[1] = 2;
            RoomLocations.Add(newLocation);
            foreach (RoomType room in Rooms)
            {
                List<int[]> availableRooms = RoomLocations;
                while (true)
                {
                    int RoomConnection = Program.rnd.Next(0, availableRooms.Count - 1);
                    List<Directions> availableDirections = new List<Directions>();
                    if (RoomLocations[RoomConnection][0] < 1 && RoomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.North);
                    if (RoomLocations[RoomConnection][0] >= 1 && RoomGrid[availableRooms[RoomConnection][0] - 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.South);
                    if (RoomLocations[RoomConnection][1] < 1 && RoomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] +1] == 0) availableDirections.Add(Directions.East);
                    if (RoomLocations[RoomConnection][1] >= 1 && RoomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] - 1] == 0) availableDirections.Add(Directions.West);
                    if (availableDirections.Count > 0) break;
                    availableRooms.RemoveAt(RoomConnection);
                }



            }
        }

    }
}
