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
            get { return Floor; } 
            private set { Floor = value; }
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

            floor = 1;
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
                int chance = Program.rnd.Next(0, 10);
                if (chance < 4)
                {
                    Rooms.Add(RoomType.hall);
                }
                else if (chance < 9 || Rooms.Contains(RoomType.treasure))
                {
                    Rooms.Add(RoomType.dungeon);
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
                List<Directions> availableDirections = new List<Directions>();
                while (true)
                {
                    RoomConnection = Program.rnd.Next(0, availableRooms.Count);
                    if (RoomLocations[RoomConnection][0] <= 1 && roomGrid[availableRooms[RoomConnection][0] + 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.South);
                    if (RoomLocations[RoomConnection][0] >= 1 && roomGrid[availableRooms[RoomConnection][0] - 1][availableRooms[RoomConnection][1]] == 0) availableDirections.Add(Directions.North);
                    if (RoomLocations[RoomConnection][1] <= 1 && roomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] +1] == 0) availableDirections.Add(Directions.East);
                    if (RoomLocations[RoomConnection][1] >= 1 && roomGrid[availableRooms[RoomConnection][0]][availableRooms[RoomConnection][1] - 1] == 0) availableDirections.Add(Directions.West);
                    if (availableDirections.Count > 0) break;
                    availableRooms.RemoveAt(RoomConnection);
                }
                int chance = Program.rnd.Next(0, availableDirections.Count);
                Directions Direction = availableDirections[chance];
                int x = 2;
                int y = 2;

                switch (Direction)
                {
                    case Directions.North:
                        y = availableRooms[RoomConnection][0] - 1;
                        x = availableRooms[RoomConnection][1];
                        break;
                    case Directions.South:
                        y = availableRooms[RoomConnection][0] + 1;
                        x = availableRooms[RoomConnection][1];
                        break;
                    case Directions.East:
                        y = availableRooms[RoomConnection][0];
                        x = availableRooms[RoomConnection][1] + 1;
                        break;
                    case Directions.West:
                        y = availableRooms[RoomConnection][0];
                        x = availableRooms[RoomConnection][1] - 1;
                        break;
                }
                int[] location = new int[2];
                location[0] = y;
                location[1] = x;
                RoomLocations.Add(location);

                switch (room)
                {
                    case RoomType.hall:
                        roomGrid[y][x] = 2;
                        break;

                    case RoomType.dungeon:
                        roomGrid[y][x] = 3;
                        break;

                    case RoomType.treasure:
                        roomGrid[y][x] = 4;
                        break;
                }

            }

            while (true)
            {
                // places the final room
                // tries to place it in the bottom right corner of the grid
                // checks available spaces and places it in the nearest space.
                if (RoomGrid[2][0] == 0 && (RoomGrid[1][0] > 0 || RoomGrid[2][1] > 0)) RoomGrid[2][0] = 5;
                else if (RoomGrid[2][1] == 0) RoomGrid[2][1] = 5;
                else if (RoomGrid[1][0] == 0) RoomGrid[1][0] = 5;
                else if (RoomGrid[2][2] == 0) RoomGrid[2][2] = 5;
                else RoomGrid[0][0] = 5; // if none of the above spaces are available this space will be available due to it having 6 rooms in a 3x3 grid already.
                break;
            }

        }

    }
}
