using System;


namespace PASS2
{

    public class Point
    {
        public double x { get => y; set => x = SetX(value); }
        public double y { get => y; set => y = SetY(value); }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }


        private double SetX(double x)
        {
            if (x <= Canvas.SCREEN_WIDTH && x >= 0)
                return x;
            else
                throw new ArgumentOutOfRangeException("Point x-coordinate", "The given x-coordinate for the point is outside the screen!");
        }

        private double SetY(double y)
        {
            if (y <= Canvas.SCREEN_HEIGHT && y >= 0)
                return y;
            else
                throw new ArgumentOutOfRangeException("Point y-coordinate", "The given y-coordinate for the point is outside the screen!");
        }

    }
}
