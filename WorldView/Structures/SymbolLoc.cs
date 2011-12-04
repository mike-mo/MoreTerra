using System;
using System.Drawing;

namespace MoreTerra.Structures
{
	// Used to store both the point and the count of a marker we need to draw on the map.
	// This way we can set up a filter system to show only sets large enough to be of interest.
    public class MarkerLoc
    {
        private Point _pv;
        private int _count;

		#region Constructors
		public MarkerLoc(Point p, int c)
        {
            _pv = p;
            _count = c;
        }
		#endregion

		#region GetSet Functions
		public Point pv
		{
			get
			{
				return _pv;
			}
		}

		public int count
		{
			get
			{
				return _count;
			}
		}
		#endregion
	}
}
