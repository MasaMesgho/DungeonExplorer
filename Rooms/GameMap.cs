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

    public class GameMap
    {
        private int[] PlayerLocation = new int[2];
        public int[] playerLocation
        {
            get { return PlayerLocation; }
            private set { PlayerLocation = value; }
        }

        private List<int[]> visited = new List<int[]>();

        private int Floor;
        public int floor
        {
            get; private set;
        }

        private List<RoomType> Rooms = new List<RoomType>();

        private List<int[]> RoomGrid = new List<int[]>();
        public List<int[]> roomGrid
        {
            get { return RoomGrid; }
            private set { RoomGrid = value; }
        }

        public GameMap()
        {

            Floor = 1;
            GenerateRooms();
            CreateConnections();
            PlayerLocation[0] = 0;
            PlayerLocation[1] = 2;
            visited.Add(playerLocation);
        }

        /// <summary>
        /// Clears the Floor and rebuilds it
        /// </summary>
        public Room NewFloor()
        {
            Floor = Floor + 1;
            Rooms.Clear();
            GenerateRooms();
            CreateConnections();
            PlayerLocation[0] = 0;
            PlayerLocation[1] = 2;
            visited.Add(playerLocation);
            return new EntryRoom();
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
                int[] tempInts = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    tempInts[j] = 0;
                }
                RoomGrid.Add(tempInts);
            }
            roomGrid[0][2] = 1;
            int[] newLocation = new int[2];
            newLocation[0] = 0;
            newLocation[1] = 2;
            RoomLocations.Add(newLocation);
            foreach (RoomType room in Rooms)
            {
                int RoomConnection;
                List<int[]> availableRooms = RoomLocations;
                while (true)
                {
                    RoomConnection = Program.rnd.Next(0, availableRooms.Count - 1);
                    List<Directions> availableDirections = new List<Directions>();
                    if (RoomLocations[RoomConnection][0] < 1 && roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.North);
                    if (RoomLocations[RoomConnection][0] >= 1 && roomGrid[availableRooms[RoomConnection][0] - 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.South);
                    if (RoomLocations[RoomConnection][1] < 1 && roomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] +1] == 0) availableDirections.Add(Directions.East);
                    if (RoomLocations[RoomConnection][1] >= 1 && roomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] - 1] == 0) availableDirections.Add(Directions.West);
                    if (availableDirections.Count > 0) break;
                    availableRooms.RemoveAt(RoomConnection);
                }

                roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] = 1;
                switch (room)
                {
                    case RoomType.hall:
                        roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] = 2;
                        break;

                    case RoomType.treasure:
                        roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] = 3;
                        break;

                    case RoomType.dungeon:
                        roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] = 4;
                        break;
                }

            }
        }

    }
}
