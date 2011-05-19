namespace WorldView
{
    public class Point
    {
        private float x;
        private float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                this.x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }
        }

        public override string ToString()
        {
            return string.Format(string.Format("({0},{1})", this.x, this.y));
        }
    }
}
