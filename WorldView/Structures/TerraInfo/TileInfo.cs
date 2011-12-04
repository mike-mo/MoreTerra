using System;
using System.Drawing;

namespace MoreTerra.Structures.TerraInfo
{
	public class TileInfo
	{
		public String name;
		public String autoGenType;
		public String blendWith;
		public String colorName;
		public String markerName;

		public Color color;

		public Int32 tileImage;

		public Boolean Important
		{
			get
			{
				if (autoGenType == String.Empty)
					return true;
				return false;
			}
		}
	}
}
