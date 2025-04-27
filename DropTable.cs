using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public enum TableType
    {
        Room,
        Enemy
    }
    public class DropTable
    {
        // public so that it can be checked for testing purposes
        // private set as i only want it changed within the class
        public List<int> Table
        {
            get;
            private set;
        }
        private int Level;

        public DropTable (TableType type, int level)
        {
            Level = level;
            Table = GenerateTable(type);
        }

        /// <summary>
        /// Generates the Drop Table depending on they type of drop table wanted
        /// </summary>
        /// <param name="type">Enemy or room</param>
        /// <returns>returns the generated table</returns>
        private List<int> GenerateTable(TableType type)
        {
            List<int> table = new List<int>();
            switch (type)
            {
                case TableType.Room:
                    // uses AddRange to add 10 items to the List
                    // each item represents 10% chance
                    // uses enumerable.Repeat to add multiple times, to avoid messy for loops
                    table.AddRange(Enumerable.Repeat(0, 4));
                    table.AddRange(Enumerable.Repeat(1, 5));
                    table.AddRange(Enumerable.Repeat(2, 1));
                    break;
                case TableType.Enemy:
                    // enemies have a 10% higher chance of having a sword and less chance of getting a health potion
                    table.AddRange(Enumerable.Repeat(0, 4));
                    table.AddRange(Enumerable.Repeat(1, 4));
                    table.AddRange(Enumerable.Repeat(2, 2));
                    break;
                default:
                    break;
            }
            return table;
        }
        /// <summary>
        /// gets an item from the drop table and returns it, or null if no item.
        /// </summary>
        /// <returns>the Item or null</returns>
        public Item GetDrop()
        {
            // Uses rnd class to generate a random number between 0 and 9
            // uses the result to generate a random item and returns it.
            int result = Program.rnd.Next(0, 9);
            // returns an item or null if no item is generated
            switch (result)
            {
                case 0:
                    return null;
                case 1:
                    return new Potion(Level);
                case 2:
                    return new Sword(Level);
                default:
                    Console.WriteLine("Something Went Wrong with generating an item.");
                    return null;
            }

        }

    }
}
