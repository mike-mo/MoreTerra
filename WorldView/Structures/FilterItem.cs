using System;

namespace MoreTerra.Structures
{
	public class FilterItem
	{
		private Boolean filterState;
		private Int32 filterMin;
		private Int32 filterMax;

		#region Constructors
		public FilterItem()
		{
			filterState = false;
			filterMin = 0;
			filterMax = 255;
		}
		#endregion

		#region GetSet Functions
		public Boolean State
		{
			get
			{
				return filterState;
			}
		}

		public Int32 Min
		{
			get
			{
				return filterMin;
			}

			set
			{
				if (value < 0)
					value = 0;

				if (value > filterMax)
					value = filterMax;

				filterMin = value;
			}
		}

		public Int32 Max
		{
			get
			{
				return filterMax;
			}

			set
			{
				if (value < 0)
					value = 0;

				if (value < filterMin)
					value =  filterMin;

				filterMax = value;
			}
		}
		#endregion
	}
}
