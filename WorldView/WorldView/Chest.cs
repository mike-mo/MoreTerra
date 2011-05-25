namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public class Chest
    {
        private int chestId;
        private Point coordinates;
        private List<Item> items;

        public Chest(int chestId, Point coordinates)
        {
            this.chestId = chestId;
            this.coordinates = coordinates;
            this.items = new List<Item>();
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
        }

        public List<Item> Items
        {
            get
            {
                return this.items;
            }
        }

        public int ChestId
        {
            get
            {
                return this.chestId;
            }
        }
    }
}
