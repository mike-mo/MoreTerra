using System;

namespace MoreTerra.Structures
{
	public class Tile
	{
		// Total flags
		// Active 0x01
		// Wall 0x04
		// Honey 0x08
		// Lava 0x10
		// Important 0x20
		// Wire 0x40
		// Wire2 0x80
		// Wire3 0x100
		// Halftile 0x200
		// Slope 0x400
		// Actuator 0x800
		// inActive 0x1000
		private Int16 flags;
		private Byte tileType;
		private PointInt16 tileFrame;
		private Byte tileColor;
		private Byte wallType;
		private Byte wallColor;
		private Byte liquidLevel;
        private Byte slope;

		#region Constructors

		public Tile()
		{

		}

		public Tile(Tile copy)
		{
			flags = copy.flags;
			tileType = copy.tileType;
			tileFrame = copy.tileFrame;
			wallType = copy.wallType;
			liquidLevel = copy.liquidLevel;
		}

		public void Reset()
		{
			flags = 0;
			tileType = 0;
			tileFrame.X = 0;
			tileFrame.Y = 0;
			tileColor = 0;
			wallType = 0;
			wallColor = 0;
			liquidLevel = 0;
		}
		#endregion

		#region GetSet Functions
        public Byte WallColor
		{
			get
			{
                return this.wallColor;
			}
			set
			{
                this.wallColor = value;
			}
		}

        public Byte TileColor
        {
            get
            {
                return this.tileColor;
            }
            set
            {
                this.tileColor = value;
            }
        }
		public Boolean Active
		{
			get
			{
				return (flags & 0x01) != 0;
			}
			set
			{
				flags = (Int16) ((flags & 0xFFFE) + (value ? 0x01 : 0x00));
			}
		}

		public Byte TileType
		{
			get
			{
				return tileType;
			}
			set
			{
				tileType = value;
			}
		}
		
		public PointInt16 Frame
		{
			get
			{
				return tileFrame;
			}
			set
			{
				tileFrame = value;
			}
		}

		public Boolean Wall
		{
			get
			{
				return (flags & 0x04) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFFFB) + (value ? 0x04 : 0x00));
			}
		}

		public Byte WallType
		{
			get {
				return wallType;
			}
			set {
				wallType = value;
			}
		}

		public Boolean Honey
		{
			get
			{
				return (flags & 0x08) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFFF7) + (value ? 0x08 : 0x00));
			}
		}

		public Byte LiquidLevel
		{
			get
			{
				return liquidLevel;
			}
			set
			{
				liquidLevel = value;
			}
		}

		public Boolean Lava
		{
			get
			{
				return (flags & 0x10) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFFEF) + (value ? 0x10 : 0x00));
			}
		}

		public Boolean Important
		{
			get
			{
				return (flags & 0x20) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFFDF) + (value ? 0x20 : 0x00));
			}
		}

		public Boolean RedWire
		{
			get
			{
				return (flags & 0x40) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFFBF) + (value ? 0x40 : 0x00));
			}
		}

		public Boolean BlueWire
		{
			get
			{
				return (flags & 0x80) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFF7F) + (value ? 0x80 : 0x00));
			}
		}

		public Boolean GreenWire
		{
			get
			{
				return (flags & 0x100) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFEFF) + (value ? 0x100 : 0x00));
			}
		}

		public Boolean Halftile
		{
			get
			{
				return (flags & 0x200) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xFDFF) + (value ? 0x200 : 0x00));
			}
		}

		public Byte Slope
		{
			get
			{
                return slope;
			}
			set
			{
                this.slope = value;
			}
		}

		public Boolean Actuator
		{
			get
			{
				return (flags & 0x800) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xF7FF) + (value ? 0x800 : 0x00));
			}
		}

		public Boolean Inactive
		{
			get
			{
				return (flags & 0x1000) != 0;
			}
			set
			{
				flags = (Int16)((flags & 0xEFFF) + (value ? 0x1000 : 0x00));
			}
		}

		public Int32 Size
		{
			get
			{

				Int32 size;

				size = 4;
				if (Active == true)
				{
					size++;
					if (Important)
						size += 4;
				}

				if (Wall)
					size++;

				if (LiquidLevel > 0)
					size += 3;


				return size;
			}
		}
		#endregion

		#region Overrides
		public override String  ToString()
		{
			String ret = "";
			String val;

			if (Active == true)
			{
				if (Important == true)
				{
					val = String.Format("01 {0:X2} {1:X2} {2:X2} {3:X2} {4:X2} ", tileType,
						(tileFrame.X & 0xFF), ((tileFrame.X & 0xFF00) / 256),
						(tileFrame.Y & 0xFF), ((tileFrame.Y & 0xFF00) / 256));
				}
				else
				{
					val = String.Format("01 {0:X2} ", tileType);
				}
			}
			else
			{
				val = "00 ";
			}

			ret += val;

			if (Wall == true)
			{
				val = String.Format("01 {0:X2} ", wallType);
			}
			else
			{
				val = "00 ";
			}

			ret += val;

			if (LiquidLevel > 0)
			{
				val = String.Format("01 {0:X2} {1:X2} {2:X2}", liquidLevel, Lava ? 1 : 0, Honey ? 1 : 0);
			}
			else
			{
				val = "00";
			}

			ret += val;

 			return ret;
		}

		public override bool Equals(object obj)
		{
			Tile secondTile = (Tile) obj;

			if (Size != secondTile.Size)
				return false;

			if (Size.ToString() != secondTile.ToString())
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
