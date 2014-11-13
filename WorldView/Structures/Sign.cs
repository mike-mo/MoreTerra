using System;
using System.Drawing;

namespace MoreTerra.Structures
{
	public enum SignType
	{
		Sign = 0,
		Tombstone
	}

	public class Sign
	{
		private Int32 signId;
		private Boolean activeSign;
		private String signText;
		private Point signPosition;
		private SignType signType;

		public Sign()
		{
			signId = -1;
			activeSign = false;
			signText = String.Empty;
			signPosition = new Point(0, 0);
			signType = SignType.Sign;
		}

		public Sign(Int32 id, Boolean active, String text, Point pos)
		{
			signId = id;
			activeSign = active;
			signText = text;
			signPosition = pos;
			signType = SignType.Sign;
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

		public SignType Type
		{
			get
			{
				return this.signType;
			}
			set
			{
				this.signType = value;
			}
		}
		#endregion

	}
}
