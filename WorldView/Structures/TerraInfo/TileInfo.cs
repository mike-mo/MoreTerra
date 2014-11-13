using System;
using System.Drawing;

namespace MoreTerra.Structures.TerraInfo
{
	public class TileInfo
	{
		public String name;
		public String colorName;
		public String markerName;

		public Color color;
        public Color officialColor;

		public Int32 tileImage;

        public Boolean important;

		public Boolean Important
		{
			get
			{
                return important;
			}
		}
	}
}
