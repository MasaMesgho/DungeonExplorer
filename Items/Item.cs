using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DungeonExplorer
{

    public abstract class Item : IUsable
    {
        // all items will need an ID, Name and Description
        // these are all protected values as I do not want them changed outside of the class
        // they have a public version so that I can retrieve the values outside of the class
        protected int ID;
        public int iD
        {
            get { return ID; }
            protected set { ID = value; }
        }

        protected string Name;
        public string name
        {
            get { return Name; }
            protected set { Name = value; }
        }
        protected string Description;
        public string description
        {
            get { return Description; }
            protected set { Description = value; }
        }
        // constuctors will be unique to the type of item so an empty constructor is shown here
        public Item() { }
        // use method to be compliant with interface.
        public abstract void Use(Player player);


    }
}
