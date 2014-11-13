using System;
using System.Drawing;

namespace MoreTerra.Structures
{
    public class Rect
    {
        private Point topLeft;
        private Point bottomRight;

		#region Constructors
        public Rect(int left, int right, int top, int bottom)
        {
            this.topLeft = new Point(left, top);
            this.bottomRight = new Point(right, bottom);
        }
		#endregion

		#region GetSet Functions
        public Point TopLeft
        {
            get
            {
                return this.topLeft;
            }
        }

        public Point BottomRight
        {
            get
            {
                return this.bottomRight;
            }
        }
#endregion

		#region Overrides
        public override string ToString()
        {
            return string.Format("{0},{1}", this.topLeft, this.bottomRight);
        }
		#endregion

    }
}
