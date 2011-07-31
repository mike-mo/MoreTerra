using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MoreTerra.Structures
{
	/// <summary>
	/// Used to store both the point and the count of a symbol we need to draw on the map.
	/// </summary>
	/// <remarks>
	/// This way we can place the number directly in the symbol to make it easier to see how
	/// big a spot is as the symbol covers up the actual pixels.
	/// </remarks>
    public class SymbolLoc
    {
        private Point _pv;
        private int _count;

        public SymbolLoc(Point p, int c)
        {
            _pv = p;
            _count = c;
        }

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
    }

}
