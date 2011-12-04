using System;
using System.Drawing;

namespace MoreTerra.Structures.TerraInfo
{
	public class ColorInfo
	{
		public String name;
		public Color color;
		public Boolean isCustom;

		public ColorInfo()
		{
			name = String.Empty;
			color = Color.Black;
			isCustom = false;
		}
	}
}
