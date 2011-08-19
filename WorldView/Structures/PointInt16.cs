using System;

namespace MoreTerra.Structures
{
	// A very basic Point class for handling 16-bit Integers.
	public struct PointInt16
	{
		private Int16 _x;
		private Int16 _y;

		#region Constructors
		public PointInt16(Int16 x, Int16 y)
		{
			_x = x;
			_y = y;
		}
		#endregion
		
		#region GetSet Functions
		public Int16 X
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

		public Int16 Y
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
