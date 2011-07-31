using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreTerra.Structures
{
	class FilterItem
	{
		private Boolean _filterOn;
		private Int32 _min;
		private Int32 _max;

		public FilterItem()
		{
			_filterOn = false;
			_min = 0;
			_max = 255;
		}

		public Boolean filterOn
		{
			get
			{
				return _filterOn;
			}
		}

		public Int32 min
		{
			get
			{
				return _min;
			}

			set
			{
				if (value < 0)
					value = 0;

				if (value > _max)
					value = _max;

				_min = value;
			}
		}

		public Int32 max
		{
			get
			{
				return _max;
			}

			set
			{
				if (value < 0)
					value = 0;

				if (value < _min)
					value = _min;

				_max = value;
			}
		}
			
	}
}
