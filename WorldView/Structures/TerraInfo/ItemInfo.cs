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
		public Int32 id;
		public Int32 imageId;
//		private Sprite tex;
		public Int32 stackSize;

		public ItemInfo()
		{
			isCustom = false;
//			tex = null;
		}

/*		public Sprite Tex
		{
			get
			{
				return tex;
			}
			set
			{
				tex = value;
			}
		}*/
	}
}
