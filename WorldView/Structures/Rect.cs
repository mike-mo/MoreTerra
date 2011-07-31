namespace MoreTerra.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public class Rect
    {
        private Point topLeft;
        private Point bottomRight;

        public Rect(int left, int right, int top, int bottom)
        {
            this.topLeft = new Point(left, top);
            this.bottomRight = new Point(right, bottom);
        }

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

        public override string ToString()
        {
            return string.Format("{0},{1}", this.topLeft, this.bottomRight);
        }

    }
}
