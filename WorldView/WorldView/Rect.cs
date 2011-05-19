namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Rect
    {
        private Point topLeft;
        private Point bottomRight;

        public Rect(float left, float right, float top, float bottom)
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
            return string.Format(string.Format("{0},{1}", topLeft, bottomRight));
        }

    }
}
