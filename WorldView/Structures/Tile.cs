using System;

namespace MoreTerra.Structures
{
	public class Tile
	{
		private Boolean isActive;
		private Byte tileType;
		private PointInt16 tileFrame;
		private Boolean isLighted;
		private Boolean isWall;
		private Byte wallType;
		private Boolean isLiquid;
		private Byte liquidLevel;
		private Boolean isLava;

		private Boolean isImportant;
		private Int32 tileSize;
		private Int64 filePos;

		#region Constructors
		public Tile()
		{

		}

		public Tile(Tile copy)
		{
			isActive = copy.isActive;
			tileType = copy.tileType;
			tileFrame = copy.tileFrame;
			isLighted = copy.isLighted;
			isWall = copy.isWall;
			wallType = copy.wallType;
			isLiquid = copy.isLiquid;
			liquidLevel = copy.liquidLevel;
			isLava = copy.isLava;

			isImportant = copy.isImportant;
			tileSize = copy.tileSize;
		}
		#endregion

		public void calcSize()
		{
			Int32 size;

			size = 4;
			if (isActive == true)
			{
				size++;
				if (isImportant)
					size += 4;
			}

			if (isWall)
				size++;

			if (isLiquid)
				size += 2;

			tileSize = size;
		}

		#region GetSet Functions
		public Boolean Active
		{
			get
			{
				return isActive;
			}
			set
			{
				isActive = value;
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
				return isLighted;
			}
			set
			{
				isLighted = value;
			}
		}

		public Boolean Wall
		{
			get
			{
				return isWall;
			}
			set
			{
				isWall = value;
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
				return isLiquid;
			}
			set
			{
				isLiquid = value;
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
				return isLava;
			}
			set
			{
				isLava = value;
			}
		}

		public Boolean Important
		{
			get
			{
				return isImportant;
			}
			set
			{
				isImportant = value;
			}
		}

		public Int64 FilePos
		{
			get
			{
				return filePos;
			}
			set
			{
				filePos = value;
			}
		}

		public Int32 Size
		{
			get
			{
				return tileSize;
			}
		}

		#endregion

		#region Overrides
		public override String  ToString()
		{
			String ret = "";
			String val;

			if (isActive == true)
			{
				if (isImportant == true)
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

			if (isLighted == true)
				val = "01 ";
			else
				val = "00 ";

			ret += val;

			if (isWall == true)
			{
				val = String.Format("01 {0:X2} ", wallType);
			}
			else
			{
				val = "00 ";
			}

			ret += val;

			if (isLiquid == true)
			{
				val = String.Format("01 {0:X2} {1:X2}", liquidLevel, isLava ? 1 : 0);
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

			if (tileSize != secondTile.tileSize)
				return false;

			if (tileSize.ToString() != secondTile.ToString())
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
