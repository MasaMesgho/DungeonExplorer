using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace DungeonExplorer
{
    // stores room types for use outside of functions
    public enum RoomType
    {
        dungeon,
        hall,
        treasure,
        final,
        Entry
    }

    public enum ExitDirection
    {
        left,
        right,
        forward,
        down,
        None
    }

    public abstract class Room
    {

        // this class contains all information and actions for rooms
        // needed variables are below with a rng generator (rnd)
        protected string Description;
        public string description;

        protected int Floor;

        public List<ExitDirection> Exits { get; protected set; } = new List<ExitDirection>();

        protected RoomType Type;
        public RoomType type
        {
            get { return Type; }
            protected set { Type = value; }
        }

        public Directions EntryDirection { get; protected set; }

        // room inventory can be gotten from outside the class
        // so has a protected set and a public get
        protected List<Item> RoomInventory = new List<Item>();
        public List<Item> roomInventory
        { 
            get { return RoomInventory; }
            protected set { RoomInventory = value; }
        }
        protected DropTable dropTable;

        public bool EmptyRoom { get; protected set; }

        public Room() { }

        /// <summary>
        /// fills the room with items from the drop table
        /// </summary>
        /// <param name="amount">the amount of chances for a drop </param>
        public void GenerateItems(int amount)
        {
            // tries for an item for each amount given
            for (int i = 0; i < amount; i++)
            {
                // gets a drop from the table, if it is an item, adds it to the inventory
                Item item = dropTable.GetDrop();
                if (item != null) RoomInventory.Add(item);
            }
        }
        /// <summary>
        /// adds a item to the rooms inventory
        /// </summary>
        /// <param name="item"> the item being added </param>
        public void AddItem(Item item)
        {
            if (item!=null) roomInventory.Add(item);
        }
        /// <summary>
        /// removes an item from the rooms inventory
        /// </summary>
        /// <param name="item"> the item being removed </param>
        public void RemoveItem(Item item)
        {
            roomInventory.Remove(item);
        }

        /// <summary>
        /// generates enemies based on room level.
        /// virtual so can be overwritten for final and entry
        /// </summary>
        /// <returns> a list of enemies </returns>
        public virtual List<Creature> EnemyEncounter()
        {
            int chance  = Program.rnd.Next(0, 2);
            int enemyNumber = Program.rnd.Next(0,4);
            List<Creature> creatures = new List<Creature>();
            if (EmptyRoom) return creatures;
            for (int i = 0; i < enemyNumber; i++)
            {
                if (chance == 1) creatures.Add(new Goblin(Floor));
                else creatures.Add(new Slime(Floor));
            }
            return creatures;
        }

        /// <summary>
        /// adds the exits to a room
        /// </summary>
        /// <param name="entryDirection"> the entry direction</param>
        /// <param name="availableDirections"> the available directions from the room </param>
        public void AddExits(List<Directions> availableDirections)
        {
            // uses the entry direction as a reference point
            // adds the exits based on available directions from the entry direction
            switch (EntryDirection)
            {
                case Directions.North:
                    if (availableDirections.Contains(Directions.East)) Exits.Add(ExitDirection.left);
                    if (availableDirections.Contains(Directions.South)) Exits.Add(ExitDirection.forward);
                    if (availableDirections.Contains(Directions.West)) Exits.Add(ExitDirection.right);
                    break;
                case Directions.South:
                    if (availableDirections.Contains(Directions.West)) Exits.Add(ExitDirection.left);
                    if (availableDirections.Contains(Directions.North)) Exits.Add(ExitDirection.forward);
                    if (availableDirections.Contains(Directions.East)) Exits.Add(ExitDirection.right);
                    break;
                case Directions.East:
                    if (availableDirections.Contains(Directions.South)) Exits.Add(ExitDirection.left);
                    if (availableDirections.Contains(Directions.West)) Exits.Add(ExitDirection.forward);
                    if (availableDirections.Contains(Directions.North)) Exits.Add(ExitDirection.right);
                    break;
                case Directions.West:
                    if (availableDirections.Contains(Directions.North)) Exits.Add(ExitDirection.left);
                    if (availableDirections.Contains(Directions.East)) Exits.Add(ExitDirection.forward);
                    if (availableDirections.Contains(Directions.South)) Exits.Add(ExitDirection.right);
                    break;
                default:
                    break;
            }
            if (Exits.Count == 0) Exits.Add(ExitDirection.None);
        }

        /// <summary>
        /// finds the exit direction from the given directions
        /// </summary>
        /// <param name="entryDirection"> the entry direction</param>
        /// <param name="availableDirections"> the available directions from the room </param>
        public Directions GetExitDirection(ExitDirection exit)
        {
            // uses default so that it can be returned
            Directions direction = default;

            // based on the entry direction, converts the exit chosen to a direction
            switch (EntryDirection)
            {
                case Directions.North:
                    if (exit == ExitDirection.left) direction = Directions.East;
                    if (exit == ExitDirection.forward) direction = Directions.South;
                    if (exit == ExitDirection.right) direction = Directions.West;
                    break;
                case Directions.South:
                    if (exit == ExitDirection.left) direction = Directions.West;
                    if (exit == ExitDirection.forward) direction = Directions.North;
                    if (exit == ExitDirection.right) direction = Directions.East;
                    break;
                case Directions.East:
                    if (exit == ExitDirection.left) direction = Directions.South;
                    if (exit == ExitDirection.forward) direction = Directions.West;
                    if (exit == ExitDirection.right) direction = Directions.North;
                    break;
                case Directions.West:
                    if (exit == ExitDirection.left) direction = Directions.North;
                    if (exit == ExitDirection.forward) direction = Directions.East;
                    if (exit == ExitDirection.right) direction = Directions.South;
                    break;
                default:
                    break;
            }
            if (exit == ExitDirection.down) direction = Directions.Down;

            return direction;
        }

    }
}