using System;

namespace MoreTerra.Structures
{
	public class Tile
	{
		private Byte flags;
		private Byte tileType;
		private PointInt16 tileFrame;
		private Byte wallType;
		private Byte liquidLevel;

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
		#endregion

		#region GetSet Functions
		public Boolean Active
		{
			get
			{
				return (flags & 0x01) != 0;
			}
			set
			{
				flags = (Byte) ((flags & 0xFE) + (value ? 0x01 : 0x00));
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
		
		public Boolean Lighted
		{
			get
			{
				return (flags & 0x02) != 0;
			}
			set
			{
				flags = (Byte)((flags & 0xFD) + (value ? 0x02 : 0x00));
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
				flags = (Byte)((flags & 0xFB) + (value ? 0x04 : 0x00));
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

		public Boolean Liquid
		{
			get
			{
				return (flags & 0x08) != 0;
			}
			set
			{
				flags = (Byte)((flags & 0xF7) + (value ? 0x08 : 0x00));
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
				flags = (Byte)((flags & 0xEF) + (value ? 0x10 : 0x00));
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
				flags = (Byte)((flags & 0xDF) + (value ? 0x20 : 0x00));
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

				if (Liquid)
					size += 2;


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

			if (Lighted == true)
				val = "01 ";
			else
				val = "00 ";

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

			if (Liquid == true)
			{
				val = String.Format("01 {0:X2} {1:X2}", liquidLevel, Lava ? 1 : 0);
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
