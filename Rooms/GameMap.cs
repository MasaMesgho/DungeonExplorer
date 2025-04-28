using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // directions to make passing direction variables easier
    public enum Directions
    {
        North,
        West,
        East,
        South
    }

    // This class defines a game map, including features for generating floors and getting the current room
    // as well as movement between rooms based on the player location
    public class GameMap
    {
        // initial variables are defined here and populated elsewhere
        // private is mostly used for encapsulation but publics are available for attributes to be tested/ used elsewhere

        // stores the layers location within the map, uses y and x locations
        // private only as this only needs to be accessed within the class
        private int[] PlayerLocation = new int[2];

        // stores visited locations
        // private as this only needs to be accessed within the class
        private List<int[]> visited = new List<int[]>();

        // stores which floor the player is currently on
        // has a public get as the current floor is needed outside the class
        private int Floor;
        public int floor
        {
            get { return Floor; } 
            private set { Floor = value; }
        }

        // holds a list of 5 rooms which are generated within the class and used 
        private List<RoomType> Rooms = new List<RoomType>();

        //holds a 3x3 grid for the rooms, uses a private for use within the class
        //has a public get as it is used in the testing class to ensure floors are generated successfully
        private List<int[]> RoomGrid = new List<int[]>();
        public List<int[]> roomGrid
        {
            get { return RoomGrid; }
            private set { RoomGrid = value; }
        }

        // opens a new map, newfloor needs to be called so that the initial room can be provided as a constructor is not the place for returning objects.
        public GameMap()
        {
            floor = 0;
        }

        /// <summary>
        /// Clears the Floor and rebuilds it using inbuilt methods
        /// </summary>
        public Room NewFloor()
        {
            Floor = Floor + 1;
            Rooms.Clear();
            visited.Clear();
            GenerateRooms();
            CreateConnections();
            // sets the player location the the starting room
            PlayerLocation[0] = 0;
            PlayerLocation[1] = 2;
            // adds the starting room to visited
            visited.Add(PlayerLocation);

            // stores the available directions from the starting room
            List<Directions> availableDirections = new List<Directions>();

            // sets the entry direction from north as rooms need an entry direction.
            Directions entryDirection = Directions.North;

            // gets the available directions from the starting room and stores them
            if (RoomGrid[0][1] > 1) availableDirections.Add(Directions.West);
            if (RoomGrid[1][2] > 1) availableDirections.Add(Directions.South);
            // returns the entry room
            return new EntryRoom(entryDirection,availableDirections, false);
        }
        /// <summary>
        /// Moves the player in the specified direction
        /// </summary>
        /// <param name="Direction"> The direction the player is moving </param>
        /// <returns>the room the player has moved too </returns>
        public Room Move(Directions Direction)
        {
            // based on the direction, moves the players location
            switch (Direction)
            {
                case Directions.North:
                    PlayerLocation[0]--;
                    break;
                case Directions.South:
                    PlayerLocation[0]++;
                    break;
                case Directions.West:
                    PlayerLocation[1]--;
                    break;
                case Directions.East:
                    PlayerLocation[1]++;
                    break;
            }
            // here to make sure the player location is valid
            Debug.Assert(PlayerLocation[0] >=0 && PlayerLocation[0] <=2, "Y axis out of range.");
            Debug.Assert(PlayerLocation[1] >= 0 && PlayerLocation[1] <= 2, "X axis out of range.");
            // here so that there is a room at that location
            Debug.Assert(RoomGrid[PlayerLocation[0]][PlayerLocation[1]] != 0, "No Room in that location");

            // gets the room ID to be generated from the players location
            int newRoom = RoomGrid[PlayerLocation[0]][PlayerLocation[1]];

            // Entry Direction variable to be given to the new room
            Directions entryDirection = default;
            // flips the direction that the user moved to the room from
            switch (Direction)
            {
                case Directions.North:
                    entryDirection = Directions.South;
                    break;
                case Directions.South:
                    entryDirection = Directions.North;
                    break;
                case Directions.West:
                    entryDirection = Directions.East;
                    break;
                case Directions.East:
                    entryDirection = Directions.West;
                    break;
            }

            // holds a list of room connections based on the new player location
            List<Directions> availableDirections = new List<Directions>();


            // checks the adjacent spaces to the players location and adds any rooms found as a available direction
            if (PlayerLocation[0] > 0 && RoomGrid[PlayerLocation[0] - 1][PlayerLocation[1]] != 0) availableDirections.Add(Directions.North);
            if (PlayerLocation[0] < 2 && RoomGrid[PlayerLocation[0] + 1][PlayerLocation[1]] != 0) availableDirections.Add(Directions.South);
            if (PlayerLocation[1] < 2 && RoomGrid[PlayerLocation[0]][PlayerLocation[1] + 1] != 0) availableDirections.Add(Directions.East);
            if (PlayerLocation[2] > 0 && RoomGrid[PlayerLocation[0]][PlayerLocation[1] - 1] != 0) availableDirections.Add(Directions.West);
            // sees if the user has been in this location before
            bool visitedCheck = (visited.Contains(PlayerLocation));

            // if the user has not been to this location before, adds 
            if (!visitedCheck) visited.Add(PlayerLocation);

            // generates a new room and returns it
            switch (newRoom)
            {
                case 1:
                    return new EntryRoom(entryDirection, availableDirections, visitedCheck);
                case 2:
                    return new Hall(entryDirection, availableDirections, visitedCheck);
                case 3:
                    return new Dungeon(entryDirection, availableDirections, visitedCheck);
                case 4:
                    return new TreasureRoom(entryDirection, availableDirections, visitedCheck);
                case 5:
                    return new FinalRoom(entryDirection, availableDirections, visitedCheck);
                default:
                    return default;
            }
        }

        /// <summary>
        /// Generates 5 Rooms for the Map to be used in creating connections
        /// </summary>
        private void GenerateRooms()
        {
            // runs 5 times
            for (int i = 0; i < 5; i++)
            {
                int chance = Program.rnd.Next(0, 10);
                // adds a random room
                // a max of 1 treasure room can be generated
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

        /// <summary>
        /// Populates a 3x3 grid for the room locations, then assigns the rooms to locations on the grid connected to another room.
        /// </summary>
        private void CreateConnections()
        {
            // creates a list of the room locations to be used when connecting rooms.
            List<int[]> RoomLocations = new List<int[]>();
            // generates a 3x3 grid of 0
            for (int i = 0; i < 3; i++)
            {
                int[] tempInts = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    tempInts[j] = 0;
                }
                RoomGrid.Add(tempInts);
            }
            // sets the top right of the grid to be the starting room ID 1 and adds it to the room locations
            roomGrid[0][2] = 1;
            int[] newLocation = new int[2];
            newLocation[0] = 0;
            newLocation[1] = 2;
            RoomLocations.Add(newLocation);

            // adds each room generated by GenerateRooms to the 3x3 grid
            // adds them next to already added rooms
            foreach (RoomType room in Rooms)
            {
                // int to store which room is selected as the one the new room will be placed next to.
                int RoomConnection;
                // new list for storing rooms that can be connected to
                // this is different as some rooms need to be removed as they cannot be connected to due to no free space.
                List<int[]> availableRooms = RoomLocations;
                // list to store the available directions the connection could be in.
                List<Directions> availableDirections = new List<Directions>();
                // while loop to make sure the room has a valid space before closing the loop.
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
