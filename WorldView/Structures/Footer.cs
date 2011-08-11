using System;

namespace MoreTerra.Structures
{
	public class Footer
	{
		private Boolean activeFooter;
		private String worldName;
		private Int32 worldId;

		#region GetSet Functions
		public Boolean Active
		{
			get
			{
				return activeFooter;
			}
			set
			{
				activeFooter = value;
			}
		}

		public String Name
		{
			get
			{
				return worldName;
			}
			set
			{
				worldName = value;
			}
		}

		public Int32 Id
		{
			get
			{
				return worldId;
			}
			set
			{
				worldId = value;
			}
		}
		#endregion
	}
}
