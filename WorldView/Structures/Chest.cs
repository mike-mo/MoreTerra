using System;
using System.Collections.Generic;
using System.Drawing;

namespace MoreTerra.Structures
{
	public enum ChestType
	{
		Unknown = -1,
		Chest = 0,
		GoldChest,
		LockedGoldChest,
		ShadowChest,
		LockedShadowChest,
        Barrel = 5,
        TrashCan,
        EbonwoodChest,
        RichMahoganyChest,
        PearlwoodChest,
        IvyChest = 10,
        IceChest,
        LivingWoodChest,
        SkywareChest,
        ShadewoodChest,
        WebCoveredChest = 15,
        LihzahrdChest,
        WaterChest,
        JungleChest,
        CorruptionChest,
        CrimsonChest = 20,
        HallowedChest,
        FrozenChest,
        LockedJungleChest,
        LockedCorruptionChest,
        LockedCrimsonChest = 25,
        LockedHallowedChest,
        LockedFrozenChest = 27
	}


    public class Chest
    {
		private Boolean activeChest;
        private int chestId;
        private Point coordinates;
        private List<Item> items;
		private ChestType chestType;

		#region Constructors
        public Chest(int chestId, Point coordinates)
        {
			this.activeChest = true;
            this.chestId = chestId;
            this.coordinates = coordinates;
            this.items = new List<Item>();
			this.chestType = ChestType.Unknown;
        }

		public Chest()
		{
			this.chestId = 0;
			this.items = new List<Item>();
			this.chestType = ChestType.Unknown;
		}
		#endregion

        public void AddItem(Item item)
        {
            this.items.Add(item);
        }

		#region GetSet Functions
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
			set
			{
				this.chestId = value;
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

		public ChestType Type
		{
			get
			{
				return this.chestType;
			}
			set
			{
				this.chestType = value;
			}
		}
		#endregion
    }
}
