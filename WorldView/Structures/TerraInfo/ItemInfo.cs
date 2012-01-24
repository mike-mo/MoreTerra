using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Structures.TerraInfo
{
	public class ItemInfo
	{
		public Boolean isCustom;
		public String name;
		public String type;
		public String recolor;
		public String droppedBy;
		public String foundIn;
		public Int32 netId;
		public Int32 imageId;
		public Int32 stackSize;

		public ItemInfo()
		{
			isCustom = false;
		}
	}
}
