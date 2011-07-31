namespace MoreTerra.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Basic Item Representation
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        private string name;

        /// <summary>
        /// The count of the items
        /// </summary>
        private int count;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">name of the item</param>
        /// <param name="count">number of the items</param>
        public Item(string name, int count)
        {
            this.name = name;
            this.count = count;
        }

		public Item()
		{
			this.name = null;
			this.count = 0;
		}

        /// <summary>
        /// Property returns name of item
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
			set
			{
				this.name = value;
			}
        }

        /// <summary>
        /// Property returns number of items currently stacked
        /// </summary>
        public int Count
        {
            get
            {
                return this.count;
            }
			set
			{
				this.count = value;
			}
        }

        public override string ToString()
        {
            if(count == 1)
            {
                return this.name;
            }
            else
            {
                return string.Format("{0}, Count: {1}", this.name, this.count);
            }
            
        }
    
    }
}
