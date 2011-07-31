namespace MoreTerra.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public class Chest
    {
		private Boolean activeChest;
        private int chestId;
        private Point coordinates;
        private List<Item> items;

        public Chest(int chestId, Point coordinates)
        {
			this.activeChest = true;
            this.chestId = chestId;
            this.coordinates = coordinates;
            this.items = new List<Item>();
        }

		public Chest()
		{
			this.chestId = 0;
		}

        public void AddItem(Item item)
        {
            this.items.Add(item);
        }

        public Point Coordinates
        {
            get
            {
                return this.coordinates;
            }
			set
			{
				this.coordinates = value;
			}
        }

        public List<Item> Items
        {
            get
            {
                return this.items;
            }
			set
			{
				this.items = value;
			}
        }

        public int ChestId
        {
            get
            {
                return this.chestId;
            }
        }

		public Boolean Active
		{
			get
			{
				return this.activeChest;
			}
			set
			{
				this.activeChest = value;
			}
		}
    }
}
