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
        final
    }

    public abstract class Room
    {

        // this class contains all information and actions for rooms
        // needed variables are below with a rng generator (rnd)
        private string description;
        private RoomType roomType;

        // room inventory can be gotten from outside the class
        // so has a protected set and a public get
        protected List<Item> RoomInventory;
        public List<Item> roomInventory
        { 
            get { return RoomInventory; }
            protected set { RoomInventory = value; }
        }
        protected DropTable dropTable;
        /// <summary>
        /// Generates the Room with random attributes if not specified.
        /// </summary>
        /// <param name="roomType">
        /// The Type of room if specifying, leave blank if not specifying.
        /// </param>
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
            roomInventory.Add(item);
        }
        /// <summary>
        /// removes an item from the rooms inventory
        /// </summary>
        /// <param name="item"> the item being removed </param>
        public void RemoveItem(Item item)
        {
            roomInventory.Remove(item);
        }

    }
}