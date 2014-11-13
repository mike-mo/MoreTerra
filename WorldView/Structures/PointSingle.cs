using System;

namespace MoreTerra.Structures
{
	// A basic Point structure dealing with Single precision numbers.
	public struct PointSingle
	{
		private Single _x;
		private Single _y;

		#region Constructors
		
		public PointSingle(Single x, Single y)
		{
			_x = x;
			_y = y;
		}
		#endregion

		#region GetSet Functions
		public Single X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public Single Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}
		#endregion
	}
}
