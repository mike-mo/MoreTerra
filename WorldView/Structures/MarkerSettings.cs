using System;

namespace MoreTerra.Structures
{
	public class MarkerSettings
	{
		private Boolean drawMarker;
		private Boolean useFilter;
		private Int32 filterMin;
		private Int32 filterMax;

		#region Constructors
		public MarkerSettings()
		{
			drawMarker = true;
			useFilter = false;
			filterMin = 1;
			filterMax = 255;
		}

		public MarkerSettings(MarkerSettings copy)
		{
			drawMarker = copy.drawMarker;
			useFilter = copy.useFilter;
			filterMin = copy.filterMin;
			filterMax = copy.filterMax;
		}
		#endregion

		#region GetSet Functions
		public Boolean Drawing
		{
			get
			{
				return drawMarker;
			}
			set
			{
				drawMarker = value;
			}
		}

		public Boolean Filtering
		{
			get
			{
				return useFilter;
			}
			set
			{
				useFilter = value;
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
				if (value < 1)
					value = 1;

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
