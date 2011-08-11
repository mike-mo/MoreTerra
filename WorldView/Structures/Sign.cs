using System;
using System.Drawing;

namespace MoreTerra.Structures
{
	public class Sign
	{
		private Int32 signId;
		private Boolean activeSign;
		private String signText;
		private Point signPosition;

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

		#region GetSet Functions
		public Int32 Id
		{
			get
			{
				return this.signId;
			}
			set
			{
				this.signId = value;
			}
		}

		public Boolean Active
		{
			get
			{
				return this.activeSign;
			}
			set
			{
				this.activeSign = value;
			}
		}

		public String Text
		{
			get
			{
				return this.signText;
			}
			set
			{
				this.signText = value;
			}
		}

		public Point Position
		{
			get
			{
				return this.signPosition;
			}
			set
			{
				this.signPosition = value;
			}
		}
#endregion

	}
}
