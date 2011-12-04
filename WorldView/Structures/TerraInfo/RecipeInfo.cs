using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Structures.TerraInfo
{
	public class RecipeInfo
	{
		public String name;
		public String craftingSpot;
		public Int32 numberMade;
		public Dictionary<String, Int32> materials;

		public RecipeInfo()
		{
			materials = new Dictionary<String, Int32>();
		}
	}
}
