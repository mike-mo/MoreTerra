using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Structures.TerraInfo
{
	class CraftingSpotInfo
	{
		public String name;
		public String type;
		public List<String> tileSets;

		public CraftingSpotInfo()
		{
			name = String.Empty;
			type = String.Empty;
			tileSets = new List<String>();
		}
	}
}
