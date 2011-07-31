using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MoreTerra.Structures
{
	public class Sign
	{
		public Int32 signId;
		public Boolean activeSign;
		public String signText;
		public Point signPosition;

		public Sign()
		{
			signId = -1;
			activeSign = false;
			signText = String.Empty;
			signPosition = new Point(0, 0);
		}

		public Sign(Int32 id, Boolean active, String text, Point pos)
		{
			signId = id;
			activeSign = active;
			signText = text;
			signPosition = pos;
		}
	}
}
